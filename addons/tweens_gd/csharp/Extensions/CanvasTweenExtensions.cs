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
    /// <summary>Starts a CanvasLayerOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, Vector2> TweenOffset(this CanvasLayer target,
        Vector2 to, Duration duration, Action<CanvasLayerOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<CanvasLayer, Vector2> TweenOffset(this CanvasLayer target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasLayerOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenOffsetX(this CanvasLayer target,
        double to, Duration duration, Action<CanvasLayerOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenOffsetX(this CanvasLayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasLayerOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenOffsetY(this CanvasLayer target,
        double to, Duration duration, Action<CanvasLayerOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenOffsetY(this CanvasLayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasLayerScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, Vector2> TweenScale(this CanvasLayer target,
        Vector2 to, Duration duration, Action<CanvasLayerScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerScaleTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<CanvasLayer, Vector2> TweenScale(this CanvasLayer target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasLayerScaleXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenScaleX(this CanvasLayer target,
        double to, Duration duration, Action<CanvasLayerScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerScaleXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerScaleXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenScaleX(this CanvasLayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerScaleXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasLayerScaleYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenScaleY(this CanvasLayer target,
        double to, Duration duration, Action<CanvasLayerScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerScaleYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerScaleYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenScaleY(this CanvasLayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerScaleYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasLayerRotationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasLayer, float> TweenRotation(this CanvasLayer target,
        double to, Duration duration, Action<CanvasLayerRotationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasLayerRotationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasLayerRotationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CanvasLayer, float> TweenRotation(this CanvasLayer target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasLayerRotationTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasModulateColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasModulate, Color> TweenColor(this CanvasModulate target,
        Color to, Duration duration, Action<CanvasModulateColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasModulateColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasModulateColorTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CanvasModulate, Color> TweenColor(this CanvasModulate target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasModulateColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CanvasModulateColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CanvasModulate, float> TweenColorAlpha(this CanvasModulate target,
        double to, Duration duration, Action<CanvasModulateColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CanvasModulateColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CanvasModulateColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CanvasModulate, float> TweenColorAlpha(this CanvasModulate target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CanvasModulateColorAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DScrollOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, Vector2> TweenScrollOffset(this Parallax2D target,
        Vector2 to, Duration duration, Action<Parallax2DScrollOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DScrollOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DScrollOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, Vector2> TweenScrollOffset(this Parallax2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DScrollOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DScrollOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, float> TweenScrollOffsetX(this Parallax2D target,
        double to, Duration duration, Action<Parallax2DScrollOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DScrollOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DScrollOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, float> TweenScrollOffsetX(this Parallax2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DScrollOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DScrollOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, float> TweenScrollOffsetY(this Parallax2D target,
        double to, Duration duration, Action<Parallax2DScrollOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DScrollOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DScrollOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, float> TweenScrollOffsetY(this Parallax2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DScrollOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DScrollScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, Vector2> TweenScrollScale(this Parallax2D target,
        Vector2 to, Duration duration, Action<Parallax2DScrollScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DScrollScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DScrollScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, Vector2> TweenScrollScale(this Parallax2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DScrollScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DScrollScaleXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, float> TweenScrollScaleX(this Parallax2D target,
        double to, Duration duration, Action<Parallax2DScrollScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DScrollScaleXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DScrollScaleXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, float> TweenScrollScaleX(this Parallax2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DScrollScaleXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DScrollScaleYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, float> TweenScrollScaleY(this Parallax2D target,
        double to, Duration duration, Action<Parallax2DScrollScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DScrollScaleYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DScrollScaleYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, float> TweenScrollScaleY(this Parallax2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DScrollScaleYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DAutoscrollTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, Vector2> TweenAutoscroll(this Parallax2D target,
        Vector2 to, Duration duration, Action<Parallax2DAutoscrollTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DAutoscrollTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DAutoscrollTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, Vector2> TweenAutoscroll(this Parallax2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DAutoscrollTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DAutoscrollXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, float> TweenAutoscrollX(this Parallax2D target,
        double to, Duration duration, Action<Parallax2DAutoscrollXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DAutoscrollXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DAutoscrollXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, float> TweenAutoscrollX(this Parallax2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DAutoscrollXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Parallax2DAutoscrollYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Parallax2D, float> TweenAutoscrollY(this Parallax2D target,
        double to, Duration duration, Action<Parallax2DAutoscrollYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Parallax2DAutoscrollYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Parallax2DAutoscrollYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Parallax2D, float> TweenAutoscrollY(this Parallax2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Parallax2DAutoscrollYTween { To = (float)to, Duration = duration }, options), playback);
}
