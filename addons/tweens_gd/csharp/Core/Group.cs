// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace tweens.gd;

/// <summary>A playback handle for several tweens running as one parallel step.</summary>
/// <remarks>Created by node.Tween(first, second, ...) or <see cref="Of"/>. Await <see cref="End"/> before
/// starting the next step. If a member stops early or faults, the group cancels its siblings.
/// In GDScript, use Tweens.play_all(target, definitions) or TweensGdGroup.of(handles).</remarks>
public sealed class Group
{
    private readonly TweenInstance[] members;
    private TaskCompletionSource<Reason>? completion;
    private int unsettled;
    private bool stopping;
    private Reason? firstStop;
    private List<Exception>? errors;

    /// <summary>The playback handles supplied when the group was created.</summary>
    public IReadOnlyList<TweenInstance> Members => members;
    /// <summary>True after every member has settled.</summary>
    public bool IsTerminal { get; private set; }
    /// <summary>Completed if every member completed; otherwise the first early stop reason. Null while running.</summary>
    public Reason? CompletionReason { get; private set; }
    /// <summary>A member's exception, or an AggregateException for several failures. Null when no member faulted.</summary>
    public Exception? Error { get; private set; }
    /// <summary>True when at least one member is active and every active member is explicitly paused.</summary>
    public bool IsPaused
    {
        get
        {
            var anyActive = false;
            foreach (var member in members)
            {
                if (member.IsTerminal) continue;
                if (!member.IsPaused) return false;
                anyActive = true;
            }
            return anyActive;
        }
    }

    /// <summary>Shared completion task. Returns the completion reason; member errors fault the task.</summary>
    public Task<Reason> End
    {
        get
        {
            if (completion is null)
            {
                completion = new();
                if (IsTerminal) SetCompletion();
            }
            return completion.Task;
        }
    }

    private Group(TweenInstance[] members)
    {
        this.members = members;
        unsettled = members.Length;
        // Subscribe first: settling one member can cancel, and so settle, the others.
        foreach (var member in members)
            if (!member.IsSettled) member.Settled += OnSettled;
        // Snapshot first: cancellation can settle a subscribed member during this pass.
        foreach (var member in members.Where(member => member.IsSettled).ToArray())
            OnSettled(member);
    }

    /// <summary>Groups existing playback handles, including handles on different targets. Starts no new tweens.</summary>
    /// <param name="tweens">At least one non-null playback handle. Already-ended handles are accepted.</param>
    public static Group Of(params TweenInstance[] tweens)
    {
        ArgumentNullException.ThrowIfNull(tweens);
        if (tweens.Length == 0) throw new ArgumentException("A group needs at least one tween.", nameof(tweens));
        if (Array.IndexOf(tweens, null) >= 0) throw new ArgumentNullException(nameof(tweens), "A group cannot contain null.");
        return new Group([.. tweens]);
    }

    /// <summary>Explicitly pauses every member.</summary>
    public void Pause()
    {
        foreach (var member in members) member.Pause();
    }

    /// <summary>Clears every member's explicit pause. Scene and owner pause modes still apply.</summary>
    public void Resume()
    {
        foreach (var member in members) member.Resume();
    }

    /// <summary>Stops every active member with <see cref="Reason.Cancelled"/>.</summary>
    public void Cancel()
    {
        foreach (var member in members) member.Cancel();
    }

    private void OnSettled(TweenInstance member)
    {
        unsettled--;
        if (member.Error is not null) (errors ??= []).Add(member.Error);
        if (member.Error is not null || member.CompletionReason != Reason.Completed)
        {
            firstStop ??= member.CompletionReason;
            CancelSiblings();
        }
        if (unsettled == 0) Settle();
    }

    private void CancelSiblings()
    {
        if (stopping) return;
        stopping = true;
        foreach (var sibling in members)
            if (!sibling.IsTerminal) sibling.Cancel();
    }

    private void Settle()
    {
        IsTerminal = true;
        CompletionReason = firstStop ?? Reason.Completed;
        Error = errors switch
        {
            null => null,
            [var single] => single,
            _ => new AggregateException(errors),
        };
        SetCompletion();
    }

    private void SetCompletion()
    {
        if (Error is not null) completion?.TrySetException(Error);
        else completion?.TrySetResult(CompletionReason!.Value);
    }
}
