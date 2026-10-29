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
    /// <summary>Starts a ColorRectColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<ColorRect, Color> TweenColor(this ColorRect target,
        Color to, Duration duration, Action<ColorRectColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ColorRectColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ColorRectColorTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<ColorRect, Color> TweenColor(this ColorRect target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ColorRectColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ColorRectColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<ColorRect, float> TweenColorAlpha(this ColorRect target,
        double to, Duration duration, Action<ColorRectColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ColorRectColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a ColorRectColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<ColorRect, float> TweenColorAlpha(this ColorRect target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ColorRectColorAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a LabelVisibleRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label, float> TweenVisibleRatio(this Label target,
        double to, Duration duration, Action<LabelVisibleRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new LabelVisibleRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a LabelVisibleRatioTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Label, float> TweenVisibleRatio(this Label target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new LabelVisibleRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a RichTextLabelVisibleRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<RichTextLabel, float> TweenVisibleRatio(this RichTextLabel target,
        double to, Duration duration, Action<RichTextLabelVisibleRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new RichTextLabelVisibleRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a RichTextLabelVisibleRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<RichTextLabel, float> TweenVisibleRatio(this RichTextLabel target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new RichTextLabelVisibleRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTintUnderTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, Color> TweenTintUnder(this TextureProgressBar target,
        Color to, Duration duration, Action<TextureProgressBarTintUnderTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTintUnderTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTintUnderTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, Color> TweenTintUnder(this TextureProgressBar target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTintUnderTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTintUnderAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenTintUnderAlpha(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarTintUnderAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTintUnderAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTintUnderAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenTintUnderAlpha(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTintUnderAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTintOverTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, Color> TweenTintOver(this TextureProgressBar target,
        Color to, Duration duration, Action<TextureProgressBarTintOverTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTintOverTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTintOverTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, Color> TweenTintOver(this TextureProgressBar target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTintOverTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTintOverAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenTintOverAlpha(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarTintOverAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTintOverAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTintOverAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenTintOverAlpha(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTintOverAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTintProgressTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, Color> TweenTintProgress(this TextureProgressBar target,
        Color to, Duration duration, Action<TextureProgressBarTintProgressTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTintProgressTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTintProgressTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, Color> TweenTintProgress(this TextureProgressBar target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTintProgressTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTintProgressAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenTintProgressAlpha(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarTintProgressAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTintProgressAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTintProgressAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenTintProgressAlpha(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTintProgressAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarRadialInitialAngleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenRadialInitialAngle(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarRadialInitialAngleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarRadialInitialAngleTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarRadialInitialAngleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenRadialInitialAngle(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarRadialInitialAngleTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarRadialFillDegreesTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenRadialFillDegrees(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarRadialFillDegreesTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarRadialFillDegreesTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarRadialFillDegreesTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenRadialFillDegrees(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarRadialFillDegreesTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarRadialCenterOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, Vector2> TweenRadialCenterOffset(this TextureProgressBar target,
        Vector2 to, Duration duration, Action<TextureProgressBarRadialCenterOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarRadialCenterOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarRadialCenterOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, Vector2> TweenRadialCenterOffset(this TextureProgressBar target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarRadialCenterOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarRadialCenterOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenRadialCenterOffsetX(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarRadialCenterOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarRadialCenterOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarRadialCenterOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenRadialCenterOffsetX(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarRadialCenterOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarRadialCenterOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenRadialCenterOffsetY(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarRadialCenterOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarRadialCenterOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarRadialCenterOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenRadialCenterOffsetY(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarRadialCenterOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTextureProgressOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, Vector2> TweenTextureProgressOffset(this TextureProgressBar target,
        Vector2 to, Duration duration, Action<TextureProgressBarTextureProgressOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTextureProgressOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTextureProgressOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, Vector2> TweenTextureProgressOffset(this TextureProgressBar target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTextureProgressOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTextureProgressOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenTextureProgressOffsetX(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarTextureProgressOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTextureProgressOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTextureProgressOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenTextureProgressOffsetX(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTextureProgressOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a TextureProgressBarTextureProgressOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<TextureProgressBar, float> TweenTextureProgressOffsetY(this TextureProgressBar target,
        double to, Duration duration, Action<TextureProgressBarTextureProgressOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new TextureProgressBarTextureProgressOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a TextureProgressBarTextureProgressOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<TextureProgressBar, float> TweenTextureProgressOffsetY(this TextureProgressBar target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new TextureProgressBarTextureProgressOffsetYTween { To = (float)to, Duration = duration }, options), playback);
}
