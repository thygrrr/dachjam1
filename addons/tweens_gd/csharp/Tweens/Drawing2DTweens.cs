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

/// <summary>Animates Sprite2D.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Sprite2DOffsetTween() : PropertyTween<Sprite2D, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates Sprite2D.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Sprite2DOffsetXTween() : PropertyTween<Sprite2D, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Sprite2D.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Sprite2DOffsetYTween() : PropertyTween<Sprite2D, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Sprite2D.RegionRect in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Sprite2DRegionRectTween() : PropertyTween<Sprite2D, Rect2>(
    static n => n.RegionRect, static (n, value) => n.RegionRect = value, Interpolators.Rect2);

/// <summary>Animates Line2D.Width in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Line2DWidthTween() : PropertyTween<Line2D, float>(
    static n => n.Width, static (n, value) => n.Width = value, Interpolators.Float);

/// <summary>Animates Line2D.DefaultColor in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Line2DDefaultColorTween() : PropertyTween<Line2D, Color>(
    static n => n.DefaultColor, static (n, value) => n.DefaultColor = value, Interpolators.Color);

/// <summary>Animates Line2D.DefaultColor.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Line2DDefaultColorAlphaTween() : PropertyTween<Line2D, float>(
    static n => n.DefaultColor.A, static (n, value) => { var current = n.DefaultColor; current.A = value; n.DefaultColor = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.Color in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DColorTween() : PropertyTween<Polygon2D, Color>(
    static n => n.Color, static (n, value) => n.Color = value, Interpolators.Color);

/// <summary>Animates Polygon2D.Color.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DColorAlphaTween() : PropertyTween<Polygon2D, float>(
    static n => n.Color.A, static (n, value) => { var current = n.Color; current.A = value; n.Color = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DOffsetTween() : PropertyTween<Polygon2D, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates Polygon2D.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DOffsetXTween() : PropertyTween<Polygon2D, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DOffsetYTween() : PropertyTween<Polygon2D, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.TextureOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureOffsetTween() : PropertyTween<Polygon2D, Vector2>(
    static n => n.TextureOffset, static (n, value) => n.TextureOffset = value, Interpolators.Vector2);

/// <summary>Animates Polygon2D.TextureOffset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureOffsetXTween() : PropertyTween<Polygon2D, float>(
    static n => n.TextureOffset.X, static (n, value) => { var current = n.TextureOffset; current.X = value; n.TextureOffset = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.TextureOffset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureOffsetYTween() : PropertyTween<Polygon2D, float>(
    static n => n.TextureOffset.Y, static (n, value) => { var current = n.TextureOffset; current.Y = value; n.TextureOffset = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.TextureScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureScaleTween() : PropertyTween<Polygon2D, Vector2>(
    static n => n.TextureScale, static (n, value) => n.TextureScale = value, Interpolators.Vector2);

/// <summary>Animates Polygon2D.TextureScale.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureScaleXTween() : PropertyTween<Polygon2D, float>(
    static n => n.TextureScale.X, static (n, value) => { var current = n.TextureScale; current.X = value; n.TextureScale = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.TextureScale.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureScaleYTween() : PropertyTween<Polygon2D, float>(
    static n => n.TextureScale.Y, static (n, value) => { var current = n.TextureScale; current.Y = value; n.TextureScale = current; }, Interpolators.Float);

/// <summary>Animates Polygon2D.TextureRotation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Polygon2DTextureRotationTween() : PropertyTween<Polygon2D, float>(
    static n => n.TextureRotation, static (n, value) => n.TextureRotation = value, Interpolators.Float);
