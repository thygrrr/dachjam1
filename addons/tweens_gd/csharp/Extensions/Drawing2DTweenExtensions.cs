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
    /// <summary>Starts a Sprite2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Sprite2D, Vector2> TweenOffset(this Sprite2D target,
        Vector2 to, Duration duration, Action<Sprite2DOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Sprite2DOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Sprite2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Sprite2D, Vector2> TweenOffset(this Sprite2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Sprite2DOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Sprite2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Sprite2D, float> TweenOffsetX(this Sprite2D target,
        double to, Duration duration, Action<Sprite2DOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Sprite2DOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Sprite2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Sprite2D, float> TweenOffsetX(this Sprite2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Sprite2DOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Sprite2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Sprite2D, float> TweenOffsetY(this Sprite2D target,
        double to, Duration duration, Action<Sprite2DOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Sprite2DOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Sprite2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Sprite2D, float> TweenOffsetY(this Sprite2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Sprite2DOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Sprite2DRegionRectTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Sprite2D, Rect2> TweenRegionRect(this Sprite2D target,
        Rect2 to, Duration duration, Action<Sprite2DRegionRectTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Sprite2DRegionRectTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Sprite2DRegionRectTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Sprite2D, Rect2> TweenRegionRect(this Sprite2D target,
        Rect2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Sprite2DRegionRectTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Line2DWidthTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Line2D, float> TweenWidth(this Line2D target,
        double to, Duration duration, Action<Line2DWidthTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Line2DWidthTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Line2DWidthTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Line2D, float> TweenWidth(this Line2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Line2DWidthTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Line2DDefaultColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Line2D, Color> TweenDefaultColor(this Line2D target,
        Color to, Duration duration, Action<Line2DDefaultColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Line2DDefaultColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Line2DDefaultColorTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Line2D, Color> TweenDefaultColor(this Line2D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Line2DDefaultColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Line2DDefaultColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Line2D, float> TweenDefaultColorAlpha(this Line2D target,
        double to, Duration duration, Action<Line2DDefaultColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Line2DDefaultColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Line2DDefaultColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Line2D, float> TweenDefaultColorAlpha(this Line2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Line2DDefaultColorAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, Color> TweenColor(this Polygon2D target,
        Color to, Duration duration, Action<Polygon2DColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DColorTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Polygon2D, Color> TweenColor(this Polygon2D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenColorAlpha(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, float> TweenColorAlpha(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DColorAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, Vector2> TweenOffset(this Polygon2D target,
        Vector2 to, Duration duration, Action<Polygon2DOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Polygon2D, Vector2> TweenOffset(this Polygon2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenOffsetX(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Polygon2D, float> TweenOffsetX(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenOffsetY(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Polygon2D, float> TweenOffsetY(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, Vector2> TweenTextureOffset(this Polygon2D target,
        Vector2 to, Duration duration, Action<Polygon2DTextureOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, Vector2> TweenTextureOffset(this Polygon2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenTextureOffsetX(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DTextureOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, float> TweenTextureOffsetX(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenTextureOffsetY(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DTextureOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, float> TweenTextureOffsetY(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, Vector2> TweenTextureScale(this Polygon2D target,
        Vector2 to, Duration duration, Action<Polygon2DTextureScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, Vector2> TweenTextureScale(this Polygon2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureScaleXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenTextureScaleX(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DTextureScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureScaleXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureScaleXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, float> TweenTextureScaleX(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureScaleXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureScaleYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenTextureScaleY(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DTextureScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureScaleYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureScaleYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, float> TweenTextureScaleY(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureScaleYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Polygon2DTextureRotationTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Polygon2D, float> TweenTextureRotation(this Polygon2D target,
        double to, Duration duration, Action<Polygon2DTextureRotationTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Polygon2DTextureRotationTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Polygon2DTextureRotationTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Polygon2D, float> TweenTextureRotation(this Polygon2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Polygon2DTextureRotationTween { To = (float)to, Duration = duration }, options), playback);
}
