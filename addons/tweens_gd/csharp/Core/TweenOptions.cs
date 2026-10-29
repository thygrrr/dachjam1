// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace tweens.gd;

// The generator exposes these options directly only on definitions with the specified value type.
[AttributeUsage(AttributeTargets.Property)]
internal sealed class TweenValueOptionAttribute : Attribute
{
    public TweenValueOptionAttribute(Type valueType) => ValueType = valueType;
    public Type ValueType { get; }
}

/// <summary>Value behavior during delay and after natural completion. Cancellation does not restore the initial value.</summary>
/// <remarks>A relative tween that follows concurrent property changes removes only its own offset when restoring.</remarks>
[Flags]
public enum FillMode
{
    /// <summary>Leave the property untouched during delay; restore its initial value on natural completion.</summary>
    None = 0,
    /// <summary>Apply From during delay; restore the initial value on natural completion.</summary>
    ApplyFromDuringDelay = 1,
    /// <summary>Keep the final value on natural completion; leave the property untouched during delay.</summary>
    RetainFinalValue = 2,
    /// <summary>Apply From during delay and keep the final value on natural completion.</summary>
    Both = ApplyFromDuringDelay | RetainFinalValue,
}

/// <summary>The update phase used by playback.</summary>
public enum TweenProcessMode
{
    /// <summary>Advance on process ticks.</summary>
    Process,
    /// <summary>Advance on physics ticks.</summary>
    Physics,
}

/// <summary>How playback responds to scene and owner pausing.</summary>
public enum TweenPauseMode
{
    /// <summary>Follow the owner's CanProcess; without an owner, follow scene-tree pause.</summary>
    Bound,
    /// <summary>Follow scene-tree pause, independently of the owner's process mode.</summary>
    SceneTree,
    /// <summary>Ignore scene and owner pausing. Explicit handle pauses still apply.</summary>
    Always,
}

/// <summary>Current timeline state, independently of explicit pausing.</summary>
public enum TweenState
{
    /// <summary>Waiting for the initial delay.</summary>
    Delayed,
    /// <summary>Advancing a forward or return leg.</summary>
    Playing,
    /// <summary>Holding between legs or cycles.</summary>
    Interval,
    /// <summary>Ended naturally.</summary>
    Completed,
    /// <summary>Stopped before natural completion.</summary>
    Cancelled,
    /// <summary>Stopped because playback or a callback failed.</summary>
    Faulted,
}

/// <summary>Why playback ended. Faults are reported separately through Error and End.</summary>
public enum Reason
{
    /// <summary>Every scheduled cycle completed.</summary>
    Completed,
    /// <summary>Playback was explicitly cancelled.</summary>
    Cancelled,
    /// <summary>The target was queued for deletion, or found freed or disposed.</summary>
    /// <remarks>Free() removes a node from the scene tree before deleting it, so a node that owns its tween, the
    /// default for node targets, reports <see cref="OwnerExited"/> instead.</remarks>
    TargetFreed,
    /// <summary>The owner left the scene tree, or a separate owner node was queued for deletion.</summary>
    OwnerExited,
    /// <summary>The scheduler or scene tree was disposed.</summary>
    RunnerDisposed,
}

