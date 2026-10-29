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

/// <summary>Animates Node2D.Skew in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Skew2DTween() : PropertyTween<Node2D, float>(
    static n => n.Skew, static (n, value) => n.Skew = value, Interpolators.Float);

/// <summary>Animates Node2D.GlobalSkew in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalSkew2DTween() : PropertyTween<Node2D, float>(
    static n => n.GlobalSkew, static (n, value) => n.GlobalSkew = value, Interpolators.Float);

/// <summary>Animates Node2D.GlobalScale in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalScale2DTween() : PropertyTween<Node2D, Vector2>(
    static n => n.GlobalScale, static (n, value) => n.GlobalScale = value, Interpolators.Vector2);

/// <summary>Animates Node2D.GlobalScale.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalScale2DXTween() : PropertyTween<Node2D, float>(
    static n => n.GlobalScale.X, static (n, value) => { var current = n.GlobalScale; current.X = value; n.GlobalScale = current; }, Interpolators.Float);

/// <summary>Animates Node2D.GlobalScale.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalScale2DYTween() : PropertyTween<Node2D, float>(
    static n => n.GlobalScale.Y, static (n, value) => { var current = n.GlobalScale; current.Y = value; n.GlobalScale = current; }, Interpolators.Float);

/// <summary>Animates Node3D global orientation with shortest-path quaternion interpolation. Preserves global position and basis scale; replaces shear, matching Godot global rotation.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class GlobalQuaternion3DTween() : PropertyTween<Node3D, Quaternion>(
    static n => Quaternion.FromEuler(n.GlobalRotation), static (n, value) => n.GlobalRotation = value.GetEuler(), Interpolators.Quaternion);
