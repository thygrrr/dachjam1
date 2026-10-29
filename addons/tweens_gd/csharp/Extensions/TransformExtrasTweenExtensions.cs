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
    /// <summary>Starts a Skew2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenSkew(this Node2D target,
        double to, Duration duration, Action<Skew2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Skew2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Skew2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenSkew(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Skew2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalSkew2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalSkew(this Node2D target,
        double to, Duration duration, Action<GlobalSkew2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalSkew2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalSkew2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalSkew(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalSkew2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalScale2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenGlobalScale(this Node2D target,
        Vector2 to, Duration duration, Action<GlobalScale2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalScale2DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalScale2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenGlobalScale(this Node2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalScale2DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalScale2DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalScaleX(this Node2D target,
        double to, Duration duration, Action<GlobalScale2DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalScale2DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalScale2DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalScaleX(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalScale2DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalScale2DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalScaleY(this Node2D target,
        double to, Duration duration, Action<GlobalScale2DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalScale2DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalScale2DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalScaleY(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalScale2DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalQuaternion3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Quaternion> TweenGlobalQuaternion(this Node3D target,
        Quaternion to, Duration duration, Action<GlobalQuaternion3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalQuaternion3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalQuaternion3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Quaternion> TweenGlobalQuaternion(this Node3D target,
        Quaternion to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalQuaternion3DTween { To = to, Duration = duration }, options), playback);
}
