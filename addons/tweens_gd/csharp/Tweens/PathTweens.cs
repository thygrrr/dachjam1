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

/// <summary>Animates PathFollow2D.Progress in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow2DProgressTween() : PropertyTween<PathFollow2D, float>(
    static n => n.Progress, static (n, value) => n.Progress = value, Interpolators.Float);

/// <summary>Animates PathFollow2D.ProgressRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow2DProgressRatioTween() : PropertyTween<PathFollow2D, float>(
    static n => n.ProgressRatio, static (n, value) => n.ProgressRatio = value, Interpolators.Float);

/// <summary>Animates PathFollow2D.HOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow2DHOffsetTween() : PropertyTween<PathFollow2D, float>(
    static n => n.HOffset, static (n, value) => n.HOffset = value, Interpolators.Float);

/// <summary>Animates PathFollow2D.VOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow2DVOffsetTween() : PropertyTween<PathFollow2D, float>(
    static n => n.VOffset, static (n, value) => n.VOffset = value, Interpolators.Float);

/// <summary>Animates PathFollow3D.Progress in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow3DProgressTween() : PropertyTween<PathFollow3D, float>(
    static n => n.Progress, static (n, value) => n.Progress = value, Interpolators.Float);

/// <summary>Animates PathFollow3D.ProgressRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow3DProgressRatioTween() : PropertyTween<PathFollow3D, float>(
    static n => n.ProgressRatio, static (n, value) => n.ProgressRatio = value, Interpolators.Float);

/// <summary>Animates PathFollow3D.HOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow3DHOffsetTween() : PropertyTween<PathFollow3D, float>(
    static n => n.HOffset, static (n, value) => n.HOffset = value, Interpolators.Float);

/// <summary>Animates PathFollow3D.VOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PathFollow3DVOffsetTween() : PropertyTween<PathFollow3D, float>(
    static n => n.VOffset, static (n, value) => n.VOffset = value, Interpolators.Float);
