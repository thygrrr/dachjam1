// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss
// SPDX-FileCopyrightText: 2020 Björn Ottosson
// Adapted from Oklab; see THIRD-PARTY-NOTICES.md.

#nullable enable
using System;
using Godot;

namespace tweens.gd;

/// <summary>The coordinates used to interpolate colors.</summary>
public enum ColorSpace
{
    /// <summary>Perceptual OKLab coordinates. The default.</summary>
    Oklab,
    /// <summary>Godot's sRGB-encoded RGB components.</summary>
    Srgb,
    /// <summary>Linear-light RGB components.</summary>
    LinearRgb,
}

/// <summary>How opacity participates in color interpolation.</summary>
public enum AlphaMode
{
    /// <summary>Premultiply working color coordinates by alpha; unpremultiply before writing. The default.</summary>
    Premultiplied,
    /// <summary>Interpolate working color coordinates independently of alpha.</summary>
    Straight,
}

/// <summary>The RGB encoding expected by the target Godot API; alpha is always linear.</summary>
public enum ColorEncoding
{
    /// <summary>Ordinary Godot Color values. The default.</summary>
    Srgb,
    /// <summary>An API explicitly taking linear RGB values.</summary>
    LinearRgb,
}

/// <summary>Converts ordinary Godot colors to and from interpolation coordinates.</summary>
internal static class ColorInterpolation
{
    internal static void Validate(ColorSpace space, AlphaMode alpha, ColorEncoding encoding)
    {
        if ((uint)space > 2) throw new ArgumentOutOfRangeException(nameof(space));
        if ((uint)alpha > 1) throw new ArgumentOutOfRangeException(nameof(alpha));
        if ((uint)encoding > 1) throw new ArgumentOutOfRangeException(nameof(encoding));
    }

    internal static Vector4 Encode(Color color, ColorSpace space, AlphaMode alpha, ColorEncoding encoding)
    {
        if (space == ColorSpace.Srgb)
        {
            if (encoding == ColorEncoding.LinearRgb) color = color.LinearToSrgb();
        }
        else if (encoding == ColorEncoding.Srgb) color = color.SrgbToLinear();
        var rgb = new Vector3(color.R, color.G, color.B);
        if (space == ColorSpace.Oklab) rgb = ToOklab(rgb);
        if (alpha == AlphaMode.Premultiplied) rgb *= color.A;
        return new(rgb.X, rgb.Y, rgb.Z, color.A);
    }

    internal static Color Decode(Vector4 value, ColorSpace space, AlphaMode alpha, ColorEncoding encoding)
    {
        var rgb = new Vector3(value.X, value.Y, value.Z);
        if (alpha == AlphaMode.Premultiplied) rgb = value.W == 0 ? Vector3.Zero : rgb / value.W;
        if (space == ColorSpace.Oklab) rgb = FromOklab(rgb);
        var result = new Color(rgb.X, rgb.Y, rgb.Z, value.W);
        if (space == ColorSpace.Srgb) return encoding == ColorEncoding.Srgb ? result : result.SrgbToLinear();
        return encoding == ColorEncoding.LinearRgb ? result : result.LinearToSrgb();
    }

    // https://bottosson.github.io/posts/oklab/ (2021-01-25 matrices).
    private static Vector3 ToOklab(Vector3 rgb)
    {
        var l = MathF.Cbrt(0.4122214708f * rgb.X + 0.5363325363f * rgb.Y + 0.0514459929f * rgb.Z);
        var m = MathF.Cbrt(0.2119034982f * rgb.X + 0.6806995451f * rgb.Y + 0.1073969566f * rgb.Z);
        var s = MathF.Cbrt(0.0883024619f * rgb.X + 0.2817188376f * rgb.Y + 0.6299787005f * rgb.Z);
        return new(0.2104542553f * l + 0.7936177850f * m - 0.0040720468f * s,
            1.9779984951f * l - 2.4285922050f * m + 0.4505937099f * s,
            0.0259040371f * l + 0.7827717662f * m - 0.8086757660f * s);
    }

    private static Vector3 FromOklab(Vector3 lab)
    {
        var l = lab.X + 0.3963377774f * lab.Y + 0.2158037573f * lab.Z;
        var m = lab.X - 0.1055613458f * lab.Y - 0.0638541728f * lab.Z;
        var s = lab.X - 0.0894841775f * lab.Y - 1.2914855480f * lab.Z;
        l *= l * l; m *= m * m; s *= s * s;
        return new(4.0767416621f * l - 3.3077115913f * m + 0.2309699292f * s,
            -1.2684380046f * l + 2.6097574011f * m - 0.3413193965f * s,
            -0.0041960863f * l - 0.7034186147f * m + 1.7076147010f * s);
    }
}
