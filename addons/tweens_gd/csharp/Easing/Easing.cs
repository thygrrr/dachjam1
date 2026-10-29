// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss
// SPDX-FileCopyrightText: 2020 Jeffrey Lanters

// Easing math adapted from unity-tweens. See THIRD-PARTY-NOTICES.md.
#nullable enable
using System;
using System.Numerics;
using Godot;

namespace tweens.gd {
  /// <summary>Samples easing curves without starting playback.</summary>
  public static class Easing {
    /// <summary>Default centered join width, shared by TweenOptions, TweenOptionsBuilder, and Evaluate.</summary>
    internal const double DefaultBlend = 0.1;

    /// <summary>Samples a curve at progress clamped to [0, 1]. Eased weight may overshoot.</summary>
    /// <remarks>Skew moves the paired In/Out split in [0, 1]; 0.5 preserves the authored pair.
    /// Blend is the transition width around that split; zero directly splices the legs.
    /// In GDScript, use Tweens.Easing.evaluate(ease, progress, blend_type, blend, skew).</remarks>
    public static float Evaluate(EaseType ease, float progress, BlendType blendType = BlendType.Makima, double blend = DefaultBlend, double skew = 0.5)
      => GetFunction(ease, blendType, blend, skew)(Math.Clamp(progress, 0, 1));

    const float ConstantA = 1.70158f;
    const float ConstantB = ConstantA * 1.525f;
    const float ConstantC = ConstantA + 1f;
    const float ConstantD = 2f * Mathf.Pi / 3f;
    const float ConstantE = 2f * Mathf.Pi / 4.5f;
    const float ConstantF = 7.5625f;
    const float ConstantG = 2.75f;

    // Original one-bit flags keep their values. Jump uses two-of-four codes in
    // bits 58-61 (In) and 62/63/6/7 (Out); OR-ing different codes is invalid.
    // Every nonzero legacy enum contains a bit in 0-5, which compositions exclude.
    const long LegMask = (1L << 13) - 1;
    const int CurveCount = 30;
    const long CompositionMask = ~63L;
    static ulong LegBits(long bits, bool exit) {
      var curves = (ulong)(((bits >> (exit ? 21 : 8)) & LegMask)
        | (((bits >> (exit ? 42 : 34)) & 255) << 13)
        | (((bits >> (exit ? 54 : 50)) & 15) << 21));
      var jump = exit ? ((bits >> 62) & 3) | ((bits >> 4) & 12) : (bits >> 58) & 15;
      return curves | (jump switch {
        0 => 0UL, 3 => 1UL << 25, 5 => 1UL << 26, 6 => 1UL << 27,
        9 => 1UL << 28, 10 => 1UL << 29,
        _ => 3UL << 30, // Invalid codes fail the single-curve check below.
      });
    }

