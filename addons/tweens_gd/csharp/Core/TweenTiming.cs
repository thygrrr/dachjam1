// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;

namespace tweens.gd;

/// <summary>Immutable clock and easing settings, independent of storage and value policy.</summary>
public readonly record struct TweenTiming
{
    /// <summary>A <see cref="Repeats"/> value that repeats until cancelled.</summary>
    public const int Infinite = -1;

    /// <summary>Duration of one leg, in seconds. Defaults to zero; must be finite and nonnegative.</summary>
    public Duration Duration { get; init; }
    private readonly double? factorDuration;
    /// <summary>Scales <see cref="Duration"/> at start: the tween lasts FactorDuration * Duration + DeltaDuration.</summary>
    public double FactorDuration { get => factorDuration ?? 1; init => factorDuration = value == 1 ? null : value; }
    /// <summary>Added to <see cref="Duration"/> at start, after <see cref="FactorDuration"/>.</summary>
    public Duration DeltaDuration { get; init; }
    /// <summary>Signed gap before the first leg. Positive values wait after capture; negative values overlap or pre-roll.</summary>
    public Duration Delay { get; init; }
    private readonly double? factorDelay;
    /// <summary>Scales the signed gap at start: FactorDelay * Delay + DeltaDelay.</summary>
    public double FactorDelay { get => factorDelay ?? 1; init => factorDelay = value == 1 ? null : value; }
    /// <summary>Added to <see cref="Delay"/> at start, after <see cref="FactorDelay"/>.</summary>
    public Duration DeltaDelay { get; init; }
    /// <summary>Hold between the forward and return legs, in seconds. Defaults to zero.</summary>
    public Duration PingPongInterval { get; init; }
    /// <summary>Hold between cycles, in seconds. No hold follows the final cycle.</summary>
    public Duration RepeatInterval { get; init; }
    /// <summary>Start this many seconds into the first leg, after the delay. Must not exceed the adjusted duration.</summary>
    public Duration Offset { get; init; }
    /// <summary>Cycles after the first, or <see cref="Infinite"/>. A ping-pong cycle includes both legs.</summary>
    public int Repeats { get; init; }
    /// <summary>Return to the start after each forward leg. Duration applies to each leg; defaults to false.</summary>
    public bool PingPong { get; init; }
    private readonly FillMode fill;
    // Encode the default so default(TweenOptions) and new TweenOptions() behave identically.
    /// <summary>Value behavior during delay and after natural completion. Defaults to RetainFinalValue.</summary>
    public FillMode Fill { get => fill ^ FillMode.RetainFinalValue; init => fill = value ^ FillMode.RetainFinalValue; }
    /// <summary>Combine one In and one Out with |, use an InOut pair, or select a single curve. Legacy EaseType names retain their original shapes.</summary>
    public EaseType Ease { get; init; }
    /// <summary>How mixed In/Out legs join. Defaults to Makima; matching families keep their conventional shape.</summary>
    public BlendType BlendType { get; init; }
    private readonly double? blend;
    /// <summary>Centered transition width in [0, 1]. Defaults to 0.1 (45%–55% at the neutral split); zero directly splices the halves.</summary>
    public double Blend { get => blend ?? Easing.DefaultBlend; init => blend = value == Easing.DefaultBlend ? null : value; }
    private readonly double? skew;
    /// <summary>In/Out split in [0, 1]: 0 selects Out, 0.5 is balanced, 1 selects In. Applies to paired easing flags.</summary>
    public double Skew { get => skew ?? 0.5; init => skew = value == 0.5 ? null : value; }
    private readonly double? weks;
    /// <summary>Independent ping-pong return split in [0, 1]: 0 front-loads the return, 0.5 is balanced, 1 rear-loads it.</summary>
    public double Weks { get => weks ?? 0.5; init => weks = value == 0.5 ? null : value; }
    /// <summary>Custom progress-to-weight function instead of Ease. Mutually exclusive with Curve.</summary>
    public Func<float, float>? EaseFunction { get; init; }
    /// <summary>Custom progress-to-weight curve instead of Ease. Copied per playback; mutually exclusive with EaseFunction.</summary>
    public Godot.Curve? Curve { get; init; }
}
