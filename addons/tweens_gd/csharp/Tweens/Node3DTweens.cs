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

/// <summary>Animates Node3D Position3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position3DTween() : PropertyTween<Node3D, Vector3>(
    static n => n.Position, static (n, value) => n.Position = value, Interpolators.Vector3);

/// <summary>Animates Node3D Position3DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position3DXTween() : PropertyTween<Node3D, float>(
    static n => n.Position.X, static (n, value) => { var current = n.Position; current.X = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Node3D Position3DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position3DYTween() : PropertyTween<Node3D, float>(
    static n => n.Position.Y, static (n, value) => { var current = n.Position; current.Y = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Node3D Position3DZ; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position3DZTween() : PropertyTween<Node3D, float>(
    static n => n.Position.Z, static (n, value) => { var current = n.Position; current.Z = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Node3D GlobalPosition3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition3DTween() : PropertyTween<Node3D, Vector3>(
    static n => n.GlobalPosition, static (n, value) => n.GlobalPosition = value, Interpolators.Vector3);

/// <summary>Animates Node3D GlobalPosition3DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition3DXTween() : PropertyTween<Node3D, float>(
    static n => n.GlobalPosition.X, static (n, value) => { var current = n.GlobalPosition; current.X = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Node3D GlobalPosition3DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition3DYTween() : PropertyTween<Node3D, float>(
    static n => n.GlobalPosition.Y, static (n, value) => { var current = n.GlobalPosition; current.Y = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Node3D GlobalPosition3DZ; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition3DZTween() : PropertyTween<Node3D, float>(
    static n => n.GlobalPosition.Z, static (n, value) => { var current = n.GlobalPosition; current.Z = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Node3D Scale3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale3DTween() : PropertyTween<Node3D, Vector3>(
    static n => n.Scale, static (n, value) => n.Scale = value, Interpolators.Vector3);

/// <summary>Animates Node3D Scale3DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale3DXTween() : PropertyTween<Node3D, float>(
    static n => n.Scale.X, static (n, value) => { var current = n.Scale; current.X = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Node3D Scale3DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale3DYTween() : PropertyTween<Node3D, float>(
    static n => n.Scale.Y, static (n, value) => { var current = n.Scale; current.Y = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Node3D Scale3DZ; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale3DZTween() : PropertyTween<Node3D, float>(
    static n => n.Scale.Z, static (n, value) => { var current = n.Scale; current.Z = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Node3D Rotation3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Rotation3DTween() : PropertyTween<Node3D, Vector3>(
    static n => n.Rotation, static (n, value) => n.Rotation = value, Interpolators.Vector3);

/// <summary>Animates Node3D Rotation3DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Rotation3DXTween() : PropertyTween<Node3D, float>(
    static n => n.Rotation.X, static (n, value) => { var current = n.Rotation; current.X = value; n.Rotation = current; }, Interpolators.Float);

/// <summary>Animates Node3D Rotation3DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Rotation3DYTween() : PropertyTween<Node3D, float>(
    static n => n.Rotation.Y, static (n, value) => { var current = n.Rotation; current.Y = value; n.Rotation = current; }, Interpolators.Float);

/// <summary>Animates Node3D Rotation3DZ; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Rotation3DZTween() : PropertyTween<Node3D, float>(
    static n => n.Rotation.Z, static (n, value) => { var current = n.Rotation; current.Z = value; n.Rotation = current; }, Interpolators.Float);

/// <summary>Animates Node3D GlobalRotation3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalRotation3DTween() : PropertyTween<Node3D, Vector3>(
    static n => n.GlobalRotation, static (n, value) => n.GlobalRotation = value, Interpolators.Vector3);

/// <summary>Animates Node3D GlobalRotation3DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalRotation3DXTween() : PropertyTween<Node3D, float>(
    static n => n.GlobalRotation.X, static (n, value) => { var current = n.GlobalRotation; current.X = value; n.GlobalRotation = current; }, Interpolators.Float);

/// <summary>Animates Node3D GlobalRotation3DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalRotation3DYTween() : PropertyTween<Node3D, float>(
    static n => n.GlobalRotation.Y, static (n, value) => { var current = n.GlobalRotation; current.Y = value; n.GlobalRotation = current; }, Interpolators.Float);

/// <summary>Animates Node3D GlobalRotation3DZ; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalRotation3DZTween() : PropertyTween<Node3D, float>(
    static n => n.GlobalRotation.Z, static (n, value) => { var current = n.GlobalRotation; current.Z = value; n.GlobalRotation = current; }, Interpolators.Float);

/// <summary>Animates Node3D Quaternion3D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Quaternion3DTween() : PropertyTween<Node3D, Quaternion>(
    static n => n.Quaternion, static (n, value) => n.Quaternion = value, Interpolators.Quaternion);

