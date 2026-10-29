// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Godot;

namespace tweens.gd;

/// <summary>A manually driven playback scheduler, also used by the automatic Godot runner.</summary>
public sealed partial class TweenScheduler : IDisposable
{
    private readonly List<TweenInstance> instances = [];
    private readonly int thread = System.Environment.CurrentManagedThreadId;
    private bool updating, disposed;
    /// <summary>Reported after a failed tween is cleaned up. Exceptions in observers are ignored.</summary>
    public event Action<Exception>? UnhandledException;
    /// <summary>Number of unfinished roots, including paused roots. Each Chain counts once.</summary>
    public int ActiveCount
    {
        get
        {
            EnsureThread();
            var count = 0;
            foreach (var instance in instances) if (!instance.IsTerminal) count++;
            return count;
        }
    }

    /// <summary>Starts one definition and returns its playback handle. Node targets own their playback.</summary>
    /// <remarks>Non-node targets need no owner for manual playback. The GDScript counterpart is scheduler.add.</remarks>
    public TweenInstance<TTarget, TValue> Add<TTarget, TValue>(TTarget target,
        ITweenDefinition<TTarget, TValue> definition, PlaybackOptions options = default) where TTarget : class where TValue : struct
        => AddCore(target, definition, target as Node, null, options);

    /// <summary>Animate a separate target, binding playback to an in-tree owner node.</summary>
    public TweenInstance<TTarget, TValue> Add<TTarget, TValue>(TTarget target,
        ITweenDefinition<TTarget, TValue> definition, Node owner, PlaybackOptions options = default) where TTarget : class where TValue : struct
    {
        ArgumentNullException.ThrowIfNull(owner);
        return AddCore(target, definition, owner, null, options);
    }

    internal TweenInstance<TTarget, TValue> AddCore<TTarget, TValue>(TTarget target,
        ITweenDefinition<TTarget, TValue> definition, Node? owner, SceneTree? tree, PlaybackOptions options = default)
        where TTarget : class where TValue : struct
    {
        ValidateStart(target, owner, tree, options);
        ArgumentNullException.ThrowIfNull(definition);
        var instance = new TweenInstance<TTarget, TValue>(this, target, definition.CreatePlayback(), owner, tree, options);
        Enroll(instance);
        return instance;
    }

    // Generated overloads retain the concrete struct type. A constrained interface
    // call snapshots it without allocating an interface box.
    internal TweenInstance<TTarget, TValue> AddValue<TTarget, TValue, TDefinition>(TTarget target,
        in TDefinition definition, Node? owner, SceneTree? tree, PlaybackOptions options)
        where TTarget : class where TValue : struct where TDefinition : struct, ITweenDefinition<TTarget, TValue>
    {
        ValidateStart(target, owner, tree, options);
        var instance = new TweenInstance<TTarget, TValue>(this, target, definition.CreatePlayback(), owner, tree, options);
        Enroll(instance);
        return instance;
    }

    internal void ValidateStart(object target, Node? owner, SceneTree? tree, PlaybackOptions options)
    {
        EnsureThread();
        ObjectDisposedException.ThrowIf(disposed, this);
        ArgumentNullException.ThrowIfNull(target);
        if (target is GodotObject native)
        {
            TweenRuntime.EnsureMainThread();
            if (!GodotObject.IsInstanceValid(native))
                throw new ArgumentException("The tween target has been disposed.", nameof(target));
        }
        if (target is Node node && !ReferenceEquals(node, owner))
            throw new ArgumentException("Node targets must use their own node lifetime.", nameof(owner));
        if (owner is not null) TweenRuntime.ValidateOwner(owner);
        if (tree is not null) TweenRuntime.ValidateTree(tree);
        if (owner is not null && tree is not null && owner.GetTree() != tree)
            throw new ArgumentException("The owner must belong to the supplied scene tree.", nameof(owner));
        options.Validate();
    }

    internal void Enroll(TweenInstance instance)
    {
        // A binding's Copy hook can dispose the scheduler before enrollment.
        if (disposed)
        {
            instance.Finish(Reason.RunnerDisposed);
            throw new ObjectDisposedException(nameof(TweenScheduler));
        }
        instances.Add(instance);
        if (instance.CheckTarget()) instance.BindLifetime();
    }

    /// <summary>Snapshots a linked list of definitions on one target. Each delay is relative to the previous entry's own end.</summary>
    public Chain AddChain<TTarget>(TTarget target, System.Collections.Generic.IReadOnlyList<ITweenDefinition<TTarget>> definitions,
        Node? owner = null, PlaybackOptions options = default) where TTarget : class
        => AddChainCore(target, definitions, owner ?? target as Node, null, options);

