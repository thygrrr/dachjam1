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

/// <summary>Animates CanvasLayer.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerOffsetTween() : PropertyTween<CanvasLayer, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates CanvasLayer.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerOffsetXTween() : PropertyTween<CanvasLayer, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates CanvasLayer.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerOffsetYTween() : PropertyTween<CanvasLayer, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates CanvasLayer.Scale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerScaleTween() : PropertyTween<CanvasLayer, Vector2>(
    static n => n.Scale, static (n, value) => n.Scale = value, Interpolators.Vector2);

/// <summary>Animates CanvasLayer.Scale.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerScaleXTween() : PropertyTween<CanvasLayer, float>(
    static n => n.Scale.X, static (n, value) => { var current = n.Scale; current.X = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates CanvasLayer.Scale.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerScaleYTween() : PropertyTween<CanvasLayer, float>(
    static n => n.Scale.Y, static (n, value) => { var current = n.Scale; current.Y = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates CanvasLayer.Rotation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasLayerRotationTween() : PropertyTween<CanvasLayer, float>(
    static n => n.Rotation, static (n, value) => n.Rotation = value, Interpolators.Float);

/// <summary>Animates CanvasModulate.Color in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasModulateColorTween() : PropertyTween<CanvasModulate, Color>(
    static n => n.Color, static (n, value) => n.Color = value, Interpolators.Color);

/// <summary>Animates CanvasModulate.Color.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CanvasModulateColorAlphaTween() : PropertyTween<CanvasModulate, float>(
    static n => n.Color.A, static (n, value) => { var current = n.Color; current.A = value; n.Color = current; }, Interpolators.Float);

/// <summary>Animates Parallax2D.ScrollOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DScrollOffsetTween() : PropertyTween<Parallax2D, Vector2>(
    static n => n.ScrollOffset, static (n, value) => n.ScrollOffset = value, Interpolators.Vector2);

/// <summary>Animates Parallax2D.ScrollOffset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DScrollOffsetXTween() : PropertyTween<Parallax2D, float>(
    static n => n.ScrollOffset.X, static (n, value) => { var current = n.ScrollOffset; current.X = value; n.ScrollOffset = current; }, Interpolators.Float);

/// <summary>Animates Parallax2D.ScrollOffset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DScrollOffsetYTween() : PropertyTween<Parallax2D, float>(
    static n => n.ScrollOffset.Y, static (n, value) => { var current = n.ScrollOffset; current.Y = value; n.ScrollOffset = current; }, Interpolators.Float);

/// <summary>Animates Parallax2D.ScrollScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DScrollScaleTween() : PropertyTween<Parallax2D, Vector2>(
    static n => n.ScrollScale, static (n, value) => n.ScrollScale = value, Interpolators.Vector2);

/// <summary>Animates Parallax2D.ScrollScale.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DScrollScaleXTween() : PropertyTween<Parallax2D, float>(
    static n => n.ScrollScale.X, static (n, value) => { var current = n.ScrollScale; current.X = value; n.ScrollScale = current; }, Interpolators.Float);

/// <summary>Animates Parallax2D.ScrollScale.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DScrollScaleYTween() : PropertyTween<Parallax2D, float>(
    static n => n.ScrollScale.Y, static (n, value) => { var current = n.ScrollScale; current.Y = value; n.ScrollScale = current; }, Interpolators.Float);

/// <summary>Animates Parallax2D.Autoscroll in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DAutoscrollTween() : PropertyTween<Parallax2D, Vector2>(
    static n => n.Autoscroll, static (n, value) => n.Autoscroll = value, Interpolators.Vector2);

/// <summary>Animates Parallax2D.Autoscroll.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DAutoscrollXTween() : PropertyTween<Parallax2D, float>(
    static n => n.Autoscroll.X, static (n, value) => { var current = n.Autoscroll; current.X = value; n.Autoscroll = current; }, Interpolators.Float);

/// <summary>Animates Parallax2D.Autoscroll.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Parallax2DAutoscrollYTween() : PropertyTween<Parallax2D, float>(
    static n => n.Autoscroll.Y, static (n, value) => { var current = n.Autoscroll; current.Y = value; n.Autoscroll = current; }, Interpolators.Float);
