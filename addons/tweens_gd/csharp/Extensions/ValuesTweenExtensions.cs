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
    /// <summary>Starts a FloatTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, float> TweenFloat(this Node target,
        double to, Duration duration, Action<FloatTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new FloatTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a FloatTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, float> TweenFloat(this Node target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new FloatTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a DoubleTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, double> TweenDouble(this Node target,
        double to, Duration duration, Action<DoubleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DoubleTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a DoubleTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, double> TweenDouble(this Node target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DoubleTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Vector2Tween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, Vector2> TweenVector2(this Node target,
        Vector2 to, Duration duration, Action<Vector2Tween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Vector2Tween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Vector2Tween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, Vector2> TweenVector2(this Node target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Vector2Tween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Vector3Tween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, Vector3> TweenVector3(this Node target,
        Vector3 to, Duration duration, Action<Vector3Tween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Vector3Tween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Vector3Tween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, Vector3> TweenVector3(this Node target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Vector3Tween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Vector4Tween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, Vector4> TweenVector4(this Node target,
        Vector4 to, Duration duration, Action<Vector4Tween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Vector4Tween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Vector4Tween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, Vector4> TweenVector4(this Node target,
        Vector4 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Vector4Tween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ColorTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, Color> TweenColor(this Node target,
        Color to, Duration duration, Action<ColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ColorTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ColorTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, Color> TweenColor(this Node target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ColorTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a QuaternionTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, Quaternion> TweenQuaternion(this Node target,
        Quaternion to, Duration duration, Action<QuaternionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new QuaternionTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a QuaternionTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, Quaternion> TweenQuaternion(this Node target,
        Quaternion to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new QuaternionTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Rect2Tween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node, Rect2> TweenRect2(this Node target,
        Rect2 to, Duration duration, Action<Rect2Tween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Rect2Tween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Rect2Tween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node, Rect2> TweenRect2(this Node target,
        Rect2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Rect2Tween { To = to, Duration = duration }, options), playback);
}
