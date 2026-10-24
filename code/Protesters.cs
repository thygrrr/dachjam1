using System;
using System.Linq;
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
    private const float BumpDistance = 0.25f;

    [Export] public Color Tint { get; set; } = new(0.837501f, 0.484983f, 0f);

    public Vector3I Start { get; private set; }
    public Vector3I? End { get; private set; }
    public Node3D Head => _head;

    public bool Selected
    {
        get => _head.Visible;
        set => _head.Visible = value;
    }

    private GridMap _grid;
    private Node3D _head;
    private Godot.Vector3 _headGoal;
    private float _headYaw;
    private float _celebratedAt = float.NaN;
    private Passives _passives;

    private Stream<Citizen, Flow, Activation, Startle, Matrix4X3Custom> _cubes;
    private Stream<Activation> _activations;

    public override void _Ready()
    {
        base._Ready();

        _grid = GetParent<GridMap>();
        _head = GetNode<Node3D>("Head");
        var start = GetNode<GeometryInstance3D>("Start");
        var end = GetNodeOrNull<GeometryInstance3D>("End");
        Start = CellOf(start);
        End = end is not null ? CellOf(end) : null;

        SetInstanceShaderParameter("tint", Tint);
        foreach (var marker in _head.GetChildren().OfType<GeometryInstance3D>().Append(start).Append(end)) Paint(marker);

        _cubes = World.Query<Citizen, Flow, Activation, Startle, Matrix4X3Custom>().Has(Self).Stream();
        _activations = World.Query<Activation>().Has(Self).Stream();
    }

    /// <summary>
    ///     Recruits the start cell. Released citizens return to <paramref name="passives"/>.
    /// </summary>
    public void Begin(Passives passives)
    {
        _passives = passives;
        Join(Start, Vector3.Zero, 0);
        _headGoal = _head.Position = _grid.MapToLocal(Start);
        _headYaw = _head.Rotation.Y;
    }

    /// <summary>
    ///     Recruits a cell's citizens into trail position <paramref name="slot"/>; <paramref name="entry"/> points to
    ///     the previous cell of the trail.
    /// </summary>
    public void Join(Vector3I cell, Vector3 entry, int slot)
    {
        foreach (var citizen in _passives.CitizensAt(cell))
        {
            Adopt(citizen);
            citizen.Ref<Flow>() = new Flow(entry, Vector3.Zero, Vector3.Zero, slot);
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

    /// <summary>
    ///     Nudges the head towards a blocked cell; it springs back on its own.
    /// </summary>
    public void Bump(Vector3I direction) => _head.Position += new Godot.Vector3(direction.X, 0, direction.Z) * BumpDistance;

    /// <summary>
    ///     Sends a wave of light along the trail, from its start to its head.
    /// </summary>
    public void Celebrate() => _celebratedAt = Now;

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
        var celebration = float.IsNaN(_celebratedAt) ? -1 : time - _celebratedAt;

        _cubes.Job((time, delta, celebration), static ((float time, float delta, float celebration) uniform,
            ref Citizen citizen, ref Flow flow, ref Activation activation, ref Startle startle, ref Matrix4X3Custom matrix) =>
        {
            flow.Out = Vector3.Lerp(flow.Out, flow.OutGoal, 1 - MathF.Exp(-uniform.delta * SteerSharpness));
            activation.Value = CitizenMotion.MoveTowards(activation.Value, activation.Goal, uniform.delta * ActivationRate);

            var pulse = 0f;
            var afterglow = 0f;
            if (uniform.celebration >= 0) pulse = CitizenMotion.Wave(flow.Slot, uniform.celebration, out afterglow);

            var blend = CitizenMotion.SmoothStep(activation.Value);
            var swirl = CitizenMotion.Swirl(citizen, flow, uniform.time, out var scale);
            var position = Vector3.Lerp(CitizenMotion.Wander(citizen, uniform.time), swirl, blend)
                           + CitizenMotion.Flinch(citizen, ref startle, uniform.delta)
                           + Vector3.UnitY * (0.3f * pulse * blend);

            matrix = new Matrix4X3Custom(position, float.Lerp(1, scale, blend) * (1 + 0.5f * pulse))
            {
                Emission = blend * CitizenMotion.Glow(citizen, uniform.time) * (1 + 3 * pulse + afterglow),
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

    // Recolors a glowing marker's emission with the stream's tint.
    private void Paint(GeometryInstance3D marker)
    {
        if (marker is not MeshInstance3D { Mesh: { } mesh } || mesh.SurfaceGetMaterial(0) is not BaseMaterial3D material) return;

        var tinted = (BaseMaterial3D) material.Duplicate();
        tinted.Emission = Tint;
        marker.MaterialOverride = tinted;
    }

    private Vector3I CellOf(Node3D marker) => _grid.LocalToMap(_grid.ToLocal(marker.GlobalPosition));
}
