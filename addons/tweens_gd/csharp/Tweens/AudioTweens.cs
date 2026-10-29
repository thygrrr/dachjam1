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

/// <summary>Animates AudioStreamPlayer AudioVolumeDb; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioVolumeDbTween() : PropertyTween<AudioStreamPlayer, float>(
    static n => n.VolumeDb, static (n, value) => n.VolumeDb = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer AudioVolumeLinear; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioVolumeLinearTween() : PropertyTween<AudioStreamPlayer, float>(
    static n => n.VolumeLinear, static (n, value) => n.VolumeLinear = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer AudioPitchScale; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioPitchScaleTween() : PropertyTween<AudioStreamPlayer, float>(
    static n => n.PitchScale, static (n, value) => n.PitchScale = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer2D AudioVolumeDb2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioVolumeDb2DTween() : PropertyTween<AudioStreamPlayer2D, float>(
    static n => n.VolumeDb, static (n, value) => n.VolumeDb = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer2D AudioVolumeLinear2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioVolumeLinear2DTween() : PropertyTween<AudioStreamPlayer2D, float>(
    static n => n.VolumeLinear, static (n, value) => n.VolumeLinear = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer2D AudioPitchScale2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioPitchScale2DTween() : PropertyTween<AudioStreamPlayer2D, float>(
    static n => n.PitchScale, static (n, value) => n.PitchScale = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D AudioVolumeDb3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioVolumeDb3DTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.VolumeDb, static (n, value) => n.VolumeDb = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D AudioVolumeLinear3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioVolumeLinear3DTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.VolumeLinear, static (n, value) => n.VolumeLinear = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D AudioPitchScale3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioPitchScale3DTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.PitchScale, static (n, value) => n.PitchScale = value, Interpolators.Float);

