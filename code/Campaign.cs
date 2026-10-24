using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace jam;

/// <summary>
///     Plays the levels in order, each sandwiched between its interludes: pretext, fog clears, intro, play, outro, fog
///     returns. The fog thickens to the environment's own density between levels and clears to <see cref="PlayFog"/>,
///     just enough to show the streets and the streams. Interludes are scenes shown on <see cref="Stage"/>; the
///     campaign waits until they free themselves (see <c>code/interlude.gd</c>).
///     Keeps the camera rig over the current level and the camera focus on its selected stream.
/// </summary>
public partial class Campaign : Node3D
{
    private const float CameraSharpness = 3f;

    [Export] public PackedScene[] Levels { get; set; } = [];
    [Export] public Node3D CameraRig { get; set; }
    [Export] public Node3D Focus { get; set; }
    [Export] public WorldEnvironment World { get; set; }
    [Export] public Node Stage { get; set; }
    [Export] public float PlayFog { get; set; } = 0.08f;
    [Export] public double FogTime { get; set; } = 2;
    [Export] public double AdvanceDelay { get; set; } = 4;

    public Level Current { get; private set; }
    public int Index { get; private set; } = -1;
    public float ThickFog { get; private set; }

    private Environment _fog;
    private Tween _fade;

    public override void _Ready()
    {
        base._Ready();

        // A copy, so fading the fog never changes the shared resource.
        _fog = World.Environment = (Environment) World.Environment.Duplicate();
        ThickFog = _fog.VolumetricFogDensity;
        Load(0);
    }

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
        _fade?.Kill();
        _fog.VolumetricFogDensity = ThickFog;

        Index = index;
        Current = Levels[index].Instantiate<Level>();
        Current.Locked = true;
        AddChild(Current);

        CameraRig.GlobalPosition = Current.Center;
        Focus.GlobalPosition = Current.SelectedHead.GlobalPosition;
        Run(Current);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        var t = 1 - Mathf.Exp(-(float) delta * CameraSharpness);
        CameraRig.GlobalPosition = CameraRig.GlobalPosition.Lerp(Current.Center, t);
        Focus.GlobalPosition = Focus.GlobalPosition.Lerp(Current.SelectedHead.GlobalPosition, t);
    }

    private async void Run(Level level)
    {
        foreach (var step in Sequence(level))
        {
            await step;
            if (!IsInstanceValid(this) || level != Current) return;
        }
    }

    // After the last level, the fog stays thick.
    private IEnumerable<Task> Sequence(Level level)
    {
        yield return Show(level.Pretext);
        yield return Fade(PlayFog, Tween.EaseType.Out);
        yield return Show(level.Intro);
        level.Locked = false;

        yield return Solved(level);
        yield return Wait(AdvanceDelay);
        yield return Show(level.Outro);
        yield return Fade(ThickFog, Tween.EaseType.In);
        if (Index + 1 < Levels.Length) Load(Index + 1);
    }

    private async Task Show(PackedScene interlude)
    {
        if (interlude is null) return;

        var node = interlude.Instantiate();
        var exited = ToSignal(node, Node.SignalName.TreeExited);
        Stage.AddChild(node);
        await exited;
    }

    // Visibility falls off exponentially with density, so the density eases exponentially, too.
    private async Task Fade(float density, Tween.EaseType ease)
    {
        _fade?.Kill();
        _fade = CreateTween();
        _fade.TweenProperty(_fog, "volumetric_fog_density", density, FogTime)
            .SetTrans(Tween.TransitionType.Expo).SetEase(ease);
        await ToSignal(_fade, Tween.SignalName.Finished);
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
