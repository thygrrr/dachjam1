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
        using var scene = GD.Load<PackedScene>("res://campaign.tscn");
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

    private static void PressKey(Key key)
    {
        Input.ParseInputEvent(new InputEventKey { PhysicalKeycode = key, Pressed = true });
        Input.ParseInputEvent(new InputEventKey { PhysicalKeycode = key, Pressed = false });
    }

    // Skips the interludes on the way, as a player would with Space.
    private bool SkippingUntil(Func<bool> condition) => FramesUntil(() =>
    {
        if (Stage.GetChildCount() > 0) PressKey(Key.Space);
        return condition();
    });

    private void Unlocked() => Assert.True(SkippingUntil(() => !Level.Locked), "The level stayed locked.");

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

        Assert.True(SkippingUntil(() => Campaign.Index == 1));
        Assert.Equal(2, Level.Puzzle.Trails.Count);
        Assert.Equal(_entitiesBefore + Level1Cells * PerCell, ECS.World.Count);
    }

    [Fact]
    public void Arm_RisesFromRestToStraightAbove()
    {
        var rest = new Vector3(-5, 6, 1);

        Assert.True(jam.Campaign.Arm(rest, 12, 0).IsEqualApprox(rest));
        Assert.True(jam.Campaign.Arm(rest, 12, 1).IsEqualApprox(new Vector3(0, 12, 0)));

        var halfway = jam.Campaign.Arm(rest, 12, 0.5f);
        Assert.True(new Vector2(halfway.X, halfway.Z).Normalized().IsEqualApprox(new Vector2(-5, 1).Normalized()));
        Assert.True(halfway.Y > rest.Y);
    }

    [Fact]
    public void Camera_StaysAboveTheBuildingsAndFacesTheSelectedStream()
    {
        var camera = _godot.Tree.Root.GetViewport().GetCamera3D();
        for (var index = 0; index < Campaign.Levels.Length; index++)
        {
            Campaign.Load(index);
            Frames(2);

            Assert.True(camera.GlobalPosition.IsFinite(), $"Level {index}: {camera.GlobalPosition}");
            Assert.True(camera.GlobalPosition.Y > Level.Center.Y + 4, $"Level {index}: {camera.GlobalPosition}");
            Assert.False(camera.IsPositionBehind(Level.SelectedHead.GlobalPosition), $"Level {index}");
        }
    }

    [Fact]
    public void SolvedLevel_RaisesTheCameraAboveTheBoard()
    {
        Campaign.OverviewTime = 0.01;
        var camera = _godot.Tree.Root.GetViewport().GetCamera3D();

        SolveLevel0();

        Assert.True(SkippingUntil(() => Mathf.IsEqualApprox(Campaign.Overview, 1)), "The camera never rose.");
        Frames(2);
        var above = camera.GlobalPosition - Level.Center;
        Assert.True(new Vector2(above.X, above.Z).Length() < 1.5f, $"The camera is not above the board: {above}.");
        Assert.True(above.Y > 8);
    }

    [Fact]
    public void Fog_ClearsForPlay_WithoutChangingTheEnvironmentResource()
    {
        var resource = GD.Load<Godot.Environment>(WorldEnvironment);
        Assert.Equal(resource.FogDensity, Campaign.PlayFog);
        Assert.Equal(Campaign.ThickFog, Fog.FogDensity);

        Unlocked();

        Assert.Equal(Campaign.PlayFog, Fog.FogDensity, 0.001f);
        Assert.Equal(Campaign.PlayFog, GD.Load<Godot.Environment>(WorldEnvironment).FogDensity);
    }

    [Fact]
    public void ThickFog_HidesTheBoard()
    {
        var distance = Level.Center.DistanceTo(_godot.Tree.Root.GetViewport().GetCamera3D().GlobalPosition);

        Assert.True(jam.Campaign.FogAmount(Campaign.ThickFog, distance) > 0.999f);
        Assert.True(jam.Campaign.FogAmount(Campaign.PlayFog, distance) < 0.5f);
    }

    [Fact]
    public void FogDensity_InvertsFogAmount()
    {
        Assert.Equal(0.01f, jam.Campaign.FogDensity(jam.Campaign.FogAmount(0.01f, 8), 8), 0.0001f);
        Assert.Equal(1f, jam.Campaign.FogDensity(jam.Campaign.FogAmount(1f, 8), 8), 0.001f);
        Assert.True(float.IsFinite(jam.Campaign.FogDensity(jam.Campaign.FogAmount(100f, 8), 8)));
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
    public void Interludes_SandwichEachLevel()
    {
        Campaign.AdvanceDelay = 0.01;
        Campaign.Levels = [WithInterludes(Campaign.Levels[0]), WithInterludes(Campaign.Levels[1])];
        Campaign.Load(0);

        var pretext = Showing("Pretext");
        Frames(5);
        Assert.Equal(Campaign.ThickFog, Fog.FogDensity);
        pretext.QueueFree();

        var intro = Showing("Intro");
        Assert.Equal(Campaign.PlayFog, Fog.FogDensity, 0.001f);
        Assert.False(Level.Locked);
        SolveLevel0();
        intro.QueueFree();

        var outro = Showing("Outro");
        Assert.Equal(Campaign.PlayFog, Fog.FogDensity, 0.001f);
        outro.QueueFree();

        Showing("Pretext");
        Assert.Equal(1, Campaign.Index);
        Assert.Equal(Campaign.ThickFog, Fog.FogDensity);
        Assert.False(Stage.HasNode("End"));
    }

    [Fact]
    public void EndCard_AppearsAfterTheLastOutro_AndStaysVisible()
    {
        Campaign.AdvanceDelay = 0.01;
        Campaign.Levels = [WithInterludes(Campaign.Levels[0])];
        Campaign.Load(0);

        Showing("Pretext").QueueFree();
        var intro = Showing("Intro");
        SolveLevel0();
        intro.QueueFree();
        var outro = Showing("Outro");
        Frames(5);
        Assert.False(Stage.HasNode("End"));

        outro.QueueFree();
        var end = Showing("End");
        Assert.Equal("res://end.tscn", end.SceneFilePath);
        Assert.Equal(Campaign.ThickFog, Fog.FogDensity);
        Assert.Equal(0, Campaign.Index);

        PressKey(Key.Space);
        Frames(10);
        Assert.True(GodotObject.IsInstanceValid(end));
        Assert.Same(end, Stage.GetNode("End"));

        Campaign.Load(0);
        Frames(2);
        Assert.False(Stage.HasNode("End"));
    }

    [Fact]
    public void Intro_PlaysOverTheLevel_UntilItIsSolved()
    {
        Unlocked();
        var intro = Showing("Interlude");

        PressKey(Key.Space);
        Frames(2);
        Assert.True(GodotObject.IsInstanceValid(intro), "Space skipped the intro instead of reaching the level.");

        SolveLevel0();
        Assert.True(Level.Puzzle.IsSolved);
        Assert.True(FramesUntil(() => !GodotObject.IsInstanceValid(intro)), "The intro outlived the level.");
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

    [Theory]
    [InlineData(Key.Tab)]
    [InlineData(Key.Space)]
    [InlineData(Key.Enter)]
    [InlineData(Key.KpEnter)]
    public void NextStreamKeys_SelectTheNextStream(Key key)
    {
        Campaign.Load(1);
        var teal = Level.GetNode<Protesters>("Teal");
        Unlocked();

        PressKey(key);
        Frames(2);

        Assert.Same(teal.Head, Level.SelectedHead);
        Assert.True(teal.Selected);
        Assert.False(Orange.Selected);
    }

    [Fact]
    public void Space_SkipsAnInterlude()
    {
        var interlude = GD.Load<PackedScene>("res://interludes/interlude.tscn").Instantiate();
        interlude.Set("hold", 60);
        _godot.Tree.Root.AddChild(interlude);
        Frames(2);

        PressKey(Key.Space);

        Assert.True(FramesUntil(() => !GodotObject.IsInstanceValid(interlude)));
    }

    [Fact]
    public void Intro_TypesTheTitleAndFreesItself()
    {
        var intro = GD.Load<PackedScene>("res://intro.tscn").Instantiate<Control>();
        intro.Set("fade", 0.01);
        intro.Set("hold", 0.01);
        intro.Set("author_delay", 0.01);
        intro.Set("linger", 0.01);
        intro.Set("typing", new[] { 0.01f });
        intro.Set("title_hold", 0.01);
        var exe = intro.GetNode<Label>("quote/quote-full/quote-exe");
        _godot.Tree.Root.AddChild(intro);

        Assert.True(FramesUntil(() => exe.VisibleCharacters == exe.GetTotalCharacterCount()), "The title was never typed.");
        Assert.Equal(1, exe.SelfModulate.A);
        Assert.Equal(0, intro.GetNode<Label>("quote/quote-full").SelfModulate.A);
        Assert.True(FramesUntil(() => !GodotObject.IsInstanceValid(intro)));
    }

    [Theory]
    [InlineData(800, 800, "de")]
    [InlineData(400, 800, "de")]
    [InlineData(1600, 900, "de")]
    [InlineData(800, 800, "en")]
    [InlineData(400, 800, "en")]
    [InlineData(1600, 900, "en")]
    [InlineData(800, 800, "fr")]
    [InlineData(400, 800, "fr")]
    [InlineData(1600, 900, "fr")]
    public void Intro_PositionsTheTitleForEachLanguage(int width, int height, string locale)
    {
        var previous = TranslationServer.GetLocale();
        var window = new SubViewport { Size = new Vector2I(width, height) };
        try
        {
            TranslationServer.SetLocale(locale);
            var intro = GD.Load<PackedScene>("res://intro.tscn").Instantiate<Control>();
            intro.Set("fade", 60);
            var full = intro.GetNode<Label>("quote/quote-full");
            var exe = intro.GetNode<Label>("quote/quote-full/quote-exe");
            _godot.Tree.Root.AddChild(window);
            window.AddChild(intro);
            Frames(3);

            var titleText = exe.Tr(exe.Text).ToString();
            var word = titleText.Split('.')[0];
            var at = full.Tr(full.Text).ToString().IndexOf(word, StringComparison.Ordinal);
            Assert.True(at >= 0, "The quote lost its wordplay.");
            var anchor = full.GetCharacterBounds(at);
            var title = exe.GetCharacterBounds(0);
            Assert.True(anchor.HasArea(), "The quote has no title word.");
            Assert.True((exe.Position + title.Position).IsEqualApprox(anchor.Position));
            Assert.Equal(anchor.Size, title.Size);
            Assert.Equal(locale switch { "fr" => "gris.exe", "en" => "grey.exe", _ => "grau.exe" }, titleText);
            var author = intro.GetNode<Label>("quote/quote-author");
            Assert.Equal(locale switch { "fr" => "Proverb", "en" => "Swinburne", _ => "R. L. Stevenson" },
                author.Tr(author.Text).ToString());
        }
        finally
        {
            window.Free();
            TranslationServer.SetLocale(previous);
        }
    }

    [Fact]
    public void OpeningScreen_IsTheSameGreyAsTheSplash()
    {
        Assert.Equal(ProjectSettings.GetSetting("application/boot_splash/bg_color"),
            ProjectSettings.GetSetting("rendering/environment/defaults/default_clear_color"));
    }

    [Theory]
    [InlineData("res://interludes/pretext-0.tscn")]
    [InlineData("res://interludes/intro-0.tscn")]
    [InlineData("res://interludes/outro-0.tscn")]
    public void Level0Quotes_BreakOnlyWhereWritten(string path)
    {
        var interlude = GD.Load<PackedScene>(path).Instantiate<Control>();
        interlude.Set("fade", 60);
        var quote = interlude.GetNode<Label>("Quote");
        var window = new SubViewport { Size = new Vector2I(800, 800) };
        _godot.Tree.Root.AddChild(window);
        window.AddChild(interlude);
        Frames(2);

        Assert.Equal(quote.Tr(quote.Text).ToString().Split('\n').Length, quote.GetLineCount());
        window.Free();
    }

    [Fact]
    public void Space_SkipsTheIntro()
    {
        var intro = GD.Load<PackedScene>("res://intro.tscn").Instantiate();
        _godot.Tree.Root.AddChild(intro);
        Frames(2);

        PressKey(Key.Space);

        Assert.True(FramesUntil(() => !GodotObject.IsInstanceValid(intro)));
    }

    [Fact]
    public void FreeingTheScene_DespawnsItsCitizens()
    {
        Assert.Equal(_entitiesBefore + Level0Cells * PerCell, ECS.World.Count);

        _root.Free();

        Assert.Equal(_entitiesBefore, ECS.World.Count);
    }
}
