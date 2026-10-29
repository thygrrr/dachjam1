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
    /// <summary>Starts a AudioVolumeDbTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer, float> TweenVolumeDb(this AudioStreamPlayer target,
        double to, Duration duration, Action<AudioVolumeDbTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioVolumeDbTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioVolumeDbTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer, float> TweenVolumeDb(this AudioStreamPlayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioVolumeDbTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioVolumeLinearTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer, float> TweenVolumeLinear(this AudioStreamPlayer target,
        double to, Duration duration, Action<AudioVolumeLinearTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioVolumeLinearTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioVolumeLinearTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer, float> TweenVolumeLinear(this AudioStreamPlayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioVolumeLinearTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioPitchScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer, float> TweenPitchScale(this AudioStreamPlayer target,
        double to, Duration duration, Action<AudioPitchScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioPitchScaleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioPitchScaleTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer, float> TweenPitchScale(this AudioStreamPlayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioPitchScaleTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioVolumeDb2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenVolumeDb(this AudioStreamPlayer2D target,
        double to, Duration duration, Action<AudioVolumeDb2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioVolumeDb2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioVolumeDb2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenVolumeDb(this AudioStreamPlayer2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioVolumeDb2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioVolumeLinear2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenVolumeLinear(this AudioStreamPlayer2D target,
        double to, Duration duration, Action<AudioVolumeLinear2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioVolumeLinear2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioVolumeLinear2DTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenVolumeLinear(this AudioStreamPlayer2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioVolumeLinear2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioPitchScale2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenPitchScale(this AudioStreamPlayer2D target,
        double to, Duration duration, Action<AudioPitchScale2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioPitchScale2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioPitchScale2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer2D, float> TweenPitchScale(this AudioStreamPlayer2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioPitchScale2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioVolumeDb3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenVolumeDb(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioVolumeDb3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioVolumeDb3DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioVolumeDb3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenVolumeDb(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioVolumeDb3DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioVolumeLinear3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenVolumeLinear(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioVolumeLinear3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioVolumeLinear3DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioVolumeLinear3DTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenVolumeLinear(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioVolumeLinear3DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AudioPitchScale3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenPitchScale(this AudioStreamPlayer3D target,
        double to, Duration duration, Action<AudioPitchScale3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AudioPitchScale3DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AudioPitchScale3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<AudioStreamPlayer3D, float> TweenPitchScale(this AudioStreamPlayer3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AudioPitchScale3DTween { To = (float)to, Duration = duration }, options), playback);
}
