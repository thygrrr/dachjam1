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
    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenAlbedoColor(this BaseMaterial3D target,
        Color to, Duration duration, SceneTree tree, Action<MaterialAlbedoColorTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialAlbedoColorTween { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenAlbedoColor(this BaseMaterial3D target,
        Color to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialAlbedoColorTween { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenAlbedoColor(this BaseMaterial3D target,
        Color to, Duration duration, Node owner, Action<MaterialAlbedoColorTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialAlbedoColorTween { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenAlbedoColor(this BaseMaterial3D target,
        Color to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialAlbedoColorTween { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenAlbedoAlpha(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialAlbedoAlphaTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialAlbedoAlphaTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenAlbedoAlpha(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialAlbedoAlphaTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenAlbedoAlpha(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialAlbedoAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialAlbedoAlphaTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenAlbedoAlpha(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialAlbedoAlphaTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallic(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialMetallicTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialMetallicTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallic(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialMetallicTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallic(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialMetallicTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialMetallicTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallic(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialMetallicTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallicSpecular(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialMetallicSpecularTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialMetallicSpecularTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallicSpecular(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialMetallicSpecularTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallicSpecular(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialMetallicSpecularTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialMetallicSpecularTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenMetallicSpecular(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialMetallicSpecularTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenRoughness(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialRoughnessTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialRoughnessTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenRoughness(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialRoughnessTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenRoughness(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialRoughnessTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialRoughnessTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenRoughness(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialRoughnessTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionEnergyMultiplier(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialEmissionEnergyMultiplierTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialEmissionEnergyMultiplierTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionEnergyMultiplier(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialEmissionEnergyMultiplierTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionEnergyMultiplier(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialEmissionEnergyMultiplierTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialEmissionEnergyMultiplierTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionEnergyMultiplier(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialEmissionEnergyMultiplierTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionIntensity(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialEmissionIntensityTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialEmissionIntensityTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionIntensity(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialEmissionIntensityTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionIntensity(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialEmissionIntensityTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialEmissionIntensityTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenEmissionIntensity(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialEmissionIntensityTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenNormalScale(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialNormalScaleTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialNormalScaleTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenNormalScale(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialNormalScaleTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenNormalScale(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialNormalScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialNormalScaleTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenNormalScale(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialNormalScaleTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenEmission(this BaseMaterial3D target,
        Color to, Duration duration, SceneTree tree, Action<MaterialEmissionTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialEmissionTween { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenEmission(this BaseMaterial3D target,
        Color to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialEmissionTween { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenEmission(this BaseMaterial3D target,
        Color to, Duration duration, Node owner, Action<MaterialEmissionTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialEmissionTween { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Color> TweenEmission(this BaseMaterial3D target,
        Color to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialEmissionTween { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, Action<MaterialUv1OffsetTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetTween { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetTween { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, Action<MaterialUv1OffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetTween { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetTween { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUv1OffsetXTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetXTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetXTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUv1OffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetXTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetXTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUv1OffsetYTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetYTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetYTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUv1OffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetYTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetYTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUv1OffsetZTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetZTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetZTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUv1OffsetZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1OffsetZTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1OffsetZTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, Action<MaterialUv1ScaleTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleTween { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleTween { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, Action<MaterialUv1ScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleTween { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUv1Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleTween { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUv1ScaleXTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleXTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleXTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUv1ScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleXTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleXTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUv1ScaleYTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleYTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleYTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUv1ScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleYTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleYTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUv1ScaleZTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleZTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleZTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUv1ScaleZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUv1ScaleZTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUv1ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUv1ScaleZTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, Action<MaterialUV2OffsetTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetTween { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetTween { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, Action<MaterialUV2OffsetTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetTween { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Offset(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetTween { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUV2OffsetXTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetXTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetXTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUV2OffsetXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetXTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetXTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUV2OffsetYTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetYTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetYTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUV2OffsetYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetYTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetYTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUV2OffsetZTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetZTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetZTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUV2OffsetZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2OffsetZTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2OffsetZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2OffsetZTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, Action<MaterialUV2ScaleTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleTween { To = to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleTween { To = to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, Action<MaterialUV2ScaleTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleTween { To = to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, Vector3> TweenUV2Scale(this BaseMaterial3D target,
        Vector3 to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleTween { To = to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUV2ScaleXTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleXTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleX(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleXTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUV2ScaleXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleXTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleX(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleXTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUV2ScaleYTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleYTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleY(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleYTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUV2ScaleYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleYTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleY(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleYTween { To = (float)to, Duration = duration }, options), owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, Action<MaterialUV2ScaleZTween>? configure = null, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleZTween { To = (float)to, Duration = duration }, configure), tree, owner, playback);

    /// <summary>Animates the supplied material in the tree; optional owner binds playback to that node.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, SceneTree tree, TweenOptions options, Node? owner = null, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleZTween { To = (float)to, Duration = duration }, options), tree, owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, Action<MaterialUV2ScaleZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new MaterialUV2ScaleZTween { To = (float)to, Duration = duration }, configure), owner, playback);

    /// <summary>Animates the supplied material with node-bound playback lifetime.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<BaseMaterial3D, float> TweenUV2ScaleZ(this BaseMaterial3D target,
        double to, Duration duration, Node owner, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new MaterialUV2ScaleZTween { To = (float)to, Duration = duration }, options), owner, playback);
}
