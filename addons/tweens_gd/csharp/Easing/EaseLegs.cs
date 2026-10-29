// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
namespace tweens.gd;

/// <summary>In curves. Combine one In and one Out with |; a single curve runs on its own.</summary>
public static class In
{
    /// <summary>Omit this leg; the other curve runs over the entire duration.</summary>
    public const EaseType None = 0;
    /// <summary>Constant velocity.</summary>
    public const EaseType Linear = (EaseType)(1L << 8);
    /// <summary>Sinusoidal easing.</summary>
    public const EaseType Sine = (EaseType)(1L << 9);
    /// <summary>Quadratic easing.</summary>
    public const EaseType Quad = (EaseType)(1L << 10);
    /// <summary>Cubic easing.</summary>
    public const EaseType Cubic = (EaseType)(1L << 11);
    /// <summary>Quartic easing.</summary>
    public const EaseType Quart = (EaseType)(1L << 12);
    /// <summary>Quintic easing.</summary>
    public const EaseType Quint = (EaseType)(1L << 13);
    /// <summary>Exponential easing.</summary>
    public const EaseType Expo = (EaseType)(1L << 14);
    /// <summary>Circular easing.</summary>
    public const EaseType Circ = (EaseType)(1L << 15);
    /// <summary>Alias for the default 30% Back curve.</summary>
    public const EaseType Back = Back30;
    /// <summary>Alias for the default 30% Elastic curve.</summary>
    public const EaseType Elastic = Elastic30;
    /// <summary>Alias for the default 30% Bounce curve.</summary>
    public const EaseType Bounce = Bounce30;
    /// <summary>Cubic smoothing with zero endpoint velocity.</summary>
    public const EaseType SmoothStep = (EaseType)(1L << 19);
    /// <summary>Quintic smoothing with zero endpoint velocity and acceleration.</summary>
    public const EaseType SmootherStep = (EaseType)(1L << 20);
    /// <summary>Back with 10% overshoot of the full tween range.</summary>
    public const EaseType Back10 = (EaseType)(1L << 16);
    /// <summary>Elastic with 10% overshoot of the full tween range.</summary>
    public const EaseType Elastic10 = (EaseType)(1L << 17);
    /// <summary>Back with 20% overshoot of the full tween range.</summary>
    public const EaseType Back20 = (EaseType)(1L << 34);
    /// <summary>Back with 30% overshoot of the full tween range.</summary>
    public const EaseType Back30 = (EaseType)(1L << 35);
    /// <summary>Back with 40% overshoot of the full tween range.</summary>
    public const EaseType Back40 = (EaseType)(1L << 36);
    /// <summary>Back with 50% overshoot of the full tween range.</summary>
    public const EaseType Back50 = (EaseType)(1L << 37);
    /// <summary>Elastic with 20% overshoot of the full tween range.</summary>
    public const EaseType Elastic20 = (EaseType)(1L << 38);
    /// <summary>Elastic with 30% overshoot of the full tween range.</summary>
    public const EaseType Elastic30 = (EaseType)(1L << 39);
    /// <summary>Elastic with 40% overshoot of the full tween range.</summary>
    public const EaseType Elastic40 = (EaseType)(1L << 40);
    /// <summary>Elastic with 50% overshoot of the full tween range.</summary>
    public const EaseType Elastic50 = (EaseType)(1L << 41);
    /// <summary>Bounce with a first rebound depth of 10% of the full tween range.</summary>
    public const EaseType Bounce10 = (EaseType)(1L << 18);
    /// <summary>Bounce with a first rebound depth of 20% of the full tween range.</summary>
    public const EaseType Bounce20 = (EaseType)(1L << 50);
    /// <summary>Bounce with 30% first rebound depth of the full tween range.</summary>
    public const EaseType Bounce30 = (EaseType)(1L << 51);
    /// <summary>Bounce with a first rebound depth of 40% of the full tween range.</summary>
    public const EaseType Bounce40 = (EaseType)(1L << 52);
    /// <summary>Bounce with 50% first rebound depth of the full tween range.</summary>
    public const EaseType Bounce50 = (EaseType)(1L << 53);
    /// <summary>Alias for the default 30% Jump curve.</summary>
    public const EaseType Jump = Jump30;
    /// <summary>Jump with 10% overshoot of the full tween range.</summary>
    public const EaseType Jump10 = (EaseType)((1L << 58) | (1L << 59));
    /// <summary>Jump with 20% overshoot of the full tween range.</summary>
    public const EaseType Jump20 = (EaseType)((1L << 58) | (1L << 60));
    /// <summary>Jump with 30% overshoot of the full tween range.</summary>
    public const EaseType Jump30 = (EaseType)((1L << 59) | (1L << 60));
    /// <summary>Jump with 40% overshoot of the full tween range.</summary>
    public const EaseType Jump40 = (EaseType)((1L << 58) | (1L << 61));
    /// <summary>Jump with 50% overshoot of the full tween range.</summary>
    public const EaseType Jump50 = (EaseType)((1L << 59) | (1L << 61));
}

