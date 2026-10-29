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

/// <summary>Animates Camera2D.Zoom in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera2DZoomTween() : PropertyTween<Camera2D, Vector2>(
    static n => n.Zoom, static (n, value) => n.Zoom = value, Interpolators.Vector2);

/// <summary>Animates Camera2D.Zoom.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera2DZoomXTween() : PropertyTween<Camera2D, float>(
    static n => n.Zoom.X, static (n, value) => { var current = n.Zoom; current.X = value; n.Zoom = current; }, Interpolators.Float);

/// <summary>Animates Camera2D.Zoom.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera2DZoomYTween() : PropertyTween<Camera2D, float>(
    static n => n.Zoom.Y, static (n, value) => { var current = n.Zoom; current.Y = value; n.Zoom = current; }, Interpolators.Float);

/// <summary>Animates Camera2D.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera2DOffsetTween() : PropertyTween<Camera2D, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates Camera2D.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera2DOffsetXTween() : PropertyTween<Camera2D, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Camera2D.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera2DOffsetYTween() : PropertyTween<Camera2D, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Camera3D.Fov in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DFovTween() : PropertyTween<Camera3D, float>(
    static n => n.Fov, static (n, value) => n.Fov = value, Interpolators.Float);

/// <summary>Animates Camera3D.Size in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DSizeTween() : PropertyTween<Camera3D, float>(
    static n => n.Size, static (n, value) => n.Size = value, Interpolators.Float);

/// <summary>Animates Camera3D.HOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DHOffsetTween() : PropertyTween<Camera3D, float>(
    static n => n.HOffset, static (n, value) => n.HOffset = value, Interpolators.Float);

/// <summary>Animates Camera3D.VOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DVOffsetTween() : PropertyTween<Camera3D, float>(
    static n => n.VOffset, static (n, value) => n.VOffset = value, Interpolators.Float);

/// <summary>Animates Camera3D.Near in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DNearTween() : PropertyTween<Camera3D, float>(
    static n => n.Near, static (n, value) => n.Near = value, Interpolators.Float);

/// <summary>Animates Camera3D.Far in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DFarTween() : PropertyTween<Camera3D, float>(
    static n => n.Far, static (n, value) => n.Far = value, Interpolators.Float);

/// <summary>Animates Camera3D.FrustumOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DFrustumOffsetTween() : PropertyTween<Camera3D, Vector2>(
    static n => n.FrustumOffset, static (n, value) => n.FrustumOffset = value, Interpolators.Vector2);

/// <summary>Animates Camera3D.FrustumOffset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DFrustumOffsetXTween() : PropertyTween<Camera3D, float>(
    static n => n.FrustumOffset.X, static (n, value) => { var current = n.FrustumOffset; current.X = value; n.FrustumOffset = current; }, Interpolators.Float);

/// <summary>Animates Camera3D.FrustumOffset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Camera3DFrustumOffsetYTween() : PropertyTween<Camera3D, float>(
    static n => n.FrustumOffset.Y, static (n, value) => { var current = n.FrustumOffset; current.Y = value; n.FrustumOffset = current; }, Interpolators.Float);
