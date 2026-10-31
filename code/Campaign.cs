using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using tweens.gd;

namespace jam;

/// <summary>
///     Plays the levels in order, each sandwiched between its interludes: pretext, fog clears, play with the intro playing
///     over its start, outro, fog returns. Between levels the fog is <see cref="ThickFog"/>, hiding the board entirely;
///     for play it clears to <see cref="PlayFog"/>, the environment's own density. Interludes are scenes shown on
///     <see cref="Stage"/>; the campaign waits until they free themselves (see <c>code/interlude.gd</c>), except for the
///     intro, which leaves input to the level and is cut short once the level is solved.
///     Keeps the camera rig over the current level and the camera focus on its selected stream; on boards wider than
///     <see cref="StillRadius"/>, the rig follows the selected stream too. Once a level is solved, the camera rises to
///     look straight down on the whole board, see <see cref="Overview"/>.
/// </summary>
public partial class Campaign : Node3D
{
    private const float CameraSharpness = 3f;

    [Export] public PackedScene[] Levels { get; set; } = [];
    [Export] public PackedScene EndCard { get; set; }
    [Export] public Node3D CameraRig { get; set; }
    [Export] public Node3D Focus { get; set; }
    [Export] public WorldEnvironment World { get; set; }
    [Export] public Node Stage { get; set; }

    /// <summary>Fog density between levels: thick enough to hide the board from the camera completely.</summary>
    [Export] public float ThickFog { get; set; } = 1;

    [Export] public double FogTime { get; set; } = 2;
    [Export] public double AdvanceDelay { get; set; } = 4;
    [Export] public double OverviewTime { get; set; } = 3;

    /// <summary>Space left around the board in the overview, as a factor of its radius.</summary>
    [Export] public float OverviewMargin { get; set; } = 1.2f;

    /// <summary>
    ///     Boards within this radius keep the camera rig at their center. On larger ones, the rig follows the selected
    ///     stream, up to the board's radius less this much from the center.
    /// </summary>
    [Export] public float StillRadius { get; set; } = 4.5f;

    public Level Current { get; private set; }
    public int Index { get; private set; } = -1;

    /// <summary>Fog density during play, the environment's own.</summary>
    public float PlayFog { get; private set; }

    /// <summary>
    ///     How far the camera has risen from following the selected stream (0) to straight above the board (1).
    /// </summary>
    public float Overview { get; private set; }

    /// <summary>
    ///     Glow settings for the Mobile renderer. Its 10-bit color buffer clamps HDR at twice white, so the emissive
    ///     overshoot that drives the halo on Forward+ is mostly gone before the glow pass; a zero threshold with more
    ///     strength and bloom brings the halo back to a similar size. Tuned by eye on Vulkan and WebGPU Mobile.
    /// </summary>
    public const float MobileGlowHdrThreshold = 0;
    public const float MobileGlowStrength = 1.2f;
    public const float MobileGlowBloom = 0.6f;

    private Environment _fog;
    private TweenInstance _fade;
    private TweenInstance _rise;

    // The camera rig's child; its position is the camera's offset from the rig, resting where the scene puts it.
    private Node3D _arm;
    private Godot.Vector3 _rest;

    public override void _Ready()
    {
        base._Ready();

        // A copy, so fading the fog never changes the shared resource.
        _fog = World.Environment = (Environment) World.Environment.Duplicate();
        _fog.FogEnabled = true;
        PlayFog = _fog.FogDensity;
        TuneGlow(_fog, RenderingServer.GetCurrentRenderingMethod());
        _arm = CameraRig.GetChild<Node3D>(0);
        _rest = _arm.Position;
        Load(0);
    }

    /// <summary>
    ///     Applies the renderer-specific glow settings; see <see cref="MobileGlowHdrThreshold"/>. Forward+ and
    ///     Compatibility keep the environment's own values.
    /// </summary>
    public static void TuneGlow(Environment environment, string renderingMethod)
    {
        if (renderingMethod != "mobile") return;
        environment.GlowHdrThreshold = MobileGlowHdrThreshold;
        environment.GlowStrength = MobileGlowStrength;
        environment.GlowBloom = MobileGlowBloom;
    }

    /// <summary>
    ///     How much fog hides a surface <paramref name="distance"/> away from the camera, from 0 (clear) to 1 (hidden).
    /// </summary>
    public static float FogAmount(float density, float distance) =>
        Mathf.Min(1 - Mathf.Exp(-density * distance), MaxFogAmount);

    /// <summary>The density at which fog hides a surface <paramref name="distance"/> away by <paramref name="amount"/>.</summary>
    public static float FogDensity(float amount, float distance) => -Mathf.Log(1 - amount) / distance;

    // Keeps the amount invertible: a float rounds 1 - exp(-x) to exactly 1 for large x.
    private const float MaxFogAmount = 1 - 1e-6f;

    /// <summary>
    ///     Replaces the current level in thick fog and starts its sequence, cancelling the old level's.
    /// </summary>
    public void Load(int index)
    {
        if (Current is not null)
        {
            RemoveChild(Current);
            Current.QueueFree();
        }

        foreach (var interlude in Stage.GetChildren()) interlude.QueueFree();
        _fade?.Cancel();
        _fog.FogDensity = ThickFog;
        _rise?.Cancel();
        Overview = 0;
        _arm.Position = _rest;

        Index = index;
        Current = Levels[index].Instantiate<Level>();
        Current.Locked = true;
        AddChild(Current);

        CameraRig.GlobalPosition = Rig();
        Focus.GlobalPosition = Current.SelectedHead.GlobalPosition;
        if (OS.HasFeature("web"))
        {
            var camera = GetViewport().GetCamera3D();
            GD.Print($"Level {index}: camera={camera.GlobalPosition}, focus={Focus.GlobalPosition}, " +
                     $"fov={camera.Fov}, near={camera.Near}, far={camera.Far}, " +
                     $"renderer={RenderingServer.GetCurrentRenderingMethod()}");
        }
        Run(Current);
    }