    // Calibrated for 10%, 20%, ... 50% of the full range. Paired legs have half the
    // value range, so their local peak must be twice the requested overshoot.
    // Back: peak = 4*s^3 / (27*(s+1)^2).
    static readonly float[] BackSolo = [1.701540198866824f, 2.5923889015162995f, 3.3940516581445603f, 4.155744652639195f, 4.894859521133737f];
    static readonly float[] BackPaired = [2.5923889015162995f, 4.155744652639195f, 5.619622918334311f, 7.042439379340937f, 8.44353560159325f];
    const float ElasticSoloDecay = 13;
    // Solo damping relaxes after the main swing, so the default curve rests at about 90% of its duration.
    // Paired legs run at half their former frequency.
    // Reproduce the calibrated peaks with scripts/calibrate-elastic-easing.mjs.
    const float ElasticSoloTail = 6;
    const float ElasticPairDecay = 7.537490798899971f;
    const float ElasticSoloPeriod = 0.58f;
    const float ElasticPairPeriod = 0.6732985463200285f;
    static readonly float[] ElasticSoloKick = [-0.4459853763131816f, 0.5308279022336841f, 0.9602276834507211f, 1.3110242679337114f, 1.6280394875512134f];
    static readonly float[] ElasticPairKick = [0.054242444203084675f, 0.9078807808396336f, 1.4611442537053763f, 1.9533326533438204f, 2.419656309841953f];
    static readonly float[] BounceSoloRoot = [Mathf.Sqrt(0.1f), Mathf.Sqrt(0.2f), Mathf.Sqrt(0.3f), Mathf.Sqrt(0.4f), Mathf.Sqrt(0.5f)];
    static readonly float[] BouncePairRoot = [Mathf.Sqrt(0.2f), Mathf.Sqrt(0.4f), Mathf.Sqrt(0.6f), Mathf.Sqrt(0.8f), 1];
    static readonly float[] JumpSoloLaunch = [Mathf.Sqrt(1.1f), Mathf.Sqrt(1.2f), Mathf.Sqrt(1.3f), Mathf.Sqrt(1.4f), Mathf.Sqrt(1.5f)];
    static readonly float[] JumpPairLaunch = [Mathf.Sqrt(1.2f), Mathf.Sqrt(1.4f), Mathf.Sqrt(1.6f), Mathf.Sqrt(1.8f), Mathf.Sqrt(2)];
    static readonly Func<float, float>[] InCurves =
    [
      Linear, SineIn, QuadIn, CubicIn, QuartIn, QuintIn, ExpoIn, CircIn,
      t => BackLegIn(t, 0), t => ElasticLegIn(t, 0), t => BounceLegIn(t, 0), SmoothStep, SmootherStep,
      t => BackLegIn(t, 1), t => BackLegIn(t, 2), t => BackLegIn(t, 3), t => BackLegIn(t, 4),
      t => ElasticLegIn(t, 1), t => ElasticLegIn(t, 2), t => ElasticLegIn(t, 3), t => ElasticLegIn(t, 4),
      t => BounceLegIn(t, 1), t => BounceLegIn(t, 2), t => BounceLegIn(t, 3), t => BounceLegIn(t, 4),
      t => JumpLegIn(t, 0), t => JumpLegIn(t, 1), t => JumpLegIn(t, 2), t => JumpLegIn(t, 3), t => JumpLegIn(t, 4)
    ];
    static readonly Func<float, float>[] OutCurves =
    [
      Linear, SineOut, QuadOut, CubicOut, QuartOut, QuintOut, ExpoOut, CircOut,
      t => BackLegOut(t, 0), t => ElasticLegOut(t, 0), t => BounceLegOut(t, 0), SmoothStep, SmootherStep,
      t => BackLegOut(t, 1), t => BackLegOut(t, 2), t => BackLegOut(t, 3), t => BackLegOut(t, 4),
      t => ElasticLegOut(t, 1), t => ElasticLegOut(t, 2), t => ElasticLegOut(t, 3), t => ElasticLegOut(t, 4),
      t => BounceLegOut(t, 1), t => BounceLegOut(t, 2), t => BounceLegOut(t, 3), t => BounceLegOut(t, 4),
      t => JumpLegOut(t, 0), t => JumpLegOut(t, 1), t => JumpLegOut(t, 2), t => JumpLegOut(t, 3), t => JumpLegOut(t, 4)
    ];
    static readonly Func<float, float>[] InOutCurves =
    [
      Linear, SineInOut, QuadInOut, CubicInOut, QuartInOut, QuintInOut, ExpoInOut, CircInOut,
      t => BackPair(t, 0), t => ElasticPair(t, 0), t => BouncePair(t, 0), SmoothStep, SmootherStep,
      t => BackPair(t, 1), t => BackPair(t, 2), t => BackPair(t, 3), t => BackPair(t, 4),
      t => ElasticPair(t, 1), t => ElasticPair(t, 2), t => ElasticPair(t, 3), t => ElasticPair(t, 4),
      t => BouncePair(t, 1), t => BouncePair(t, 2), t => BouncePair(t, 3), t => BouncePair(t, 4),
      t => JumpPair(t, 0), t => JumpPair(t, 1), t => JumpPair(t, 2), t => JumpPair(t, 3), t => JumpPair(t, 4)
    ];
    static readonly Func<float, float>[,] Compositions = CreateCompositions();

    static Func<float, float>[,] CreateCompositions() {
      var functions = new Func<float, float>[CurveCount, CurveCount];
      for (var i = 0; i < CurveCount; i++)
        for (var o = 0; o < CurveCount; o++) {
          functions[i, o] = Compose(i, o, BlendType.Makima, (float)DefaultBlend);
        }
      return functions;
    }

