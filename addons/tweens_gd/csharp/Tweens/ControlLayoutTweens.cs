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

/// <summary>Animates Control.PivotOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPivotOffsetTween() : PropertyTween<Control, Vector2>(
    static n => n.PivotOffset, static (n, value) => n.PivotOffset = value, Interpolators.Vector2);

/// <summary>Animates Control.PivotOffset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPivotOffsetXTween() : PropertyTween<Control, float>(
    static n => n.PivotOffset.X, static (n, value) => { var current = n.PivotOffset; current.X = value; n.PivotOffset = current; }, Interpolators.Float);

/// <summary>Animates Control.PivotOffset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPivotOffsetYTween() : PropertyTween<Control, float>(
    static n => n.PivotOffset.Y, static (n, value) => { var current = n.PivotOffset; current.Y = value; n.PivotOffset = current; }, Interpolators.Float);

/// <summary>Animates Control.PivotOffsetRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPivotOffsetRatioTween() : PropertyTween<Control, Vector2>(
    static n => n.PivotOffsetRatio, static (n, value) => n.PivotOffsetRatio = value, Interpolators.Vector2);

/// <summary>Animates Control.PivotOffsetRatio.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPivotOffsetRatioXTween() : PropertyTween<Control, float>(
    static n => n.PivotOffsetRatio.X, static (n, value) => { var current = n.PivotOffsetRatio; current.X = value; n.PivotOffsetRatio = current; }, Interpolators.Float);

/// <summary>Animates Control.PivotOffsetRatio.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlPivotOffsetRatioYTween() : PropertyTween<Control, float>(
    static n => n.PivotOffsetRatio.Y, static (n, value) => { var current = n.PivotOffsetRatio; current.Y = value; n.PivotOffsetRatio = current; }, Interpolators.Float);

/// <summary>Animates Control.CustomMinimumSize in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlCustomMinimumSizeTween() : PropertyTween<Control, Vector2>(
    static n => n.CustomMinimumSize, static (n, value) => n.CustomMinimumSize = value, Interpolators.Vector2);

/// <summary>Animates Control.CustomMinimumSize.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlCustomMinimumSizeXTween() : PropertyTween<Control, float>(
    static n => n.CustomMinimumSize.X, static (n, value) => { var current = n.CustomMinimumSize; current.X = value; n.CustomMinimumSize = current; }, Interpolators.Float);

/// <summary>Animates Control.CustomMinimumSize.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlCustomMinimumSizeYTween() : PropertyTween<Control, float>(
    static n => n.CustomMinimumSize.Y, static (n, value) => { var current = n.CustomMinimumSize; current.Y = value; n.CustomMinimumSize = current; }, Interpolators.Float);

/// <summary>Animates Control.CustomMaximumSize in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlCustomMaximumSizeTween() : PropertyTween<Control, Vector2>(
    static n => n.CustomMaximumSize, static (n, value) => n.CustomMaximumSize = value, Interpolators.Vector2);

/// <summary>Animates Control.CustomMaximumSize.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlCustomMaximumSizeXTween() : PropertyTween<Control, float>(
    static n => n.CustomMaximumSize.X, static (n, value) => { var current = n.CustomMaximumSize; current.X = value; n.CustomMaximumSize = current; }, Interpolators.Float);

/// <summary>Animates Control.CustomMaximumSize.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlCustomMaximumSizeYTween() : PropertyTween<Control, float>(
    static n => n.CustomMaximumSize.Y, static (n, value) => { var current = n.CustomMaximumSize; current.Y = value; n.CustomMaximumSize = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPosition in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPositionTween() : PropertyTween<Control, Vector2>(
    static n => n.OffsetTransformPosition, static (n, value) => n.OffsetTransformPosition = value, Interpolators.Vector2);

/// <summary>Animates Control.OffsetTransformPosition.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPositionXTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPosition.X, static (n, value) => { var current = n.OffsetTransformPosition; current.X = value; n.OffsetTransformPosition = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPosition.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPositionYTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPosition.Y, static (n, value) => { var current = n.OffsetTransformPosition; current.Y = value; n.OffsetTransformPosition = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPositionRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPositionRatioTween() : PropertyTween<Control, Vector2>(
    static n => n.OffsetTransformPositionRatio, static (n, value) => n.OffsetTransformPositionRatio = value, Interpolators.Vector2);

