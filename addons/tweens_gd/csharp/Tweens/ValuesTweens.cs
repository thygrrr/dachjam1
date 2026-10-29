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

/// <summary>Reusable Float callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class FloatTween() : PropertyTween<Node, float>(
    static _ => 0, static (_, _) => {}, Interpolators.Float)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Double callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class DoubleTween() : PropertyTween<Node, double>(
    static _ => 0, static (_, _) => {}, Interpolators.Double)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Vector2 callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Vector2Tween() : PropertyTween<Node, Vector2>(
    static _ => Vector2.Zero, static (_, _) => {}, Interpolators.Vector2)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Vector3 callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Vector3Tween() : PropertyTween<Node, Vector3>(
    static _ => Vector3.Zero, static (_, _) => {}, Interpolators.Vector3)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Vector4 callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Vector4Tween() : PropertyTween<Node, Vector4>(
    static _ => Vector4.Zero, static (_, _) => {}, Interpolators.Vector4)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Color callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ColorTween() : PropertyTween<Node, Color>(
    static _ => new Color(0, 0, 0, 0), static (_, _) => {}, Interpolators.Color)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Quaternion callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class QuaternionTween() : PropertyTween<Node, Quaternion>(
    static _ => Quaternion.Identity, static (_, _) => {}, Interpolators.Quaternion)
{
    protected override bool ReadsWrittenValue => false;
}

/// <summary>Reusable Rect2 callback-value definition. Samples values without writing a node property.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Rect2Tween() : PropertyTween<Node, Rect2>(
    static _ => default(Rect2), static (_, _) => {}, Interpolators.Rect2)
{
    protected override bool ReadsWrittenValue => false;
}
