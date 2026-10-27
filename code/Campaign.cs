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

    /// <summary>
    ///     Glow settings for the Mobile renderer. Its 10-bit color buffer clamps HDR at twice white, so the emissive
    ///     overshoot that drives the halo on Forward+ is mostly gone before the glow pass; a zero threshold with more
    ///     strength and bloom brings the halo back to a similar size. Tuned by eye on Vulkan and WebGPU Mobile.
    /// </summary>
    public const float MobileGlowHdrThreshold = 0;
    public const float MobileGlowStrength = 1.2f;
    public const float MobileGlowBloom = 0.6f;

    /// <summary>
    ///     Classic fog standing in for the volumetric fog on renderers without it (Mobile, Compatibility). The
    ///     volumetric fog fills the streets between the blocks, so an exponential depth fog carries the fade and a
    ///     height fog below the block tops keeps the streets hazier than the roofs. Densities are the volumetric
    ///     density times <see cref="FallbackFogDensityScale"/>; the camera sits about 8 units from the board.
    /// </summary>
    public const float FallbackFogDensityScale = 0.5f;
    public const float FallbackFogHeight = 2f;
    public const float FallbackFogHeightDensity = 1f;

    /// <summary>Whether the fades drive the volumetric fog (Forward+) or the classic fallback fog.</summary>
    public bool VolumetricFog { get; private set; } = true;

    private Environment _fog;
    private Tween _fade;

    public override void _Ready()
    {
        base._Ready();

        // A copy, so fading the fog never changes the shared resource.
        _fog = World.Environment = (Environment) World.Environment.Duplicate();
        ThickFog = _fog.VolumetricFogDensity;
        var method = RenderingServer.GetCurrentRenderingMethod();
        TuneGlow(_fog, method);
        VolumetricFog = TuneFog(_fog, method);
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
    ///     Keeps the volumetric fog on Forward+ (returns true); elsewhere swaps it for the classic fallback fog in the
    ///     volumetric fog's color and returns false. The density is set separately, see <see cref="SetFogDensity"/>.
    /// </summary>
    public static bool TuneFog(Environment environment, string renderingMethod)
    {
        if (renderingMethod == "forward_plus") return true;
        environment.VolumetricFogEnabled = false;
        environment.FogEnabled = true;
        environment.FogMode = Environment.FogModeEnum.Exponential;
        environment.FogLightColor = environment.VolumetricFogAlbedo;
        environment.FogLightEnergy = 1;
        environment.FogSunScatter = 0;
        environment.FogAerialPerspective = 0;
        environment.FogSkyAffect = 1;
        environment.FogHeight = FallbackFogHeight;
        environment.FogHeightDensity = FallbackFogHeightDensity;
        return false;
    }

    /// <summary>Applies a volumetric fog density, scaled for the fallback fog where that is in use.</summary>
    public static void SetFogDensity(Environment environment, bool volumetric, float density)
    {
        if (volumetric) environment.VolumetricFogDensity = density;
        else environment.FogDensity = density * FallbackFogDensityScale;
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
        SetFogDensity(_fog, VolumetricFog, ThickFog);

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
        var property = VolumetricFog ? "volumetric_fog_density" : "fog_density";
        var target = VolumetricFog ? density : density * FallbackFogDensityScale;
        _fade.TweenProperty(_fog, property, target, FogTime)
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
