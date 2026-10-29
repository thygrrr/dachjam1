// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using Godot;

namespace tweens.gd;

/// <summary>An immutable, reusable value curve. Preparation owns its keys; sampling allocates no managed memory.</summary>
/// <remarks>Stops are percentages. Missing final keys hold; missing initial keys require capture before sampling.</remarks>
public sealed class KeyframeCurve<T> where T : struct
{
    private readonly CurveKey<T>[] keys;
    private readonly CurveVector[] values, tangents;
    private readonly Func<float, float>?[] eases;
    private readonly Interpolation interpolation;
    private readonly ColorSpace colorSpace;
    private readonly AlphaMode alphaMode;
    private readonly ColorEncoding colorEncoding;
    /// <summary>Color policy prepared into this curve. It is independent of playback timing.</summary>
    public ColorPolicy Policy => new() { Space = colorSpace, AlphaMode = alphaMode, Encoding = colorEncoding };
    /// <summary>Whether playback must supply a captured value at zero percent.</summary>
    public bool NeedsStart => keys[0].At != 0;
    /// <summary>Number of supplied keys, including a synthetic final hold when required.</summary>
    public int Count => keys.Length;
    internal T FirstValue => keys[0].Value;
    internal T LastValue => keys[^1].Value;

    /// <summary>Copies strictly increasing finite keys and prepares values and tangents. Quaternion squared lengths must be finite and nonzero.</summary>
    public KeyframeCurve(ReadOnlySpan<CurveKey<T>> keys, Interpolation interpolation = default,
        ColorSpace colorSpace = default, AlphaMode alphaMode = default, ColorEncoding colorEncoding = default)
    {
        if (keys.IsEmpty) throw new ArgumentException("A curve needs at least one key.", nameof(keys));
        ColorInterpolation.Validate(colorSpace, alphaMode, colorEncoding);
        this.interpolation = interpolation;
        this.colorSpace = colorSpace; this.alphaMode = alphaMode; this.colorEncoding = colorEncoding;
        var hold = keys[^1].At < 100;
        this.keys = new CurveKey<T>[keys.Length + (hold ? 1 : 0)];
        keys.CopyTo(this.keys);
        if (hold) this.keys[^1] = new(100, keys[^1].Value, Interpolation.Step);
        values = new CurveVector[this.keys.Length];
        tangents = new CurveVector[this.keys.Length];
        eases = new Func<float, float>?[this.keys.Length];
        for (var i = 0; i < this.keys.Length; i++)
        {
            var key = this.keys[i];
            if (!double.IsFinite(key.At) || key.At < 0 || key.At > 100 || i > 0 && key.At <= this.keys[i - 1].At)
                throw new ArgumentException($"Invalid or duplicate stop at key {i}; use increasing percentages in [0, 100].", nameof(keys));
            if (key.At == 0 && key.Interpolation is not null)
                throw new ArgumentException("A zero-percent key has no arriving segment.", nameof(keys));
            values[i] = CurveValues<T>.Encode(key.Value, colorSpace, alphaMode, colorEncoding);
            var mode = key.Interpolation ?? interpolation;
            if (mode.Kind == 3) eases[i] = Easing.GetFunction(mode.Ease);
        }
        PrepareTangents();
    }

    /// <summary>Creates evenly spaced keys from at least two values.</summary>
    public static KeyframeCurve<T> EvenlySpaced(ReadOnlySpan<T> values, Interpolation interpolation = default,
        ColorSpace colorSpace = default, AlphaMode alphaMode = default, ColorEncoding colorEncoding = default)
    {
        if (values.Length < 2) throw new ArgumentException("An array channel needs at least two values.", nameof(values));
        var keys = new CurveKey<T>[values.Length];
        for (var i = 0; i < keys.Length; i++) keys[i] = new(100.0 * i / (keys.Length - 1), values[i]);
        return new(keys, interpolation, colorSpace, alphaMode, colorEncoding);
    }

