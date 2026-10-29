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

/// <summary>A playback handle for one tween, independent of its reusable definition.</summary>
/// <remarks>Await <see cref="End"/> before starting the next step. Use the scheduler's creating thread
/// (Godot's main thread for node tweens). The GDScript counterpart is TweensGdHandle.</remarks>
public abstract class TweenInstance
{
    private TaskCompletionSource<Reason>? completion;
    private bool settled;
    private int operationDepth;
    private bool paused;
    private Action? exitHandler;
    internal readonly TweenScheduler Scheduler;
    internal readonly Node? Owner;
    private readonly GodotObject? nativeTarget;
    private readonly SceneTree? tree;
    internal readonly TweenProcessMode Mode;
    private readonly TweenPauseMode pauseMode;
    internal readonly bool Unscaled;
    private protected readonly Playback Clock;
    internal Playback Timing => Clock;
    internal void AccumulateError(Exception error)
    {
        var existing = Error is AggregateException aggregate ? aggregate.Flatten().InnerExceptions.ToArray()
            : Error is null ? [] : new[] { Error };
        var incoming = error is AggregateException multiple ? multiple.Flatten().InnerExceptions.ToArray() : new[] { error };
        var errors = existing.Concat(incoming).Distinct().ToArray();
        Error = errors.Length == 1 ? errors[0] : new AggregateException(errors);
        State = TweenState.Faulted;
    }
    private WeakReference<TweenInstance>? coordinator;
    internal void Coordinate(TweenInstance root) => coordinator = new(root);
    private TweenInstance? Coordinator => coordinator is not null && coordinator.TryGetTarget(out var root) ? root : null;
    internal bool PlaybackInterrupted => !(Coordinator?.CanAdvance() ?? CanAdvance());

    /// <summary>Current timeline state. Explicit pausing does not change it.</summary>
    public TweenState State { get; protected set; } = TweenState.Delayed;
    /// <summary>True once playback completed, was cancelled, or faulted.</summary>
    public bool IsTerminal => State is TweenState.Completed or TweenState.Cancelled or TweenState.Faulted;
    /// <summary>The reason playback ended. Null while running; inspect <see cref="Error"/> for faults.</summary>
    public Reason? CompletionReason { get; private set; }
    /// <summary>The exception that faulted playback, or null.</summary>
    public Exception? Error { get; private set; }
    /// <summary>Uneased progress in [0, 1]. Decreases on the ping-pong return leg.</summary>
    public float Progress => Clock.Progress;
    public bool IsSettled => settled;
    /// <summary>Raised once the tween has settled, before its completion task is resolved.</summary>
    internal event Action<TweenInstance>? Settled;
    /// <summary>Explicit playback pause. Owner and scene pause modes apply separately.</summary>
    public bool IsPaused
    {
        get => Coordinator?.IsPaused ?? paused;
        set { Scheduler.EnsureThread(); if (IsTerminal) return; if (Coordinator is { } root) root.IsPaused = value; else paused = value; }
    }

    /// <summary>Shared completion task. Returns the completion reason; playback errors fault the task.</summary>
    public Task<Reason> End
    {
        get
        {
            Scheduler.EnsureThread();
            if (completion is null)
            {
                completion = new();
                if (settled) SetCompletion();
            }
            return completion.Task;
        }
    }

    internal TweenInstance(TweenScheduler scheduler, TweenOptionsBuilder options, Node? owner, GodotObject? nativeTarget, SceneTree? tree, PlaybackOptions playback = default)
    {
        Scheduler = scheduler;
        Owner = owner;
        this.nativeTarget = nativeTarget;
        this.tree = tree;
        playback.Validate();
        Mode = playback.ProcessMode;
        pauseMode = playback.PauseMode;
        Unscaled = playback.UseUnscaledTime;
        ColorInterpolation.Validate(options.ColorSpace, options.AlphaMode, options.ColorEncoding);
        Clock = Playback.FromTiming(options.ToOptions().Timing);
    }

    /// <summary>Explicitly pauses playback.</summary>
    public void Pause() => IsPaused = true;
    /// <summary>Clears the explicit pause. Scene and owner pause modes still apply.</summary>
    public void Resume() => IsPaused = false;
    /// <summary>Stops active playback with <see cref="Reason.Cancelled"/>. Ended playback is unchanged.</summary>
    public void Cancel()
    {
        Scheduler.EnsureThread();
        if (!IsTerminal && Coordinator is { } root) root.Finish(Reason.Cancelled);
        else Finish(Reason.Cancelled);
    }

