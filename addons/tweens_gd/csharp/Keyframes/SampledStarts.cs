// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using Godot;

namespace tweens.gd;

public sealed partial class TweenScheduler
{
    /// <summary>Starts a sampled definition without boxing it. Node targets own playback.</summary>
    public TweenInstance<TTarget, TValue> Add<TTarget, TValue>(TTarget target, in Tweens.Sampled<TTarget, TValue> definition,
        PlaybackOptions options = default) where TTarget : class where TValue : struct
        => AddValue<TTarget, TValue, Tweens.Sampled<TTarget, TValue>>(target, in definition, target as Node, null, options);

    /// <summary>Starts a sampled definition without boxing it, using an in-tree owner.</summary>
    public TweenInstance<TTarget, TValue> Add<TTarget, TValue>(TTarget target, in Tweens.Sampled<TTarget, TValue> definition,
        Node owner, PlaybackOptions options = default) where TTarget : class where TValue : struct
    {
        System.ArgumentNullException.ThrowIfNull(owner);
        return AddValue<TTarget, TValue, Tweens.Sampled<TTarget, TValue>>(target, in definition, owner, null, options);
    }
}

public static partial class TweenExtensions
{
    /// <summary>Starts a sampled definition on this node without boxing it.</summary>
    public static TweenInstance<TTarget, TValue> Tween<TTarget, TValue>(this TTarget target,
        in Tweens.Sampled<TTarget, TValue> definition, PlaybackOptions options = default) where TTarget : Node where TValue : struct
        => TweenRuntime.GetRunner(target).Scheduler.AddValue<TTarget, TValue, Tweens.Sampled<TTarget, TValue>>(
            target, in definition, target, null, options);

    /// <summary>Starts a sampled definition on a shared resource without boxing it.</summary>
    public static TweenInstance<TTarget, TValue> Tween<TTarget, TValue>(this TTarget target,
        in Tweens.Sampled<TTarget, TValue> definition, SceneTree tree, Node? owner = null, PlaybackOptions options = default)
        where TTarget : Resource where TValue : struct
        => TweenRuntime.GetRunner(tree).Scheduler.AddValue<TTarget, TValue, Tweens.Sampled<TTarget, TValue>>(
            target, in definition, owner, tree, options);

    /// <summary>Starts a sampled definition on a shared resource using this owner's lifetime.</summary>
    public static TweenInstance<TTarget, TValue> Tween<TTarget, TValue>(this TTarget target,
        in Tweens.Sampled<TTarget, TValue> definition, Node owner, PlaybackOptions options = default)
        where TTarget : Resource where TValue : struct
    {
        TweenRuntime.ValidateOwner(owner);
        return target.Tween(in definition, owner.GetTree(), owner, options);
    }

    /// <summary>Starts a sampled resource definition using this node's lifetime without boxing it.</summary>
    public static TweenInstance<TTarget, TValue> Tween<TTarget, TValue>(this Node owner, TTarget target,
        in Tweens.Sampled<TTarget, TValue> definition, PlaybackOptions options = default) where TTarget : Resource where TValue : struct
        => target.Tween(in definition, owner, options);
}
