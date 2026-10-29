// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss
#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace tweens.gd;

/// <summary>A flat list of linked definitions on one target. Signed delays overlap the previous entry's end.</summary>
public sealed class Chain
{
    internal sealed class Root : TweenInstance
    {
        internal ExecutionPlan? Plan;
        internal Root(TweenScheduler scheduler, object target, Node? owner, SceneTree? tree, PlaybackOptions options)
            : base(scheduler, new TweenOptionsBuilder(), owner, target as GodotObject, tree, options) { }
        internal override void Initialize() { }
        internal override void Advance(double delta)
        {
            BeginOperation();
            try { Plan!.Advance(delta); }
            catch (Exception error) { Finish(Reason.Cancelled, error); }
            finally { EndOperation(); }
        }
        protected override void InvokeTerminal(Reason reason, bool faulted) => Plan?.Stop(reason);
        protected override void Release() => Plan?.Release();
    }

    internal readonly Root Controller;
    internal Chain(Root controller) => Controller = controller;
    public Task<Reason> End => Controller.End;
    public Task<Reason> AwaitDecommissionAsync(CancellationToken token = default) => Controller.AwaitDecommissionAsync(token);
    public bool IsTerminal => Controller.IsTerminal;
    public bool IsSettled => Controller.IsSettled;
    public Reason? CompletionReason => Controller.CompletionReason;
    public Exception? Error => Controller.Error;
    public bool IsPaused => Controller.IsPaused;
    public double Elapsed => Controller.Plan!.Elapsed;
    public double Duration => Controller.Plan!.Duration;
    public int EntryCount => Controller.Plan!.EntryCount;
    public int ActiveCount => Controller.Plan!.ActiveCount;
    public int PendingCount => Controller.Plan!.PendingCount;
    public void Pause() => Controller.Pause();
    public void Resume() => Controller.Resume();
    public void Cancel() => Controller.Cancel();
}
