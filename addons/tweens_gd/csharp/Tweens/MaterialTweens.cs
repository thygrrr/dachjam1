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

/// <summary>Animates BaseMaterial3D.AlbedoColor on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialAlbedoColorTween() : PropertyTween<BaseMaterial3D, Color>(
    static m => m.AlbedoColor, static (m, value) => m.AlbedoColor = value, Interpolators.Color);

/// <summary>Animates BaseMaterial3D.AlbedoColor.A on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialAlbedoAlphaTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.AlbedoColor.A, static (m, value) => { var current = m.AlbedoColor; current.A = value; m.AlbedoColor = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Metallic on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialMetallicTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Metallic, static (m, value) => m.Metallic = value, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.MetallicSpecular on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialMetallicSpecularTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.MetallicSpecular, static (m, value) => m.MetallicSpecular = value, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Roughness on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialRoughnessTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Roughness, static (m, value) => m.Roughness = value, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.EmissionEnergyMultiplier on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialEmissionEnergyMultiplierTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.EmissionEnergyMultiplier, static (m, value) => m.EmissionEnergyMultiplier = value, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.EmissionIntensity on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialEmissionIntensityTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.EmissionIntensity, static (m, value) => m.EmissionIntensity = value, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.NormalScale on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialNormalScaleTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.NormalScale, static (m, value) => m.NormalScale = value, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Emission on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialEmissionTween() : PropertyTween<BaseMaterial3D, Color>(
    static m => m.Emission, static (m, value) => m.Emission = value, Interpolators.Color);

/// <summary>Animates BaseMaterial3D.Uv1Offset on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1OffsetTween() : PropertyTween<BaseMaterial3D, Vector3>(
    static m => m.Uv1Offset, static (m, value) => m.Uv1Offset = value, Interpolators.Vector3);

/// <summary>Animates BaseMaterial3D.Uv1Offset.X on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1OffsetXTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Uv1Offset.X, static (m, value) => { var current = m.Uv1Offset; current.X = value; m.Uv1Offset = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Uv1Offset.Y on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1OffsetYTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Uv1Offset.Y, static (m, value) => { var current = m.Uv1Offset; current.Y = value; m.Uv1Offset = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Uv1Offset.Z on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1OffsetZTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Uv1Offset.Z, static (m, value) => { var current = m.Uv1Offset; current.Z = value; m.Uv1Offset = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Uv1Scale on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1ScaleTween() : PropertyTween<BaseMaterial3D, Vector3>(
    static m => m.Uv1Scale, static (m, value) => m.Uv1Scale = value, Interpolators.Vector3);

/// <summary>Animates BaseMaterial3D.Uv1Scale.X on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1ScaleXTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Uv1Scale.X, static (m, value) => { var current = m.Uv1Scale; current.X = value; m.Uv1Scale = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Uv1Scale.Y on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1ScaleYTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Uv1Scale.Y, static (m, value) => { var current = m.Uv1Scale; current.Y = value; m.Uv1Scale = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.Uv1Scale.Z on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUv1ScaleZTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.Uv1Scale.Z, static (m, value) => { var current = m.Uv1Scale; current.Z = value; m.Uv1Scale = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.UV2Offset on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2OffsetTween() : PropertyTween<BaseMaterial3D, Vector3>(
    static m => m.UV2Offset, static (m, value) => m.UV2Offset = value, Interpolators.Vector3);

/// <summary>Animates BaseMaterial3D.UV2Offset.X on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2OffsetXTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.UV2Offset.X, static (m, value) => { var current = m.UV2Offset; current.X = value; m.UV2Offset = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.UV2Offset.Y on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2OffsetYTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.UV2Offset.Y, static (m, value) => { var current = m.UV2Offset; current.Y = value; m.UV2Offset = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.UV2Offset.Z on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2OffsetZTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.UV2Offset.Z, static (m, value) => { var current = m.UV2Offset; current.Z = value; m.UV2Offset = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.UV2Scale on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2ScaleTween() : PropertyTween<BaseMaterial3D, Vector3>(
    static m => m.UV2Scale, static (m, value) => m.UV2Scale = value, Interpolators.Vector3);

/// <summary>Animates BaseMaterial3D.UV2Scale.X on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2ScaleXTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.UV2Scale.X, static (m, value) => { var current = m.UV2Scale; current.X = value; m.UV2Scale = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.UV2Scale.Y on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2ScaleYTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.UV2Scale.Y, static (m, value) => { var current = m.UV2Scale; current.Y = value; m.UV2Scale = current; }, Interpolators.Float);

/// <summary>Animates BaseMaterial3D.UV2Scale.Z on the supplied resource, including all its shared users.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialUV2ScaleZTween() : PropertyTween<BaseMaterial3D, float>(
    static m => m.UV2Scale.Z, static (m, value) => { var current = m.UV2Scale; current.Z = value; m.UV2Scale = current; }, Interpolators.Float);
