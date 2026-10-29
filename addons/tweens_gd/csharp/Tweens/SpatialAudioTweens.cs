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

/// <summary>Animates AudioStreamPlayer2D.PanningStrength in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer2DPanningStrengthTween() : PropertyTween<AudioStreamPlayer2D, float>(
    static n => n.PanningStrength, static (n, value) => n.PanningStrength = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer2D.MaxDistance in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer2DMaxDistanceTween() : PropertyTween<AudioStreamPlayer2D, float>(
    static n => n.MaxDistance, static (n, value) => n.MaxDistance = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.PanningStrength in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DPanningStrengthTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.PanningStrength, static (n, value) => n.PanningStrength = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.MaxDistance in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DMaxDistanceTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.MaxDistance, static (n, value) => n.MaxDistance = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer2D.Attenuation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer2DAttenuationTween() : PropertyTween<AudioStreamPlayer2D, float>(
    static n => n.Attenuation, static (n, value) => n.Attenuation = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.UnitSize in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DUnitSizeTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.UnitSize, static (n, value) => n.UnitSize = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.EmissionAngleDegrees in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DEmissionAngleDegreesTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.EmissionAngleDegrees, static (n, value) => n.EmissionAngleDegrees = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.EmissionAngleFilterAttenuationDb in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DEmissionAngleFilterAttenuationDbTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.EmissionAngleFilterAttenuationDb, static (n, value) => n.EmissionAngleFilterAttenuationDb = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.AttenuationFilterCutoffHz in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DAttenuationFilterCutoffHzTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.AttenuationFilterCutoffHz, static (n, value) => n.AttenuationFilterCutoffHz = value, Interpolators.Float);

/// <summary>Animates AudioStreamPlayer3D.AttenuationFilterDb in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AudioStreamPlayer3DAttenuationFilterDbTween() : PropertyTween<AudioStreamPlayer3D, float>(
    static n => n.AttenuationFilterDb, static (n, value) => n.AttenuationFilterDb = value, Interpolators.Float);