    /// <summary>Shares this prepared curve when zero is explicit, otherwise prepares an independent captured start.</summary>
    public KeyframeCurve<T> CaptureStart(T initial)
    {
        if (!NeedsStart) return this;
        var captured = new CurveKey<T>[keys.Length + 1];
        captured[0] = new(0, initial);
        keys.CopyTo(captured, 1);
        return new(captured, interpolation, colorSpace, alphaMode, colorEncoding);
    }

    /// <summary>Samples normalized eased progress. Outside [0, 1], non-step segments extrapolate endpoint tangents;
    /// step segments and implicit final holds retain their endpoint. Progress scaled to percent must remain finite.</summary>
    public T Sample(double progress)
    {
        if (!double.IsFinite(progress)) throw new ArgumentOutOfRangeException(nameof(progress));
        if (NeedsStart) throw new InvalidOperationException("Capture this curve's starting value before sampling.");
        var stop = progress * 100;
        if (!double.IsFinite(stop)) throw new ArgumentOutOfRangeException(nameof(progress));
        var low = 0; var high = keys.Length - 1;
        while (low <= high)
        {
            var middle = low + (high - low) / 2;
            var keyProgress = keys[middle].At / 100;
            if (progress == keyProgress) return CurveValues<T>.Exact(keys[middle].Value);
            if (progress < keyProgress) high = middle - 1; else low = middle + 1;
        }
        var segment = Math.Clamp(high, 0, keys.Length - 2);
        var width = keys[segment + 1].At - keys[segment].At;
        var u = (stop - keys[segment].At) / width;
        var mode = keys[segment + 1].Interpolation ?? interpolation;
        if (typeof(T) == typeof(Quaternion))
        {
            var weight = u;
            if (mode.Kind == 2) weight = u < 1 ? 0 : 1;
            else if (mode.Kind == 3)
            {
                var ease = eases[segment + 1]!;
                const float h = 0.0001f;
                weight = u < 0 ? u * (ease(h) - ease(0)) / h
                    : u > 1 ? 1 + (u - 1) * (ease(1) - ease(1 - h)) / h : ease((float)u);
            }
            return (T)(object)Interpolators.Quaternion((Quaternion)(object)keys[segment].Value,
                (Quaternion)(object)keys[segment + 1].Value, (float)weight);
        }
        CurveVector value;
        if (stop < 0 || stop > 100)
        {
            var end = stop < 0 ? 0 : keys.Length - 1;
            var derivative = mode.Kind == 0 ? tangents[end] : mode.Kind == 2 ? default
                : Slope(values[segment], values[segment + 1], width);
            if (mode.Kind == 3)
            {
                const float h = 0.0001f;
                var ease = eases[segment + 1]!;
                derivative *= stop < 0 ? (ease(h) - ease(0)) / h : (ease(1) - ease(1 - h)) / h;
            }
            value = values[end] + derivative * (stop - keys[end].At);
        }
        else if (mode.Kind == 2) value = values[segment];
        else if (mode.Kind != 0)
        {
            if (mode.Kind == 3) u = eases[segment + 1]!((float)u);
            value = values[segment] * (1 - u) + values[segment + 1] * u;
        }
        else
        {
            var u2 = u * u; var u3 = u2 * u;
            value = default;
            for (var c = 0; c < CurveValues<T>.Dimensions; c++)
            {
                var a = values[segment][c]; var b = values[segment + 1][c];
                var m = tangents[segment][c]; var n = tangents[segment + 1][c];
                // Scale only this segment's arithmetic; distant large keys must not underflow small values.
                var scale = Math.Max(1, Math.Max(Math.Max(Math.Abs(a), Math.Abs(b)), Math.Max(Math.Abs(m), Math.Abs(n))));
                var sampled = a / scale * (2 * u3 - 3 * u2 + 1) + m / scale * ((u3 - 2 * u2 + u) * width)
                    + b / scale * (-2 * u3 + 3 * u2) + n / scale * ((u3 - u2) * width);
                value = value.With(c, Math.Clamp(sampled, Math.Min(a / scale, b / scale), Math.Max(a / scale, b / scale)) * scale);
            }
        }
        return CurveValues<T>.Decode(value, colorSpace, alphaMode, colorEncoding);
    }

