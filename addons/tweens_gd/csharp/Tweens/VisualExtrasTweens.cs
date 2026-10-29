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

/// <summary>Animates SpringArm3D.SpringLength in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpringArm3DSpringLengthTween() : PropertyTween<SpringArm3D, float>(
    static n => n.SpringLength, static (n, value) => n.SpringLength = value, Interpolators.Float);

/// <summary>Animates Decal.Modulate in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalModulateTween() : PropertyTween<Decal, Color>(
    static n => n.Modulate, static (n, value) => n.Modulate = value, Interpolators.Color);

/// <summary>Animates Decal.Modulate.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalModulateAlphaTween() : PropertyTween<Decal, float>(
    static n => n.Modulate.A, static (n, value) => { var current = n.Modulate; current.A = value; n.Modulate = current; }, Interpolators.Float);

/// <summary>Animates Decal.Size in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalSizeTween() : PropertyTween<Decal, Vector3>(
    static n => n.Size, static (n, value) => n.Size = value, Interpolators.Vector3);

/// <summary>Animates Decal.Size.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalSizeXTween() : PropertyTween<Decal, float>(
    static n => n.Size.X, static (n, value) => { var current = n.Size; current.X = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates Decal.Size.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalSizeYTween() : PropertyTween<Decal, float>(
    static n => n.Size.Y, static (n, value) => { var current = n.Size; current.Y = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates Decal.Size.Z in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalSizeZTween() : PropertyTween<Decal, float>(
    static n => n.Size.Z, static (n, value) => { var current = n.Size; current.Z = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates Decal.EmissionEnergy in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DecalEmissionEnergyTween() : PropertyTween<Decal, float>(
    static n => n.EmissionEnergy, static (n, value) => n.EmissionEnergy = value, Interpolators.Float);

/// <summary>Animates FogVolume.Size in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class FogVolumeSizeTween() : PropertyTween<FogVolume, Vector3>(
    static n => n.Size, static (n, value) => n.Size = value, Interpolators.Vector3);

/// <summary>Animates FogVolume.Size.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class FogVolumeSizeXTween() : PropertyTween<FogVolume, float>(
    static n => n.Size.X, static (n, value) => { var current = n.Size; current.X = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates FogVolume.Size.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class FogVolumeSizeYTween() : PropertyTween<FogVolume, float>(
    static n => n.Size.Y, static (n, value) => { var current = n.Size; current.Y = value; n.Size = current; }, Interpolators.Float);

/// <summary>Animates FogVolume.Size.Z in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class FogVolumeSizeZTween() : PropertyTween<FogVolume, float>(
    static n => n.Size.Z, static (n, value) => { var current = n.Size; current.Z = value; n.Size = current; }, Interpolators.Float);
