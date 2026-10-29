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
    /// <summary>Starts a ControlPivotOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenPivotOffset(this Control target,
        Vector2 to, Duration duration, Action<ControlPivotOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPivotOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPivotOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector2> TweenPivotOffset(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPivotOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPivotOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenPivotOffsetX(this Control target,
        double to, Duration duration, Action<ControlPivotOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPivotOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPivotOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenPivotOffsetX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPivotOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPivotOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenPivotOffsetY(this Control target,
        double to, Duration duration, Action<ControlPivotOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPivotOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPivotOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenPivotOffsetY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPivotOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPivotOffsetRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenPivotOffsetRatio(this Control target,
        Vector2 to, Duration duration, Action<ControlPivotOffsetRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPivotOffsetRatioTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPivotOffsetRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenPivotOffsetRatio(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPivotOffsetRatioTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPivotOffsetRatioXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenPivotOffsetRatioX(this Control target,
        double to, Duration duration, Action<ControlPivotOffsetRatioXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPivotOffsetRatioXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPivotOffsetRatioXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenPivotOffsetRatioX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPivotOffsetRatioXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPivotOffsetRatioYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenPivotOffsetRatioY(this Control target,
        double to, Duration duration, Action<ControlPivotOffsetRatioYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPivotOffsetRatioYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPivotOffsetRatioYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenPivotOffsetRatioY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPivotOffsetRatioYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlCustomMinimumSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenCustomMinimumSize(this Control target,
        Vector2 to, Duration duration, Action<ControlCustomMinimumSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlCustomMinimumSizeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlCustomMinimumSizeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenCustomMinimumSize(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlCustomMinimumSizeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlCustomMinimumSizeXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenCustomMinimumSizeX(this Control target,
        double to, Duration duration, Action<ControlCustomMinimumSizeXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlCustomMinimumSizeXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlCustomMinimumSizeXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenCustomMinimumSizeX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlCustomMinimumSizeXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlCustomMinimumSizeYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenCustomMinimumSizeY(this Control target,
        double to, Duration duration, Action<ControlCustomMinimumSizeYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlCustomMinimumSizeYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlCustomMinimumSizeYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenCustomMinimumSizeY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlCustomMinimumSizeYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlCustomMaximumSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenCustomMaximumSize(this Control target,
        Vector2 to, Duration duration, Action<ControlCustomMaximumSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlCustomMaximumSizeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlCustomMaximumSizeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenCustomMaximumSize(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlCustomMaximumSizeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlCustomMaximumSizeXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenCustomMaximumSizeX(this Control target,
        double to, Duration duration, Action<ControlCustomMaximumSizeXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlCustomMaximumSizeXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlCustomMaximumSizeXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenCustomMaximumSizeX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlCustomMaximumSizeXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlCustomMaximumSizeYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenCustomMaximumSizeY(this Control target,
        double to, Duration duration, Action<ControlCustomMaximumSizeYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlCustomMaximumSizeYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlCustomMaximumSizeYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenCustomMaximumSizeY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlCustomMaximumSizeYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPositionTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPosition(this Control target,
        Vector2 to, Duration duration, Action<ControlOffsetTransformPositionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPositionTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPositionTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPosition(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPositionTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPositionXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionX(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPositionXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPositionXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPositionXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPositionXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPositionYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionY(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPositionYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPositionYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPositionYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPositionYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPositionRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPositionRatio(this Control target,
        Vector2 to, Duration duration, Action<ControlOffsetTransformPositionRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPositionRatioTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPositionRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPositionRatio(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPositionRatioTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPositionRatioXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionRatioX(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPositionRatioXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPositionRatioXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPositionRatioXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionRatioX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPositionRatioXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPositionRatioYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionRatioY(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPositionRatioYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPositionRatioYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPositionRatioYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPositionRatioY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPositionRatioYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformScale(this Control target,
        Vector2 to, Duration duration, Action<ControlOffsetTransformScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformScale(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformScaleXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformScaleX(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformScaleXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformScaleXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformScaleX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformScaleXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformScaleYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformScaleY(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformScaleYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformScaleYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformScaleY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformScaleYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPivotTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPivot(this Control target,
        Vector2 to, Duration duration, Action<ControlOffsetTransformPivotTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPivotTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPivotTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPivot(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPivotTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPivotXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotX(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPivotXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPivotXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPivotXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPivotXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPivotYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotY(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPivotYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPivotYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPivotYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPivotYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPivotRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPivotRatio(this Control target,
        Vector2 to, Duration duration, Action<ControlOffsetTransformPivotRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPivotRatioTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPivotRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenOffsetTransformPivotRatio(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPivotRatioTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPivotRatioXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotRatioX(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPivotRatioXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPivotRatioXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPivotRatioXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotRatioX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPivotRatioXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformPivotRatioYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotRatioY(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformPivotRatioYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformPivotRatioYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformPivotRatioYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformPivotRatioY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformPivotRatioYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlSizeFlagsStretchRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenSizeFlagsStretchRatio(this Control target,
        double to, Duration duration, Action<ControlSizeFlagsStretchRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlSizeFlagsStretchRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlSizeFlagsStretchRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenSizeFlagsStretchRatio(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlSizeFlagsStretchRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTransformRotationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTransformRotation(this Control target,
        double to, Duration duration, Action<ControlOffsetTransformRotationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTransformRotationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTransformRotationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetTransformRotation(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTransformRotationTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlAnchorLeftTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenAnchorLeft(this Control target,
        double to, Duration duration, Action<ControlAnchorLeftTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlAnchorLeftTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlAnchorLeftTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenAnchorLeft(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlAnchorLeftTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetLeftTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetLeft(this Control target,
        double to, Duration duration, Action<ControlOffsetLeftTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetLeftTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetLeftTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenOffsetLeft(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetLeftTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlAnchorTopTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenAnchorTop(this Control target,
        double to, Duration duration, Action<ControlAnchorTopTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlAnchorTopTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlAnchorTopTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenAnchorTop(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlAnchorTopTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetTopTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTop(this Control target,
        double to, Duration duration, Action<ControlOffsetTopTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetTopTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetTopTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenOffsetTop(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetTopTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlAnchorRightTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenAnchorRight(this Control target,
        double to, Duration duration, Action<ControlAnchorRightTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlAnchorRightTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlAnchorRightTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenAnchorRight(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlAnchorRightTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetRightTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetRight(this Control target,
        double to, Duration duration, Action<ControlOffsetRightTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetRightTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetRightTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenOffsetRight(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetRightTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlAnchorBottomTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenAnchorBottom(this Control target,
        double to, Duration duration, Action<ControlAnchorBottomTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlAnchorBottomTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlAnchorBottomTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenAnchorBottom(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlAnchorBottomTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetBottomTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenOffsetBottom(this Control target,
        double to, Duration duration, Action<ControlOffsetBottomTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetBottomTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetBottomTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenOffsetBottom(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetBottomTween { To = (float)to, Duration = duration }, options), playback);
}
