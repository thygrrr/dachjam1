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
    /// <summary>Starts a AudioStreamPlayer2DPanningStrengthTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenPanningStrength(this AudioStreamPlayer2D target,
        double to, Duration duration, Action<AudioStreamPlayer2DPanningStrengthTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer2DPanningStrengthTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer2DPanningStrengthTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenPanningStrength(this AudioStreamPlayer2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer2DPanningStrengthTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer2DMaxDistanceTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenMaxDistance(this AudioStreamPlayer2D target,
        double to, Duration duration, Action<AudioStreamPlayer2DMaxDistanceTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer2DMaxDistanceTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer2DMaxDistanceTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenMaxDistance(this AudioStreamPlayer2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer2DMaxDistanceTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DPanningStrengthTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenPanningStrength(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DPanningStrengthTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DPanningStrengthTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DPanningStrengthTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenPanningStrength(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DPanningStrengthTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DMaxDistanceTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenMaxDistance(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DMaxDistanceTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DMaxDistanceTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DMaxDistanceTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenMaxDistance(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DMaxDistanceTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer2DAttenuationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenAttenuation(this AudioStreamPlayer2D target,
        double to, Duration duration, Action<AudioStreamPlayer2DAttenuationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer2DAttenuationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer2DAttenuationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenAttenuation(this AudioStreamPlayer2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer2DAttenuationTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DUnitSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenUnitSize(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DUnitSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DUnitSizeTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DUnitSizeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenUnitSize(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DUnitSizeTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DEmissionAngleDegreesTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenEmissionAngleDegrees(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DEmissionAngleDegreesTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DEmissionAngleDegreesTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DEmissionAngleDegreesTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenEmissionAngleDegrees(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DEmissionAngleDegreesTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DEmissionAngleFilterAttenuationDbTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenEmissionAngleFilterAttenuationDb(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DEmissionAngleFilterAttenuationDbTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DEmissionAngleFilterAttenuationDbTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DEmissionAngleFilterAttenuationDbTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenEmissionAngleFilterAttenuationDb(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DEmissionAngleFilterAttenuationDbTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DAttenuationFilterCutoffHzTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenAttenuationFilterCutoffHz(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DAttenuationFilterCutoffHzTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DAttenuationFilterCutoffHzTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DAttenuationFilterCutoffHzTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenAttenuationFilterCutoffHz(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DAttenuationFilterCutoffHzTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioStreamPlayer3DAttenuationFilterDbTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenAttenuationFilterDb(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioStreamPlayer3DAttenuationFilterDbTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioStreamPlayer3DAttenuationFilterDbTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioStreamPlayer3DAttenuationFilterDbTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenAttenuationFilterDb(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioStreamPlayer3DAttenuationFilterDbTween { To = (float)to, Duration = duration }, options), playback);
}
