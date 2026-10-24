using System.Diagnostics;
using Godot;
using jam;
using twodog.Testing;
using twodog.Testing.Xunit;

namespace dachjam.Tests;

[Collection<HeadlessCollection>]
public sealed class MainSceneTests : IDisposable
{
    private const int Cells = 16;
    private const int PerCell = CitizenMotion.PerCell;

    private readonly HeadlessFixture _godot;
    private readonly Node _root;
    private readonly int _entitiesBefore = ECS.World.Count;

    private Level Level => _root.GetNode<Level>("Level");
    private Protesters Protesters => _root.GetNode<Protesters>("Level/Protesters");
    private Passives Passives => _root.GetNode<Passives>("Level/Passives");

    public MainSceneTests(HeadlessFixture godot)
    {
        _godot = godot;
        using var scene = GD.Load<PackedScene>((string) ProjectSettings.GetSetting("application/run/main_scene"));
        _root = scene.Instantiate();
        godot.Tree.Root.AddChild(_root);
    }

    public void Dispose()
    {
        if (GodotObject.IsInstanceValid(_root)) _root.Free();
    }

    private void Frames(int count)
    {
        for (var i = 0; i < count; i++) _godot.Engine.Iteration();
    }

    private bool FramesUntil(Func<bool> condition)
    {
        var clock = Stopwatch.StartNew();
        while (!condition() && clock.Elapsed < TimeSpan.FromSeconds(5)) _godot.Engine.Iteration();
        return condition();
    }

    [Fact]
    public void Start_RecruitsOnlyTheStartCell()
    {
        Frames(2);

        Assert.Equal(PerCell, Protesters.Count);
        Assert.Equal((Cells - 1) * PerCell, Passives.Count);
        Assert.Equal(Cells * PerCell, Passives.Multimesh.InstanceCount);
        Assert.Equal(Cells * PerCell, Protesters.Multimesh.InstanceCount);
        Assert.Equal(PerCell, Protesters.Multimesh.VisibleInstanceCount);
    }

    [Fact]
    public void Input_ExtendsTheSelectedStream()
    {
        Input.ParseInputEvent(new InputEventAction { Action = "ui_up", Pressed = true });
        Input.ParseInputEvent(new InputEventAction { Action = "ui_up", Pressed = false });
        Frames(2);

        Assert.Equal(2, Level.Puzzle.Trails[0].Cells.Count);
        Assert.Equal(2 * PerCell, Protesters.Count);
    }

    [Fact]
    public void SteppingBack_ReturnsCitizensOnceTheyFaded()
    {
        Assert.Equal(MoveKind.Extended, Level.Step(0, Vector3I.Right));
        Assert.Equal(MoveKind.Retracted, Level.Step(0, Vector3I.Left));
        Assert.Equal(2 * PerCell, Protesters.Count);

        Assert.True(FramesUntil(() => Passives.Count == (Cells - 1) * PerCell));
        Assert.Equal(PerCell, Protesters.Count);
    }

    [Fact]
    public void Restart_ReleasesEverythingButTheStart()
    {
        Level.Step(0, Vector3I.Right);
        Level.Step(0, Vector3I.Right);

        Level.Restart();

        Assert.Single(Level.Puzzle.Trails[0].Cells);
        Assert.True(FramesUntil(() => Protesters.Count == PerCell));
    }

    [Fact]
    public void LongWayRound_SolvesLevel0()
    {
        using var solved = GodotAssert.ExpectSignal(Level, Level.SignalName.Solved);
        Vector3I[] directions = [Vector3I.Right, Vector3I.Back, Vector3I.Left, Vector3I.Forward];

        foreach (var direction in directions)
        {
            for (var i = 0; i < (direction == Vector3I.Right ? 3 : 4); i++) Level.Step(0, direction);
        }

        solved.AssertEmitted();
        Frames(2);
        Assert.Equal(Cells * PerCell, Protesters.Count);
        Assert.Equal(0, Passives.Count);
    }

    [Fact]
    public void FreeingTheScene_DespawnsItsCitizens()
    {
        Assert.Equal(_entitiesBefore + Cells * PerCell, ECS.World.Count);

        _root.Free();

        Assert.Equal(_entitiesBefore, ECS.World.Count);
    }
}