    // Analytic derivative of the paired first half (the second half is symmetric).
    static float PairSlope(int family, float time) {
      var t = Math.Min(time, 1 - time);
      var x = 2 * t;
      return family switch {
        0 => 1,
        1 => Mathf.Pi * Mathf.Sin(Mathf.Pi * t) / 2,
        2 => 4 * t,
        3 => 12 * t * t,
        4 => 32 * t * t * t,
        5 => 80 * t * t * t * t,
        6 => 10 * Mathf.Log(2) * Mathf.Pow(2, 20 * t - 10),
        7 => x / Mathf.Sqrt(1 - x * x),
        8 or >= 13 and <= 16 => BackSlope(x, family == 8 ? 0 : family - 12),
        9 or >= 17 and <= 20 => ElasticSlope(1 - x, family == 9 ? 0 : family - 16),
        10 or >= 21 and <= 24 => BounceSlope(1 - x, family == 10 ? 0 : family - 20),
        >= 25 and <= 29 => JumpSlope(1 - x, family - 25),
        11 => 6 * t * (1 - t),
        _ => 30 * t * t * (1 - t) * (1 - t),
      };
    }

    static float BounceSlope(float t, int level) {
      var r = BouncePairRoot[level];
      var scale = 1 + 3.5f * r;
      var u = t * scale;
      if (u >= 1 + 3 * r) u -= 1 + 3.25f * r;
      else if (u >= 1 + 2 * r) u -= 1 + 2.5f * r;
      else if (u >= 1) u -= 1 + r;
      return 2 * scale * u;
    }

    static float Hermite(float u, float y0, float y1, float m0, float m1) {
      var u2 = u * u;
      var u3 = u2 * u;
      return (2 * u3 - 3 * u2 + 1) * y0 + (u3 - 2 * u2 + u) * m0
        + (-2 * u3 + 3 * u2) * y1 + (u3 - u2) * m1;
    }

    // Modified Akima (makima) slope at the midpoint from the four surrounding slopes. The legs' edge
    // velocities stand in for the outer secants. Each half stays on its side of 0.5, so the weights never both vanish.
    static float MakimaSlope(float s0, float s1, float s2, float s3) {
      var w1 = Math.Abs(s3 - s2) + Math.Abs(s3 + s2) / 2;
      var w2 = Math.Abs(s1 - s0) + Math.Abs(s1 + s0) / 2;
      return (w1 * s1 + w2 * s2) / (w1 + w2);
    }

    static Func<float, float> SplitProfile(int family, float split) {
      var pair = InOutCurves[family];
      var entry = InCurves[family];
      var exit = OutCurves[family];
      if (split == 0) return exit;
      if (split == 1) return entry;
      if (split == 0.5f) return pair;
      return t => {
        if (t == 0 || t == 1) return t;
        if (t <= split) {
          var x = t / split;
          var solo = Math.Max(0, 2 * split - 1);
          return split * ((1 - solo) * 2 * pair(x / 2) + solo * entry(x));
        }
        var span = 1 - split;
        var u = (t - split) / span;
        var mix = Math.Max(0, 2 * span - 1);
        return split + span * ((1 - mix) * (2 * pair(0.5f + u / 2) - 1) + mix * exit(u));
      };
    }

