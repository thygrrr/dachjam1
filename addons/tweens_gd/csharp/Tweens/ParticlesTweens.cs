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

/// <summary>Animates GpuParticles2D.SpeedScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles2DSpeedScaleTween() : PropertyTween<GpuParticles2D, double>(
    static n => n.SpeedScale, static (n, value) => n.SpeedScale = value, Interpolators.Double);

/// <summary>Animates GpuParticles2D.Lifetime in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles2DLifetimeTween() : PropertyTween<GpuParticles2D, double>(
    static n => n.Lifetime, static (n, value) => n.Lifetime = value, Interpolators.Double);

/// <summary>Animates GpuParticles2D.Explosiveness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles2DExplosivenessTween() : PropertyTween<GpuParticles2D, float>(
    static n => n.Explosiveness, static (n, value) => n.Explosiveness = value, Interpolators.Float);

/// <summary>Animates GpuParticles2D.Randomness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles2DRandomnessTween() : PropertyTween<GpuParticles2D, float>(
    static n => n.Randomness, static (n, value) => n.Randomness = value, Interpolators.Float);

/// <summary>Animates GpuParticles2D.AmountRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles2DAmountRatioTween() : PropertyTween<GpuParticles2D, float>(
    static n => n.AmountRatio, static (n, value) => n.AmountRatio = value, Interpolators.Float);

/// <summary>Animates GpuParticles3D.SpeedScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles3DSpeedScaleTween() : PropertyTween<GpuParticles3D, double>(
    static n => n.SpeedScale, static (n, value) => n.SpeedScale = value, Interpolators.Double);

/// <summary>Animates GpuParticles3D.Lifetime in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles3DLifetimeTween() : PropertyTween<GpuParticles3D, double>(
    static n => n.Lifetime, static (n, value) => n.Lifetime = value, Interpolators.Double);

/// <summary>Animates GpuParticles3D.Explosiveness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles3DExplosivenessTween() : PropertyTween<GpuParticles3D, float>(
    static n => n.Explosiveness, static (n, value) => n.Explosiveness = value, Interpolators.Float);

/// <summary>Animates GpuParticles3D.Randomness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles3DRandomnessTween() : PropertyTween<GpuParticles3D, float>(
    static n => n.Randomness, static (n, value) => n.Randomness = value, Interpolators.Float);

/// <summary>Animates GpuParticles3D.AmountRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GpuParticles3DAmountRatioTween() : PropertyTween<GpuParticles3D, float>(
    static n => n.AmountRatio, static (n, value) => n.AmountRatio = value, Interpolators.Float);

/// <summary>Animates CpuParticles2D.SpeedScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DSpeedScaleTween() : PropertyTween<CpuParticles2D, double>(
    static n => n.SpeedScale, static (n, value) => n.SpeedScale = value, Interpolators.Double);

/// <summary>Animates CpuParticles2D.Lifetime in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DLifetimeTween() : PropertyTween<CpuParticles2D, double>(
    static n => n.Lifetime, static (n, value) => n.Lifetime = value, Interpolators.Double);

/// <summary>Animates CpuParticles2D.Explosiveness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DExplosivenessTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Explosiveness, static (n, value) => n.Explosiveness = value, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Randomness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DRandomnessTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Randomness, static (n, value) => n.Randomness = value, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Direction in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DDirectionTween() : PropertyTween<CpuParticles2D, Vector2>(
    static n => n.Direction, static (n, value) => n.Direction = value, Interpolators.Vector2);

/// <summary>Animates CpuParticles2D.Direction.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DDirectionXTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Direction.X, static (n, value) => { var current = n.Direction; current.X = value; n.Direction = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Direction.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DDirectionYTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Direction.Y, static (n, value) => { var current = n.Direction; current.Y = value; n.Direction = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Gravity in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DGravityTween() : PropertyTween<CpuParticles2D, Vector2>(
    static n => n.Gravity, static (n, value) => n.Gravity = value, Interpolators.Vector2);

/// <summary>Animates CpuParticles2D.Gravity.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DGravityXTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Gravity.X, static (n, value) => { var current = n.Gravity; current.X = value; n.Gravity = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Gravity.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DGravityYTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Gravity.Y, static (n, value) => { var current = n.Gravity; current.Y = value; n.Gravity = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Spread in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DSpreadTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Spread, static (n, value) => n.Spread = value, Interpolators.Float);

/// <summary>Animates CpuParticles2D.EmissionSphereRadius in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DEmissionSphereRadiusTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.EmissionSphereRadius, static (n, value) => n.EmissionSphereRadius = value, Interpolators.Float);

/// <summary>Animates CpuParticles2D.EmissionRectExtents in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DEmissionRectExtentsTween() : PropertyTween<CpuParticles2D, Vector2>(
    static n => n.EmissionRectExtents, static (n, value) => n.EmissionRectExtents = value, Interpolators.Vector2);

