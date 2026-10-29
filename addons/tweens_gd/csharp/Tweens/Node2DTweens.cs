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

/// <summary>Animates Node2D Position2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position2DTween() : PropertyTween<Node2D, Vector2>(
    static n => n.Position, static (n, value) => n.Position = value, Interpolators.Vector2);

/// <summary>Animates Node2D Position2DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position2DXTween() : PropertyTween<Node2D, float>(
    static n => n.Position.X, static (n, value) => { var current = n.Position; current.X = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Node2D Position2DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Position2DYTween() : PropertyTween<Node2D, float>(
    static n => n.Position.Y, static (n, value) => { var current = n.Position; current.Y = value; n.Position = current; }, Interpolators.Float);

/// <summary>Animates Node2D GlobalPosition2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition2DTween() : PropertyTween<Node2D, Vector2>(
    static n => n.GlobalPosition, static (n, value) => n.GlobalPosition = value, Interpolators.Vector2);

/// <summary>Animates Node2D GlobalPosition2DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition2DXTween() : PropertyTween<Node2D, float>(
    static n => n.GlobalPosition.X, static (n, value) => { var current = n.GlobalPosition; current.X = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Node2D GlobalPosition2DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalPosition2DYTween() : PropertyTween<Node2D, float>(
    static n => n.GlobalPosition.Y, static (n, value) => { var current = n.GlobalPosition; current.Y = value; n.GlobalPosition = current; }, Interpolators.Float);

/// <summary>Animates Node2D Scale2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale2DTween() : PropertyTween<Node2D, Vector2>(
    static n => n.Scale, static (n, value) => n.Scale = value, Interpolators.Vector2);

/// <summary>Animates Node2D Scale2DX; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale2DXTween() : PropertyTween<Node2D, float>(
    static n => n.Scale.X, static (n, value) => { var current = n.Scale; current.X = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Node2D Scale2DY; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Scale2DYTween() : PropertyTween<Node2D, float>(
    static n => n.Scale.Y, static (n, value) => { var current = n.Scale; current.Y = value; n.Scale = current; }, Interpolators.Float);

/// <summary>Animates Node2D Rotation2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Rotation2DTween() : PropertyTween<Node2D, float>(
    static n => n.Rotation, static (n, value) => n.Rotation = value, Interpolators.Float);

/// <summary>Animates Node2D GlobalRotation2D; values use Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalRotation2DTween() : PropertyTween<Node2D, float>(
    static n => n.GlobalRotation, static (n, value) => n.GlobalRotation = value, Interpolators.Float);

