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
    /// <summary>Starts a PathFollow2DProgressTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow2D, float> TweenProgress(this PathFollow2D target,
        double to, Duration duration, Action<PathFollow2DProgressTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow2DProgressTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow2DProgressTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow2D, float> TweenProgress(this PathFollow2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow2DProgressTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow2DProgressRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow2D, float> TweenProgressRatio(this PathFollow2D target,
        double to, Duration duration, Action<PathFollow2DProgressRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow2DProgressRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow2DProgressRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow2D, float> TweenProgressRatio(this PathFollow2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow2DProgressRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow2DHOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow2D, float> TweenHOffset(this PathFollow2D target,
        double to, Duration duration, Action<PathFollow2DHOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow2DHOffsetTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow2DHOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow2D, float> TweenHOffset(this PathFollow2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow2DHOffsetTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow2DVOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow2D, float> TweenVOffset(this PathFollow2D target,
        double to, Duration duration, Action<PathFollow2DVOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow2DVOffsetTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow2DVOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow2D, float> TweenVOffset(this PathFollow2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow2DVOffsetTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow3DProgressTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow3D, float> TweenProgress(this PathFollow3D target,
        double to, Duration duration, Action<PathFollow3DProgressTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow3DProgressTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow3DProgressTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow3D, float> TweenProgress(this PathFollow3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow3DProgressTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow3DProgressRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow3D, float> TweenProgressRatio(this PathFollow3D target,
        double to, Duration duration, Action<PathFollow3DProgressRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow3DProgressRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow3DProgressRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow3D, float> TweenProgressRatio(this PathFollow3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow3DProgressRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow3DHOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow3D, float> TweenHOffset(this PathFollow3D target,
        double to, Duration duration, Action<PathFollow3DHOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow3DHOffsetTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow3DHOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow3D, float> TweenHOffset(this PathFollow3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow3DHOffsetTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a PathFollow3DVOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<PathFollow3D, float> TweenVOffset(this PathFollow3D target,
        double to, Duration duration, Action<PathFollow3DVOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new PathFollow3DVOffsetTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a PathFollow3DVOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<PathFollow3D, float> TweenVOffset(this PathFollow3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new PathFollow3DVOffsetTween { To = (float)to, Duration = duration }, options), playback);
}