/// <summary>Immutable timing and value configuration. Use with expressions to vary a reusable value.</summary>
public readonly record struct TweenOptions
{
    /// <summary>A value that repeats until cancelled.</summary>
    public const int Infinite = TweenTiming.Infinite;
    /// <summary>Clock, easing, and fill settings. Defaults match the individual option properties.</summary>
    public TweenTiming Timing { get; init; }
    /// <summary>Color working space, opacity handling, and boundary encoding.</summary>
    public ColorPolicy ColorPolicy { get; init; }

    /// <inheritdoc cref="TweenTiming.Duration"/>
    public Duration Duration { get => Timing.Duration; init => Timing = Timing with { Duration = value }; }
    /// <inheritdoc cref="TweenTiming.FactorDuration"/>
    public double FactorDuration { get => Timing.FactorDuration; init => Timing = Timing with { FactorDuration = value }; }
    /// <inheritdoc cref="TweenTiming.DeltaDuration"/>
    public Duration DeltaDuration { get => Timing.DeltaDuration; init => Timing = Timing with { DeltaDuration = value }; }
    /// <inheritdoc cref="TweenTiming.Delay"/>
    public Duration Delay { get => Timing.Delay; init => Timing = Timing with { Delay = value }; }
    /// <inheritdoc cref="TweenTiming.FactorDelay"/>
    public double FactorDelay { get => Timing.FactorDelay; init => Timing = Timing with { FactorDelay = value }; }
    /// <inheritdoc cref="TweenTiming.DeltaDelay"/>
    public Duration DeltaDelay { get => Timing.DeltaDelay; init => Timing = Timing with { DeltaDelay = value }; }
    /// <inheritdoc cref="TweenTiming.PingPongInterval"/>
    public Duration PingPongInterval { get => Timing.PingPongInterval; init => Timing = Timing with { PingPongInterval = value }; }
    /// <inheritdoc cref="TweenTiming.RepeatInterval"/>
    public Duration RepeatInterval { get => Timing.RepeatInterval; init => Timing = Timing with { RepeatInterval = value }; }
    /// <inheritdoc cref="TweenTiming.Offset"/>
    public Duration Offset { get => Timing.Offset; init => Timing = Timing with { Offset = value }; }
    /// <inheritdoc cref="TweenTiming.Repeats"/>
    public int Repeats { get => Timing.Repeats; init => Timing = Timing with { Repeats = value }; }
    /// <inheritdoc cref="TweenTiming.PingPong"/>
    public bool PingPong { get => Timing.PingPong; init => Timing = Timing with { PingPong = value }; }
    /// <inheritdoc cref="TweenTiming.Fill"/>
    public FillMode Fill { get => Timing.Fill; init => Timing = Timing with { Fill = value }; }
    /// <inheritdoc cref="TweenTiming.Ease"/>
    public EaseType Ease { get => Timing.Ease; init => Timing = Timing with { Ease = value }; }
    /// <inheritdoc cref="TweenTiming.BlendType"/>
    public BlendType BlendType { get => Timing.BlendType; init => Timing = Timing with { BlendType = value }; }
    /// <inheritdoc cref="TweenTiming.Blend"/>
    public double Blend { get => Timing.Blend; init => Timing = Timing with { Blend = value }; }
    /// <inheritdoc cref="TweenTiming.Skew"/>
    public double Skew { get => Timing.Skew; init => Timing = Timing with { Skew = value }; }
    /// <inheritdoc cref="TweenTiming.Weks"/>
    public double Weks { get => Timing.Weks; init => Timing = Timing with { Weks = value }; }
    /// <inheritdoc cref="TweenTiming.EaseFunction"/>
    public Func<float, float>? EaseFunction { get => Timing.EaseFunction; init => Timing = Timing with { EaseFunction = value }; }
    /// <inheritdoc cref="TweenTiming.Curve"/>
    public Godot.Curve? Curve { get => Timing.Curve; init => Timing = Timing with { Curve = value }; }
    /// <summary>Color interpolation coordinates. Defaults to OKLab; does not affect scalar alpha or relative arithmetic.</summary>
    public ColorSpace ColorSpace { get => ColorPolicy.Space; init => ColorPolicy = ColorPolicy with { Space = value }; }
    /// <summary>Color opacity handling. Defaults to Premultiplied.</summary>
    public AlphaMode AlphaMode { get => ColorPolicy.AlphaMode; init => ColorPolicy = ColorPolicy with { AlphaMode = value }; }
    /// <summary>RGB encoding at the target API boundary. Defaults to ordinary Godot sRGB colors.</summary>
    public ColorEncoding ColorEncoding { get => ColorPolicy.Encoding; init => ColorPolicy = ColorPolicy with { Encoding = value }; }
    /// <summary>Skip callbacks after the target or owner becomes invalid. Defaults to false.</summary>
    public bool SuppressCallbacksWhenTargetInvalid { get; init; }

    internal void CopyTo(TweenOptionsBuilder target)
    {
        target.Duration = Duration;
        target.FactorDuration = FactorDuration;
        target.DeltaDuration = DeltaDuration;
        target.Delay = Delay;
        target.FactorDelay = FactorDelay;
        target.DeltaDelay = DeltaDelay;
        target.PingPongInterval = PingPongInterval;
        target.RepeatInterval = RepeatInterval;
        target.Offset = Offset;
        target.Repeats = Repeats;
        target.PingPong = PingPong;
        target.Fill = Fill;
        target.Ease = Ease;
        target.BlendType = BlendType;
        target.Blend = Blend;
        target.Skew = Skew;
        target.Weks = Weks;
        target.EaseFunction = EaseFunction;
        target.Curve = Curve;
        target.ColorSpace = ColorSpace;
        target.AlphaMode = AlphaMode;
        target.ColorEncoding = ColorEncoding;
        target.SuppressCallbacksWhenTargetInvalid = SuppressCallbacksWhenTargetInvalid;
    }
}

/// <summary>Mutable options used by convenience-method configurators and custom class definitions.</summary>
public class TweenOptionsBuilder
{
    /// <inheritdoc cref="TweenOptions.Infinite"/>
    public const int Infinite = TweenOptions.Infinite;

