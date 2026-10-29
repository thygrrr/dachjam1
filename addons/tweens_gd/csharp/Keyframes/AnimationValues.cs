// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using Godot;

namespace tweens.gd;

internal static class AnimationValues
{
    internal static void Validate(Variant value)
    {
        var valid = value.VariantType switch
        {
            Variant.Type.Int => true,
            Variant.Type.Float => double.IsFinite(value.AsDouble()),
            Variant.Type.Vector2 => value.AsVector2().IsFinite(),
            Variant.Type.Vector3 => value.AsVector3().IsFinite(),
            Variant.Type.Vector4 => value.AsVector4().IsFinite(),
            Variant.Type.Rect2 => value.AsRect2().Position.IsFinite() && value.AsRect2().Size.IsFinite(),
            Variant.Type.Quaternion => value.AsQuaternion().LengthSquared() is > 0 and < float.PositiveInfinity,
            Variant.Type.Color => float.IsFinite(value.AsColor().R) && float.IsFinite(value.AsColor().G)
                && float.IsFinite(value.AsColor().B) && float.IsFinite(value.AsColor().A),
            _ => false,
        };
        if (!valid) throw new ArgumentException("Animation keys must be finite supported values; quaternion keys must be nonzero.");
    }

    internal static Variant Coerce(Variant value, Variant.Type type, bool uniform)
    {
        if (value.VariantType == type) return value;
        if (value.VariantType is Variant.Type.Int or Variant.Type.Float)
        {
            var number = value.AsDouble();
            if (type == Variant.Type.Float) return number;
            if (type == Variant.Type.Int) return checked((long)Math.Round(number, MidpointRounding.AwayFromZero));
            if (uniform && type == Variant.Type.Vector2) return new Vector2((float)number, (float)number);
            if (uniform && type == Variant.Type.Vector3) return new Vector3((float)number, (float)number, (float)number);
        }
        throw new ArgumentException($"Key type {value.VariantType} does not match channel type {type}.");
    }

    internal static T Read<T>(Variant value) where T : struct
    {
        var type = typeof(T) == typeof(double) ? Variant.Type.Float : typeof(T) == typeof(int) || typeof(T) == typeof(long) ? Variant.Type.Int
            : typeof(T) == typeof(Vector2) ? Variant.Type.Vector2 : typeof(T) == typeof(Vector3) ? Variant.Type.Vector3
            : typeof(T) == typeof(Vector4) ? Variant.Type.Vector4 : typeof(T) == typeof(Color) ? Variant.Type.Color
            : typeof(T) == typeof(Quaternion) ? Variant.Type.Quaternion : Variant.Type.Rect2;
        if (value.VariantType != type) throw new ArgumentException($"Channel changed type from {type} to {value.VariantType}.");
        if (typeof(T) == typeof(double)) return (T)(object)value.AsDouble();
        if (typeof(T) == typeof(int)) return (T)(object)checked((int)value.AsInt64());
        if (typeof(T) == typeof(long)) return (T)(object)value.AsInt64();
        if (typeof(T) == typeof(Vector2)) return (T)(object)value.AsVector2();
        if (typeof(T) == typeof(Vector3)) return (T)(object)value.AsVector3();
        if (typeof(T) == typeof(Vector4)) return (T)(object)value.AsVector4();
        if (typeof(T) == typeof(Color)) return (T)(object)value.AsColor();
        if (typeof(T) == typeof(Quaternion)) return (T)(object)value.AsQuaternion();
        return (T)(object)value.AsRect2();
    }

    internal static Variant Write<T>(T value) where T : struct
    {
        if (typeof(T) == typeof(double)) return Variant.From((double)(object)value);
        if (typeof(T) == typeof(int)) return Variant.From((int)(object)value);
        if (typeof(T) == typeof(long)) return Variant.From((long)(object)value);
        if (typeof(T) == typeof(Vector2)) return Variant.From((Vector2)(object)value);
        if (typeof(T) == typeof(Vector3)) return Variant.From((Vector3)(object)value);
        if (typeof(T) == typeof(Vector4)) return Variant.From((Vector4)(object)value);
        if (typeof(T) == typeof(Color)) return Variant.From((Color)(object)value);
        if (typeof(T) == typeof(Quaternion)) return Variant.From((Quaternion)(object)value);
        return Variant.From((Rect2)(object)value);
    }

    internal static void ValidatePath(Node target, string path)
    {
        var parts = path.Split(':');
        var found = false;
        var type = Variant.Type.Nil;
        var properties = target.GetPropertyList();
        foreach (var property in properties)
        {
            using (property)
            {
                if (property["name"].AsString() != parts[0]) continue;
                found = true; type = (Variant.Type)property["type"].AsInt32();
                break;
            }
        }
        if (!found) throw new ArgumentException($"Channel '{path}' does not resolve on {target.GetType().Name}.");
        for (var i = 1; i < parts.Length; i++)
        {
            var component = parts[i];
            type = type switch
            {
                Variant.Type.Vector2 when component is "x" or "y" => Variant.Type.Float,
                Variant.Type.Vector3 when component is "x" or "y" or "z" => Variant.Type.Float,
                Variant.Type.Vector4 or Variant.Type.Quaternion when component is "x" or "y" or "z" or "w" => Variant.Type.Float,
                Variant.Type.Color when component is "r" or "g" or "b" or "a" => Variant.Type.Float,
                Variant.Type.Rect2 when component is "position" or "size" or "end" => Variant.Type.Vector2,
                _ => throw new ArgumentException($"Channel '{path}' has an unsupported component '{component}'. Paths cannot traverse Objects."),
            };
        }
    }
}
