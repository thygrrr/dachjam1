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
    /// <summary>Starts a AnimatedSprite2DSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AnimatedSprite2D, float> TweenSpeedScale(this AnimatedSprite2D target,
        double to, Duration duration, Action<AnimatedSprite2DSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AnimatedSprite2DSpeedScaleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AnimatedSprite2DSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AnimatedSprite2D, float> TweenSpeedScale(this AnimatedSprite2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AnimatedSprite2DSpeedScaleTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AnimatedSprite3DSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AnimatedSprite3D, float> TweenSpeedScale(this AnimatedSprite3D target,
        double to, Duration duration, Action<AnimatedSprite3DSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AnimatedSprite3DSpeedScaleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AnimatedSprite3DSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AnimatedSprite3D, float> TweenSpeedScale(this AnimatedSprite3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AnimatedSprite3DSpeedScaleTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a AnimationPlayerSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AnimationPlayer, float> TweenSpeedScale(this AnimationPlayer target,
        double to, Duration duration, Action<AnimationPlayerSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AnimationPlayerSpeedScaleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a AnimationPlayerSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AnimationPlayer, float> TweenSpeedScale(this AnimationPlayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AnimationPlayerSpeedScaleTween { To = (float)to, Duration = duration }, options), playback);
}
