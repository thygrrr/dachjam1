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
    /// <summary>Starts a material-uniform definition and returns its playback handle.</summary>
    /// <remarks>Uses the supplied tree and optional owner. Configure runs before snapshotting and can override duration.</remarks>
    public static TweenInstance<ShaderMaterial, TValue> TweenShaderParameter<TValue>(this ShaderMaterial target,
        string parameter, TValue to, Duration duration, SceneTree tree,
        Action<ShaderParameterTween<TValue>>? configure = null, Node? owner = null, PlaybackOptions playback = default) where TValue : struct
        => target.Tween(ConfigureDefinition(new ShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Starts a material-uniform definition and returns its playback handle.</summary>
    /// <remarks>Uses the supplied tree and optional owner. Copies options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<ShaderMaterial, TValue> TweenShaderParameter<TValue>(this ShaderMaterial target,
        string parameter, TValue to, Duration duration, SceneTree tree,
        TweenOptions options, Node? owner = null, PlaybackOptions playback = default) where TValue : struct
        => target.Tween(ApplyOptions(new ShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Starts a material-uniform definition and returns its playback handle.</summary>
    /// <remarks>The owner controls playback lifetime. Configure runs before snapshotting and can override duration.</remarks>
    public static TweenInstance<ShaderMaterial, TValue> TweenShaderParameter<TValue>(this ShaderMaterial target,
        string parameter, TValue to, Duration duration, Node owner,
        Action<ShaderParameterTween<TValue>>? configure = null, PlaybackOptions playback = default) where TValue : struct
        => target.Tween(ConfigureDefinition(new ShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Starts a material-uniform definition and returns its playback handle.</summary>
    /// <remarks>The owner controls playback lifetime. Copies options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<ShaderMaterial, TValue> TweenShaderParameter<TValue>(this ShaderMaterial target,
        string parameter, TValue to, Duration duration, Node owner,
        TweenOptions options, PlaybackOptions playback = default) where TValue : struct
        => target.Tween(ApplyOptions(new ShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Starts an instance-uniform definition on this CanvasItem and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override duration. The endpoint must match the uniform type.</remarks>
    public static TweenInstance<CanvasItem, TValue> TweenInstanceShaderParameter<TValue>(this CanvasItem target,
        string parameter, TValue to, Duration duration, Action<CanvasItemInstanceShaderParameterTween<TValue>>? configure = null, PlaybackOptions playback = default)
        where TValue : struct
        => target.Tween(ConfigureDefinition(new CanvasItemInstanceShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts an instance-uniform definition on this CanvasItem and returns its playback handle.</summary>
    /// <remarks>Copies options; the explicit duration takes precedence. The endpoint must match the uniform type.</remarks>
    public static TweenInstance<CanvasItem, TValue> TweenInstanceShaderParameter<TValue>(this CanvasItem target,
        string parameter, TValue to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        where TValue : struct
        => target.Tween(ApplyOptions(new CanvasItemInstanceShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, options), playback);

    /// <summary>Starts an instance-uniform definition on this GeometryInstance3D and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override duration. The endpoint must match the uniform type.</remarks>
    public static TweenInstance<GeometryInstance3D, TValue> TweenInstanceShaderParameter<TValue>(this GeometryInstance3D target,
        string parameter, TValue to, Duration duration, Action<GeometryInstanceShaderParameterTween<TValue>>? configure = null, PlaybackOptions playback = default)
        where TValue : struct
        => target.Tween(ConfigureDefinition(new GeometryInstanceShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts an instance-uniform definition on this GeometryInstance3D and returns its playback handle.</summary>
    /// <remarks>Copies options; the explicit duration takes precedence. The endpoint must match the uniform type.</remarks>
    public static TweenInstance<GeometryInstance3D, TValue> TweenInstanceShaderParameter<TValue>(this GeometryInstance3D target,
        string parameter, TValue to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        where TValue : struct
        => target.Tween(ApplyOptions(new GeometryInstanceShaderParameterTween<TValue>(parameter) { To = to, Duration = duration }, options), playback);
}