    private static double Slope(double from, double to, double width)
    {
        var difference = to - from;
        return FiniteSlope(double.IsFinite(difference) ? difference / width : to / width - from / width);
    }

    private static double FiniteSlope(double value) => Math.Clamp(value, -double.MaxValue, double.MaxValue);

    private static CurveVector Slope(CurveVector from, CurveVector to, double width)
        => new(Slope(from.X, to.X, width), Slope(from.Y, to.Y, width), Slope(from.Z, to.Z, width), Slope(from.W, to.W, width));

    private void PrepareTangents()
    {
        if (keys.Length < 2 || typeof(T) == typeof(Quaternion)) return;
        var slopes = new double[keys.Length + 3];
        for (var component = 0; component < CurveValues<T>.Dimensions; component++)
        {
            for (var i = 0; i < keys.Length - 1; i++)
                slopes[i + 2] = Slope(values[i][component], values[i + 1][component], keys[i + 1].At - keys[i].At);
            slopes[1] = keys.Length == 2 ? slopes[2] : FiniteSlope(2 * slopes[2] - slopes[3]);
            slopes[0] = FiniteSlope(2 * slopes[1] - slopes[2]);
            slopes[keys.Length + 1] = keys.Length == 2 ? slopes[2] : FiniteSlope(2 * slopes[keys.Length] - slopes[keys.Length - 1]);
            slopes[keys.Length + 2] = FiniteSlope(2 * slopes[keys.Length + 1] - slopes[keys.Length]);
            for (var i = 0; i < keys.Length; i++)
            {
                var scale = Math.Max(1, Math.Max(Math.Max(Math.Abs(slopes[i]), Math.Abs(slopes[i + 1])),
                    Math.Max(Math.Abs(slopes[i + 2]), Math.Abs(slopes[i + 3]))));
                var left = Math.Abs(slopes[i + 1] / scale - slopes[i] / scale) + 0.5 * Math.Abs(slopes[i + 1] / scale + slopes[i] / scale);
                var right = Math.Abs(slopes[i + 3] / scale - slopes[i + 2] / scale) + 0.5 * Math.Abs(slopes[i + 3] / scale + slopes[i + 2] / scale);
                var weight = left + right == 0 ? 0 : left / (left + right);
                var tangent = slopes[i + 1] * (1 - weight) + slopes[i + 2] * weight;
                if (i > 0 && i < keys.Length - 1 && Math.Sign(slopes[i + 1]) != Math.Sign(slopes[i + 2])) tangent = 0;
                tangents[i] = tangents[i].With(component, tangent);
            }
            for (var i = 0; i < keys.Length - 1; i++)
            {
                var delta = slopes[i + 2];
                var sign = Math.Sign(delta);
                var a = Math.Max(0, sign * tangents[i][component]);
                var b = Math.Max(0, sign * tangents[i + 1][component]);
                var largest = Math.Max(double.Epsilon, Math.Max(a, b));
                var x = a / largest; var y = b / largest;
                var length = Math.Sqrt(x * x + y * y);
                // Compare and limit magnitudes without squaring huge ratios or dividing by tiny slopes.
                if (largest / 3 > Math.Abs(delta) / length)
                {
                    a = (3 * x / length) * Math.Abs(delta);
                    b = (3 * y / length) * Math.Abs(delta);
                }
                tangents[i] = tangents[i].With(component, sign * a);
                tangents[i + 1] = tangents[i + 1].With(component, sign * b);
            }
        }
    }
}

