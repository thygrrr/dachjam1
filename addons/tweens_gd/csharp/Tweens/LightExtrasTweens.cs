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

/// <summary>Animates PointLight2D.TextureScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PointLight2DTextureScaleTween() : PropertyTween<PointLight2D, float>(
    static n => n.TextureScale, static (n, value) => n.TextureScale = value, Interpolators.Float);

/// <summary>Animates PointLight2D.Height in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PointLight2DHeightTween() : PropertyTween<PointLight2D, float>(
    static n => n.Height, static (n, value) => n.Height = value, Interpolators.Float);

/// <summary>Animates PointLight2D.Offset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PointLight2DOffsetTween() : PropertyTween<PointLight2D, Vector2>(
    static n => n.Offset, static (n, value) => n.Offset = value, Interpolators.Vector2);

/// <summary>Animates PointLight2D.Offset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PointLight2DOffsetXTween() : PropertyTween<PointLight2D, float>(
    static n => n.Offset.X, static (n, value) => { var current = n.Offset; current.X = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates PointLight2D.Offset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class PointLight2DOffsetYTween() : PropertyTween<PointLight2D, float>(
    static n => n.Offset.Y, static (n, value) => { var current = n.Offset; current.Y = value; n.Offset = current; }, Interpolators.Float);

/// <summary>Animates Light2D.ShadowColor in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Light2DShadowColorTween() : PropertyTween<Light2D, Color>(
    static n => n.ShadowColor, static (n, value) => n.ShadowColor = value, Interpolators.Color);

/// <summary>Animates Light2D.ShadowColor.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Light2DShadowColorAlphaTween() : PropertyTween<Light2D, float>(
    static n => n.ShadowColor.A, static (n, value) => { var current = n.ShadowColor; current.A = value; n.ShadowColor = current; }, Interpolators.Float);

/// <summary>Animates Light3D.LightTemperature in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Light3DLightTemperatureTween() : PropertyTween<Light3D, float>(
    static n => n.LightTemperature, static (n, value) => n.LightTemperature = value, Interpolators.Float);

/// <summary>Animates Light3D.LightIndirectEnergy in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Light3DLightIndirectEnergyTween() : PropertyTween<Light3D, float>(
    static n => n.LightIndirectEnergy, static (n, value) => n.LightIndirectEnergy = value, Interpolators.Float);

/// <summary>Animates Light3D.LightVolumetricFogEnergy in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Light3DLightVolumetricFogEnergyTween() : PropertyTween<Light3D, float>(
    static n => n.LightVolumetricFogEnergy, static (n, value) => n.LightVolumetricFogEnergy = value, Interpolators.Float);

/// <summary>Animates Light3D.ShadowOpacity in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Light3DShadowOpacityTween() : PropertyTween<Light3D, float>(
    static n => n.ShadowOpacity, static (n, value) => n.ShadowOpacity = value, Interpolators.Float);

/// <summary>Animates OmniLight3D.OmniAttenuation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class OmniLight3DOmniAttenuationTween() : PropertyTween<OmniLight3D, float>(
    static n => n.OmniAttenuation, static (n, value) => n.OmniAttenuation = value, Interpolators.Float);

/// <summary>Animates SpotLight3D.SpotAttenuation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpotLight3DSpotAttenuationTween() : PropertyTween<SpotLight3D, float>(
    static n => n.SpotAttenuation, static (n, value) => n.SpotAttenuation = value, Interpolators.Float);

/// <summary>Animates SpotLight3D.SpotAngleAttenuation in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class SpotLight3DSpotAngleAttenuationTween() : PropertyTween<SpotLight3D, float>(
    static n => n.SpotAngleAttenuation, static (n, value) => n.SpotAngleAttenuation = value, Interpolators.Float);
