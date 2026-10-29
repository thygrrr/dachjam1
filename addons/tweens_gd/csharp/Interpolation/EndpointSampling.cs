// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using Godot;

namespace tweens.gd;

// Per-playback working values. Storage bindings and the clock never perform color conversion.
internal struct EndpointSampling<TValue> where TValue : struct
{
    private Vector4 colorFrom, colorTo;
    private ColorPolicy policy;
    internal bool IsOffset;

    internal void PrepareColor(TValue from, TValue to, TweenOptions options)
    {
        policy = options.ColorPolicy;
        colorFrom = policy.Encode((Color)(object)from);
        colorTo = policy.Encode((Color)(object)to);
    }

    internal TValue Color(TValue from, TValue to, float weight)
    {
        if (IsOffset) return (TValue)(object)((Color)(object)from).Lerp((Color)(object)to, weight);
        if (weight == 0) return from;
        if (weight == 1) return to;
        return (TValue)(object)policy.Decode(colorFrom.Lerp(colorTo, weight));
    }
}
