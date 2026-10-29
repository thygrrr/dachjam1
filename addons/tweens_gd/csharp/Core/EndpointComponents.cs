// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;

using Godot;

namespace tweens.gd;

/// <summary>Builds vector and color endpoints from component collections, such as [400, 180].</summary>
internal static class EndpointComponents
{
    internal static Vector2 ToVector2(ReadOnlySpan<double> to)
    {
        Require(to, 2);
        return new Vector2((float)to[0], (float)to[1]);
    }

    internal static Vector3 ToVector3(ReadOnlySpan<double> to)
    {
        Require(to, 3);
        return new Vector3((float)to[0], (float)to[1], (float)to[2]);
    }

    internal static Vector4 ToVector4(ReadOnlySpan<double> to)
    {
        Require(to, 4);
        return new Vector4((float)to[0], (float)to[1], (float)to[2], (float)to[3]);
    }

    // Three components leave the alpha opaque.
    internal static Color ToColor(ReadOnlySpan<double> to) => to.Length switch
    {
        3 => new Color((float)to[0], (float)to[1], (float)to[2]),
        4 => new Color((float)to[0], (float)to[1], (float)to[2], (float)to[3]),
        _ => throw new ArgumentException($"A color endpoint needs 3 or 4 components, not {to.Length}.", nameof(to)),
    };

    private static void Require(ReadOnlySpan<double> to, int count)
    {
        if (to.Length != count)
            throw new ArgumentException($"This endpoint needs {count} components, not {to.Length}.", nameof(to));
    }
}