    internal Chain AddChainCore<TTarget>(TTarget target, System.Collections.Generic.IReadOnlyList<ITweenDefinition<TTarget>> definitions,
        Node? owner, SceneTree? tree, PlaybackOptions options) where TTarget : class
    {
        ValidateStart(target, owner, tree, options);
        ArgumentNullException.ThrowIfNull(definitions);
        if (definitions.Count == 0) throw new ArgumentException("A Chain needs at least one definition.", nameof(definitions));
        var root = new Chain.Root(this, target, owner, tree, options);
        var leaves = new TweenInstance[definitions.Count];
        try
        {
            for (var i = 0; i < leaves.Length; i++)
            {
                ArgumentNullException.ThrowIfNull(definitions[i]);
                leaves[i] = definitions[i].Snapshot(this, target, owner, tree, options);
                leaves[i].Coordinate(root);
            }
            root.Plan = new ExecutionPlan(root, leaves);
        }
        catch
        {
            foreach (var leaf in leaves) leaf?.Finish(Reason.Cancelled);
            throw;
        }
        Enroll(root);
        return new Chain(root);
    }

    /// <summary>Starts a list in parallel with one playback policy.</summary>
    public Group AddAll<TTarget>(TTarget target, IReadOnlyList<ITweenDefinition<TTarget>> definitions,
        Node? owner = null, PlaybackOptions options = default) where TTarget : class
    {
        owner ??= target as Node;
        ValidateStart(target, owner, null, options);
        ArgumentNullException.ThrowIfNull(definitions);
        if (definitions.Count == 0) throw new ArgumentException("A Group needs at least one definition.");
        var started = new TweenInstance[definitions.Count];
        try
        {
            for (var i = 0; i < started.Length; i++)
            {
                ArgumentNullException.ThrowIfNull(definitions[i]);
                started[i] = definitions[i].Snapshot(this, target, owner, null, options);
                Enroll(started[i]);
            }
        }
        catch
        {
            foreach (var leaf in started) leaf?.Cancel();
            throw;
        }
        return Group.Of(started);
    }

    /// <summary>Advances one process mode by a finite, nonnegative delta in seconds.</summary>
    /// <remarks>Unscaled delta defaults to delta. Tweens added during callbacks advance on the next update.
    /// Use the creating thread; recursive updates are rejected.</remarks>
    public void Update(Duration delta, Duration? unscaledDelta = null, TweenProcessMode mode = TweenProcessMode.Process)
    {
        EnsureThread();
        ObjectDisposedException.ThrowIf(disposed, this);
        Playback.Nonnegative(delta, nameof(delta));
        Playback.Nonnegative(unscaledDelta ?? delta, nameof(unscaledDelta));
        if (!Enum.IsDefined(mode)) throw new ArgumentOutOfRangeException(nameof(mode));
        if (updating) throw new InvalidOperationException("Recursive scheduler updates are not supported.");
        updating = true;
        try
        {
            var count = instances.Count;
            for (var i = 0; i < count && !disposed; i++)
            {
                var instance = instances[i];
                // Lifetimes are checked even for a paused or different-lane tween.
                if (instance.CheckTarget() && instance.Mode == mode && instance.CanAdvance())
                    instance.Advance(instance.Unscaled ? unscaledDelta ?? delta : delta);
            }
        }
        finally
        {
            updating = false;
            Compact();
        }
    }

    /// <summary>Cancels every active tween. The scheduler remains usable.</summary>
    public void CancelAll()
    {
        EnsureThread();
        var count = instances.Count;
        for (var i = 0; i < count && i < instances.Count; i++) instances[i].Cancel();
    }

    internal void CancelOwner(Node owner, bool descendants)
    {
        // Decide every match before cancellation callbacks can free or reparent nodes.
        var matches = new List<TweenInstance>();
        foreach (var instance in instances)
        {
            if (instance.IsTerminal) continue;
            var node = instance.Owner;
            if (node == owner || (descendants && GodotObject.IsInstanceValid(node) && owner.IsAncestorOf(node)))
                matches.Add(instance);
        }
        foreach (var instance in matches) instance.Cancel();
    }

    private void Compact()
    {
        var kept = 0;
        for (var i = 0; i < instances.Count; i++)
        {
            if (instances[i].IsTerminal) continue;
            if (kept != i) instances[kept] = instances[i];
            kept++;
        }
        if (kept < instances.Count) instances.RemoveRange(kept, instances.Count - kept);
    }


    internal void EnsureThread()
    {
        if (thread != System.Environment.CurrentManagedThreadId)
            throw new InvalidOperationException("Use the scheduler on its creating thread (Godot's main thread for node tweens).");
    }

    internal void Report(Exception error)
    {
        try { UnhandledException?.Invoke(error); }
        catch { /* An error observer must not interrupt other tweens or teardown. */ }
    }

    /// <summary>Ends every active tween with RunnerDisposed and releases the scheduler. Safe to call repeatedly.</summary>
    public void Dispose()
    {
        EnsureThread();
        if (disposed) return;
        disposed = true;
        var count = instances.Count;
        for (var i = 0; i < count; i++) instances[i].Finish(Reason.RunnerDisposed);
        if (!updating) instances.Clear();
        UnhandledException = null;
    }
}