    /// <inheritdoc cref="TweenOptions.Duration"/>
    public Duration Duration { get; set; }
    /// <inheritdoc cref="TweenOptions.FactorDuration"/>
    public double FactorDuration { get; set; } = 1;
    /// <inheritdoc cref="TweenOptions.DeltaDuration"/>
    public Duration DeltaDuration { get; set; }
    /// <inheritdoc cref="TweenOptions.Delay"/>
    public Duration Delay { get; set; }
    /// <inheritdoc cref="TweenOptions.FactorDelay"/>
    public double FactorDelay { get; set; } = 1;
    /// <inheritdoc cref="TweenOptions.DeltaDelay"/>
    public Duration DeltaDelay { get; set; }
    /// <inheritdoc cref="TweenOptions.PingPongInterval"/>
    public Duration PingPongInterval { get; set; }
    /// <inheritdoc cref="TweenOptions.RepeatInterval"/>
    public Duration RepeatInterval { get; set; }
    /// <inheritdoc cref="TweenOptions.Offset"/>
    public Duration Offset { get; set; }
    /// <inheritdoc cref="TweenOptions.Repeats"/>
    public int Repeats { get; set; }
    /// <inheritdoc cref="TweenOptions.PingPong"/>
    public bool PingPong { get; set; }
    /// <inheritdoc cref="TweenOptions.Fill"/>
    public FillMode Fill { get; set; } = FillMode.RetainFinalValue;
    /// <inheritdoc cref="TweenOptions.Ease"/>
    public EaseType Ease { get; set; }
    /// <inheritdoc cref="TweenOptions.BlendType"/>
    public BlendType BlendType { get; set; }
    /// <inheritdoc cref="TweenOptions.Blend"/>
    public double Blend { get; set; } = Easing.DefaultBlend;
    /// <inheritdoc cref="TweenOptions.Skew"/>
    public double Skew { get; set; } = 0.5;
    /// <inheritdoc cref="TweenOptions.Weks"/>
    public double Weks { get; set; } = 0.5;
    /// <inheritdoc cref="TweenOptions.EaseFunction"/>
    public Func<float, float>? EaseFunction { get; set; }
    /// <inheritdoc cref="TweenOptions.Curve"/>
    public Godot.Curve? Curve { get; set; }
    /// <inheritdoc cref="TweenOptions.ColorSpace"/>
    [TweenValueOption(typeof(Godot.Color))]
    public ColorSpace ColorSpace { get; set; }
    /// <inheritdoc cref="TweenOptions.AlphaMode"/>
    [TweenValueOption(typeof(Godot.Color))]
    public AlphaMode AlphaMode { get; set; }
    /// <inheritdoc cref="TweenOptions.ColorEncoding"/>
    [TweenValueOption(typeof(Godot.Color))]
    public ColorEncoding ColorEncoding { get; set; }
    /// <inheritdoc cref="TweenOptions.SuppressCallbacksWhenTargetInvalid"/>
    public bool SuppressCallbacksWhenTargetInvalid { get; set; }

    internal TweenOptions ToOptions() => new()
    {
        Duration = Duration,
        FactorDuration = FactorDuration,
        DeltaDuration = DeltaDuration,
        Delay = Delay,
        FactorDelay = FactorDelay,
        DeltaDelay = DeltaDelay,
        PingPongInterval = PingPongInterval,
        RepeatInterval = RepeatInterval,
        Offset = Offset,
        Repeats = Repeats,
        PingPong = PingPong,
        Fill = Fill,
        Ease = Ease,
        BlendType = BlendType,
        Blend = Blend,
        Skew = Skew,
        Weks = Weks,
        EaseFunction = EaseFunction,
        Curve = Curve,
        ColorSpace = ColorSpace,
        AlphaMode = AlphaMode,
        ColorEncoding = ColorEncoding,
        SuppressCallbacksWhenTargetInvalid = SuppressCallbacksWhenTargetInvalid,
    };
}

internal sealed class Playback
{
    private readonly double duration, delay, turn, offset, span, total;
    private readonly bool pingPong;
    private readonly int repeats;
    private double elapsed;
    internal double Gap => delay;
    internal double InnerDelay => Math.Max(0, delay);
    internal double Remaining => total - offset;
    internal float Progress { get; private set; }
    /// <summary>True on the ping-pong return leg and its final hold.</summary>
    internal bool Returning { get; private set; }
    /// <summary>Index of the current cycle; relative tweens add one full offset per cycle before it.</summary>
    internal double Cycle { get; private set; }
    internal bool Started { get; private set; }
    internal bool Completed { get; private set; }
    internal TweenState State { get; private set; } = TweenState.Delayed;
    /// <summary>Whether the adjusted delay is longer than zero.</summary>
    internal bool HasDelay => delay > 0;

