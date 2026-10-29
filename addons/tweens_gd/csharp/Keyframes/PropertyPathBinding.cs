// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using Godot;

namespace tweens.gd;

// Paths are validated by AnimationTrack before any batch member is enrolled.
internal sealed class PropertyPathBinding<T>(NodePath path) : TweenBinding<Node, T> where T : struct
{
    public override TweenBinding<Node, T> Copy() => this;
    protected override T Read(Node target)
    {
        using var value = target.GetIndexed(path);
        return AnimationValues.Read<T>(value);
    }
    protected override void Write(Node target, T value)
    {
        using var variant = AnimationValues.Write(value);
        target.SetIndexed(path, variant);
    }
}