    static float SoloSlope(int family, float t, bool exit) {
      if (exit) t = 1 - t;
      if (family == 11) return 6 * t * (1 - t);
      if (family == 12) return 30 * t * t * (1 - t) * (1 - t);
      if (family == 1) return Mathf.Pi / 2 * Mathf.Sin(Mathf.Pi / 2 * t);
      if (family == 6) return 10 * Mathf.Log(2) * Mathf.Pow(2, 10 * t - 10);
      if (family == 7) return t / Mathf.Sqrt(1 - t * t);
      if (family is 8 or >= 13 and <= 16) {
        var s = BackSolo[family == 8 ? 0 : family - 12];
        return 3 * (s + 1) * t * t - 2 * s * t;
      }
      if (family is 9 or >= 17 and <= 20) {
        var u = 1 - t;
        var omega = Mathf.Tau / ElasticSoloPeriod;
        var decay = (ElasticSoloDecay - 2 * ElasticSoloTail * u) * Mathf.Log(2);
        var kick = ElasticSoloKick[family == 9 ? 0 : family - 16];
        return ElasticScale(kick, false) * Mathf.Pow(2, -ElasticSoloDecay * u + ElasticSoloTail * u * u)
          * ((decay + kick * omega) * Mathf.Cos(omega * u) + (omega - kick * decay) * Mathf.Sin(omega * u));
      }
      if (family is 10 or >= 21 and <= 29) {
        var jump = family >= 25;
        var level = jump ? family - 25 : family == 10 ? 0 : family - 20;
        var r = BounceSoloRoot[level];
        var a = jump ? JumpSoloLaunch[level] : 1;
        var scale = jump ? a + 2.5f * r : 1 + 3.5f * r;
        var u = (1 - t) * scale;
        if (jump) return 2 * scale * ((u < a + r ? a : u < a + 2 * r ? a + 1.5f * r : a + 2.25f * r) - u);
        if (u >= 1 + 3 * r) u -= 1 + 3.25f * r;
        else if (u >= 1 + 2 * r) u -= 1 + 2.5f * r;
        else if (u >= 1) u -= 1 + r;
        return 2 * scale * u;
      }
      return family is >= 2 and <= 5 ? family * Mathf.Pow(t, family - 1) : 1;
    }

    static float SplitSlope(int family, float t, float split) {
      if (split == 0.5f) return PairSlope(family, t);
      var exit = t > split;
      var span = exit ? 1 - split : split;
      var x = exit ? (t - split) / span : t / span;
      var mix = Math.Max(0, 2 * span - 1);
      return (1 - mix) * PairSlope(family, exit ? 0.5f + x / 2 : x / 2) + mix * SoloSlope(family, x, exit);
    }

    static Func<float, float> Compose(int i, int o, BlendType method, float blend, float split = 0.5f) {
      if (split == 0) return OutCurves[o];
      if (split == 1) return InCurves[i];
      var entry = SplitProfile(i, split);
      var exit = SplitProfile(o, split);
      if (i == o) return entry;
      var h = blend * Math.Min(split, 1 - split);
      var left = split - h;
      var right = split + h;
      if (left == split) return t => t <= split ? entry(t) : exit(t);
      var y0 = entry(left);
      var y1 = exit(right);
      var v0 = SplitSlope(i, left, split);
      var v1 = SplitSlope(o, right, split);
      var d0 = (split - y0) / h;
      var d1 = (y1 - split) / h;
      // Outer tangents stay exact. Hermite solves for equal acceleration at the midpoint, then limits
      // the shared tangent to avoid introducing reversals in monotone legs.
      var middle = method == BlendType.Makima ? MakimaSlope(v0, d0, d1, v1)
        : Math.Clamp((3 * (d0 + d1) - v0 - v1) / 4, 0, 3 * Math.Max(0, Math.Min(d0, d1)));
      return t => {
        if (t <= left) return entry(t);
        if (t >= right) return exit(t);
        if (method is BlendType.Makima or BlendType.Hermite)
          return t <= split
            ? Hermite((t - left) / h, y0, split, h * v0, h * middle)
            : Hermite((t - split) / h, split, y1, h * middle, h * v1);
        var u = (t - left) / (2 * h);
        var weight = method == BlendType.SmoothStep ? SmoothStep(u) : u;
        return entry(t) * (1 - weight) + exit(t) * weight;
      };
    }

    internal static void ValidateBlend(BlendType blendType, double blend) {
      if (!Enum.IsDefined(blendType)) throw new ArgumentOutOfRangeException(nameof(blendType));
      if (!double.IsFinite(blend) || blend < 0 || blend > 1) throw new ArgumentOutOfRangeException(nameof(blend));
    }