internal readonly record struct CurveVector(double X, double Y = 0, double Z = 0, double W = 0)
{
    internal double this[int i] => i == 0 ? X : i == 1 ? Y : i == 2 ? Z : W;
    internal CurveVector With(int i, double v) => i == 0 ? this with { X = v } : i == 1 ? this with { Y = v }
        : i == 2 ? this with { Z = v } : this with { W = v };
    public static CurveVector operator +(CurveVector a, CurveVector b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    public static CurveVector operator -(CurveVector a, CurveVector b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    public static CurveVector operator *(CurveVector a, double b) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);
    public static CurveVector operator /(CurveVector a, double b) => a * (1 / b);
}

internal static class CurveValues<T> where T : struct
{
    internal static int Dimensions => typeof(T) == typeof(Vector2) ? 2 : typeof(T) == typeof(Vector3) ? 3
        : typeof(T) == typeof(Color) || typeof(T) == typeof(Vector4) || typeof(T) == typeof(Rect2) || typeof(T) == typeof(Quaternion) ? 4 : 1;
    internal static T Exact(T value) => typeof(T) == typeof(Quaternion) ? (T)(object)((Quaternion)(object)value).Normalized() : value;
    internal static CurveVector Encode(T value, ColorSpace space, AlphaMode alpha, ColorEncoding encoding)
    {
        CurveVector result;
        if (typeof(T) == typeof(float)) result = new((float)(object)value);
        else if (typeof(T) == typeof(double)) result = new((double)(object)value);
        else if (typeof(T) == typeof(int)) result = new((int)(object)value);
        else if (typeof(T) == typeof(long)) result = new((long)(object)value);
        else if (typeof(T) == typeof(Vector2)) { var v = (Vector2)(object)value; result = new(v.X, v.Y); }
        else if (typeof(T) == typeof(Vector3)) { var v = (Vector3)(object)value; result = new(v.X, v.Y, v.Z); }
        else if (typeof(T) == typeof(Vector4)) { var v = (Vector4)(object)value; result = new(v.X, v.Y, v.Z, v.W); }
        else if (typeof(T) == typeof(Rect2)) { var v = (Rect2)(object)value; result = new(v.Position.X, v.Position.Y, v.Size.X, v.Size.Y); }
        else if (typeof(T) == typeof(Color)) { var v = ColorInterpolation.Encode((Color)(object)value, space, alpha, encoding); result = new(v.X, v.Y, v.Z, v.W); }
        else if (typeof(T) == typeof(Quaternion))
        {
            var v = (Quaternion)(object)value;
            if (!(v.LengthSquared() is > 0 and < float.PositiveInfinity))
                throw new ArgumentException("Quaternion keys must have finite, nonzero squared length.");
            result = new(v.X, v.Y, v.Z, v.W);
        }
        else throw new NotSupportedException($"Keyframes do not support {typeof(T).Name} values.");
        for (var i = 0; i < Dimensions; i++) if (!double.IsFinite(result[i])) throw new ArgumentException("Keyframe values must be finite.");
        return result;
    }
    internal static T Decode(CurveVector v, ColorSpace space, AlphaMode alpha, ColorEncoding encoding)
    {
        if (typeof(T) == typeof(float)) return (T)(object)(float)v.X;
        if (typeof(T) == typeof(double)) return (T)(object)v.X;
        if (typeof(T) == typeof(int)) return (T)(object)(int)Math.Clamp(Math.Round(v.X, MidpointRounding.AwayFromZero), int.MinValue, int.MaxValue);
        if (typeof(T) == typeof(long))
        {
            var rounded = Math.Round(v.X, MidpointRounding.AwayFromZero);
            return (T)(object)(rounded >= 9223372036854775808.0 ? long.MaxValue
                : rounded <= -9223372036854775808.0 ? long.MinValue : (long)rounded);
        }
        if (typeof(T) == typeof(Vector2)) return (T)(object)new Vector2((float)v.X, (float)v.Y);
        if (typeof(T) == typeof(Vector3)) return (T)(object)new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        if (typeof(T) == typeof(Vector4)) return (T)(object)new Vector4((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
        if (typeof(T) == typeof(Rect2)) return (T)(object)new Rect2((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
        return (T)(object)ColorInterpolation.Decode(new((float)v.X, (float)v.Y, (float)v.Z, (float)v.W), space, alpha, encoding);
    }
}
