// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss
#nullable enable
using System;
using System.Linq;

namespace tweens.gd;

/// <summary>Private, explicit timeline shared by single playback and Chains.</summary>
internal sealed class ExecutionPlan
{
    private sealed class Entry(TweenInstance leaf, double activation, double start, double end)
    {
        internal readonly TweenInstance Leaf = leaf;
        internal readonly double Activation = activation, Start = start, End = end;
        internal bool Active;
        internal bool Activating;
    }

    private readonly TweenInstance root;
    private Entry[] entries;
    private readonly double[] boundaries;
    private int boundary, phase, index, reassertFrom;
    private bool first = true, batch;
    private double cursor;
    internal double Elapsed => Math.Max(0, cursor);
    internal double Duration { get; }
    internal int EntryCount { get; }
    internal int ActiveCount => entries.Count(e => e.Active && !e.Leaf.IsTerminal);
    internal int PendingCount => root.IsTerminal ? 0 : entries.Count(e => !e.Active);

    internal ExecutionPlan(TweenInstance root, TweenInstance[] leaves)
    {
        this.root = root;
        EntryCount = leaves.Length;
        entries = new Entry[leaves.Length];
        var times = new double[leaves.Length * 3 + 1];
        var timeCount = 0;
        var anchor = 0.0;
        for (var i = 0; i < leaves.Length; i++)
        {
            if (!double.IsFinite(anchor)) throw new ArgumentException("An infinite entry cannot have a successor.");
            var timing = leaves[i].Timing;
            var start = anchor + timing.Gap;
            var activation = Math.Min(anchor, start);
            var end = start + timing.Remaining;
            if (!double.IsFinite(start) || !double.IsFinite(activation) ||
                !double.IsPositiveInfinity(timing.Remaining) && !double.IsFinite(end))
                throw new ArgumentException("The Chain schedule overflows.");
            entries[i] = new(leaves[i], activation, start, end);
            times[timeCount++] = activation;
            times[timeCount++] = start;
            if (double.IsFinite(end)) times[timeCount++] = end;
            cursor = Math.Min(cursor, activation);
            Duration = Math.Max(Duration, end);
            anchor = end;
        }
        times[timeCount++] = cursor;
        Array.Sort(times, 0, timeCount);
        var unique = 1;
        for (var i = 1; i < timeCount; i++)
            if (times[i] != times[unique - 1]) times[unique++] = times[i];
        Array.Resize(ref times, unique);
        boundaries = times;
    }

    internal void Advance(double delta)
    {
        var horizon = first ? delta : Math.Min(double.MaxValue, cursor + delta);
        first = false;
        while (!root.IsTerminal && !root.IsPaused)
        {
            if (!batch)
            {
                if (boundary < boundaries.Length && boundaries[boundary] <= horizon)
                    cursor = boundaries[boundary++];
                else cursor = horizon;
                batch = true;
                phase = index = 0;
                reassertFrom = entries.Length;
            }
            // Complete existing work before capturing new work at this timestamp.
            if (phase == 0)
            {
                while (index < entries.Length)
                {
                    var entry = entries[index];
                    if (entry.Active && !entry.Leaf.IsTerminal && !Sample(entry)) return;
                    index++;
                    if (StopRequested()) return;
                }
                phase = 1;
                index = 0;
            }
            if (phase == 1)
            {
                while (index < entries.Length)
                {
                    var entry = entries[index];
                    if (!entry.Active && entry.Activation <= cursor)
                    {
                        // Reordered starts can activate an older definition after a later one.
                        // Preserve capture-before-activation, then reassert source-order priority.
                        if (HasActiveAfter(index))
                            reassertFrom = Math.Min(reassertFrom, index + 1);
                        entry.Active = true;
                        entry.Activating = true;
                        entry.Leaf.Initialize();
                        Observe(entry);
                        if (StopRequested()) return;
                    }
                    if (entry.Activating && !entry.Leaf.IsTerminal && !Sample(entry)) return;
                    entry.Activating = false;
                    index++;
                    if (StopRequested()) return;
                }
                phase = 2;
                index = reassertFrom;
            }
            while (index < entries.Length)
            {
                var entry = entries[index];
                if (entry.Active && !entry.Leaf.IsTerminal && !Sample(entry)) return;
                index++;
                if (StopRequested()) return;
            }
            batch = false;
            if (entries.All(e => e.Leaf.IsSettled))
            {
                if (!root.IsTerminal) root.Finish(Reason.Completed);
                return;
            }
            if (cursor == horizon) return;
        }
    }

    private bool HasActiveAfter(int current)
    {
        for (var i = current + 1; i < entries.Length; i++)
            if (entries[i].Active && !entries[i].Leaf.IsTerminal) return true;
        return false;
    }

    private bool Sample(Entry entry)
    {
        // Use the exact profile boundaries rather than losing an ulp subtracting large timestamps.
        var local = cursor == entry.End ? entry.Leaf.Timing.InnerDelay + entry.Leaf.Timing.Remaining
            : cursor == entry.Start ? entry.Leaf.Timing.InnerDelay : cursor - entry.Activation;
        var consumed = entry.Leaf.SampleAt(local);
        Observe(entry);
        return !StopRequested() && (consumed || entry.Leaf.IsTerminal);
    }

    private void Observe(Entry entry)
    {
        if (entry.Leaf.IsTerminal && entry.Leaf != root &&
            (entry.Leaf.Error is not null || entry.Leaf.CompletionReason != Reason.Completed))
            root.Finish(entry.Leaf.CompletionReason ?? Reason.Cancelled, entry.Leaf.Error);
    }

    private bool StopRequested() => root.IsTerminal || !root.CanAdvance();

    internal void Stop(Reason reason)
    {
        foreach (var entry in entries)
            if (!entry.Leaf.IsTerminal) entry.Leaf.Finish(reason == Reason.Completed ? Reason.Cancelled : reason);
    }

    internal void Release()
    {
        foreach (var entry in entries)
            if (entry.Leaf != root && entry.Leaf.Error is { } error) root.AccumulateError(error);
        entries = [];
    }
}
