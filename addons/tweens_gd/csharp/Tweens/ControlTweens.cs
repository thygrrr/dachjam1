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

/// <summary>Animates Control ControlPosition; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPositionTween() : PropertyTween<Control, Vector2>(
    static n => n.Position, static (n, value) => n.Position = value, Interpolators.Vector2);

/// <summary>Animates Control ControlPositionX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPositionXTween() : PropertyTween<Control, float>(
    static n => n.Position.X, static (n, value) => { var current = n.Position; current.X = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Control ControlPositionY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPositionYTween() : PropertyTween<Control, float>(
    static n => n.Position.Y, static (n, value) => { var current = n.Position; current.Y = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Control ControlGlobalPosition; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlGlobalPositionTween() : PropertyTween<Control, Vector2>(
    static n => n.GlobalPosition, static (n, value) => n.GlobalPosition = value, Interpolators.Vector2);

/// <summary>Animates Control ControlGlobalPositionX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlGlobalPositionXTween() : PropertyTween<Control, float>(
    static n => n.GlobalPosition.X, static (n, value) => { var current = n.GlobalPosition; current.X = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Control ControlGlobalPositionY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlGlobalPositionYTween() : PropertyTween<Control, float>(
    static n => n.GlobalPosition.Y, static (n, value) => { var current = n.GlobalPosition; current.Y = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Control ControlSize; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlSizeTween() : PropertyTween<Control, Vector2>(
    static n => n.Size, static (n, value) => n.Size = value, Interpolators.Vector2);

/// <summary>Animates Control ControlSizeX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlSizeXTween() : PropertyTween<Control, float>(
    static n => n.Size.X, static (n, value) => { var current = n.Size; current.X = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates Control ControlSizeY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlSizeYTween() : PropertyTween<Control, float>(
    static n => n.Size.Y, static (n, value) => { var current = n.Size; current.Y = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates Control ControlScale; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlScaleTween() : PropertyTween<Control, Vector2>(
    static n => n.Scale, static (n, value) => n.Scale = value, Interpolators.Vector2);

/// <summary>Animates Control ControlScaleX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlScaleXTween() : PropertyTween<Control, float>(
    static n => n.Scale.X, static (n, value) => { var current = n.Scale; current.X = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Control ControlScaleY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlScaleYTween() : PropertyTween<Control, float>(
    static n => n.Scale.Y, static (n, value) => { var current = n.Scale; current.Y = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Control ControlRotation; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlRotationTween() : PropertyTween<Control, float>(
    static n => n.Rotation, static (n, value) => n.Rotation = value, Interpolators.Float);

/// <summary>Animates Godot.Range RangeValue; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class RangeValueTween() : PropertyTween<Godot.Range, double>(
    static n => n.Value, static (n, value) => n.Value = value, Interpolators.Double);

/// <summary>Animates Control ControlAnchorMin; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlAnchorMinTween() : PropertyTween<Control, Vector2>(
    static n => new Vector2(n.AnchorLeft, n.AnchorTop), static (n, value) => { n.SetAnchor(Side.Left, value.X); n.SetAnchor(Side.Top, value.Y); }, Interpolators.Vector2);

/// <summary>Animates Control ControlAnchorMax; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlAnchorMaxTween() : PropertyTween<Control, Vector2>(
    static n => new Vector2(n.AnchorRight, n.AnchorBottom), static (n, value) => { n.SetAnchor(Side.Right, value.X); n.SetAnchor(Side.Bottom, value.Y); }, Interpolators.Vector2);

/// <summary>Animates Control ControlOffsets; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetsTween() : PropertyTween<Control, Vector4>(
    static n => new Vector4(n.OffsetLeft, n.OffsetTop, n.OffsetRight, n.OffsetBottom), static (n, value) => { n.OffsetLeft = value.X; n.OffsetTop = value.Y; n.OffsetRight = value.Z; n.OffsetBottom = value.W; }, Interpolators.Vector4);
