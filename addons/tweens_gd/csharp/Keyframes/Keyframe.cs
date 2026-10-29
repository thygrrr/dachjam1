// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using Godot;

namespace tweens.gd;

/// <summary>A percentage in [0, 100]. Integer stops convert implicitly; fractional stops use Of.</summary>
public readonly record struct Percent
{
    internal double Value { get; }
    private Percent(double value)
    {
        if (!double.IsFinite(value) || value < 0 || value > 100) throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }
    /// <summary>Creates an explicitly fractional percentage stop.</summary>
    public static Percent Of(double value) => new(value);
    /// <summary>Converts an integer percentage stop.</summary>
    public static implicit operator Percent(int value) => new(value);
}

/// <summary>A scalar key accepting ordinary integer, float and double literals.</summary>
public readonly record struct Real(double Value)
{
    /// <summary>Converts a scalar literal.</summary>
    public static implicit operator Real(double value) => new(value);
    /// <summary>Converts a scalar literal.</summary>
    public static implicit operator Real(float value) => new(value);
    /// <summary>Converts a scalar literal.</summary>
    public static implicit operator Real(int value) => new(value);
}

/// <summary>A scalar or vector key. Its dimension is validated against the target; a scalar scale is uniform.</summary>
public readonly record struct Vec
{
    internal int Dimensions { get; }
    internal Vector3 Value { get; }
    private Vec(int dimensions, Vector3 value) { Dimensions = dimensions; Value = value; }
    /// <summary>Converts a scalar rotation or uniform scale.</summary>
    public static implicit operator Vec(double value) => new(1, new((float)value, 0, 0));
    /// <summary>Converts a scalar rotation or uniform scale.</summary>
    public static implicit operator Vec(float value) => (Vec)(double)value;
    /// <summary>Converts a scalar rotation or uniform scale.</summary>
    public static implicit operator Vec(int value) => (Vec)(double)value;
    /// <summary>Converts a Godot vector.</summary>
    public static implicit operator Vec(Vector2 value) => new(2, new(value.X, value.Y, 0));
    /// <summary>Converts a Godot vector.</summary>
    public static implicit operator Vec(Vector3 value) => new(3, value);
    /// <summary>Converts a tuple of vector components.</summary>
    public static implicit operator Vec((double X, double Y) value) => (Vec)new Vector2((float)value.X, (float)value.Y);
    /// <summary>Converts a tuple of vector components.</summary>
    public static implicit operator Vec((float X, float Y) value) => (Vec)new Vector2(value.X, value.Y);
    /// <summary>Converts a tuple of vector components.</summary>
    public static implicit operator Vec((double X, double Y, double Z) value) => (Vec)new Vector3((float)value.X, (float)value.Y, (float)value.Z);
    /// <summary>Converts a tuple of vector components.</summary>
    public static implicit operator Vec((float X, float Y, float Z) value) => (Vec)new Vector3(value.X, value.Y, value.Z);
    internal Variant ToVariant() => Dimensions <= 1 ? Variant.From(Value.X)
        : Dimensions == 2 ? Variant.From(new Vector2(Value.X, Value.Y)) : Variant.From(Value);
}

public static partial class Tweens
{
    /// <summary>A sparse key at one percentage stop. Unset channels have no key at that stop.</summary>
    public readonly record struct Keyframe
    {
        internal double Stop { get; init; }
        internal double? X { get; init; }
        internal double? Y { get; init; }
        internal double? Z { get; init; }
        internal Vec? Position { get; init; }
        internal Vec? Rotation { get; init; }
        internal Vec? RotationDegrees { get; init; }
        internal Vec? Scale { get; init; }
        internal Godot.Quaternion? Quaternion { get; init; }
        internal double? Skew { get; init; }
        internal Godot.Color? Modulate { get; init; }
        internal Godot.Color? SelfModulate { get; init; }
        internal double? Alpha { get; init; }
        internal double? Transparency { get; init; }
        internal Interpolation? Interpolation { get; init; }
        internal (string Path, Variant Value)[]? Paths { get; init; }

        /// <summary>Creates a sparse key. Interpolation applies to the segments arriving at this key.</summary>
        public static Keyframe At(Percent at, double? x = null, double? y = null, double? z = null,
            Vec? position = null, Vec? rotation = null, Vec? rotationDegrees = null, Vec? scale = null,
            Godot.Quaternion? quaternion = null, double? skew = null, Godot.Color? modulate = null, double? alpha = null,
            Godot.Color? selfModulate = null, double? transparency = null, Interpolation? interpolation = null)
            => new() { Stop = at.Value, X = x, Y = y, Z = z, Position = position, Rotation = rotation,
                RotationDegrees = rotationDegrees, Scale = scale, Quaternion = quaternion, Skew = skew,
                Modulate = modulate, Alpha = alpha, SelfModulate = selfModulate, Transparency = transparency, Interpolation = interpolation };

        /// <summary>Creates a key for explicit property paths. Paths stay on the target and its value components.</summary>
        public static Keyframe At(Percent at, params (string Path, Variant Value)[] paths)
        {
            ArgumentNullException.ThrowIfNull(paths);
            return new() { Stop = at.Value, Paths = (ValueTuple<string, Variant>[])paths.Clone() };
        }
    }
}
