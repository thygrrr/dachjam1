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
    /// <summary>Starts a Camera2DZoomTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera2D, Vector2> TweenZoom(this Camera2D target,
        Vector2 to, Duration duration, Action<Camera2DZoomTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera2DZoomTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera2DZoomTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera2D, Vector2> TweenZoom(this Camera2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera2DZoomTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera2DZoomXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera2D, float> TweenZoomX(this Camera2D target,
        double to, Duration duration, Action<Camera2DZoomXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera2DZoomXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera2DZoomXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera2D, float> TweenZoomX(this Camera2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera2DZoomXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera2DZoomYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera2D, float> TweenZoomY(this Camera2D target,
        double to, Duration duration, Action<Camera2DZoomYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera2DZoomYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera2DZoomYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera2D, float> TweenZoomY(this Camera2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera2DZoomYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera2D, Vector2> TweenOffset(this Camera2D target,
        Vector2 to, Duration duration, Action<Camera2DOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera2DOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera2DOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera2D, Vector2> TweenOffset(this Camera2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera2DOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera2D, float> TweenOffsetX(this Camera2D target,
        double to, Duration duration, Action<Camera2DOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera2DOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera2DOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera2D, float> TweenOffsetX(this Camera2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera2DOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera2D, float> TweenOffsetY(this Camera2D target,
        double to, Duration duration, Action<Camera2DOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera2DOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera2DOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera2D, float> TweenOffsetY(this Camera2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera2DOffsetYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DFovTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenFov(this Camera3D target,
        double to, Duration duration, Action<Camera3DFovTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DFovTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DFovTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera3D, float> TweenFov(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DFovTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenSize(this Camera3D target,
        double to, Duration duration, Action<Camera3DSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DSizeTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DSizeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera3D, float> TweenSize(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DSizeTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DHOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenHOffset(this Camera3D target,
        double to, Duration duration, Action<Camera3DHOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DHOffsetTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DHOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera3D, float> TweenHOffset(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DHOffsetTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DVOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenVOffset(this Camera3D target,
        double to, Duration duration, Action<Camera3DVOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DVOffsetTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DVOffsetTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera3D, float> TweenVOffset(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DVOffsetTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DNearTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenNear(this Camera3D target,
        double to, Duration duration, Action<Camera3DNearTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DNearTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DNearTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera3D, float> TweenNear(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DNearTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DFarTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenFar(this Camera3D target,
        double to, Duration duration, Action<Camera3DFarTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DFarTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DFarTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Camera3D, float> TweenFar(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DFarTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DFrustumOffsetTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, Vector2> TweenFrustumOffset(this Camera3D target,
        Vector2 to, Duration duration, Action<Camera3DFrustumOffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DFrustumOffsetTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DFrustumOffsetTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Camera3D, Vector2> TweenFrustumOffset(this Camera3D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DFrustumOffsetTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DFrustumOffsetXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenFrustumOffsetX(this Camera3D target,
        double to, Duration duration, Action<Camera3DFrustumOffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DFrustumOffsetXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DFrustumOffsetXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Camera3D, float> TweenFrustumOffsetX(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DFrustumOffsetXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Camera3DFrustumOffsetYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Camera3D, float> TweenFrustumOffsetY(this Camera3D target,
        double to, Duration duration, Action<Camera3DFrustumOffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Camera3DFrustumOffsetYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Camera3DFrustumOffsetYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Camera3D, float> TweenFrustumOffsetY(this Camera3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Camera3DFrustumOffsetYTween { To = (float)to, Duration = duration }, options), playback);
}