    // Cheat: keys 1 to 9 load levels 0 to 8, even during interludes.
    public override void _Input(InputEvent ev)
    {
        if (ev is not InputEventKey { Pressed: true, Echo: false } key) return;

        var index = key.Keycode - Key.Key1;
        if (index < 0 || index > 8 || index >= Levels.Length) return;

        Load((int) index);
        GetViewport().SetInputAsHandled();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        var t = 1 - Mathf.Exp(-(float) delta * CameraSharpness);
        CameraRig.GlobalPosition = CameraRig.GlobalPosition.Lerp(Rig(), t);
        var focus = Current.SelectedHead.GlobalPosition.Lerp(Current.Center, Overview);
        Focus.GlobalPosition = Focus.GlobalPosition.Lerp(focus, t);
        if (Overview > 0) _arm.Position = Arm(_rest, OverviewHeight(), Overview);
    }

    // Where the camera rig belongs: towards the selected stream on large boards, back to the center for the overview.
    private Godot.Vector3 Rig()
    {
        var center = Current.Center;
        var offset = (Current.SelectedHead.GlobalPosition - center) with { Y = 0 };
        var rig = center + offset.LimitLength(Mathf.Max(0, Current.Radius - StillRadius));
        return rig.Lerp(center, Overview);
    }

    /// <summary>
    ///     The camera's offset on its way from <paramref name="rest"/> (<paramref name="weight"/> 0) to
    ///     <paramref name="height"/> straight above the rig (1). It swings up along an arc: elevation and distance blend,
    ///     the heading stays, so the view turns down onto the board without spinning.
    /// </summary>
    public static Godot.Vector3 Arm(Godot.Vector3 rest, float height, float weight)
    {
        var heading = new Godot.Vector3(rest.X, 0, rest.Z);
        var elevation = Mathf.Lerp(Mathf.Atan2(rest.Y, heading.Length()), Mathf.Pi / 2, weight);
        var distance = Mathf.Lerp(rest.Length(), height, weight);
        return distance * (Mathf.Cos(elevation) * heading.Normalized() + Mathf.Sin(elevation) * Godot.Vector3.Up);
    }

    // High enough for the camera's vertical field of view to take in the board's radius, with a margin.
    private float OverviewHeight()
    {
        var fov = GetViewport().GetCamera3D()?.Fov ?? 40;
        return Current.Radius * OverviewMargin / Mathf.Tan(Mathf.DegToRad(fov) / 2);
    }

    private async void Run(Level level)
    {
        foreach (var step in Sequence(level))
        {
            await step;
            if (!IsInstanceValid(this) || level != Current) return;
        }
    }

    // After the last level, the end card stays over the thick fog.
    private IEnumerable<Task> Sequence(Level level)
    {
        yield return Show(level.Pretext);
        yield return Fade(PlayFog);
        level.Locked = false;
        Overlay(level.Intro);

        yield return Solved(level);
        Dismiss();
        Rise();
        yield return Wait(AdvanceDelay);
        yield return Show(level.Outro);
        yield return Fade(ThickFog);
        if (Index + 1 < Levels.Length) Load(Index + 1);
        else Overlay(EndCard);
    }

    // Not awaited: the interlude plays over the level, leaving all input to it.
    private void Overlay(PackedScene interlude)
    {
        if (interlude is null) return;

        var node = interlude.Instantiate();
        Stage.AddChild(node);
        node.SetProcessUnhandledInput(false);
    }

    // Cuts short the interludes still playing, see code/interlude.gd.
    private void Dismiss()
    {
        foreach (var interlude in Stage.GetChildren())
        {
            if (interlude.HasMethod("skip")) interlude.Call("skip");
        }
    }

    private async Task Show(PackedScene interlude)
    {
        if (interlude is null) return;

        var node = interlude.Instantiate();
        var exited = ToSignal(node, Node.SignalName.TreeExited);
        Stage.AddChild(node);
        await exited;
    }

    // Fades the fog amount at the camera's distance from the board rather than the density: the densities lie orders
    // of magnitude apart, and nearly all of the visible change happens at the low end.
    private async Task Fade(float density)
    {
        _fade?.Cancel();
        var distance = _rest.Length();
        var from = FogAmount(_fog.FogDensity, distance);
        _fade = this.TweenFloat(FogAmount(density, distance), FogTime, fade =>
        {
            fade.From = from;
            fade.Ease = InOut.Sine;
            fade.OnUpdate = (_, amount) => _fog.FogDensity = FogDensity(amount, distance);
        });
        if (await _fade.End == Reason.Completed) _fog.FogDensity = density;
    }

    // Not awaited: the camera keeps rising while the campaign moves on.
    private void Rise()
    {
        _rise?.Cancel();
        _rise = this.TweenFloat(1, OverviewTime, rise =>
        {
            rise.From = Overview;
            rise.Ease = In.Sine | Out.Cubic;
            rise.OnUpdate = (_, value) => Overview = value;
        });
    }

    private async Task Wait(double seconds)
    {
        var tween = CreateTween();
        tween.TweenInterval(seconds);
        await ToSignal(tween, Tween.SignalName.Finished);
    }

    private async Task Solved(Level level)
    {
        if (!level.Puzzle.IsSolved) await ToSignal(level, Level.SignalName.Solved);
    }
}
