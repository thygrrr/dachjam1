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

/// <summary>Relative arithmetic for <c>By</c>. Quaternion offsets rotate about the value's own (local) axes.</summary>
/// <remarks>The typeof checks are JIT constants, so each value type compiles to its own branch without boxing.</remarks>
internal static class Offsets<T> where T : struct
{
    internal static bool Supported => typeof(T) == typeof(int) || typeof(T) == typeof(float) || typeof(T) == typeof(double)
        || typeof(T) == typeof(Vector2) || typeof(T) == typeof(Vector3) || typeof(T) == typeof(Vector4)
        || typeof(T) == typeof(Color) || typeof(T) == typeof(Quaternion) || typeof(T) == typeof(Rect2);

    internal static T Zero => typeof(T) == typeof(Quaternion) ? (T)(object)Quaternion.Identity : default;

    internal static T Add(T value, T offset)
    {
        if (typeof(T) == typeof(int)) return (T)(object)Saturate((long)(int)(object)value + (int)(object)offset);
        if (typeof(T) == typeof(float)) return (T)(object)((float)(object)value + (float)(object)offset);
        if (typeof(T) == typeof(double)) return (T)(object)((double)(object)value + (double)(object)offset);
        if (typeof(T) == typeof(Vector2)) return (T)(object)((Vector2)(object)value + (Vector2)(object)offset);
        if (typeof(T) == typeof(Vector3)) return (T)(object)((Vector3)(object)value + (Vector3)(object)offset);
        if (typeof(T) == typeof(Vector4)) return (T)(object)((Vector4)(object)value + (Vector4)(object)offset);
        if (typeof(T) == typeof(Color)) return (T)(object)((Color)(object)value + (Color)(object)offset);
        if (typeof(T) == typeof(Quaternion)) return (T)(object)Rotate((Quaternion)(object)value, (Quaternion)(object)offset);
        if (typeof(T) == typeof(Rect2))
        {
            var (rect, by) = ((Rect2)(object)value, (Rect2)(object)offset);
            return (T)(object)new Rect2(rect.Position + by.Position, rect.Size + by.Size);
        }
        throw Unsupported();
    }

    /// <summary>The value that <see cref="Add"/> turns into <paramref name="value"/> with this offset.</summary>
    internal static T Remove(T value, T offset)
    {
        if (typeof(T) == typeof(int)) return (T)(object)Saturate((long)(int)(object)value - (int)(object)offset);
        if (typeof(T) == typeof(float)) return (T)(object)((float)(object)value - (float)(object)offset);
        if (typeof(T) == typeof(double)) return (T)(object)((double)(object)value - (double)(object)offset);
        if (typeof(T) == typeof(Vector2)) return (T)(object)((Vector2)(object)value - (Vector2)(object)offset);
        if (typeof(T) == typeof(Vector3)) return (T)(object)((Vector3)(object)value - (Vector3)(object)offset);
        if (typeof(T) == typeof(Vector4)) return (T)(object)((Vector4)(object)value - (Vector4)(object)offset);
        if (typeof(T) == typeof(Color)) return (T)(object)((Color)(object)value - (Color)(object)offset);
        if (typeof(T) == typeof(Quaternion))
        {
            var by = (Quaternion)(object)offset;
            return (T)(object)Rotate((Quaternion)(object)value, new Quaternion(-by.X, -by.Y, -by.Z, by.W));
        }
        if (typeof(T) == typeof(Rect2))
        {
            var (rect, by) = ((Rect2)(object)value, (Rect2)(object)offset);
            return (T)(object)new Rect2(rect.Position - by.Position, rect.Size - by.Size);
        }
        throw Unsupported();
    }

    internal static NotSupportedException Unsupported() => new($"By, factors and deltas do not support {typeof(T).Name} "
        + "values. Use int, float, double, a vector, Color, Quaternion or Rect2.");

    private static int Saturate(long value) => (int)Math.Clamp(value, int.MinValue, int.MaxValue);

    // Offsets are unit rotations from slerp, so the conjugate in Remove is their inverse.
    private static Quaternion Rotate(Quaternion value, Quaternion offset)
    {
        if (value.LengthSquared() == 0) throw new ArgumentException("Quaternion values must be nonzero.");
        return (value.Normalized() * offset).Normalized();
    }
}