    /// <summary>Waits for playback to end. The token cancels this wait, leaving playback running.</summary>
    /// <remarks>Wait cancellation throws OperationCanceledException. Use <see cref="Cancel"/> to stop playback.
    /// The GDScript counterpart is handle.wait(cancellation), which returns WAIT_CANCELLED.</remarks>
    public Task<Reason> AwaitDecommissionAsync(CancellationToken cancellationToken = default)
        => cancellationToken.CanBeCanceled ? End.WaitAsync(cancellationToken) : End;

    internal void BindLifetime()
    {
        if (Owner is null) return;
        exitHandler = () => Finish(ReferenceEquals(Owner, nativeTarget) && Owner.IsQueuedForDeletion()
            ? Reason.TargetFreed : Reason.OwnerExited);
        Owner.TreeExiting += exitHandler;
    }

    /// <summary>True when the target or owner can no longer safely participate in playback.</summary>
    protected bool InvalidTargetOrOwner =>
        (nativeTarget is not null && (!GodotObject.IsInstanceValid(nativeTarget) ||
            (nativeTarget is Node targetNode && targetNode.IsQueuedForDeletion()))) ||
        (Owner is not null && (!GodotObject.IsInstanceValid(Owner) || Owner.IsQueuedForDeletion() || !Owner.IsInsideTree()));

    internal bool CheckTarget()
    {
        if (IsTerminal) return false;
        if (nativeTarget is not null && (!GodotObject.IsInstanceValid(nativeTarget) ||
            (nativeTarget is Node targetNode && targetNode.IsQueuedForDeletion())))
            Finish(Reason.TargetFreed);
        else if (Owner is not null && (!GodotObject.IsInstanceValid(Owner) || Owner.IsQueuedForDeletion() || !Owner.IsInsideTree()))
            Finish(Reason.OwnerExited);
        else if (tree is not null && !GodotObject.IsInstanceValid(tree))
            Finish(Reason.RunnerDisposed);
        return !IsTerminal;
    }

    internal bool CanAdvance()
    {
        if (!CheckTarget() || paused) return false;
        if (pauseMode == TweenPauseMode.Always) return true;
        if (Owner is not null)
            return pauseMode == TweenPauseMode.SceneTree ? !Owner.GetTree().Paused : Owner.CanProcess();
        // Tree-scoped resources have no bound node; Bound and SceneTree both follow tree pause.
        return tree is null || !tree.Paused;
    }


    internal abstract void Initialize();
    internal abstract void Advance(double delta);
    internal virtual bool SampleAt(double localTime) => throw new NotSupportedException();
    /// <summary>Runs the terminal callbacks before completion resolves.</summary>
    protected abstract void InvokeTerminal(Reason reason, bool faulted);
    /// <summary>Releases resources owned by this playback.</summary>
    protected abstract void Release();

    private protected void BeginOperation() => operationDepth++;
    private protected void EndOperation()
    {
        operationDepth--;
        if (IsTerminal && !settled && operationDepth == 0) Settle();
    }

    internal void Finish(Reason reason, Exception? error = null)
    {
        if (IsTerminal)
        {
            // A callback may cancel itself and then throw. Its awaiters still need the error.
            if (error is not null && !settled)
            {
                Error = Error is null ? error : new AggregateException(Error, error);
                State = TweenState.Faulted;
            }
            else if (error is not null) Scheduler.Report(error);
            return;
        }
        CompletionReason = reason;
        Error = error;
        State = error is not null ? TweenState.Faulted : reason == Reason.Completed
            ? TweenState.Completed : TweenState.Cancelled;
        try
        {
            InvokeTerminal(reason, error is not null);
        }
        catch (Exception callbackError)
        {
            Error = Error is null ? callbackError : new AggregateException(Error, callbackError);
            State = TweenState.Faulted;
        }
        finally
        {
            if (exitHandler is not null && GodotObject.IsInstanceValid(Owner))
                Owner!.TreeExiting -= exitHandler;
            exitHandler = null;
            if (operationDepth == 0) Settle();
        }
    }

    private void Settle()
    {
        try { Release(); }
        catch (Exception releaseError)
        {
            Error = Error is null ? releaseError : new AggregateException(Error, releaseError);
            State = TweenState.Faulted;
        }
        settled = true;
        if (Error is not null && Coordinator is null) Scheduler.Report(Error);
        try { Settled?.Invoke(this); }
        catch (Exception error) { Scheduler.Report(error); }
        Settled = null;
        SetCompletion();
    }

    private void SetCompletion()
    {
        if (Error is not null) completion?.TrySetException(Error);
        else completion?.TrySetResult(CompletionReason!.Value);
    }
}

