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
    /// <summary>Starts a Position2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenPosition(this Node2D target,
        Vector2 to, Duration duration, Action<Position2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position2DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenPosition(this Node2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position2DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Position2DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenPositionX(this Node2D target,
        double to, Duration duration, Action<Position2DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position2DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position2DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenPositionX(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position2DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Position2DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenPositionY(this Node2D target,
        double to, Duration duration, Action<Position2DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position2DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position2DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenPositionY(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position2DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenGlobalPosition(this Node2D target,
        Vector2 to, Duration duration, Action<GlobalPosition2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition2DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenGlobalPosition(this Node2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition2DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition2DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalPositionX(this Node2D target,
        double to, Duration duration, Action<GlobalPosition2DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition2DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition2DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalPositionX(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition2DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition2DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalPositionY(this Node2D target,
        double to, Duration duration, Action<GlobalPosition2DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition2DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition2DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalPositionY(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition2DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenScale(this Node2D target,
        Vector2 to, Duration duration, Action<Scale2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale2DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, Vector2> TweenScale(this Node2D target,
        Vector2 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale2DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale2DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenScaleX(this Node2D target,
        double to, Duration duration, Action<Scale2DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale2DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale2DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenScaleX(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale2DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale2DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenScaleY(this Node2D target,
        double to, Duration duration, Action<Scale2DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale2DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale2DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenScaleY(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale2DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Rotation2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenRotation(this Node2D target,
        double to, Duration duration, Action<Rotation2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Rotation2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Rotation2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenRotation(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Rotation2DTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalRotation2DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalRotation(this Node2D target,
        double to, Duration duration, Action<GlobalRotation2DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalRotation2DTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalRotation2DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node2D, float> TweenGlobalRotation(this Node2D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalRotation2DTween { To = (float)to, Duration = duration }, options), playback);
}
