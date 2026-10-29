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
    /// <summary>Starts a Position3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenPosition(this Node3D target,
        Vector3 to, Duration duration, Action<Position3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenPosition(this Node3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position3DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Position3DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenPositionX(this Node3D target,
        double to, Duration duration, Action<Position3DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position3DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position3DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenPositionX(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position3DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Position3DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenPositionY(this Node3D target,
        double to, Duration duration, Action<Position3DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position3DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position3DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenPositionY(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position3DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Position3DZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenPositionZ(this Node3D target,
        double to, Duration duration, Action<Position3DZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Position3DZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Position3DZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenPositionZ(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Position3DZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenGlobalPosition(this Node3D target,
        Vector3 to, Duration duration, Action<GlobalPosition3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenGlobalPosition(this Node3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition3DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition3DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalPositionX(this Node3D target,
        double to, Duration duration, Action<GlobalPosition3DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition3DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition3DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalPositionX(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition3DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition3DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalPositionY(this Node3D target,
        double to, Duration duration, Action<GlobalPosition3DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition3DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition3DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalPositionY(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition3DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalPosition3DZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalPositionZ(this Node3D target,
        double to, Duration duration, Action<GlobalPosition3DZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalPosition3DZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalPosition3DZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalPositionZ(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalPosition3DZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenScale(this Node3D target,
        Vector3 to, Duration duration, Action<Scale3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenScale(this Node3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale3DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale3DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenScaleX(this Node3D target,
        double to, Duration duration, Action<Scale3DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale3DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale3DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenScaleX(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale3DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale3DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenScaleY(this Node3D target,
        double to, Duration duration, Action<Scale3DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale3DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale3DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenScaleY(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale3DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Scale3DZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenScaleZ(this Node3D target,
        double to, Duration duration, Action<Scale3DZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Scale3DZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Scale3DZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenScaleZ(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Scale3DZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Rotation3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenRotation(this Node3D target,
        Vector3 to, Duration duration, Action<Rotation3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Rotation3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Rotation3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenRotation(this Node3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Rotation3DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Rotation3DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenRotationX(this Node3D target,
        double to, Duration duration, Action<Rotation3DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Rotation3DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Rotation3DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenRotationX(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Rotation3DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Rotation3DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenRotationY(this Node3D target,
        double to, Duration duration, Action<Rotation3DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Rotation3DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Rotation3DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenRotationY(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Rotation3DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Rotation3DZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenRotationZ(this Node3D target,
        double to, Duration duration, Action<Rotation3DZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Rotation3DZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a Rotation3DZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenRotationZ(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Rotation3DZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalRotation3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenGlobalRotation(this Node3D target,
        Vector3 to, Duration duration, Action<GlobalRotation3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalRotation3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalRotation3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Vector3> TweenGlobalRotation(this Node3D target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalRotation3DTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalRotation3DXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalRotationX(this Node3D target,
        double to, Duration duration, Action<GlobalRotation3DXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalRotation3DXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalRotation3DXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalRotationX(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalRotation3DXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalRotation3DYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalRotationY(this Node3D target,
        double to, Duration duration, Action<GlobalRotation3DYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalRotation3DYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalRotation3DYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalRotationY(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalRotation3DYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a GlobalRotation3DZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalRotationZ(this Node3D target,
        double to, Duration duration, Action<GlobalRotation3DZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new GlobalRotation3DZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a GlobalRotation3DZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, float> TweenGlobalRotationZ(this Node3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new GlobalRotation3DZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a Quaternion3DTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Node3D, Quaternion> TweenQuaternion(this Node3D target,
        Quaternion to, Duration duration, Action<Quaternion3DTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Quaternion3DTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Quaternion3DTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Node3D, Quaternion> TweenQuaternion(this Node3D target,
        Quaternion to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Quaternion3DTween { To = to, Duration = duration }, options), playback);
}