    internal static Func<float, float> GetFunction(EaseType easeType, BlendType blendType = BlendType.Makima, double blend = DefaultBlend, double skew = 0.5) {
      ValidateBlend(blendType, blend);
      if (!double.IsFinite(skew) || skew < 0 || skew > 1) throw new ArgumentOutOfRangeException(nameof(skew));
      var bits = (long)easeType;
      if (bits != 0 && (bits & ~CompositionMask) == 0) {
        var entry = LegBits(bits, false);
        var exit = LegBits(bits, true);
        if ((entry == 0 || BitOperations.IsPow2(entry)) && (exit == 0 || BitOperations.IsPow2(exit))) {
          if (entry == 0) return OutCurves[BitOperations.TrailingZeroCount(exit)];
          if (exit == 0) return InCurves[BitOperations.TrailingZeroCount(entry)];
          var i = BitOperations.TrailingZeroCount(entry);
          var o = BitOperations.TrailingZeroCount(exit);
          return blendType == BlendType.Makima && blend == DefaultBlend && skew == 0.5 ? Compositions[i, o] : Compose(i, o, blendType, (float)blend, (float)skew);
        }
      }
      return easeType switch {
        EaseType.Linear => Linear,
        EaseType.SineIn => SineIn,
        EaseType.SineOut => SineOut,
        EaseType.SineInOut => SineInOut,
        EaseType.QuadIn => QuadIn,
        EaseType.QuadOut => QuadOut,
        EaseType.QuadInOut => QuadInOut,
        EaseType.CubicIn => CubicIn,
        EaseType.CubicOut => CubicOut,
        EaseType.CubicInOut => CubicInOut,
        EaseType.QuartIn => QuartIn,
        EaseType.QuartOut => QuartOut,
        EaseType.QuartInOut => QuartInOut,
        EaseType.QuintIn => QuintIn,
        EaseType.QuintOut => QuintOut,
        EaseType.QuintInOut => QuintInOut,
        EaseType.ExpoIn => ExpoIn,
        EaseType.ExpoOut => ExpoOut,
        EaseType.ExpoInOut => ExpoInOut,
        EaseType.CircIn => CircIn,
        EaseType.CircOut => CircOut,
        EaseType.CircInOut => CircInOut,
        EaseType.BackIn => BackIn,
        EaseType.BackOut => BackOut,
        EaseType.BackInOut => BackInOut,
        EaseType.ElasticIn => ElasticIn,
        EaseType.ElasticOut => ElasticOut,
        EaseType.ElasticInOut => ElasticInOut,
        EaseType.BounceIn => BounceIn,
        EaseType.BounceOut => BounceOut,
        EaseType.BounceInOut => BounceInOut,
        EaseType.SmoothStep => SmoothStep,
        EaseType.SmootherStep => SmootherStep,
        _ => throw new NotImplementedException($"EaseType {easeType} not implemented"),
      };
    }

    static float Linear(float time) {
      return time;
    }

    static float SmoothStep(float time) {
      return time * time * (3f - 2f * time);
    }

    static float SmootherStep(float time) {
      return time * time * time * (time * (6f * time - 15f) + 10f);
    }

    static float SineIn(float time) {
      return 1f - Mathf.Cos((time * Mathf.Pi) / 2f);
    }

    static float SineOut(float time) {
      return Mathf.Sin((time * Mathf.Pi) / 2f);
    }

    static float SineInOut(float time) {
      return -(Mathf.Cos(Mathf.Pi * time) - 1f) / 2f;
    }

    static float QuadIn(float time) {
      return time * time;
    }

    static float QuadOut(float time) {
      return 1 - (1 - time) * (1 - time);
    }

    static float QuadInOut(float time) {
      return time < 0.5f ? 2 * time * time : 1 - Mathf.Pow(-2 * time + 2, 2) / 2;
    }

    static float CubicIn(float time) {
      return time * time * time;
    }

    static float CubicOut(float time) {
      return 1 - Mathf.Pow(1 - time, 3);
    }

    static float CubicInOut(float time) {
      return time < 0.5f ? 4 * time * time * time : 1 - Mathf.Pow(-2 * time + 2, 3) / 2;
    }

    static float QuartIn(float time) {
      return time * time * time * time;
    }

    static float QuartOut(float time) {
      return 1 - Mathf.Pow(1 - time, 4);
    }

    static float QuartInOut(float time) {
      return time < 0.5 ? 8 * time * time * time * time : 1 - Mathf.Pow(-2 * time + 2, 4) / 2;
    }

    static float QuintIn(float time) {
      return time * time * time * time * time;
    }

    static float QuintOut(float time) {
      return 1 - Mathf.Pow(1 - time, 5);
    }

    static float QuintInOut(float time) {
      return time < 0.5f ? 16 * time * time * time * time * time : 1 - Mathf.Pow(-2 * time + 2, 5) / 2;
    }

