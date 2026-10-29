// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using Godot;

namespace tweens.gd;

public static partial class Tweens
{
    /// <summary>Creates reusable, deterministic progress-to-offset functions.</summary>
    /// <remarks>Scalar functions work as EaseFunction; vector and quaternion functions supply offsets to callbacks
    /// or custom interpolators. Frequency is cycles (Punch/Breathe) or noise intervals (Shake) per tween,
    /// not per second. In GDScript, use Tweens.FX with snake_case factory names and Callable samplers.</remarks>
    public static class FX
    {
        /// <summary>A damped sine, zero at the end. Phase is in cycles; phase zero starts at zero displacement.
        /// Attack zero starts with full envelope strength. Amplitude may be signed.</summary>
        public static Func<float, float> Punch(float frequency = 6, float amplitude = 1,
            float decay = 2, float phase = 0, float attack = 0)
        {
            Validate(frequency, amplitude, phase);
            var envelope = AttackRelease(attack, decay);
            return progress =>
            {
                var t = Time(progress);
                return t == 1 ? 0 : amplitude * (float)Math.Sin(Math.Tau * (frequency * (double)t + phase)) * envelope(t);
            };
        }

        /// <summary>Quintic-interpolated seeded value noise with a fading envelope. Offset is in noise coordinates.
        /// Positive attack starts at zero; attack zero permits immediate displacement. No mutable RNG or engine resource.</summary>
        public static Func<float, float> Shake(float frequency = 12, float amplitude = 1,
            int seed = 0, float offset = 0, float decay = 2, float attack = 0.1f)
        {
            Validate(frequency, amplitude, offset);
            var envelope = AttackRelease(attack, decay);
            return progress =>
            {
                var t = Time(progress);
                return t == 1 ? 0 : amplitude * (float)Noise(offset + frequency * (double)t, seed) * envelope(t);
            };
        }

        /// <summary>A repeating raised cosine between zero and amplitude. Phase is in cycles.
        /// Integer frequency and phase zero give zero at both ends; other settings deliberately preserve their phase.</summary>
        public static Func<float, float> Breathe(float frequency = 1, float amplitude = 1, float phase = 0)
        {
            Validate(frequency, amplitude, phase);
            return progress => amplitude * (float)(0.5 - 0.5 * Math.Cos(Math.Tau * (frequency * (double)Time(progress) + phase)));
        }

        /// <summary>An immediate amplitude envelope from one to zero.</summary>
        public static Func<float, float> Decay(float power = 2) => AttackRelease(0, power);

        /// <summary>Attack occupies this fraction of the duration, release the rest. Zero attack is (1-t)^decay.
        /// Positive attack uses quintic ramps with zero slope at the peak. Decay must be positive; attack is in [0,1).</summary>
        public static Func<float, float> AttackRelease(float attack = 0.1f, float decay = 2)
        {
            if (!float.IsFinite(attack) || attack < 0 || attack >= 1) throw new ArgumentOutOfRangeException(nameof(attack));
            if (!float.IsFinite(decay) || decay <= 0) throw new ArgumentOutOfRangeException(nameof(decay));
            return progress =>
            {
                var t = Time(progress);
                if (t == 1) return 0;
                if (attack == 0) return (float)Math.Pow(1 - (double)t, decay);
                if (t <= attack) return (float)Smooth(t / (double)attack);
                return (float)Math.Pow(Math.Max(0, 1 - Smooth((t - (double)attack) / (1 - (double)attack))), decay);
            };
        }

        /// <summary>Per-axis damped oscillation. Omitted frequencies are six cycles and phases are zero.</summary>
        public static Func<float, Godot.Vector2> Punch2D(Godot.Vector2 amplitude, Godot.Vector2? frequency = null,
            float decay = 2, Godot.Vector2? phase = null, float attack = 0)
        {
            var f = frequency ?? new Godot.Vector2(6, 6);
            var p = phase ?? Godot.Vector2.Zero;
            return Combine(Punch(f.X, amplitude.X, decay, p.X, attack), Punch(f.Y, amplitude.Y, decay, p.Y, attack));
        }

        /// <summary>Per-axis damped oscillation. Omitted frequencies are six cycles and phases are zero.</summary>
        public static Func<float, Godot.Vector3> Punch3D(Godot.Vector3 amplitude, Godot.Vector3? frequency = null,
            float decay = 2, Godot.Vector3? phase = null, float attack = 0)
        {
            var f = frequency ?? new Godot.Vector3(6, 6, 6);
            var p = phase ?? Godot.Vector3.Zero;
            return Combine(Punch(f.X, amplitude.X, decay, p.X, attack), Punch(f.Y, amplitude.Y, decay, p.Y, attack),
                Punch(f.Z, amplitude.Z, decay, p.Z, attack));
        }

        /// <summary>Independent noise paths. Defaults use twelve intervals and decorrelated coordinate offsets.</summary>
        public static Func<float, Godot.Vector2> Shake2D(Godot.Vector2 amplitude, Godot.Vector2? frequency = null,
            int seed = 0, Godot.Vector2? offset = null, float decay = 2, float attack = 0.1f)
        {
            var f = frequency ?? new Godot.Vector2(12, 12);
            var o = offset ?? new Godot.Vector2(0, 101.37f);
            return Combine(Shake(f.X, amplitude.X, seed, o.X, decay, attack), Shake(f.Y, amplitude.Y, seed, o.Y, decay, attack));
        }