/// <summary>A playback handle with a typed target and sampled value.</summary>
public sealed class TweenInstance<TTarget, TValue> : TweenInstance
    where TTarget : class where TValue : struct
{
    private TweenDefinition<TTarget, TValue>? definition;
    private TweenProgress easing;
    private ITweenBinding<TTarget, TValue>? binding;
    private ITweenSampler<TValue>? sampler;
    private TValue initial, from, to, by;
    // A By tween writes origin + applied. Following tweens move origin along with outside changes to the property.
    private bool relative, follows, pingPong;
    private TValue origin, applied;
    private bool started, prepared, initialized, delayFilled, updatePending, restorePrepared;
    private TValue restoreValue;
    private double sampledTime = double.NaN;
    private int samplePhase, bindingPhase;
    private ExecutionPlan? plan;
    /// <summary>The target animated by this playback.</summary>
    public TTarget Target { get; }
    /// <summary>Last sampled value, including easing and delay fill.</summary>
    public TValue Value { get; private set; }

    internal TweenInstance(TweenScheduler scheduler, TTarget target,
        TweenDefinition<TTarget, TValue> source, Node? owner, SceneTree? tree, PlaybackOptions playback = default)
        : base(scheduler, source, owner, target as GodotObject, tree, playback)
    {
        Target = target;
        definition = source;
        binding = source;
        sampler = source;
        try
        {
            if (definition.By is not null && definition.To is not null)
                throw new ArgumentException("Specify either To or By, not both.", nameof(source));
            var adjustsFrom = definition.FactorFrom != 1 || definition.DeltaFrom is not null;
            var adjustsTo = definition.FactorTo != 1 || definition.DeltaTo is not null;
            var adjustsBy = definition.FactorBy != 1 || definition.DeltaBy is not null;
            if (definition.By is null && adjustsBy)
                throw new ArgumentException("FactorBy and DeltaBy need By.", nameof(source));
            if (definition.By is not null && adjustsTo)
                throw new ArgumentException("FactorTo and DeltaTo do not apply to a By tween.", nameof(source));
            if (!double.IsFinite(definition.FactorFrom) || !double.IsFinite(definition.FactorTo) || !double.IsFinite(definition.FactorBy))
                throw new ArgumentOutOfRangeException(nameof(source), "Factors must be finite.");
            if ((definition.By is not null || adjustsFrom || adjustsTo) && !Offsets<TValue>.Supported)
                throw Offsets<TValue>.Unsupported();
            easing = new TweenProgress(definition.ToOptions().Timing);
            plan = new ExecutionPlan(this, [this]);
        }
        catch
        {
            easing.Dispose();
            throw;
        }
    }

    internal override void Initialize()
    {
        if (IsTerminal || initialized && delayFilled) return;
        BeginOperation();
        try
        {
            // Each binding hook is consumed once, even if it pauses its coordinator.
            while (bindingPhase < 6)
            {
                switch (bindingPhase++)
                {
                    case 0:
                        prepared = true;
                        binding!.PrepareTarget(Target);
                        break;
                    case 1:
                        Value = initial = binding!.ReadValue(Target);
                        break;
                    case 2:
                        from = Adjust(definition!.From ?? initial, definition.FactorFrom, definition.DeltaFrom);
                        break;
                    case 3:
                        to = Adjust(definition!.To ?? initial, definition.FactorTo, definition.DeltaTo);
                        break;
                    case 4:
                        if (definition!.By is { } offset)
                        {
                            (relative, by, origin, applied) = (true, Adjust(offset, definition.FactorBy, definition.DeltaBy),
                                from, Offsets<TValue>.Zero);
                            follows = definition.From is null && definition.FactorFrom == 1 &&
                                definition.DeltaFrom is null && binding!.FollowsTarget;
                            pingPong = definition.PingPong;
                        }
                        break;
                    case 5:
                        sampler!.Prepare(from, to);
                        break;
                }
                if (!CheckTarget() || PlaybackInterrupted) return;
            }
            if (!initialized)
            {
                initialized = true;
                definition!.OnAdd?.Invoke(this);
                if (!CheckTarget() || PlaybackInterrupted) return;
            }
            if (!delayFilled)
            {
                delayFilled = true;
                if (Clock.HasDelay && definition!.Fill.HasFlag(FillMode.ApplyFromDuringDelay)) Apply(from);
            }
        }
        catch (Exception error) { Finish(Reason.Cancelled, error); }
        finally { EndOperation(); }
    }

    internal override void Advance(double delta)
    {
        plan ??= new ExecutionPlan(this, [this]);
        plan.Advance(delta);
    }

    internal override bool SampleAt(double localTime)
    {
        BeginOperation();
        try
        {
            Initialize();
            if (!CheckTarget() || PlaybackInterrupted) return false;
            NotifyUpdate();
            if (!CheckTarget() || PlaybackInterrupted) return false;
            if (sampledTime != localTime)
            {
                sampledTime = localTime;
                samplePhase = 0;
                Clock.SampleAt(localTime);
                State = Clock.State == TweenState.Completed ? TweenState.Playing : Clock.State;
            }
            if (!Clock.Started) { sampledTime = double.NaN; return true; }
            if (!started)
            {
                started = true;
                definition!.OnStart?.Invoke(this);
                if (!CheckTarget() || PlaybackInterrupted) return false;
            }
            if (samplePhase == 0)
            {
                var weight = easing.Sample(Progress, Clock.Returning);
                if (!CheckTarget() || PlaybackInterrupted) return false;
                var value = relative ? Offset(weight) : sampler!.Sample(from, to, weight);
                if (!CheckTarget() || PlaybackInterrupted) return false;
                samplePhase = 1;
                Apply(value);
                if (!CheckTarget() || PlaybackInterrupted) return false;
            }
            if (!Clock.Completed) { sampledTime = double.NaN; return true; }
            if (samplePhase == 1)
            {
                if (!definition!.Fill.HasFlag(FillMode.RetainFinalValue) && !RestoreInitial()) return false;
                samplePhase = 2;
                if (!CheckTarget() || PlaybackInterrupted) return false;
            }
            Finish(Reason.Completed);
            return true;
        }
        catch (Exception error) { Finish(Reason.Cancelled, error); return false; }
        finally { EndOperation(); }
    }

    private void Apply(TValue value)
    {
        if (!CheckTarget()) return;
        binding!.WriteValue(Target, value);
        Value = value;
        updatePending = true;
        NotifyUpdate();
    }

    // factor * value + delta. The factor scales away from zero; for a quaternion that scales its rotation angle.
    private TValue Adjust(TValue value, double factor, TValue? delta)
    {
        if (factor != 1) value = sampler!.SampleOffset(Offsets<TValue>.Zero, value, (float)factor);
        return delta is { } offset ? Offsets<TValue>.Add(value, offset) : value;
    }

    // Everything By has added so far: one offset per finished cycle, unless ping-pong brought it back, plus this one.
    private TValue Offset(float weight)
    {
        Follow();
        if (!CheckTarget() || PlaybackInterrupted) return Value;
        var offset = sampler!.SampleOffset(Offsets<TValue>.Zero, by, weight);
        if (!CheckTarget() || PlaybackInterrupted) return Value;
        if (!pingPong && Clock.Cycle > 0)
            offset = Offsets<TValue>.Add(sampler.SampleOffset(Offsets<TValue>.Zero, by, (float)Clock.Cycle), offset);
        applied = offset;
        return Offsets<TValue>.Add(origin, offset);
    }

    // A value other than the last one written means something else changed the property; keep that change.
    private void Follow()
    {
        if (!follows) return;
        var current = binding!.ReadValue(Target);
        if (!EqualityComparer<TValue>.Default.Equals(current, Value)) origin = Offsets<TValue>.Remove(current, applied);
    }

    private void NotifyUpdate()
    {
        if (!updatePending || !CheckTarget() || PlaybackInterrupted) return;
        updatePending = false;
        definition!.OnUpdate?.Invoke(this, Value);
    }

    private bool RestoreInitial()
    {
        if (!CheckTarget() || PlaybackInterrupted) return false;
        if (!restorePrepared)
        {
            // A following By tween takes only its own offset back out.
            Follow();
            restoreValue = follows ? origin : initial;
            restorePrepared = true;
            if (!CheckTarget() || PlaybackInterrupted) return false;
        }
        binding!.RestoreValue(Target, restoreValue);
        Value = restoreValue;
        updatePending = true;
        NotifyUpdate();
        return true;
    }

    protected override void InvokeTerminal(Reason reason, bool faulted)
    {
        var snapshot = definition!;
        if (!initialized || snapshot is null) return;
        if (snapshot.SuppressCallbacksWhenTargetInvalid &&
            (InvalidTargetOrOwner || reason is Reason.TargetFreed or Reason.OwnerExited)) return;
        Exception? failure = null;
        try
        {
            if (!faulted)
            {
                if (reason == Reason.Completed) snapshot.OnEnd?.Invoke(this);
                else snapshot.OnCancel?.Invoke(this);
            }
        }
        catch (Exception error) { failure = error; }
        try { snapshot.OnFinally?.Invoke(this); }
        catch (Exception error) { failure = failure is null ? error : new AggregateException(failure, error); }
        if (failure is not null) throw failure;
    }

    protected override void Release()
    {
        try { if (prepared) binding?.ReleaseSnapshot(); }
        finally
        {
            definition = null;
            binding = null;
            sampler = null;
            easing.Dispose();
        }
    }
}
