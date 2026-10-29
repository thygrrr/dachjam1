// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using Godot;

namespace tweens.gd;

/// <summary>Color working space and boundary conventions. The default is premultiplied OKLab with sRGB inputs.</summary>
public readonly record struct ColorPolicy
{
    /// <summary>Working coordinates used for interpolation.</summary>
    public ColorSpace Space { get; init; }
    /// <summary>Whether working coordinates are premultiplied by alpha.</summary>
    public AlphaMode AlphaMode { get; init; }
    /// <summary>RGB encoding at the Godot API boundary; alpha remains linear and straight.</summary>
    public ColorEncoding Encoding { get; init; }

    internal Vector4 Encode(Color value) => ColorInterpolation.Encode(value, Space, AlphaMode, Encoding);
    internal Color Decode(Vector4 value) => ColorInterpolation.Decode(value, Space, AlphaMode, Encoding);
}
