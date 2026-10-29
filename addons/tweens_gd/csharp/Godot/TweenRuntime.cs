// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Godot;
using System.Runtime.CompilerServices;

namespace tweens.gd;

/// <summary>Automatic per-tree runtime. No autoload or 2dog host changes are needed.</summary>
public static class TweenRuntime
{
    private static readonly Dictionary<SceneTree, TweenRunner> runners = new();
    private static readonly ConditionalWeakTable<SceneTree, object> closingTrees = new();

    internal static void EnsureMainThread()
    {
        if (OS.GetThreadCallerId() != OS.GetMainThreadId())
            throw new InvalidOperationException("Godot tweens must be created on Godot's main thread.");
    }

    internal static void ValidateTree(SceneTree tree)
    {
        EnsureMainThread();
        if (!GodotObject.IsInstanceValid(tree) || !GodotObject.IsInstanceValid(tree.Root) || !tree.Root.IsInsideTree())
            throw new ArgumentException("The scene tree must be live and initialized.", nameof(tree));
        if (closingTrees.TryGetValue(tree, out _))
            throw new InvalidOperationException("Cannot start a tween while the scene tree is shutting down.");
    }

    internal static void ValidateOwner(Node owner)
    {
        EnsureMainThread();
        if (!GodotObject.IsInstanceValid(owner) || owner.IsQueuedForDeletion() || !owner.IsInsideTree())
            throw new ArgumentException("The owner must be a valid node inside the scene tree.", nameof(owner));
    }

    internal static TweenRunner GetRunner(Node owner)
    {
        ValidateOwner(owner);
        return GetRunner(owner.GetTree());
    }

    internal static TweenRunner GetRunner(SceneTree tree)
    {
        ValidateTree(tree);
        if (runners.TryGetValue(tree, out var existing)) return existing;
        var runner = new TweenRunner();
        runner.Initialize(tree);
        runners.Add(tree, runner);
        // Adding a sibling from _Ready can find the root busy adding children.
        Callable.From(() =>
        {
            if (GodotObject.IsInstanceValid(runner) && runners.TryGetValue(tree, out var current) && current == runner)
                tree.Root.AddChild(runner);
        }).CallDeferred();
        return runner;
    }

    internal static void Remove(SceneTree tree, TweenRunner runner)
    {
        if (runners.TryGetValue(tree, out var current) && current == runner) runners.Remove(tree);
    }

    internal static void MarkClosing(SceneTree tree) => closingTrees.GetValue(tree, static _ => new object());

    /// <summary>Number of unfinished tweens in this owner's automatic runtime.</summary>
    public static int GetActiveCount(Node owner)
    {
        ValidateOwner(owner);
        return runners.TryGetValue(owner.GetTree(), out var runner) ? runner.Scheduler.ActiveCount : 0;
    }

    internal static void Cancel(Node owner, bool descendants)
    {
        ValidateOwner(owner);
        if (runners.TryGetValue(owner.GetTree(), out var runner)) runner.Scheduler.CancelOwner(owner, descendants);
    }
}

/// <summary>Starts definitions through the automatic Godot runtime and controls node-owned playback.</summary>
public static partial class TweenExtensions
{
    /// <summary>Starts one definition on this node and returns its playback handle.</summary>
    /// <remarks>Snapshots configuration now; captures the value on the first eligible update, before a positive delay. The node must
    /// be in the tree and owns playback. In GDScript, use Tweens.play(target, definition).</remarks>
    public static TweenInstance<TTarget, TValue> Tween<TTarget, TValue>(this TTarget target,
        ITweenDefinition<TTarget, TValue> definition, PlaybackOptions options = default) where TTarget : Node where TValue : struct
        => TweenRuntime.GetRunner(target).Scheduler.Add(target, definition, options);

    /// <summary>Starts several definitions in parallel on this node and returns their group handle.</summary>
    /// <remarks>Each member honors its own motion timing under the supplied playback policy. If starting a definition throws, earlier
    /// members are cancelled. Await the group's End before starting the next step.
    /// In GDScript, use Tweens.play_all(target, [first, second, ...]).</remarks>
    /// <example><code>
    /// await sprite.Tween(new Tweens.Position2D((400, 180), 0.6),
    ///     new Tweens.ModulateAlpha(0, 0.6)).End;
    /// </code></example>
    public static Group Tween<TTarget>(this TTarget target, ITweenDefinition<TTarget> first,
        ITweenDefinition<TTarget> second, params ITweenDefinition<TTarget>[] rest) where TTarget : Node
        => Tween(target, default(PlaybackOptions), first, second, rest);

    /// <summary>Starts several definitions in parallel on this node with one explicit playback policy.</summary>
    /// <remarks>Options precede the definitions so a params list can retain arbitrary length.</remarks>
    public static Group Tween<TTarget>(this TTarget target, PlaybackOptions options, ITweenDefinition<TTarget> first,
        ITweenDefinition<TTarget> second, params ITweenDefinition<TTarget>[] rest) where TTarget : Node
    {
        ArgumentNullException.ThrowIfNull(rest);
        ITweenDefinition<TTarget>[] definitions = [first, second, .. rest];
        if (Array.IndexOf(definitions, null) >= 0)
            throw new ArgumentNullException(nameof(rest), "Definitions cannot be null.");
        var scheduler = TweenRuntime.GetRunner(target).Scheduler;
        return scheduler.AddAll(target, definitions, options: options);
    }

    /// <summary>Starts a flat, linked timeline on this node. Negative delays overlap the previous entry's end.</summary>
    public static Chain Chain<TTarget>(this TTarget target, IReadOnlyList<ITweenDefinition<TTarget>> definitions,
        PlaybackOptions options = default) where TTarget : Node
        => TweenRuntime.GetRunner(target).Scheduler.AddChain(target, definitions, options: options);

    /// <summary>Starts definitions in parallel with one playback policy.</summary>
    public static Group Tween<TTarget>(this TTarget target, IReadOnlyList<ITweenDefinition<TTarget>> definitions,
        PlaybackOptions options = default) where TTarget : Node
    {
        var scheduler = TweenRuntime.GetRunner(target).Scheduler;
        return scheduler.AddAll(target, definitions, options: options);
    }

    /// <summary>Cancels active tweens owned by this node, optionally including descendant owners.</summary>
    public static void CancelTweens(this Node owner, bool includeChildren = false)
        => TweenRuntime.Cancel(owner, includeChildren);
}
