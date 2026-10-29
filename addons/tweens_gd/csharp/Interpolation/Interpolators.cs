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

/// <summary>Value interpolation preserving easing overshoot; integer results saturate at Int32 limits.</summary>
public static class Interpolators
{
    /// <summary>Rounds to nearest with ties away from zero; overshoot saturates at Int32 limits.</summary>
    public static int Int(int from, int to, float weight)
    {
        if (!float.IsFinite(weight)) throw new ArgumentOutOfRangeException(nameof(weight));
        var value = from + ((double)to - from) * weight;
        return (int)Math.Clamp(Math.Round(value, MidpointRounding.AwayFromZero), int.MinValue, int.MaxValue);
    }

    /// <summary>Linear Float interpolation. Weight may overshoot [0, 1].</summary>
    public static float Float(float from, float to, float weight)
    {
        var difference = to - from;
        return float.IsFinite(difference) ? from + difference * weight : from * (1 - weight) + to * weight;
    }
    /// <summary>Linear Double interpolation. Weight may overshoot [0, 1].</summary>
    public static double Double(double from, double to, float weight)
    {
        var difference = to - from;
        return double.IsFinite(difference) ? from + difference * weight : from * (1 - (double)weight) + to * weight;
    }
    /// <summary>Linear Vector2 interpolation. Weight may overshoot [0, 1].</summary>
    public static Vector2 Vector2(Vector2 from, Vector2 to, float weight)
        => new(Float(from.X, to.X, weight), Float(from.Y, to.Y, weight));
    /// <summary>Linear Vector3 interpolation. Weight may overshoot [0, 1].</summary>
    public static Vector3 Vector3(Vector3 from, Vector3 to, float weight)
        => new(Float(from.X, to.X, weight), Float(from.Y, to.Y, weight), Float(from.Z, to.Z, weight));
    /// <summary>Linear Vector4 interpolation. Weight may overshoot [0, 1].</summary>
    public static Vector4 Vector4(Vector4 from, Vector4 to, float weight)
        => new(Float(from.X, to.X, weight), Float(from.Y, to.Y, weight), Float(from.Z, to.Z, weight), Float(from.W, to.W, weight));
    /// <summary>Interpolates ordinary Godot colors in premultiplied OKLab. Weight may overshoot [0, 1].</summary>
    public static Color Color(Color from, Color to, float weight) => Color(from, to, weight,
        ColorSpace.Oklab, AlphaMode.Premultiplied, ColorEncoding.Srgb);

    /// <summary>Interpolates Godot colors with explicit working coordinates, alpha handling and boundary encoding.</summary>
    public static Color Color(Color from, Color to, float weight, ColorSpace space, AlphaMode alpha,
        ColorEncoding encoding = ColorEncoding.Srgb)
    {
        ColorInterpolation.Validate(space, alpha, encoding);
        if (weight == 0) return from;
        if (weight == 1) return to;
        return ColorInterpolation.Decode(ColorInterpolation.Encode(from, space, alpha, encoding).Lerp(
            ColorInterpolation.Encode(to, space, alpha, encoding), weight), space, alpha, encoding);
    }
    /// <summary>Linear Rect2 interpolation. Weight may overshoot [0, 1].</summary>
    public static Rect2 Rect2(Rect2 from, Rect2 to, float weight)
        => new(Vector2(from.Position, to.Position, weight), Vector2(from.Size, to.Size, weight));
    /// <summary>Spherical quaternion interpolation. Weight may overshoot [0, 1].</summary>
    public static Quaternion Quaternion(Quaternion from, Quaternion to, float weight)
    {
        if (!(from.LengthSquared() is > 0 and < float.PositiveInfinity) || !(to.LengthSquared() is > 0 and < float.PositiveInfinity))
            throw new ArgumentException("Quaternion endpoints must have finite, nonzero squared length.");
        return from.Normalized().Slerp(to.Normalized(), weight).Normalized();
    }
}
