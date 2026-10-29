// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using Godot;

namespace tweens.gd;

// Owns progress shaping only. Value sampling and storage access are independent.
internal struct TweenProgress : IDisposable
{
    private Func<float, float>? forward, reverse;
    private Curve? curve;

    internal TweenProgress(TweenTiming timing)
    {
        if (timing.Curve is not null && timing.EaseFunction is not null)
            throw new ArgumentException("Specify either Curve or EaseFunction, not both.");
        forward = timing.EaseFunction ?? Easing.GetFunction(timing.Ease, timing.BlendType, timing.Blend, timing.Skew);
        reverse = timing.EaseFunction ?? Easing.GetFunction(timing.Ease, timing.BlendType, timing.Blend, 1 - timing.Weks);
        curve = null;
        if (timing.Curve is not null)
        {
            curve = (Curve)timing.Curve.Duplicate();
            forward = reverse = curve.Sample;
        }
    }

    internal float Sample(float progress, bool returning)
    {
        var weight = (returning ? reverse! : forward!)(Math.Clamp(progress, 0, 1));
        if (!float.IsFinite(weight)) throw new InvalidOperationException("Easing returned a non-finite value.");
        return weight;
    }

    public void Dispose()
    {
        forward = reverse = null;
        curve?.Dispose();
        curve = null;
    }
}
