using System.Diagnostics;
using Godot;
using jam;
using twodog.Testing;
using twodog.Testing.Xunit;

namespace dachjam.Tests;

[Collection<HeadlessCollection>]
public sealed class MainSceneTests : IDisposable
{
    private const int Level0Cells = 16;
    private const int Level1Cells = 18;
    private const int PerCell = CitizenMotion.PerCell;
    private const string WorldEnvironment = "res://environment/env_world.tres";

    private readonly HeadlessFixture _godot;
    private readonly Node _root;
    private readonly int _entitiesBefore = ECS.World.Count;

    private Campaign Campaign => _root.GetNode<Campaign>("Campaign");
    private Level Level => Campaign.Current;
    private Protesters Orange => Level.GetNode<Protesters>("Orange");
    private Passives Passives => Level.GetNode<Passives>("Passives");
    private Node Stage => _root.GetNode("Interludes");
    private Godot.Environment Fog => _root.GetNode<WorldEnvironment>("WorldEnvironment").Environment;

    public MainSceneTests(HeadlessFixture godot)
    {
        _godot = godot;
        using var scene = GD.Load<PackedScene>((string) ProjectSettings.GetSetting("application/run/main_scene"));
        _root = scene.Instantiate();
        _root.GetNode<Campaign>("Campaign").FogTime = 0.01;
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

    private static void Press(string action)
    {
        Input.ParseInputEvent(new InputEventAction { Action = action, Pressed = true });
        Input.ParseInputEvent(new InputEventAction { Action = action, Pressed = false });
    }

    private void Unlocked() => Assert.True(FramesUntil(() => !Level.Locked), "The level stayed locked.");

    private void SolveLevel0()
    {
        Vector3I[] directions = [Vector3I.Right, Vector3I.Back, Vector3I.Left, Vector3I.Forward];
        foreach (var direction in directions)
        {
            for (var i = 0; i < (direction == Vector3I.Right ? 3 : 4); i++) Level.Step(0, direction);
        }
    }

    // An interlude that lasts until the test frees it.
    private static PackedScene Interlude(string name)
    {
        var root = new Control { Name = name };
        var scene = new PackedScene();
        Assert.Equal(Error.Ok, scene.Pack(root));
        root.Free();
        return scene;
    }

    private static PackedScene WithInterludes(PackedScene scene)
    {
        var level = scene.Instantiate<Level>();
        level.Pretext = Interlude("Pretext");
        level.Intro = Interlude("Intro");
        level.Outro = Interlude("Outro");

        var packed = new PackedScene();
        Assert.Equal(Error.Ok, packed.Pack(level));
        level.Free();
        return packed;
    }

    private Node Showing(string interlude)
    {
        Assert.True(FramesUntil(() => Stage.HasNode(interlude)), $"{interlude} never showed.");
        return Stage.GetNode(interlude);
    }

    [Fact]
    public void Start_RecruitsOnlyTheStartCell()
    {
        Frames(2);

        Assert.Equal(0, Campaign.Index);
        Assert.Equal(PerCell, Orange.Count);
        Assert.Equal((Level0Cells - 1) * PerCell, Passives.Count);
        Assert.Equal(Level0Cells * PerCell, Orange.Multimesh.InstanceCount);
        Assert.Equal(PerCell, Orange.Multimesh.VisibleInstanceCount);
    }

    [Fact]
    public void Input_ExtendsTheSelectedStream()
    {
        Unlocked();

        Press("ui_up");
        Frames(2);

        Assert.Equal(2, Level.Puzzle.Trails[0].Cells.Count);
        Assert.Equal(2 * PerCell, Orange.Count);
    }

    [Fact]
    public void LockedLevel_IgnoresInput()
    {
        Assert.True(Level.Locked);

        Press("ui_up");
        Frames(1);

        Assert.Single(Level.Puzzle.Trails[0].Cells);
    }

    [Fact]
    public void SteppingBack_ReturnsCitizensOnceTheyFaded()
    {
        Assert.Equal(MoveKind.Extended, Level.Step(0, Vector3I.Right));
        Assert.Equal(MoveKind.Retracted, Level.Step(0, Vector3I.Left));
        Assert.Equal(2 * PerCell, Orange.Count);

        Assert.True(FramesUntil(() => Passives.Count == (Level0Cells - 1) * PerCell));
        Assert.Equal(PerCell, Orange.Count);
    }

    [Fact]
    public void BlockedMove_StartlesTheCitizensInTheWay()
    {
        var blocked = new Vector3I(1, 0, 2);
        Level.Step(0, Vector3I.Left);

        Assert.Equal(MoveKind.Blocked, Level.Step(0, Vector3I.Back));
        Assert.All(Passives.CitizensAt(blocked).ToArray(), citizen => Assert.Equal(1, citizen.Ref<Startle>().Amount));

        Assert.True(FramesUntil(() => Passives.CitizensAt(blocked)[0].Ref<Startle>().Amount == 0));
    }

    [Fact]
    public void Restart_ReleasesEverythingButTheStart()
    {
        Level.Step(0, Vector3I.Right);
        Level.Step(0, Vector3I.Right);

        Level.Restart();

        Assert.Single(Level.Puzzle.Trails[0].Cells);
        Assert.True(FramesUntil(() => Orange.Count == PerCell));
    }

    [Fact]
    public void LongWayRound_SolvesLevel0()
    {
        using var solved = GodotAssert.ExpectSignal(Level, Level.SignalName.Solved);

        SolveLevel0();

        solved.AssertEmitted();
        Frames(2);
        Assert.Equal(Level0Cells * PerCell, Orange.Count);
        Assert.Equal(0, Passives.Count);
    }

    [Fact]
    public void SolvedLevel_IgnoresInput()
    {
        Unlocked();
        SolveLevel0();

        Press("restart");
        Frames(2);

        Assert.True(Level.Puzzle.IsSolved);
    }

    [Fact]
    public void SolvedLevel_AdvancesToTheNextLevel_AndDespawnsTheOld()
    {
        Campaign.AdvanceDelay = 0.01;

        SolveLevel0();

        Assert.True(FramesUntil(() => Campaign.Index == 1));
        Assert.Equal(2, Level.Puzzle.Trails.Count);
        Assert.Equal(_entitiesBefore + Level1Cells * PerCell, ECS.World.Count);
    }

    [Fact]
    public void Fog_ClearsForPlay_WithoutChangingTheEnvironmentResource()
    {
        Assert.Equal(GD.Load<Godot.Environment>(WorldEnvironment).VolumetricFogDensity, Campaign.ThickFog);
        Assert.Equal(Campaign.ThickFog, Fog.VolumetricFogDensity);

        Unlocked();

        Assert.Equal(Campaign.PlayFog, Fog.VolumetricFogDensity, 0.001f);
        Assert.Equal(Campaign.ThickFog, GD.Load<Godot.Environment>(WorldEnvironment).VolumetricFogDensity);
    }

    [Fact]
    public void Glow_IsRetunedForMobile_AndLeftAloneElsewhere()
    {
        var resource = GD.Load<Godot.Environment>(WorldEnvironment);
        var environment = (Godot.Environment) resource.Duplicate();

        jam.Campaign.TuneGlow(environment, "forward_plus");
        Assert.Equal(resource.GlowHdrThreshold, environment.GlowHdrThreshold);
        Assert.Equal(resource.GlowStrength, environment.GlowStrength);

        jam.Campaign.TuneGlow(environment, "mobile");
        Assert.Equal(jam.Campaign.MobileGlowHdrThreshold, environment.GlowHdrThreshold);
        Assert.Equal(jam.Campaign.MobileGlowStrength, environment.GlowStrength);
        Assert.Equal(jam.Campaign.MobileGlowBloom, environment.GlowBloom);
        Assert.Equal(resource.GlowHdrThreshold, GD.Load<Godot.Environment>(WorldEnvironment).GlowHdrThreshold);
    }

    [Fact]
    public void Fog_FallsBackToClassicFog_WithoutVolumetricFog()
    {
        var resource = GD.Load<Godot.Environment>(WorldEnvironment);
        var environment = (Godot.Environment) resource.Duplicate();

        Assert.True(jam.Campaign.TuneFog(environment, "forward_plus"));
        Assert.True(environment.VolumetricFogEnabled);
        Assert.False(environment.FogEnabled);

        Assert.False(jam.Campaign.TuneFog(environment, "mobile"));
        Assert.False(environment.VolumetricFogEnabled);
        Assert.True(environment.FogEnabled);
        Assert.Equal(resource.VolumetricFogAlbedo, environment.FogLightColor);
        Assert.Equal(jam.Campaign.FallbackFogHeight, environment.FogHeight);

        jam.Campaign.SetFogDensity(environment, false, Campaign.PlayFog);
        Assert.Equal(Campaign.PlayFog * jam.Campaign.FallbackFogDensityScale, environment.FogDensity, 0.0001f);
        Assert.True(resource.VolumetricFogEnabled);
    }

    [Fact]
    public void Interludes_SandwichEachLevel()
    {
        Campaign.AdvanceDelay = 0.01;
        Campaign.Levels = [WithInterludes(Campaign.Levels[0]), WithInterludes(Campaign.Levels[1])];
        Campaign.Load(0);

        var pretext = Showing("Pretext");
        Frames(5);
        Assert.Equal(Campaign.ThickFog, Fog.VolumetricFogDensity);
        pretext.QueueFree();

        var intro = Showing("Intro");
        Assert.Equal(Campaign.PlayFog, Fog.VolumetricFogDensity, 0.001f);
        Assert.True(Level.Locked);
        intro.QueueFree();

        Unlocked();
        SolveLevel0();

        var outro = Showing("Outro");
        Assert.Equal(Campaign.PlayFog, Fog.VolumetricFogDensity, 0.001f);
        outro.QueueFree();

        Showing("Pretext");
        Assert.Equal(1, Campaign.Index);
        Assert.Equal(Campaign.ThickFog, Fog.VolumetricFogDensity);
    }

    [Fact]
    public void InterludeTemplate_FreesItselfWhenDone()
    {
        var interlude = GD.Load<PackedScene>("res://interludes/interlude.tscn").Instantiate();
        interlude.Set("fade", 0.01);
        interlude.Set("hold", 0.01);

        _godot.Tree.Root.AddChild(interlude);

        Assert.True(FramesUntil(() => !GodotObject.IsInstanceValid(interlude)));
    }

    [Fact]
    public void Tab_SelectsTheNextStream()
    {
        Campaign.Load(1);
        var teal = Level.GetNode<Protesters>("Teal");
        Unlocked();

        Press("ui_focus_next");
        Frames(2);

        Assert.Same(teal.Head, Level.SelectedHead);
        Assert.True(teal.Selected);
        Assert.False(Orange.Selected);
    }

    [Fact]
    public void FreeingTheScene_DespawnsItsCitizens()
    {
        Assert.Equal(_entitiesBefore + Level0Cells * PerCell, ECS.World.Count);

        _root.Free();

        Assert.Equal(_entitiesBefore, ECS.World.Count);
    }
}