/// <summary>Out curves. Combine one In and one Out with |; a single curve runs on its own.</summary>
public static class Out
{
    /// <summary>Omit this leg; the other curve runs over the entire duration.</summary>
    public const EaseType None = 0;
    /// <summary>Constant velocity.</summary>
    public const EaseType Linear = (EaseType)(1L << 21);
    /// <summary>Sinusoidal easing.</summary>
    public const EaseType Sine = (EaseType)(1L << 22);
    /// <summary>Quadratic easing.</summary>
    public const EaseType Quad = (EaseType)(1L << 23);
    /// <summary>Cubic easing.</summary>
    public const EaseType Cubic = (EaseType)(1L << 24);
    /// <summary>Quartic easing.</summary>
    public const EaseType Quart = (EaseType)(1L << 25);
    /// <summary>Quintic easing.</summary>
    public const EaseType Quint = (EaseType)(1L << 26);
    /// <summary>Exponential easing.</summary>
    public const EaseType Expo = (EaseType)(1L << 27);
    /// <summary>Circular easing.</summary>
    public const EaseType Circ = (EaseType)(1L << 28);
    /// <summary>Alias for the default 30% Back curve.</summary>
    public const EaseType Back = Back30;
    /// <summary>Alias for the default 30% Elastic curve.</summary>
    public const EaseType Elastic = Elastic30;
    /// <summary>Alias for the default 30% Bounce curve.</summary>
    public const EaseType Bounce = Bounce30;
    /// <summary>Cubic smoothing with zero endpoint velocity.</summary>
    public const EaseType SmoothStep = (EaseType)(1L << 32);
    /// <summary>Quintic smoothing with zero endpoint velocity and acceleration.</summary>
    public const EaseType SmootherStep = (EaseType)(1L << 33);
    /// <summary>Back with 10% overshoot of the full tween range.</summary>
    public const EaseType Back10 = (EaseType)(1L << 29);
    /// <summary>Elastic with 10% overshoot of the full tween range.</summary>
    public const EaseType Elastic10 = (EaseType)(1L << 30);
    /// <summary>Back with 20% overshoot of the full tween range.</summary>
    public const EaseType Back20 = (EaseType)(1L << 42);
    /// <summary>Back with 30% overshoot of the full tween range.</summary>
    public const EaseType Back30 = (EaseType)(1L << 43);
    /// <summary>Back with 40% overshoot of the full tween range.</summary>
    public const EaseType Back40 = (EaseType)(1L << 44);
    /// <summary>Back with 50% overshoot of the full tween range.</summary>
    public const EaseType Back50 = (EaseType)(1L << 45);
    /// <summary>Elastic with 20% overshoot of the full tween range.</summary>
    public const EaseType Elastic20 = (EaseType)(1L << 46);
    /// <summary>Elastic with 30% overshoot of the full tween range.</summary>
    public const EaseType Elastic30 = (EaseType)(1L << 47);
    /// <summary>Elastic with 40% overshoot of the full tween range.</summary>
    public const EaseType Elastic40 = (EaseType)(1L << 48);
    /// <summary>Elastic with 50% overshoot of the full tween range.</summary>
    public const EaseType Elastic50 = (EaseType)(1L << 49);
    /// <summary>Bounce with a first rebound depth of 10% of the full tween range.</summary>
    public const EaseType Bounce10 = (EaseType)(1L << 31);
    /// <summary>Bounce with a first rebound depth of 20% of the full tween range.</summary>
    public const EaseType Bounce20 = (EaseType)(1L << 54);
    /// <summary>Bounce with 30% first rebound depth of the full tween range.</summary>
    public const EaseType Bounce30 = (EaseType)(1L << 55);
    /// <summary>Bounce with a first rebound depth of 40% of the full tween range.</summary>
    public const EaseType Bounce40 = (EaseType)(1L << 56);
    /// <summary>Bounce with 50% first rebound depth of the full tween range.</summary>
    public const EaseType Bounce50 = (EaseType)(1L << 57);
    /// <summary>Alias for the default 30% Jump curve.</summary>
    public const EaseType Jump = Jump30;
    /// <summary>Jump with 10% overshoot of the full tween range.</summary>
    public const EaseType Jump10 = (EaseType)((1L << 62) | (1L << 63));
    /// <summary>Jump with 20% overshoot of the full tween range.</summary>
    public const EaseType Jump20 = (EaseType)((1L << 62) | (1L << 6));
    /// <summary>Jump with 30% overshoot of the full tween range.</summary>
    public const EaseType Jump30 = (EaseType)((1L << 63) | (1L << 6));
    /// <summary>Jump with 40% overshoot of the full tween range.</summary>
    public const EaseType Jump40 = (EaseType)((1L << 62) | (1L << 7));
    /// <summary>Jump with 50% overshoot of the full tween range.</summary>
    public const EaseType Jump50 = (EaseType)((1L << 63) | (1L << 7));
}

