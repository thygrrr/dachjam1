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

internal static class ShaderValues<T> where T : struct
{
    internal static Variant.Type Type => typeof(T) == typeof(float) || typeof(T) == typeof(double) ? Variant.Type.Float
        : typeof(T) == typeof(int) ? Variant.Type.Int
        : typeof(T) == typeof(Vector2) ? Variant.Type.Vector2
        : typeof(T) == typeof(Vector3) ? Variant.Type.Vector3
        : typeof(T) == typeof(Vector4) ? Variant.Type.Vector4
        : typeof(T) == typeof(Color) ? Variant.Type.Color
        : throw new NotSupportedException($"Shader tweens do not support {typeof(T).Name}.");

    internal static void ValidateType(Variant.Type actual)
    {
        if (actual != Type) throw new ArgumentException($"Shader uniform has type {actual}, but {typeof(T).Name} requires {Type}.");
    }

    internal static void Validate(T value)
    {
        var finite = value switch
        {
            float x => float.IsFinite(x), double x => double.IsFinite(x), int => true,
            Vector2 x => x.IsFinite(), Vector3 x => x.IsFinite(), Vector4 x => x.IsFinite(),
            Color x => float.IsFinite(x.R) && float.IsFinite(x.G) && float.IsFinite(x.B) && float.IsFinite(x.A),
            _ => throw new NotSupportedException($"Shader tweens do not support {typeof(T).Name}."),
        };
        if (!finite) throw new ArgumentException("Shader tween values must be finite.");
    }

    internal static T Read(Variant value)
    {
        ValidateType(value.VariantType);
        object result = typeof(T) == typeof(float) ? value.AsSingle()
            : typeof(T) == typeof(double) ? value.AsDouble()
            : typeof(T) == typeof(int) ? checked((int)value.AsInt64())
            : typeof(T) == typeof(Vector2) ? value.AsVector2()
            : typeof(T) == typeof(Vector3) ? value.AsVector3()
            : typeof(T) == typeof(Vector4) ? value.AsVector4() : value.AsColor();
        var typed = (T)result;
        Validate(typed);
        return typed;
    }

    internal static Variant Write(T value)
    {
        Validate(value);
        return value switch
        {
            float x => Variant.From(x), double x => Variant.From(x), int x => Variant.From(x),
            Vector2 x => Variant.From(x), Vector3 x => Variant.From(x), Vector4 x => Variant.From(x),
            Color x => Variant.From(x), _ => throw new NotSupportedException(),
        };
    }

    internal static T Interpolate(T from, T to, float weight)
    {
        object result = from switch
        {
            float x => Interpolators.Float(x, (float)(object)to, weight),
            double x => Interpolators.Double(x, (double)(object)to, weight),
            int x => Interpolators.Int(x, (int)(object)to, weight),
            Vector2 x => Interpolators.Vector2(x, (Vector2)(object)to, weight),
            Vector3 x => Interpolators.Vector3(x, (Vector3)(object)to, weight),
            Vector4 x => Interpolators.Vector4(x, (Vector4)(object)to, weight),
            Color x => Interpolators.Color(x, (Color)(object)to, weight),
            _ => throw new NotSupportedException(),
        };
        return (T)result;
    }
}
