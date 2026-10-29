// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;

namespace tweens.gd;

public static partial class Tweens
{
    /// <summary>A reusable storage binding and prepared value curve, with independent playback timing.</summary>
    public readonly record struct Sampled<TTarget, TValue> : ITweenDefinition<TTarget, TValue>
        where TTarget : class where TValue : struct
    {
        private readonly ITweenBinding<TTarget, TValue> binding;
        private readonly TweenDefinition<TTarget, TValue>? callbacks;
        /// <summary>The immutable value sampler, including its color policy.</summary>
        public KeyframeCurve<TValue> Curve { get; }
        /// <summary>Clock, easing, and fill settings. Vary them with a with expression without preparing new curves.</summary>
        public TweenTiming Timing { get; init; }

        /// <summary>Copies the binding configuration and shares the immutable prepared curve.</summary>
        public Sampled(ITweenBinding<TTarget, TValue> binding, KeyframeCurve<TValue> curve, TweenTiming timing = default)
        {
            ArgumentNullException.ThrowIfNull(binding);
            ArgumentNullException.ThrowIfNull(curve);
            this.binding = binding.CopyBinding();
            ArgumentNullException.ThrowIfNull(this.binding);
            Curve = curve;
            Timing = timing;
            callbacks = null;
        }

        internal Sampled(TweenDefinition<TTarget, TValue> source, KeyframeCurve<TValue> curve)
        {
            if (source.By is not null || source.FactorFrom != 1 || source.FactorTo != 1 || source.FactorBy != 1
                || source.DeltaFrom is not null || source.DeltaTo is not null || source.DeltaBy is not null)
                throw new ArgumentException("Keyframe curves cannot use relative endpoints or endpoint adjustments.", nameof(source));
            binding = callbacks = source;
            Curve = curve;
            Timing = source.ToOptions().Timing;
        }

        TweenDefinition<TTarget, TValue> ITweenDefinition<TTarget, TValue>.CreatePlayback()
        {
            ArgumentNullException.ThrowIfNull(binding);
            ArgumentNullException.ThrowIfNull(Curve);
            var copied = binding.CopyBinding();
            ArgumentNullException.ThrowIfNull(copied);
            var result = new CurveDefinition<TTarget, TValue>(copied, Curve,
                new TweenOptions { Timing = Timing, ColorPolicy = Curve.Policy });
            if (callbacks is not null)
            {
                result.OnAdd = callbacks.OnAdd;
                result.OnStart = callbacks.OnStart;
                result.OnUpdate = callbacks.OnUpdate;
                result.OnEnd = callbacks.OnEnd;
                result.OnCancel = callbacks.OnCancel;
                result.OnFinally = callbacks.OnFinally;
                result.SuppressCallbacksWhenTargetInvalid = callbacks.SuppressCallbacksWhenTargetInvalid;
            }
            return result;
        }
    }
}

public static partial class TweenExtensions
{
    /// <summary>Uses a prepared curve instead of From/To, retaining binding, timing, and callbacks.
    /// The curve owns its value policy. Relative endpoints and endpoint adjustments are rejected.</summary>
    public static Tweens.Sampled<TTarget, TValue> Through<TTarget, TValue>(
        this ITweenDefinition<TTarget, TValue> definition, KeyframeCurve<TValue> curve)
        where TTarget : class where TValue : struct
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(curve);
        return new(definition.CreatePlayback(), curve);
    }

    /// <summary>Uses evenly spaced values instead of From/To, retaining binding, timing, callbacks, and color policy.</summary>
    public static Tweens.Sampled<TTarget, TValue> Through<TTarget, TValue>(
        this ITweenDefinition<TTarget, TValue> definition, ReadOnlySpan<TValue> values, Interpolation interpolation = default)
        where TTarget : class where TValue : struct
    {
        ArgumentNullException.ThrowIfNull(definition);
        var source = definition.CreatePlayback();
        var curve = KeyframeCurve<TValue>.EvenlySpaced(values, interpolation, source.ColorSpace, source.AlphaMode, source.ColorEncoding);
        return new(source, curve);
    }
}

internal sealed class CurveDefinition<TTarget, TValue> : TweenDefinition<TTarget, TValue>
    where TTarget : class where TValue : struct
{
    private ITweenBinding<TTarget, TValue> binding;
    private readonly KeyframeCurve<TValue> curve;
    private KeyframeCurve<TValue>? playback;

    internal CurveDefinition(ITweenBinding<TTarget, TValue> binding, KeyframeCurve<TValue> curve, TweenOptions options)
    {
        this.binding = binding;
        this.curve = curve;
        From = curve.NeedsStart ? null : curve.FirstValue;
        To = curve.LastValue;
        options.CopyTo(this);
    }
    protected override void Prepare(TTarget target) => binding.PrepareTarget(target);
    internal override TweenDefinition<TTarget, TValue> Snapshot()
    {
        var snapshot = (CurveDefinition<TTarget, TValue>)base.Snapshot();
        snapshot.binding = binding.CopyBinding();
        ArgumentNullException.ThrowIfNull(snapshot.binding);
        snapshot.playback = null;
        return snapshot;
    }
    protected override TValue Read(TTarget target) => binding.ReadValue(target);
    protected override void Write(TTarget target, TValue value) => binding.WriteValue(target, value);
    protected override void Restore(TTarget target, TValue initial) => binding.RestoreValue(target, initial);
    protected override void PrepareValues(TValue from, TValue to) => playback = curve.CaptureStart(from);
    protected override TValue Interpolate(TValue from, TValue to, float weight) => playback!.Sample(weight);
    protected override void Release()
    {
        try { binding.ReleaseSnapshot(); }
        finally { playback = null; }
    }
}