/// <summary>Animates Control.OffsetTransformPositionRatio.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPositionRatioXTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPositionRatio.X, static (n, value) => { var current = n.OffsetTransformPositionRatio; current.X = value; n.OffsetTransformPositionRatio = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPositionRatio.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPositionRatioYTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPositionRatio.Y, static (n, value) => { var current = n.OffsetTransformPositionRatio; current.Y = value; n.OffsetTransformPositionRatio = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformScaleTween() : PropertyTween<Control, Vector2>(
    static n => n.OffsetTransformScale, static (n, value) => n.OffsetTransformScale = value, Interpolators.Vector2);

/// <summary>Animates Control.OffsetTransformScale.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformScaleXTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformScale.X, static (n, value) => { var current = n.OffsetTransformScale; current.X = value; n.OffsetTransformScale = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformScale.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformScaleYTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformScale.Y, static (n, value) => { var current = n.OffsetTransformScale; current.Y = value; n.OffsetTransformScale = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPivot in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPivotTween() : PropertyTween<Control, Vector2>(
    static n => n.OffsetTransformPivot, static (n, value) => n.OffsetTransformPivot = value, Interpolators.Vector2);

/// <summary>Animates Control.OffsetTransformPivot.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPivotXTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPivot.X, static (n, value) => { var current = n.OffsetTransformPivot; current.X = value; n.OffsetTransformPivot = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPivot.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPivotYTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPivot.Y, static (n, value) => { var current = n.OffsetTransformPivot; current.Y = value; n.OffsetTransformPivot = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPivotRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPivotRatioTween() : PropertyTween<Control, Vector2>(
    static n => n.OffsetTransformPivotRatio, static (n, value) => n.OffsetTransformPivotRatio = value, Interpolators.Vector2);

/// <summary>Animates Control.OffsetTransformPivotRatio.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPivotRatioXTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPivotRatio.X, static (n, value) => { var current = n.OffsetTransformPivotRatio; current.X = value; n.OffsetTransformPivotRatio = current; }, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformPivotRatio.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformPivotRatioYTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformPivotRatio.Y, static (n, value) => { var current = n.OffsetTransformPivotRatio; current.Y = value; n.OffsetTransformPivotRatio = current; }, Interpolators.Float);

/// <summary>Animates Control.SizeFlagsStretchRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlSizeFlagsStretchRatioTween() : PropertyTween<Control, float>(
    static n => n.SizeFlagsStretchRatio, static (n, value) => n.SizeFlagsStretchRatio = value, Interpolators.Float);

/// <summary>Animates Control.OffsetTransformRotation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTransformRotationTween() : PropertyTween<Control, float>(
    static n => n.OffsetTransformRotation, static (n, value) => n.OffsetTransformRotation = value, Interpolators.Float);

/// <summary>Animates Control.AnchorLeft in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlAnchorLeftTween() : PropertyTween<Control, float>(
    static n => n.AnchorLeft, static (n, value) => n.SetAnchor(Side.Left, value), Interpolators.Float);

/// <summary>Animates Control.OffsetLeft in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetLeftTween() : PropertyTween<Control, float>(
    static n => n.OffsetLeft, static (n, value) => n.OffsetLeft = value, Interpolators.Float);

/// <summary>Animates Control.AnchorTop in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlAnchorTopTween() : PropertyTween<Control, float>(
    static n => n.AnchorTop, static (n, value) => n.SetAnchor(Side.Top, value), Interpolators.Float);

/// <summary>Animates Control.OffsetTop in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetTopTween() : PropertyTween<Control, float>(
    static n => n.OffsetTop, static (n, value) => n.OffsetTop = value, Interpolators.Float);

/// <summary>Animates Control.AnchorRight in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlAnchorRightTween() : PropertyTween<Control, float>(
    static n => n.AnchorRight, static (n, value) => n.SetAnchor(Side.Right, value), Interpolators.Float);

/// <summary>Animates Control.OffsetRight in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetRightTween() : PropertyTween<Control, float>(
    static n => n.OffsetRight, static (n, value) => n.OffsetRight = value, Interpolators.Float);

/// <summary>Animates Control.AnchorBottom in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlAnchorBottomTween() : PropertyTween<Control, float>(
    static n => n.AnchorBottom, static (n, value) => n.SetAnchor(Side.Bottom, value), Interpolators.Float);

/// <summary>Animates Control.OffsetBottom in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ControlOffsetBottomTween() : PropertyTween<Control, float>(
    static n => n.OffsetBottom, static (n, value) => n.OffsetBottom = value, Interpolators.Float);
