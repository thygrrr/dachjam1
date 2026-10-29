// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
namespace tweens.gd;

/// <summary>A segment's value interpolation, independently of the tween's global easing.</summary>
public readonly record struct Interpolation
{
    internal int Kind { get; }
    internal EaseType Ease { get; }
    private Interpolation(int kind, EaseType ease = default) { Kind = kind; Ease = ease; }
    /// <summary>Limited makima tangents and cubic Hermite interpolation. The default.</summary>
    public static Interpolation Smooth => default;
    /// <summary>Linear interpolation between adjacent keys.</summary>
    public static Interpolation Linear => new(1);
    /// <summary>Hold the previous key until the arriving key is reached.</summary>
    public static Interpolation Step => new(2);
    /// <summary>Ease a linear segment using an existing tweens.gd easing function.</summary>
    public static implicit operator Interpolation(EaseType ease) => new(3, ease);
}

/// <summary>A typed value at a percentage stop in [0, 100]. Interpolation applies to the arriving segment.</summary>
public readonly record struct CurveKey<T>(double At, T Value, Interpolation? Interpolation = null) where T : struct;
