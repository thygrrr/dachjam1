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
    /// <summary>Starts a SpringArm3DSpringLengthTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<SpringArm3D, float> TweenSpringLength(this SpringArm3D target,
        double to, Duration duration, Action<SpringArm3DSpringLengthTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new SpringArm3DSpringLengthTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a SpringArm3DSpringLengthTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<SpringArm3D, float> TweenSpringLength(this SpringArm3D target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new SpringArm3DSpringLengthTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalModulateTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, Color> TweenModulate(this Decal target,
        Color to, Duration duration, Action<DecalModulateTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalModulateTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalModulateTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Decal, Color> TweenModulate(this Decal target,
        Color to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalModulateTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalModulateAlphaTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, float> TweenModulateAlpha(this Decal target,
        double to, Duration duration, Action<DecalModulateAlphaTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalModulateAlphaTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalModulateAlphaTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Decal, float> TweenModulateAlpha(this Decal target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalModulateAlphaTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, Vector3> TweenSize(this Decal target,
        Vector3 to, Duration duration, Action<DecalSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalSizeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalSizeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Decal, Vector3> TweenSize(this Decal target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalSizeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalSizeXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, float> TweenSizeX(this Decal target,
        double to, Duration duration, Action<DecalSizeXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalSizeXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalSizeXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Decal, float> TweenSizeX(this Decal target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalSizeXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalSizeYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, float> TweenSizeY(this Decal target,
        double to, Duration duration, Action<DecalSizeYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalSizeYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalSizeYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Decal, float> TweenSizeY(this Decal target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalSizeYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalSizeZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, float> TweenSizeZ(this Decal target,
        double to, Duration duration, Action<DecalSizeZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalSizeZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalSizeZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Decal, float> TweenSizeZ(this Decal target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalSizeZTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a DecalEmissionEnergyTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Decal, float> TweenEmissionEnergy(this Decal target,
        double to, Duration duration, Action<DecalEmissionEnergyTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new DecalEmissionEnergyTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a DecalEmissionEnergyTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Decal, float> TweenEmissionEnergy(this Decal target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new DecalEmissionEnergyTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a FogVolumeSizeTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<FogVolume, Vector3> TweenSize(this FogVolume target,
        Vector3 to, Duration duration, Action<FogVolumeSizeTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new FogVolumeSizeTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a FogVolumeSizeTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<FogVolume, Vector3> TweenSize(this FogVolume target,
        Vector3 to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new FogVolumeSizeTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a FogVolumeSizeXTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<FogVolume, float> TweenSizeX(this FogVolume target,
        double to, Duration duration, Action<FogVolumeSizeXTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new FogVolumeSizeXTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a FogVolumeSizeXTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<FogVolume, float> TweenSizeX(this FogVolume target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new FogVolumeSizeXTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a FogVolumeSizeYTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<FogVolume, float> TweenSizeY(this FogVolume target,
        double to, Duration duration, Action<FogVolumeSizeYTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new FogVolumeSizeYTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a FogVolumeSizeYTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<FogVolume, float> TweenSizeY(this FogVolume target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new FogVolumeSizeYTween { To = (float)to, Duration = duration }, options), playback);

    /// <summary>Starts a FogVolumeSizeZTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<FogVolume, float> TweenSizeZ(this FogVolume target,
        double to, Duration duration, Action<FogVolumeSizeZTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new FogVolumeSizeZTween { To = (float)to, Duration = duration }, configure), playback);

    /// <summary>Starts a FogVolumeSizeZTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<FogVolume, float> TweenSizeZ(this FogVolume target,
        double to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new FogVolumeSizeZTween { To = (float)to, Duration = duration }, options), playback);
}
