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
    /// <summary>Starts a LightColor2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light2D, Color> TweenColor(this Light2D target,
        Color to, Duration duration, Action<LightColor2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new LightColor2DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a LightColor2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Light2D, Color> TweenColor(this Light2D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new LightColor2DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a LightEnergy2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light2D, float> TweenEnergy(this Light2D target,
        double to, Duration duration, Action<LightEnergy2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new LightEnergy2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a LightEnergy2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Light2D, float> TweenEnergy(this Light2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new LightEnergy2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a LightColor3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light3D, Color> TweenLightColor(this Light3D target,
        Color to, Duration duration, Action<LightColor3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new LightColor3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a LightColor3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Light3D, Color> TweenLightColor(this Light3D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new LightColor3DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a LightEnergy3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Light3D, float> TweenLightEnergy(this Light3D target,
        double to, Duration duration, Action<LightEnergy3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new LightEnergy3DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a LightEnergy3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Light3D, float> TweenLightEnergy(this Light3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new LightEnergy3DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a OmniRangeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<OmniLight3D, float> TweenOmniRange(this OmniLight3D target,
        double to, Duration duration, Action<OmniRangeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new OmniRangeTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a OmniRangeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<OmniLight3D, float> TweenOmniRange(this OmniLight3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new OmniRangeTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpotRangeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpotLight3D, float> TweenSpotRange(this SpotLight3D target,
        double to, Duration duration, Action<SpotRangeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpotRangeTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpotRangeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<SpotLight3D, float> TweenSpotRange(this SpotLight3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpotRangeTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpotAngleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpotLight3D, float> TweenSpotAngle(this SpotLight3D target,
        double to, Duration duration, Action<SpotAngleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpotAngleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpotAngleTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<SpotLight3D, float> TweenSpotAngle(this SpotLight3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpotAngleTween { To = (float)to, Duration = duration }, options), playback);
}
