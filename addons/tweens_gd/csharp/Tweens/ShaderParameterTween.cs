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

/// <summary>Animates a typed uniform on the exact shared ShaderMaterial. Names and bindings are captured per playback.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ShaderParameterTween<TValue>(string parameter) : TweenDefinition<ShaderMaterial, TValue>
    where TValue : struct
{
    public string Parameter { get; set; } = parameter;
    private StringName? name;
    private ShaderWatch? watch;
    private bool? hadOverride;
    protected override bool UsesColorInterpolation => typeof(TValue) == typeof(Color);

    protected override void Prepare(ShaderMaterial target)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Parameter);
        _ = ShaderValues<TValue>.Type;
        foreach (var value in (ReadOnlySpan<TValue?>)[From, To, By, DeltaFrom, DeltaTo, DeltaBy])
            if (value is { } set) ShaderValues<TValue>.Validate(set);
        name = new StringName(Parameter);
        var shader = target.Shader;
        watch = new ShaderWatch(shader);
        using var uniforms = shader.GetShaderUniformList();
        foreach (var entry in uniforms)
        {
            using (entry)
            using (var uniform = entry.AsGodotDictionary())
            {
                if (uniform["name"].AsString() != Parameter) continue;
                ShaderValues<TValue>.ValidateType((Variant.Type)uniform["type"].AsInt32());
                return;
            }
        }
        throw new ArgumentException($"Shader has no material uniform named '{Parameter}'.", nameof(Parameter));
    }
    protected override TValue Read(ShaderMaterial target)
    {
        watch!.Validate(target.Shader);
        using var value = target.GetShaderParameter(name!);
        // Only the first read captures whether the material had an override; later reads see this tween's writes.
        hadOverride ??= value.VariantType != Variant.Type.Nil;
        if (value.VariantType != Variant.Type.Nil) return ShaderValues<TValue>.Read(value);
        using var defaultValue = RenderingServer.ShaderGetParameterDefault(target.Shader.GetRid(), name!);
        return ShaderValues<TValue>.Read(defaultValue);
    }
    protected override void Write(ShaderMaterial target, TValue value)
    {
        watch!.Validate(target.Shader);
        using var variant = ShaderValues<TValue>.Write(value);
        target.SetShaderParameter(name!, variant);
    }
    protected override void Restore(ShaderMaterial target, TValue initial)
    {
        watch!.Validate(target.Shader);
        if (hadOverride == true) Write(target, initial);
        else target.SetShaderParameter(name!, default);
    }
    protected override TValue Interpolate(TValue from, TValue to, float weight)
        => UsesColorInterpolation ? InterpolateColor(from, to, weight) : ShaderValues<TValue>.Interpolate(from, to, weight);
    protected override void Release()
    {
        watch?.Dispose(); watch = null;
        name?.Dispose(); name = null;
    }
}