/// <summary>Matching half-duration legs. Back/Elastic/Jump overshoot and Bounce first-rebound depth are percentages of the full tween range.</summary>
public static class InOut
{
    /// <summary>Constant velocity.</summary>
    public const EaseType Linear = In.Linear | Out.Linear;
    /// <summary>Sinusoidal easing.</summary>
    public const EaseType Sine = In.Sine | Out.Sine;
    /// <summary>Quadratic easing.</summary>
    public const EaseType Quad = In.Quad | Out.Quad;
    /// <summary>Cubic easing.</summary>
    public const EaseType Cubic = In.Cubic | Out.Cubic;
    /// <summary>Quartic easing.</summary>
    public const EaseType Quart = In.Quart | Out.Quart;
    /// <summary>Quintic easing.</summary>
    public const EaseType Quint = In.Quint | Out.Quint;
    /// <summary>Exponential easing.</summary>
    public const EaseType Expo = In.Expo | Out.Expo;
    /// <summary>Circular easing.</summary>
    public const EaseType Circ = In.Circ | Out.Circ;
    /// <summary>Alias for the default 30% Back curve.</summary>
    public const EaseType Back = Back30;
    /// <summary>Alias for the default 30% Elastic curve.</summary>
    public const EaseType Elastic = Elastic30;
    /// <summary>Alias for the default 30% Bounce curve.</summary>
    public const EaseType Bounce = Bounce30;
    /// <summary>Cubic smoothing with zero endpoint velocity.</summary>
    public const EaseType SmoothStep = In.SmoothStep | Out.SmoothStep;
    /// <summary>Quintic smoothing with zero endpoint velocity and acceleration.</summary>
    public const EaseType SmootherStep = In.SmootherStep | Out.SmootherStep;
    /// <summary>Back with 10% overshoot of the full tween range.</summary>
    public const EaseType Back10 = In.Back10 | Out.Back10;
    /// <summary>Elastic with 10% overshoot of the full tween range.</summary>
    public const EaseType Elastic10 = In.Elastic10 | Out.Elastic10;
    /// <summary>Back with 20% overshoot of the full tween range.</summary>
    public const EaseType Back20 = In.Back20 | Out.Back20;
    /// <summary>Back with 30% overshoot of the full tween range.</summary>
    public const EaseType Back30 = In.Back30 | Out.Back30;
    /// <summary>Back with 40% overshoot of the full tween range.</summary>
    public const EaseType Back40 = In.Back40 | Out.Back40;
    /// <summary>Back with 50% overshoot of the full tween range.</summary>
    public const EaseType Back50 = In.Back50 | Out.Back50;
    /// <summary>Elastic with 20% overshoot of the full tween range.</summary>
    public const EaseType Elastic20 = In.Elastic20 | Out.Elastic20;
    /// <summary>Elastic with 30% overshoot of the full tween range.</summary>
    public const EaseType Elastic30 = In.Elastic30 | Out.Elastic30;
    /// <summary>Elastic with 40% overshoot of the full tween range.</summary>
    public const EaseType Elastic40 = In.Elastic40 | Out.Elastic40;
    /// <summary>Elastic with 50% overshoot of the full tween range.</summary>
    public const EaseType Elastic50 = In.Elastic50 | Out.Elastic50;
    /// <summary>Bounce with a first rebound depth of 10% of the full tween range.</summary>
    public const EaseType Bounce10 = In.Bounce10 | Out.Bounce10;
    /// <summary>Bounce with a first rebound depth of 20% of the full tween range.</summary>
    public const EaseType Bounce20 = In.Bounce20 | Out.Bounce20;
    /// <summary>Bounce with 30% first rebound depth of the full tween range.</summary>
    public const EaseType Bounce30 = In.Bounce30 | Out.Bounce30;
    /// <summary>Bounce with a first rebound depth of 40% of the full tween range.</summary>
    public const EaseType Bounce40 = In.Bounce40 | Out.Bounce40;
    /// <summary>Bounce with 50% first rebound depth of the full tween range.</summary>
    public const EaseType Bounce50 = In.Bounce50 | Out.Bounce50;
    /// <summary>Alias for the default 30% Jump curve.</summary>
    public const EaseType Jump = Jump30;
    /// <summary>Jump with 10% overshoot of the full tween range.</summary>
    public const EaseType Jump10 = In.Jump10 | Out.Jump10;
    /// <summary>Jump with 20% overshoot of the full tween range.</summary>
    public const EaseType Jump20 = In.Jump20 | Out.Jump20;
    /// <summary>Jump with 30% overshoot of the full tween range.</summary>
    public const EaseType Jump30 = In.Jump30 | Out.Jump30;
    /// <summary>Jump with 40% overshoot of the full tween range.</summary>
    public const EaseType Jump40 = In.Jump40 | Out.Jump40;
    /// <summary>Jump with 50% overshoot of the full tween range.</summary>
    public const EaseType Jump50 = In.Jump50 | Out.Jump50;
}

