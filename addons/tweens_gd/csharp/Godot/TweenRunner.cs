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

internal partial class TweenRunner : Node
{
    internal readonly TweenScheduler Scheduler = new();
    internal int Ticks { get; private set; }
    private SceneTree? tree;
    private bool stopped;
    private readonly MonotonicClock clock = new(Time.GetTicksUsec);

    internal void Initialize(SceneTree sceneTree)
    {
        tree = sceneTree;
        Name = "TweensGd";
        ProcessMode = ProcessModeEnum.Always;
        ProcessPriority = 1000;
        ProcessPhysicsPriority = 1000;
        Scheduler.UnhandledException += Report;
        tree.Root.TreeExiting += RootExiting;
    }

    private static void Report(Exception error) => GD.PushError($"tweens.gd: {error}");

    public override void _Process(double delta)
    {
        Ticks++;
        if (stopped) return;
        Scheduler.Update(delta, clock.Sample());
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!stopped)
            Scheduler.Update(delta, 1.0 / Engine.PhysicsTicksPerSecond, TweenProcessMode.Physics);
    }

    private void RootExiting()
    {
        if (tree is not null) TweenRuntime.MarkClosing(tree);
        Shutdown();
        // A runner awaiting deferred attachment is not yet owned by the tree.
        if (!IsInsideTree()) Free();
    }

    public override void _ExitTree() => Shutdown();

    internal void Shutdown()
    {
        if (stopped) return;
        stopped = true;
        if (tree is not null)
        {
            TweenRuntime.Remove(tree, this);
            if (GodotObject.IsInstanceValid(tree.Root)) tree.Root.TreeExiting -= RootExiting;
        }
        Scheduler.Dispose();
        tree = null;
    }
}