    static float ExpoIn(float time) {
      return time == 0 ? 0 : Mathf.Pow(2, 10 * time - 10);
    }

    static float ExpoOut(float time) {
      return time == 1 ? 1 : 1 - Mathf.Pow(2, -10 * time);
    }

    static float ExpoInOut(float time) {
      return time == 0 ? 0 : time == 1 ? 1 : time < 0.5 ? Mathf.Pow(2, 20 * time - 10) / 2 : (2 - Mathf.Pow(2, -20 * time + 10)) / 2;
    }

    static float CircIn(float time) {
      return 1 - Mathf.Sqrt(1 - Mathf.Pow(time, 2));
    }

    static float CircOut(float time) {
      return Mathf.Sqrt(1 - Mathf.Pow(time - 1, 2));
    }

    static float CircInOut(float time) {
      return time < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * time, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * time + 2, 2)) + 1) / 2;
    }

    static float BackIn(float time) {
      return ConstantC * time * time * time - ConstantA * time * time;
    }

    static float BackOut(float time) {
      return 1f + ConstantC * Mathf.Pow(time - 1, 3) + ConstantA * Mathf.Pow(time - 1, 2);
    }

    static float BackInOut(float time) {
      return time < 0.5 ?
        Mathf.Pow(2 * time, 2) * ((ConstantB + 1) * 2 * time - ConstantB) / 2 :
        (Mathf.Pow(2 * time - 2, 2) * ((ConstantB + 1) * (time * 2 - 2) + ConstantB) + 2) / 2;
    }

    // Constant acceleration across the initial fall and three rebounds. Rebound
    // depths are h, h/4, h/16, so flight times total 1 + 2*sqrt(h) + sqrt(h) + sqrt(h)/2.
    static float BounceLegOut(float t, int level, bool paired = false) {
      if (t == 0 || t == 1) return t;
      var h = (level + 1) * (paired ? 0.2f : 0.1f);
      var r = (paired ? BouncePairRoot : BounceSoloRoot)[level];
      var u = t * (1 + 3.5f * r);
      if (u < 1) return u * u;
      if (u < 1 + 2 * r) { u -= 1 + r; return 1 - h + u * u; }
      if (u < 1 + 3 * r) { u -= 1 + 2.5f * r; return 1 - h / 4 + u * u; }
      u -= 1 + 3.25f * r;
      return 1 - h / 16 + u * u;
    }

    static float BounceLegIn(float t, int level, bool paired = false) => 1 - BounceLegOut(1 - t, level, paired);
    static float BouncePair(float t, int level) => t < 0.5f ? BounceLegIn(2 * t, level, true) / 2
      : 0.5f + BounceLegOut(2 * t - 1, level, true) / 2;

    // Launch directly into the first overshoot, then make two smaller parabolic
    // hops. A shared acceleration gives peak heights h, h/4, h/16 above the target.
    static float JumpLegOut(float t, int level, bool paired = false) {
      if (t == 0 || t == 1) return t;
      var h = (level + 1) * (paired ? 0.2f : 0.1f);
      var r = (paired ? BouncePairRoot : BounceSoloRoot)[level];
      var a = (paired ? JumpPairLaunch : JumpSoloLaunch)[level];
      var u = t * (a + 2.5f * r);
      if (u < a + r) { u -= a; return 1 + h - u * u; }
      if (u < a + 2 * r) { u -= a + 1.5f * r; return 1 + h / 4 - u * u; }
      u -= a + 2.25f * r;
      return 1 + h / 16 - u * u;
    }

    static float JumpLegIn(float t, int level, bool paired = false) => 1 - JumpLegOut(1 - t, level, paired);
    static float JumpPair(float t, int level) => t < 0.5f ? JumpLegIn(2 * t, level, true) / 2
      : 0.5f + JumpLegOut(2 * t - 1, level, true) / 2;

    static float JumpSlope(float t, int level) {
      var r = BouncePairRoot[level];
      var a = JumpPairLaunch[level];
      var scale = a + 2.5f * r;
      var u = t * scale;
      var center = u < a + r ? a : u < a + 2 * r ? a + 1.5f * r : a + 2.25f * r;
      return 2 * scale * (center - u);
    }

    static float BackLegIn(float t, int level, bool paired = false) {
      if (t == 0 || t == 1) return t;
      var s = (paired ? BackPaired : BackSolo)[level];
      return (s + 1) * t * t * t - s * t * t;
    }

    static float BackLegOut(float t, int level, bool paired = false) => 1 - BackLegIn(1 - t, level, paired);
    static float BackPair(float t, int level) => t < 0.5f ? BackLegIn(2 * t, level, true) / 2
      : 0.5f + BackLegOut(2 * t - 1, level, true) / 2;

    static float BackSlope(float t, int level) {
      var s = BackPaired[level];
      return 3 * (s + 1) * t * t - 2 * s * t;
    }

    // Normalize the damped spring's residual so it reaches the endpoint continuously.
    static float ElasticScale(float kick, bool paired) {
      var omega = Mathf.Tau / (paired ? ElasticPairPeriod : ElasticSoloPeriod);
      var residual = Mathf.Pow(2, -(paired ? ElasticPairDecay : ElasticSoloDecay - ElasticSoloTail))
        * (Mathf.Cos(omega) - kick * Mathf.Sin(omega));
      return 1 / (1 - residual);
    }

    static float ElasticLegOut(float t, int level, bool paired = false) {
      if (t == 0 || t == 1) return t;
      var angle = Mathf.Tau * t / (paired ? ElasticPairPeriod : ElasticSoloPeriod);
      var kick = (paired ? ElasticPairKick : ElasticSoloKick)[level];
      var exponent = -(paired ? ElasticPairDecay : ElasticSoloDecay) * t + (paired ? 0 : ElasticSoloTail * t * t);
      return ElasticScale(kick, paired) * (1 - Mathf.Pow(2, exponent) * (Mathf.Cos(angle) - kick * Mathf.Sin(angle)));
    }

    static float ElasticLegIn(float t, int level, bool paired = false) => 1 - ElasticLegOut(1 - t, level, paired);
    static float ElasticPair(float t, int level) => t < 0.5f ? ElasticLegIn(2 * t, level, true) / 2
      : 0.5f + ElasticLegOut(2 * t - 1, level, true) / 2;

    static float ElasticSlope(float t, int level) {
      var omega = Mathf.Tau / ElasticPairPeriod;
      var decay = ElasticPairDecay * Mathf.Log(2);
      var kick = ElasticPairKick[level];
      return ElasticScale(kick, true) * Mathf.Pow(2, -ElasticPairDecay * t) * ((decay + kick * omega) * Mathf.Cos(omega * t)
        + (omega - kick * decay) * Mathf.Sin(omega * t));
    }

    static float ElasticIn(float time) {
      return time == 0 ? 0 : time == 1 ? 1 : -Mathf.Pow(2, 10 * time - 10) * Mathf.Sin((time * 10f - 10.75f) * ConstantD);
    }

    static float ElasticOut(float time) {
      return time == 0 ? 0 : time == 1 ? 1 : Mathf.Pow(2, -10 * time) * Mathf.Sin((time * 10 - 0.75f) * ConstantD) + 1;
    }

    static float ElasticInOut(float time) {
      return time == 0 ? 0 : time == 1 ? 1 : time < 0.5 ? -(Mathf.Pow(2, 20 * time - 10) * Mathf.Sin((20 * time - 11.125f) * ConstantE)) / 2 : Mathf.Pow(2, -20 * time + 10) * Mathf.Sin((20 * time - 11.125f) * ConstantE) / 2 + 1;
    }

    static float BounceIn(float time) {
      return 1 - BounceOut(1 - time);
    }

    static float BounceOut(float time) {
      if (time < 1 / ConstantG)
        return ConstantF * time * time;
      else if (time < 2 / ConstantG)
        return ConstantF * (time -= 1.5f / ConstantG) * time + 0.75f;
      else if (time < 2.5f / ConstantG)
        return ConstantF * (time -= 2.25f / ConstantG) * time + 0.9375f;
      else
        return ConstantF * (time -= 2.625f / ConstantG) * time + 0.984375f;
    }

    static float BounceInOut(float time) {
      return time < 0.5f ? (1 - BounceOut(1 - 2 * time)) / 2 : (1 + BounceOut(2 * time - 1)) / 2;
    }
  }
}