        /// <summary>Independent noise paths. Defaults use twelve intervals and decorrelated coordinate offsets.</summary>
        public static Func<float, Godot.Vector3> Shake3D(Godot.Vector3 amplitude, Godot.Vector3? frequency = null,
            int seed = 0, Godot.Vector3? offset = null, float decay = 2, float attack = 0.1f)
        {
            var f = frequency ?? new Godot.Vector3(12, 12, 12);
            var o = offset ?? new Godot.Vector3(0, 101.37f, 203.71f);
            return Combine(Shake(f.X, amplitude.X, seed, o.X, decay, attack), Shake(f.Y, amplitude.Y, seed, o.Y, decay, attack),
                Shake(f.Z, amplitude.Z, seed, o.Z, decay, attack));
        }

        /// <summary>Per-axis raised cosine. Defaults use one cycle and zero phase.</summary>
        public static Func<float, Godot.Vector2> Breathe2D(Godot.Vector2 amplitude, Godot.Vector2? frequency = null, Godot.Vector2? phase = null)
        {
            var f = frequency ?? Godot.Vector2.One;
            var p = phase ?? Godot.Vector2.Zero;
            return Combine(Breathe(f.X, amplitude.X, p.X), Breathe(f.Y, amplitude.Y, p.Y));
        }

        /// <summary>Per-axis raised cosine. Defaults use one cycle and zero phase.</summary>
        public static Func<float, Godot.Vector3> Breathe3D(Godot.Vector3 amplitude, Godot.Vector3? frequency = null, Godot.Vector3? phase = null)
        {
            var f = frequency ?? Godot.Vector3.One;
            var p = phase ?? Godot.Vector3.Zero;
            return Combine(Breathe(f.X, amplitude.X, p.X), Breathe(f.Y, amplitude.Y, p.Y), Breathe(f.Z, amplitude.Z, p.Z));
        }

        /// <summary>A unit rotation offset from a damped rotation vector. Amplitude components are radians.
        /// Apply locally as baseline * sample(t); this does not interpolate Godot.Quaternion components.</summary>
        public static Func<float, Godot.Quaternion> PunchQuaternion(Godot.Vector3 amplitude, Godot.Vector3? frequency = null,
            float decay = 2, Godot.Vector3? phase = null, float attack = 0)
            => Rotation(Punch3D(amplitude, frequency, decay, phase, attack));

        /// <summary>A unit rotation offset from a noise-driven rotation vector; amplitude components are radians.</summary>
        public static Func<float, Godot.Quaternion> ShakeQuaternion(Godot.Vector3 amplitude, Godot.Vector3? frequency = null,
            int seed = 0, Godot.Vector3? offset = null, float decay = 2, float attack = 0.1f)
            => Rotation(Shake3D(amplitude, frequency, seed, offset, decay, attack));

        /// <summary>A unit rotation offset from a breathing rotation vector; amplitude components are radians.</summary>
        public static Func<float, Godot.Quaternion> BreatheQuaternion(Godot.Vector3 amplitude, Godot.Vector3? frequency = null, Godot.Vector3? phase = null)
            => Rotation(Breathe3D(amplitude, frequency, phase));

        private static Func<float, Godot.Vector2> Combine(Func<float, float> x, Func<float, float> y)
            => t => new Godot.Vector2(x(t), y(t));

        private static Func<float, Godot.Vector3> Combine(Func<float, float> x, Func<float, float> y, Func<float, float> z)
            => t => new Godot.Vector3(x(t), y(t), z(t));

        private static Func<float, Godot.Quaternion> Rotation(Func<float, Godot.Vector3> sample) => t =>
        {
            var v = sample(t);
            // Use double for the norm so even large finite amplitudes do not overflow.
            var angle = Math.Sqrt((double)v.X * v.X + (double)v.Y * v.Y + (double)v.Z * v.Z);
            if (angle == 0) return Godot.Quaternion.Identity;
            var scale = Math.Sin(angle * 0.5) / angle;
            return new Godot.Quaternion((float)(v.X * scale), (float)(v.Y * scale), (float)(v.Z * scale), (float)Math.Cos(angle * 0.5)).Normalized();
        };

        private static void Validate(float frequency, float amplitude, float offset)
        {
            if (!float.IsFinite(frequency) || frequency < 0) throw new ArgumentOutOfRangeException(nameof(frequency));
            if (!float.IsFinite(amplitude)) throw new ArgumentOutOfRangeException(nameof(amplitude));
            if (!float.IsFinite(offset)) throw new ArgumentOutOfRangeException(nameof(offset));
        }

        private static float Time(float t)
        {
            if (!float.IsFinite(t)) throw new ArgumentOutOfRangeException(nameof(t));
            return Math.Clamp(t, 0, 1);
        }

        private static double Smooth(double t) => t * t * t * (t * (6 * t - 15) + 10);

        // A periodic 20-bit lattice, with a fixed unsigned 32-bit hash shared with fx.gd.
        // Double coordinates avoid overflow when finite float offset and frequency are added.
        private static double Noise(double position, int seed)
        {
            var cell = Math.Floor(position);
            var index = (int)(cell % 1048576);
            var weight = Smooth(position - cell);
            var a = Lattice(index, seed);
            return a + (Lattice(index + 1, seed) - a) * weight;
        }

        private static double Lattice(int index, int seed)
        {
            unchecked
            {
                uint h = (uint)(index & 0xfffff) * 374761393u + (uint)seed * 668265263u;
                h = (h ^ (h >> 13)) * 1274126177u;
                h ^= h >> 16;
                return h / 4294967295.0 * 2 - 1;
            }
        }
    }
}
