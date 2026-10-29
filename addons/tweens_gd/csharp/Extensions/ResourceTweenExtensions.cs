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

public static partial class TweenExtensions
{
    /// <summary>Starts one definition on this resource and returns its playback handle.</summary>
    /// <remarks>Uses the supplied scene tree's lifetime unless an owner is supplied. The resource remains shared.
    /// Combine resource handles with Group.Of for a parallel step; GDScript also offers Tweens.play_all on resources.</remarks>
    public static TweenInstance<TResource, TValue> Tween<TResource, TValue>(this TResource target,
        ITweenDefinition<TResource, TValue> definition, SceneTree tree, Node? owner = null, PlaybackOptions options = default)
        where TResource : Resource where TValue : struct
        => TweenRuntime.GetRunner(tree).Scheduler.AddCore(target, definition, owner, tree, options);

    /// <summary>Starts one definition on this resource and returns its playback handle.</summary>
    /// <remarks>The in-tree owner controls playback lifetime. The resource remains shared.</remarks>
    public static TweenInstance<TResource, TValue> Tween<TResource, TValue>(this TResource target,
        ITweenDefinition<TResource, TValue> definition, Node owner, PlaybackOptions options = default)
        where TResource : Resource where TValue : struct
    {
        TweenRuntime.ValidateOwner(owner);
        return target.Tween(definition, owner.GetTree(), owner, options);
    }

    /// <summary>Starts one definition on a resource using this node's playback lifetime.</summary>
    public static TweenInstance<TResource, TValue> Tween<TResource, TValue>(this Node owner,
        TResource target, ITweenDefinition<TResource, TValue> definition, PlaybackOptions options = default)
        where TResource : Resource where TValue : struct
        => target.Tween(definition, owner, options);
    /// <summary>Starts linked definitions on this resource using an in-tree owner.</summary>
    public static Chain Chain<TResource>(this TResource target, IReadOnlyList<ITweenDefinition<TResource>> definitions,
        Node owner, PlaybackOptions options = default) where TResource : Resource
    {
        TweenRuntime.ValidateOwner(owner);
        return TweenRuntime.GetRunner(owner).Scheduler.AddChain(target, definitions, owner, options);
    }
}