/// <summary>Animates CpuParticles2D.EmissionRectExtents.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DEmissionRectExtentsXTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.EmissionRectExtents.X, static (n, value) => { var current = n.EmissionRectExtents; current.X = value; n.EmissionRectExtents = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles2D.EmissionRectExtents.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DEmissionRectExtentsYTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.EmissionRectExtents.Y, static (n, value) => { var current = n.EmissionRectExtents; current.Y = value; n.EmissionRectExtents = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles2D.Color in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DColorTween() : PropertyTween<CpuParticles2D, Color>(
    static n => n.Color, static (n, value) => n.Color = value, Interpolators.Color);

/// <summary>Animates CpuParticles2D.Color.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles2DColorAlphaTween() : PropertyTween<CpuParticles2D, float>(
    static n => n.Color.A, static (n, value) => { var current = n.Color; current.A = value; n.Color = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.SpeedScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DSpeedScaleTween() : PropertyTween<CpuParticles3D, double>(
    static n => n.SpeedScale, static (n, value) => n.SpeedScale = value, Interpolators.Double);

/// <summary>Animates CpuParticles3D.Lifetime in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DLifetimeTween() : PropertyTween<CpuParticles3D, double>(
    static n => n.Lifetime, static (n, value) => n.Lifetime = value, Interpolators.Double);

/// <summary>Animates CpuParticles3D.Explosiveness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DExplosivenessTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Explosiveness, static (n, value) => n.Explosiveness = value, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Randomness in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DRandomnessTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Randomness, static (n, value) => n.Randomness = value, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Direction in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DDirectionTween() : PropertyTween<CpuParticles3D, Vector3>(
    static n => n.Direction, static (n, value) => n.Direction = value, Interpolators.Vector3);

/// <summary>Animates CpuParticles3D.Direction.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DDirectionXTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Direction.X, static (n, value) => { var current = n.Direction; current.X = value; n.Direction = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Direction.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DDirectionYTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Direction.Y, static (n, value) => { var current = n.Direction; current.Y = value; n.Direction = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Direction.Z in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DDirectionZTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Direction.Z, static (n, value) => { var current = n.Direction; current.Z = value; n.Direction = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Gravity in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DGravityTween() : PropertyTween<CpuParticles3D, Vector3>(
    static n => n.Gravity, static (n, value) => n.Gravity = value, Interpolators.Vector3);

/// <summary>Animates CpuParticles3D.Gravity.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DGravityXTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Gravity.X, static (n, value) => { var current = n.Gravity; current.X = value; n.Gravity = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Gravity.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DGravityYTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Gravity.Y, static (n, value) => { var current = n.Gravity; current.Y = value; n.Gravity = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Gravity.Z in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DGravityZTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Gravity.Z, static (n, value) => { var current = n.Gravity; current.Z = value; n.Gravity = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Spread in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DSpreadTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Spread, static (n, value) => n.Spread = value, Interpolators.Float);

/// <summary>Animates CpuParticles3D.EmissionSphereRadius in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DEmissionSphereRadiusTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.EmissionSphereRadius, static (n, value) => n.EmissionSphereRadius = value, Interpolators.Float);

/// <summary>Animates CpuParticles3D.EmissionBoxExtents in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DEmissionBoxExtentsTween() : PropertyTween<CpuParticles3D, Vector3>(
    static n => n.EmissionBoxExtents, static (n, value) => n.EmissionBoxExtents = value, Interpolators.Vector3);

/// <summary>Animates CpuParticles3D.EmissionBoxExtents.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DEmissionBoxExtentsXTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.EmissionBoxExtents.X, static (n, value) => { var current = n.EmissionBoxExtents; current.X = value; n.EmissionBoxExtents = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.EmissionBoxExtents.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DEmissionBoxExtentsYTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.EmissionBoxExtents.Y, static (n, value) => { var current = n.EmissionBoxExtents; current.Y = value; n.EmissionBoxExtents = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.EmissionBoxExtents.Z in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DEmissionBoxExtentsZTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.EmissionBoxExtents.Z, static (n, value) => { var current = n.EmissionBoxExtents; current.Z = value; n.EmissionBoxExtents = current; }, Interpolators.Float);

/// <summary>Animates CpuParticles3D.Color in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DColorTween() : PropertyTween<CpuParticles3D, Color>(
    static n => n.Color, static (n, value) => n.Color = value, Interpolators.Color);

/// <summary>Animates CpuParticles3D.Color.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class CpuParticles3DColorAlphaTween() : PropertyTween<CpuParticles3D, float>(
    static n => n.Color.A, static (n, value) => { var current = n.Color; current.A = value; n.Color = current; }, Interpolators.Float);
