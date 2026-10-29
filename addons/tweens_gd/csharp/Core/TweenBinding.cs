// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;

namespace tweens.gd;

/// <summary>A typed storage binding, usable by endpoint tweens and keyframe curves.</summary>
public interface ITweenBinding<in TTarget, TValue> where TTarget : class where TValue : struct
{
    internal ITweenBinding<TTarget, TValue> CopyBinding();
    internal void PrepareTarget(TTarget target);
    internal TValue ReadValue(TTarget target);
    internal void WriteValue(TTarget target, TValue value);
    internal void RestoreValue(TTarget target, TValue initial);
    internal void ReleaseSnapshot();
    internal bool FollowsTarget { get; }
}

/// <summary>Reads and writes values independently of timing and sampling.</summary>
/// <remarks>Each playback copies the binding before preparation. Captured objects remain shared;
/// override <see cref="Copy"/> to copy custom mutable configuration. Allocate owned resources in
/// <see cref="Prepare"/> and release them in <see cref="Release"/>.</remarks>
public abstract class TweenBinding<TTarget, TValue> : ITweenBinding<TTarget, TValue>
    where TTarget : class where TValue : struct
{
    /// <summary>Copies configuration for an independent playback, before preparation.</summary>
    public virtual TweenBinding<TTarget, TValue> Copy() => (TweenBinding<TTarget, TValue>)MemberwiseClone();
    /// <summary>Prepares playback-owned resources before the initial read.</summary>
    protected virtual void Prepare(TTarget target) { }
    /// <summary>Reads the current value from the target.</summary>
    protected abstract TValue Read(TTarget target);
    /// <summary>Writes a sampled value to the target.</summary>
    protected abstract void Write(TTarget target, TValue value);
    /// <summary>Restores captured state on non-retaining completion. May remove an override.</summary>
    protected virtual void Restore(TTarget target, TValue initial) => Write(target, initial);
    /// <summary>Releases playback-owned resources after preparation was attempted, including failed preparation.</summary>
    protected virtual void Release() { }
    /// <summary>Whether reads return stored writes. Relative playback otherwise adds to its captured start.</summary>
    protected virtual bool ReadsWrittenValue => true;

    ITweenBinding<TTarget, TValue> ITweenBinding<TTarget, TValue>.CopyBinding() => Copy();
    void ITweenBinding<TTarget, TValue>.PrepareTarget(TTarget target) => Prepare(target);
    TValue ITweenBinding<TTarget, TValue>.ReadValue(TTarget target) => Read(target);
    void ITweenBinding<TTarget, TValue>.WriteValue(TTarget target, TValue value) => Write(target, value);
    void ITweenBinding<TTarget, TValue>.RestoreValue(TTarget target, TValue initial) => Restore(target, initial);
    void ITweenBinding<TTarget, TValue>.ReleaseSnapshot() => Release();
    bool ITweenBinding<TTarget, TValue>.FollowsTarget => ReadsWrittenValue;
}

/// <summary>Binds custom storage without reflection. Reuse static delegates to avoid captured closures.</summary>
public sealed class PropertyBinding<TTarget, TValue> : TweenBinding<TTarget, TValue>
    where TTarget : class where TValue : struct
{
    private readonly Func<TTarget, TValue> getter;
    private readonly Action<TTarget, TValue> setter;
    /// <summary>Defines how to read and write a value; interpolation belongs to the sampler.</summary>
    public PropertyBinding(Func<TTarget, TValue> getter, Action<TTarget, TValue> setter)
    {
        this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
    }
    /// <summary>This binding has no mutable playback state; captured delegate targets remain shared.</summary>
    public override TweenBinding<TTarget, TValue> Copy() => this;
    protected override TValue Read(TTarget target) => getter(target);
    protected override void Write(TTarget target, TValue value) => setter(target, value);
}
