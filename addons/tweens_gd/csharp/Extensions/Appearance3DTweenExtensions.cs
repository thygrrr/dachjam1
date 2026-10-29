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
    /// <summary>Starts a SpriteBase3DModulateTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpriteBase3D, Color> TweenModulate(this SpriteBase3D target,
        Color to, Duration duration, Action<SpriteBase3DModulateTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpriteBase3DModulateTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpriteBase3DModulateTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpriteBase3D, Color> TweenModulate(this SpriteBase3D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpriteBase3DModulateTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a SpriteBase3DModulateAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpriteBase3D, float> TweenModulateAlpha(this SpriteBase3D target,
        double to, Duration duration, Action<SpriteBase3DModulateAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpriteBase3DModulateAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpriteBase3DModulateAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpriteBase3D, float> TweenModulateAlpha(this SpriteBase3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpriteBase3DModulateAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpriteBase3DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpriteBase3D, Vector2> TweenOffset(this SpriteBase3D target,
        Vector2 to, Duration duration, Action<SpriteBase3DOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpriteBase3DOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpriteBase3DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<SpriteBase3D, Vector2> TweenOffset(this SpriteBase3D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpriteBase3DOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a SpriteBase3DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpriteBase3D, float> TweenOffsetX(this SpriteBase3D target,
        double to, Duration duration, Action<SpriteBase3DOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpriteBase3DOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpriteBase3DOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpriteBase3D, float> TweenOffsetX(this SpriteBase3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpriteBase3DOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpriteBase3DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpriteBase3D, float> TweenOffsetY(this SpriteBase3D target,
        double to, Duration duration, Action<SpriteBase3DOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpriteBase3DOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpriteBase3DOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpriteBase3D, float> TweenOffsetY(this SpriteBase3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpriteBase3DOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a SpriteBase3DPixelSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpriteBase3D, float> TweenPixelSize(this SpriteBase3D target,
        double to, Duration duration, Action<SpriteBase3DPixelSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpriteBase3DPixelSizeTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpriteBase3DPixelSizeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpriteBase3D, float> TweenPixelSize(this SpriteBase3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpriteBase3DPixelSizeTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DModulateTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, Color> TweenModulate(this Label3D target,
        Color to, Duration duration, Action<Label3DModulateTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DModulateTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DModulateTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Label3D, Color> TweenModulate(this Label3D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DModulateTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DModulateAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, float> TweenModulateAlpha(this Label3D target,
        double to, Duration duration, Action<Label3DModulateAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DModulateAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DModulateAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Label3D, float> TweenModulateAlpha(this Label3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DModulateAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DOutlineModulateTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, Color> TweenOutlineModulate(this Label3D target,
        Color to, Duration duration, Action<Label3DOutlineModulateTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DOutlineModulateTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DOutlineModulateTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Label3D, Color> TweenOutlineModulate(this Label3D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DOutlineModulateTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DOutlineModulateAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, float> TweenOutlineModulateAlpha(this Label3D target,
        double to, Duration duration, Action<Label3DOutlineModulateAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DOutlineModulateAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DOutlineModulateAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Label3D, float> TweenOutlineModulateAlpha(this Label3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DOutlineModulateAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, Vector2> TweenOffset(this Label3D target,
        Vector2 to, Duration duration, Action<Label3DOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Label3D, Vector2> TweenOffset(this Label3D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, float> TweenOffsetX(this Label3D target,
        double to, Duration duration, Action<Label3DOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Label3D, float> TweenOffsetX(this Label3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, float> TweenOffsetY(this Label3D target,
        double to, Duration duration, Action<Label3DOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Label3D, float> TweenOffsetY(this Label3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Label3DPixelSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label3D, float> TweenPixelSize(this Label3D target,
        double to, Duration duration, Action<Label3DPixelSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Label3DPixelSizeTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Label3DPixelSizeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Label3D, float> TweenPixelSize(this Label3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Label3DPixelSizeTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GeometryInstance3DTransparencyTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GeometryInstance3D, float> TweenTransparency(this GeometryInstance3D target,
        double to, Duration duration, Action<GeometryInstance3DTransparencyTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GeometryInstance3DTransparencyTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GeometryInstance3DTransparencyTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GeometryInstance3D, float> TweenTransparency(this GeometryInstance3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GeometryInstance3DTransparencyTween { To = (float)to, Duration = duration }, options), playback);
}
