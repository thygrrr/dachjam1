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

public static partial class TweenExtensions
{
    /// <summary>Starts a PointLight2DTextureScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PointLight2D, float> TweenTextureScale(this PointLight2D target,
        double to, Duration duration, Action<PointLight2DTextureScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PointLight2DTextureScaleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PointLight2DTextureScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PointLight2D, float> TweenTextureScale(this PointLight2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PointLight2DTextureScaleTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PointLight2DHeightTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PointLight2D, float> TweenHeight(this PointLight2D target,
        double to, Duration duration, Action<PointLight2DHeightTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PointLight2DHeightTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PointLight2DHeightTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<PointLight2D, float> TweenHeight(this PointLight2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PointLight2DHeightTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PointLight2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PointLight2D, Vector2> TweenOffset(this PointLight2D target,
        Vector2 to, Duration duration, Action<PointLight2DOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PointLight2DOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a PointLight2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<PointLight2D, Vector2> TweenOffset(this PointLight2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PointLight2DOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a PointLight2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PointLight2D, float> TweenOffsetX(this PointLight2D target,
        double to, Duration duration, Action<PointLight2DOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PointLight2DOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PointLight2DOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PointLight2D, float> TweenOffsetX(this PointLight2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PointLight2DOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PointLight2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PointLight2D, float> TweenOffsetY(this PointLight2D target,
        double to, Duration duration, Action<PointLight2DOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PointLight2DOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PointLight2DOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PointLight2D, float> TweenOffsetY(this PointLight2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PointLight2DOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Light2DShadowColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light2D, Color> TweenShadowColor(this Light2D target,
        Color to, Duration duration, Action<Light2DShadowColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Light2DShadowColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Light2DShadowColorTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Light2D, Color> TweenShadowColor(this Light2D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Light2DShadowColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Light2DShadowColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light2D, float> TweenShadowColorAlpha(this Light2D target,
        double to, Duration duration, Action<Light2DShadowColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Light2DShadowColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Light2DShadowColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Light2D, float> TweenShadowColorAlpha(this Light2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Light2DShadowColorAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Light3DLightTemperatureTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light3D, float> TweenLightTemperature(this Light3D target,
        double to, Duration duration, Action<Light3DLightTemperatureTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Light3DLightTemperatureTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Light3DLightTemperatureTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Light3D, float> TweenLightTemperature(this Light3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Light3DLightTemperatureTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Light3DLightIndirectEnergyTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light3D, float> TweenLightIndirectEnergy(this Light3D target,
        double to, Duration duration, Action<Light3DLightIndirectEnergyTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Light3DLightIndirectEnergyTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Light3DLightIndirectEnergyTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Light3D, float> TweenLightIndirectEnergy(this Light3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Light3DLightIndirectEnergyTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Light3DLightVolumetricFogEnergyTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light3D, float> TweenLightVolumetricFogEnergy(this Light3D target,
        double to, Duration duration, Action<Light3DLightVolumetricFogEnergyTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Light3DLightVolumetricFogEnergyTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Light3DLightVolumetricFogEnergyTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Light3D, float> TweenLightVolumetricFogEnergy(this Light3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Light3DLightVolumetricFogEnergyTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Light3DShadowOpacityTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light3D, float> TweenShadowOpacity(this Light3D target,
        double to, Duration duration, Action<Light3DShadowOpacityTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Light3DShadowOpacityTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Light3DShadowOpacityTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Light3D, float> TweenShadowOpacity(this Light3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Light3DShadowOpacityTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a OmniLight3DOmniAttenuationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<OmniLight3D, float> TweenOmniAttenuation(this OmniLight3D target,
        double to, Duration duration, Action<OmniLight3DOmniAttenuationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new OmniLight3DOmniAttenuationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a OmniLight3DOmniAttenuationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<OmniLight3D, float> TweenOmniAttenuation(this OmniLight3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new OmniLight3DOmniAttenuationTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpotLight3DSpotAttenuationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpotLight3D, float> TweenSpotAttenuation(this SpotLight3D target,
        double to, Duration duration, Action<SpotLight3DSpotAttenuationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpotLight3DSpotAttenuationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpotLight3DSpotAttenuationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpotLight3D, float> TweenSpotAttenuation(this SpotLight3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpotLight3DSpotAttenuationTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpotLight3DSpotAngleAttenuationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpotLight3D, float> TweenSpotAngleAttenuation(this SpotLight3D target,
        double to, Duration duration, Action<SpotLight3DSpotAngleAttenuationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpotLight3DSpotAngleAttenuationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpotLight3DSpotAngleAttenuationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpotLight3D, float> TweenSpotAngleAttenuation(this SpotLight3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpotLight3DSpotAngleAttenuationTween { To = (float)to, Duration = duration }, options), playback);
}
