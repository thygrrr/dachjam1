using System;
using fennecs;
using Godot;
using Vector3 = System.Numerics.Vector3;

namespace jam;

/// <summary>
///     One stream of thought: draws the citizens recruited into its trail, swirling along it in <see cref="Tint"/>.
///     Expects child markers <c>Start</c> (required) and <c>End</c> (optional), a <c>Head</c>, and a parent GridMap.
/// </summary>
public partial class Protesters : CitizenRenderer
{
    private const float SteerSharpness = 10f;
    private const float ActivationRate = 2.5f;
    private const float HeadSharpness = 20f;

    [Export] public Color Tint { get; set; } = new(0.837501f, 0.484983f, 0f);

    public Vector3I Start { get; private set; }
    public Vector3I? End { get; private set; }

    public bool Selected
    {
        get => _head.Visible;
        set => _head.Visible = value;
    }

    private GridMap _grid;
    private Node3D _head;
    private Godot.Vector3 _headGoal;
    private float _headYaw;
    private Passives _passives;

    private Stream<Citizen, Flow, Activation, Matrix4X3Custom> _cubes;
    private Stream<Activation> _activations;

    public override void _Ready()
    {
        base._Ready();

        _grid = GetParent<GridMap>();
        _head = GetNode<Node3D>("Head");
        Start = CellOf(GetNode<Node3D>("Start"));
        End = GetNodeOrNull<Node3D>("End") is { } end ? CellOf(end) : null;
        SetInstanceShaderParameter("tint", Tint);

        _cubes = World.Query<Citizen, Flow, Activation, Matrix4X3Custom>().Has(Self).Stream();
        _activations = World.Query<Activation>().Has(Self).Stream();
    }

    /// <summary>
    ///     Recruits the start cell. Released citizens return to <paramref name="passives"/>.
    /// </summary>
    public void Begin(Passives passives)
    {
        _passives = passives;
        Join(Start, Vector3.Zero);
        _headGoal = _head.Position = _grid.MapToLocal(Start);
        _headYaw = _head.Rotation.Y;
    }

    /// <summary>
    ///     Recruits a cell's citizens; <paramref name="entry"/> points to the previous cell of the trail.
    /// </summary>
    public void Join(Vector3I cell, Vector3 entry)
    {
        foreach (var citizen in _passives.CitizensAt(cell))
        {
            Adopt(citizen);
            citizen.Ref<Flow>() = new Flow(entry, Vector3.Zero, Vector3.Zero);
            citizen.Ref<Activation>().Goal = 1;
        }
    }

    /// <summary>
    ///     Lets a cell's citizens fade out; once idle, they return to the passives.
    /// </summary>
    public void Leave(Vector3I cell)
    {
        foreach (var citizen in _passives.CitizensAt(cell)) citizen.Ref<Activation>().Goal = 0;
    }

    /// <summary>
    ///     Points a cell's flow at the next cell of the trail, or ends it there for a zero <paramref name="exit"/>.
    /// </summary>
    public void Steer(Vector3I cell, Vector3 exit)
    {
        foreach (var citizen in _passives.CitizensAt(cell)) citizen.Ref<Flow>().OutGoal = exit;
    }

    public void MoveHead(Vector3I cell, Vector3I facing)
    {
        _headGoal = _grid.MapToLocal(cell);
        if (facing != Vector3I.Zero) _headYaw = MathF.Atan2(facing.X, facing.Z);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        HandBack();

        var t = 1 - MathF.Exp(-(float) delta * HeadSharpness);
        _head.Position = _head.Position.Lerp(_headGoal, t);
        _head.Rotation = _head.Rotation with { Y = Mathf.LerpAngle(_head.Rotation.Y, _headYaw, t) };
    }

    protected override void Animate(float time, float delta)
    {
        _cubes.Job((time, delta), static ((float time, float delta) uniform,
            ref Citizen citizen, ref Flow flow, ref Activation activation, ref Matrix4X3Custom matrix) =>
        {
            flow.Out = Vector3.Lerp(flow.Out, flow.OutGoal, 1 - MathF.Exp(-uniform.delta * SteerSharpness));
            activation.Value = CitizenMotion.MoveTowards(activation.Value, activation.Goal, uniform.delta * ActivationRate);

            var blend = CitizenMotion.SmoothStep(activation.Value);
            var swirl = CitizenMotion.Swirl(citizen, flow, uniform.time, out var scale);
            var position = Vector3.Lerp(CitizenMotion.Wander(citizen, uniform.time), swirl, blend);

            matrix = new Matrix4X3Custom(position, float.Lerp(1, scale, blend))
            {
                Emission = blend * CitizenMotion.Glow(citizen, uniform.time),
                Activation = blend,
            };
        });
    }

    // Faded-out citizens wander exactly like passives, so handing them over is seamless.
    private void HandBack()
    {
        _activations.For((in EntityRef citizen, ref Activation activation) =>
        {
            if (activation is { Goal: 0, Value: 0 }) _passives.Adopt(citizen.Entity);
        });
    }

    private Vector3I CellOf(Node3D marker) => _grid.LocalToMap(_grid.ToLocal(marker.GlobalPosition));
}
