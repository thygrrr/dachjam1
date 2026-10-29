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

/// <summary>Animates SpriteBase3D.Modulate in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpriteBase3DModulateTween() : PropertyTween<SpriteBase3D, Color>(
    static n => n.Modulate, static (n, value) => n.Modulate = value, Interpolators.Color);

/// <summary>Animates SpriteBase3D.Modulate.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpriteBase3DModulateAlphaTween() : PropertyTween<SpriteBase3D, float>(
    static n => n.Modulate.A, static (n, value) => { var current = n.Modulate; current.A = value; n.Modulate = current; }, Interpolators.Float);

/// <summary>Animates SpriteBase3D.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpriteBase3DOffsetTween() : PropertyTween<SpriteBase3D, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates SpriteBase3D.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpriteBase3DOffsetXTween() : PropertyTween<SpriteBase3D, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates SpriteBase3D.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpriteBase3DOffsetYTween() : PropertyTween<SpriteBase3D, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates SpriteBase3D.PixelSize in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpriteBase3DPixelSizeTween() : PropertyTween<SpriteBase3D, float>(
    static n => n.PixelSize, static (n, value) => n.PixelSize = value, Interpolators.Float);

/// <summary>Animates Label3D.Modulate in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DModulateTween() : PropertyTween<Label3D, Color>(
    static n => n.Modulate, static (n, value) => n.Modulate = value, Interpolators.Color);

/// <summary>Animates Label3D.Modulate.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DModulateAlphaTween() : PropertyTween<Label3D, float>(
    static n => n.Modulate.A, static (n, value) => { var current = n.Modulate; current.A = value; n.Modulate = current; }, Interpolators.Float);

/// <summary>Animates Label3D.OutlineModulate in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DOutlineModulateTween() : PropertyTween<Label3D, Color>(
    static n => n.OutlineModulate, static (n, value) => n.OutlineModulate = value, Interpolators.Color);

/// <summary>Animates Label3D.OutlineModulate.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DOutlineModulateAlphaTween() : PropertyTween<Label3D, float>(
    static n => n.OutlineModulate.A, static (n, value) => { var current = n.OutlineModulate; current.A = value; n.OutlineModulate = current; }, Interpolators.Float);

/// <summary>Animates Label3D.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DOffsetTween() : PropertyTween<Label3D, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates Label3D.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DOffsetXTween() : PropertyTween<Label3D, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Label3D.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DOffsetYTween() : PropertyTween<Label3D, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Label3D.PixelSize in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Label3DPixelSizeTween() : PropertyTween<Label3D, float>(
    static n => n.PixelSize, static (n, value) => n.PixelSize = value, Interpolators.Float);

/// <summary>Animates GeometryInstance3D.Transparency in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GeometryInstance3DTransparencyTween() : PropertyTween<GeometryInstance3D, float>(
    static n => n.Transparency, static (n, value) => n.Transparency = value, Interpolators.Float);
