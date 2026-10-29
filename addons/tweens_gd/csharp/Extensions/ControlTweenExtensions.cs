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
    /// <summary>Starts a ControlPositionTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenPosition(this Control target,
        Vector2 to, Duration duration, Action<ControlPositionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPositionTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPositionTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector2> TweenPosition(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPositionTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPositionXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenPositionX(this Control target,
        double to, Duration duration, Action<ControlPositionXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPositionXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPositionXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenPositionX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPositionXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlPositionYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenPositionY(this Control target,
        double to, Duration duration, Action<ControlPositionYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlPositionYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlPositionYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenPositionY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlPositionYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlGlobalPositionTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenGlobalPosition(this Control target,
        Vector2 to, Duration duration, Action<ControlGlobalPositionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlGlobalPositionTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlGlobalPositionTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, Vector2> TweenGlobalPosition(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlGlobalPositionTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlGlobalPositionXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenGlobalPositionX(this Control target,
        double to, Duration duration, Action<ControlGlobalPositionXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlGlobalPositionXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlGlobalPositionXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenGlobalPositionX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlGlobalPositionXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlGlobalPositionYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenGlobalPositionY(this Control target,
        double to, Duration duration, Action<ControlGlobalPositionYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlGlobalPositionYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlGlobalPositionYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Control, float> TweenGlobalPositionY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlGlobalPositionYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenSize(this Control target,
        Vector2 to, Duration duration, Action<ControlSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlSizeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlSizeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector2> TweenSize(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlSizeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlSizeXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenSizeX(this Control target,
        double to, Duration duration, Action<ControlSizeXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlSizeXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlSizeXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenSizeX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlSizeXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlSizeYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenSizeY(this Control target,
        double to, Duration duration, Action<ControlSizeYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlSizeYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlSizeYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenSizeY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlSizeYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenScale(this Control target,
        Vector2 to, Duration duration, Action<ControlScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlScaleTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector2> TweenScale(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlScaleXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenScaleX(this Control target,
        double to, Duration duration, Action<ControlScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlScaleXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlScaleXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenScaleX(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlScaleXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlScaleYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenScaleY(this Control target,
        double to, Duration duration, Action<ControlScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlScaleYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlScaleYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenScaleY(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlScaleYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlRotationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, float> TweenRotation(this Control target,
        double to, Duration duration, Action<ControlRotationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlRotationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlRotationTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, float> TweenRotation(this Control target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlRotationTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a RangeValueTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Godot.Range, double> TweenValue(this Godot.Range target,
        double to, Duration duration, Action<RangeValueTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new RangeValueTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a RangeValueTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Godot.Range, double> TweenValue(this Godot.Range target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new RangeValueTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlAnchorMinTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenAnchorMin(this Control target,
        Vector2 to, Duration duration, Action<ControlAnchorMinTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlAnchorMinTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlAnchorMinTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector2> TweenAnchorMin(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlAnchorMinTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlAnchorMaxTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector2> TweenAnchorMax(this Control target,
        Vector2 to, Duration duration, Action<ControlAnchorMaxTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlAnchorMaxTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlAnchorMaxTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector2> TweenAnchorMax(this Control target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlAnchorMaxTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ControlOffsetsTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Control, Vector4> TweenOffsets(this Control target,
        Vector4 to, Duration duration, Action<ControlOffsetsTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ControlOffsetsTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ControlOffsetsTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Control, Vector4> TweenOffsets(this Control target,
        Vector4 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ControlOffsetsTween { To = to, Duration = duration }, options), playback);
}
