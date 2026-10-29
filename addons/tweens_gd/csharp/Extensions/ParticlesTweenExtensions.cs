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
    /// <summary>Starts a GpuParticles2DSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles2D, double> TweenSpeedScale(this GpuParticles2D target,
        double to, Duration duration, Action<GpuParticles2DSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles2DSpeedScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles2DSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles2D, double> TweenSpeedScale(this GpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles2DSpeedScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles2DLifetimeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles2D, double> TweenLifetime(this GpuParticles2D target,
        double to, Duration duration, Action<GpuParticles2DLifetimeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles2DLifetimeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles2DLifetimeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles2D, double> TweenLifetime(this GpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles2DLifetimeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles2DExplosivenessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles2D, float> TweenExplosiveness(this GpuParticles2D target,
        double to, Duration duration, Action<GpuParticles2DExplosivenessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles2DExplosivenessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles2DExplosivenessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles2D, float> TweenExplosiveness(this GpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles2DExplosivenessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles2DRandomnessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles2D, float> TweenRandomness(this GpuParticles2D target,
        double to, Duration duration, Action<GpuParticles2DRandomnessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles2DRandomnessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles2DRandomnessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles2D, float> TweenRandomness(this GpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles2DRandomnessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles2DAmountRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles2D, float> TweenAmountRatio(this GpuParticles2D target,
        double to, Duration duration, Action<GpuParticles2DAmountRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles2DAmountRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles2DAmountRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles2D, float> TweenAmountRatio(this GpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles2DAmountRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles3DSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles3D, double> TweenSpeedScale(this GpuParticles3D target,
        double to, Duration duration, Action<GpuParticles3DSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles3DSpeedScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles3DSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles3D, double> TweenSpeedScale(this GpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles3DSpeedScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles3DLifetimeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles3D, double> TweenLifetime(this GpuParticles3D target,
        double to, Duration duration, Action<GpuParticles3DLifetimeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles3DLifetimeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles3DLifetimeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles3D, double> TweenLifetime(this GpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles3DLifetimeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles3DExplosivenessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles3D, float> TweenExplosiveness(this GpuParticles3D target,
        double to, Duration duration, Action<GpuParticles3DExplosivenessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles3DExplosivenessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles3DExplosivenessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles3D, float> TweenExplosiveness(this GpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles3DExplosivenessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles3DRandomnessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles3D, float> TweenRandomness(this GpuParticles3D target,
        double to, Duration duration, Action<GpuParticles3DRandomnessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles3DRandomnessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles3DRandomnessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles3D, float> TweenRandomness(this GpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles3DRandomnessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GpuParticles3DAmountRatioTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<GpuParticles3D, float> TweenAmountRatio(this GpuParticles3D target,
        double to, Duration duration, Action<GpuParticles3DAmountRatioTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GpuParticles3DAmountRatioTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GpuParticles3DAmountRatioTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<GpuParticles3D, float> TweenAmountRatio(this GpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GpuParticles3DAmountRatioTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, double> TweenSpeedScale(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DSpeedScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, double> TweenSpeedScale(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DSpeedScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DLifetimeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, double> TweenLifetime(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DLifetimeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DLifetimeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DLifetimeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, double> TweenLifetime(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DLifetimeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DExplosivenessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenExplosiveness(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DExplosivenessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DExplosivenessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DExplosivenessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenExplosiveness(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DExplosivenessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DRandomnessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenRandomness(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DRandomnessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DRandomnessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DRandomnessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenRandomness(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DRandomnessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DDirectionTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, Vector2> TweenDirection(this CpuParticles2D target,
        Vector2 to, Duration duration, Action<CpuParticles2DDirectionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DDirectionTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DDirectionTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, Vector2> TweenDirection(this CpuParticles2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DDirectionTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DDirectionXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenDirectionX(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DDirectionXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DDirectionXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DDirectionXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenDirectionX(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DDirectionXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DDirectionYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenDirectionY(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DDirectionYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DDirectionYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DDirectionYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenDirectionY(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DDirectionYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DGravityTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, Vector2> TweenGravity(this CpuParticles2D target,
        Vector2 to, Duration duration, Action<CpuParticles2DGravityTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DGravityTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DGravityTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, Vector2> TweenGravity(this CpuParticles2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DGravityTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DGravityXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenGravityX(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DGravityXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DGravityXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DGravityXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenGravityX(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DGravityXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DGravityYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenGravityY(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DGravityYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DGravityYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DGravityYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenGravityY(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DGravityYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DSpreadTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenSpread(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DSpreadTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DSpreadTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DSpreadTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenSpread(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DSpreadTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DEmissionSphereRadiusTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenEmissionSphereRadius(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DEmissionSphereRadiusTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DEmissionSphereRadiusTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DEmissionSphereRadiusTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenEmissionSphereRadius(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DEmissionSphereRadiusTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DEmissionRectExtentsTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, Vector2> TweenEmissionRectExtents(this CpuParticles2D target,
        Vector2 to, Duration duration, Action<CpuParticles2DEmissionRectExtentsTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DEmissionRectExtentsTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DEmissionRectExtentsTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, Vector2> TweenEmissionRectExtents(this CpuParticles2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DEmissionRectExtentsTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DEmissionRectExtentsXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenEmissionRectExtentsX(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DEmissionRectExtentsXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DEmissionRectExtentsXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DEmissionRectExtentsXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenEmissionRectExtentsX(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DEmissionRectExtentsXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DEmissionRectExtentsYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenEmissionRectExtentsY(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DEmissionRectExtentsYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DEmissionRectExtentsYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DEmissionRectExtentsYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenEmissionRectExtentsY(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DEmissionRectExtentsYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, Color> TweenColor(this CpuParticles2D target,
        Color to, Duration duration, Action<CpuParticles2DColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DColorTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, Color> TweenColor(this CpuParticles2D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles2DColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles2D, float> TweenColorAlpha(this CpuParticles2D target,
        double to, Duration duration, Action<CpuParticles2DColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles2DColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles2DColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles2D, float> TweenColorAlpha(this CpuParticles2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles2DColorAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DSpeedScaleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, double> TweenSpeedScale(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DSpeedScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DSpeedScaleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DSpeedScaleTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, double> TweenSpeedScale(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DSpeedScaleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DLifetimeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, double> TweenLifetime(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DLifetimeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DLifetimeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DLifetimeTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, double> TweenLifetime(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DLifetimeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DExplosivenessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenExplosiveness(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DExplosivenessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DExplosivenessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DExplosivenessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenExplosiveness(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DExplosivenessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DRandomnessTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenRandomness(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DRandomnessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DRandomnessTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DRandomnessTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenRandomness(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DRandomnessTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DDirectionTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, Vector3> TweenDirection(this CpuParticles3D target,
        Vector3 to, Duration duration, Action<CpuParticles3DDirectionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DDirectionTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DDirectionTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, Vector3> TweenDirection(this CpuParticles3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DDirectionTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DDirectionXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenDirectionX(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DDirectionXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DDirectionXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DDirectionXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenDirectionX(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DDirectionXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DDirectionYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenDirectionY(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DDirectionYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DDirectionYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DDirectionYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenDirectionY(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DDirectionYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DDirectionZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenDirectionZ(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DDirectionZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DDirectionZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DDirectionZTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenDirectionZ(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DDirectionZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DGravityTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, Vector3> TweenGravity(this CpuParticles3D target,
        Vector3 to, Duration duration, Action<CpuParticles3DGravityTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DGravityTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DGravityTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, Vector3> TweenGravity(this CpuParticles3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DGravityTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DGravityXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenGravityX(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DGravityXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DGravityXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DGravityXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenGravityX(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DGravityXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DGravityYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenGravityY(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DGravityYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DGravityYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DGravityYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenGravityY(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DGravityYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DGravityZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenGravityZ(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DGravityZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DGravityZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DGravityZTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenGravityZ(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DGravityZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DSpreadTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenSpread(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DSpreadTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DSpreadTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DSpreadTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenSpread(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DSpreadTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DEmissionSphereRadiusTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionSphereRadius(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DEmissionSphereRadiusTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DEmissionSphereRadiusTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DEmissionSphereRadiusTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionSphereRadius(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DEmissionSphereRadiusTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, Vector3> TweenEmissionBoxExtents(this CpuParticles3D target,
        Vector3 to, Duration duration, Action<CpuParticles3DEmissionBoxExtentsTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DEmissionBoxExtentsTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, Vector3> TweenEmissionBoxExtents(this CpuParticles3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DEmissionBoxExtentsTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionBoxExtentsX(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DEmissionBoxExtentsXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DEmissionBoxExtentsXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsXTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionBoxExtentsX(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DEmissionBoxExtentsXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionBoxExtentsY(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DEmissionBoxExtentsYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DEmissionBoxExtentsYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsYTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionBoxExtentsY(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DEmissionBoxExtentsYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionBoxExtentsZ(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DEmissionBoxExtentsZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DEmissionBoxExtentsZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DEmissionBoxExtentsZTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenEmissionBoxExtentsZ(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DEmissionBoxExtentsZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, Color> TweenColor(this CpuParticles3D target,
        Color to, Duration duration, Action<CpuParticles3DColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DColorTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, Color> TweenColor(this CpuParticles3D target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a CpuParticles3DColorAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<CpuParticles3D, float> TweenColorAlpha(this CpuParticles3D target,
        double to, Duration duration, Action<CpuParticles3DColorAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new CpuParticles3DColorAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a CpuParticles3DColorAlphaTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<CpuParticles3D, float> TweenColorAlpha(this CpuParticles3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new CpuParticles3DColorAlphaTween { To = (float)to, Duration = duration }, options), playback);
}