    internal Playback(TweenOptions options) : this(options.Timing)
    {
        ColorInterpolation.Validate(options.ColorSpace, options.AlphaMode, options.ColorEncoding);
    }

    internal static Playback FromTiming(TweenTiming timing) => new(timing);

    private Playback(TweenTiming options)
    {
        Easing.ValidateBlend(options.BlendType, options.Blend);
        Nonnegative(options.Duration, nameof(options.Duration));
        if (!double.IsFinite(options.FactorDuration)) throw new ArgumentOutOfRangeException(nameof(options.FactorDuration));
        if (!double.IsFinite(options.DeltaDuration)) throw new ArgumentOutOfRangeException(nameof(options.DeltaDuration));
        duration =Nonnegative(options.FactorDuration * options.Duration + options.DeltaDuration, nameof(options.Duration));
        if (!double.IsFinite(options.Delay)) throw new ArgumentOutOfRangeException(nameof(options.Delay));
        if (!double.IsFinite(options.FactorDelay)) throw new ArgumentOutOfRangeException(nameof(options.FactorDelay));
        if (!double.IsFinite(options.DeltaDelay)) throw new ArgumentOutOfRangeException(nameof(options.DeltaDelay));
        delay = options.FactorDelay * options.Delay + options.DeltaDelay;
        if (!double.IsFinite(delay)) throw new ArgumentOutOfRangeException(nameof(options.Delay));
        turn = Nonnegative(options.PingPongInterval, nameof(options.PingPongInterval));
        var repeat = Nonnegative(options.RepeatInterval, nameof(options.RepeatInterval));
        offset = Nonnegative(options.Offset, nameof(options.Offset));
        if (!double.IsFinite(options.Skew) || options.Skew < 0 || options.Skew > 1)
            throw new ArgumentOutOfRangeException(nameof(options.Skew), "Skew must be finite and in [0, 1].");
        if (!double.IsFinite(options.Weks) || options.Weks < 0 || options.Weks > 1)
            throw new ArgumentOutOfRangeException(nameof(options.Weks), "Weks must be finite and in [0, 1].");
        if (offset > duration) throw new ArgumentOutOfRangeException(nameof(options.Offset));
        if (options.Repeats < TweenOptions.Infinite) throw new ArgumentOutOfRangeException(nameof(options.Repeats));
        if ((options.Fill & ~FillMode.Both) != 0)
            throw new ArgumentException("Invalid tween mode.", nameof(options));
        pingPong = options.PingPong;
        repeats = options.Repeats;
        var infinite = options.Repeats == TweenOptions.Infinite;
        span = duration + (pingPong ? turn + duration : 0) + repeat;
        // Double arithmetic keeps int.MaxValue repeats from overflowing.
        total = infinite ? double.PositiveInfinity : span * ((double)options.Repeats + 1) - repeat;
        if (!double.IsFinite(span + delay) || !infinite && !double.IsFinite(total + delay))
            throw new ArgumentOutOfRangeException(nameof(options.Duration), "Timeline is too long.");
        if (infinite && span == 0)
            throw new ArgumentException("An infinite tween must have a nonzero cycle duration.", nameof(options));
    }


    internal static double Nonnegative(double value, string name)
    {
        if (!double.IsFinite(value) || value < 0) throw new ArgumentOutOfRangeException(name);
        return value;
    }

    internal void Advance(double delta) => SampleAt(Math.Min(double.MaxValue, elapsed + delta));

    internal void SampleAt(double localTime)
    {
        elapsed = localTime;
        Started = Completed = Returning = false;
        Progress = 0;
        Cycle = 0;
        State = TweenState.Delayed;
        if (elapsed < InnerDelay) return;
        Started = true;
        var time = Math.Min(double.MaxValue, elapsed - InnerDelay + offset);
        if (time >= total)
        {
            Returning = pingPong;
            Progress = pingPong ? 0 : 1;
            Cycle = repeats;
            Completed = true;
            State = TweenState.Completed;
            return;
        }
        var local = time % span;
        Cycle = Math.Round((time - local) / span);
        // At a cycle boundary, display the previous endpoint before restarting.
        if (local == 0 && time > 0)
        {
            local = span;
            Cycle--;
        }
        State = TweenState.Playing;
        Returning = pingPong && local > duration && local >= duration + turn;
        if (duration > 0 && local <= duration)
            Progress = (float)(local / duration);
        else if (!pingPong)
        {
            Progress = 1;
            State = TweenState.Interval;
        }
        else if (local < duration + turn)
        {
            Progress = 1;
            State = TweenState.Interval;
        }
        else if (duration > 0 && local <= duration * 2 + turn)
            Progress = (float)(1 - (local - duration - turn) / duration);
        else
        {
            Progress = 0;
            State = TweenState.Interval;
        }
    }
}
