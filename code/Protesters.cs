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

    // Springs: angular frequency and damping ratio. Below 1, they overshoot a little.
    private const float GlideFrequency = 16f;
    private const float GlideDamping = 0.7f;
    private const float TurnFrequency = 14f;
    private const float TurnDamping = 0.55f;
    private const float WobbleFrequency = 22f;
    private const float WobbleDamping = 0.25f;
    private const float SpringStep = 1f / 240;

    private const float HoverHeight = 0.2f;
    private const float BobHeight = 0.04f;
    private const float HopPerSpeed = 0.035f;
    private const float MaxHop = 0.3f;
    private const float StretchPerSpeed = 0.05f;
    private const float MaxStretch = 0.5f;
    private const float Sway = 0.35f;
    private const float BankPerTurnRate = 0.06f;
    private const float MaxBank = 0.8f;
    private const float BumpSpeed = 8f;
    private const float BumpWobble = 6f;
    private const float SelectWobble = 4f;

    [Export] public Color Tint { get; set; } = new(0.837501f, 0.484983f, 0f);

    public Vector3I Start { get; private set; }
    public Vector3I? End { get; private set; }
    public Node3D Head => _head;

    public bool Selected
    {
        get => _head.Visible;
        set
        {
            if (value && !_head.Visible) _wobbleVelocity += SelectWobble;
            _head.Visible = value;
        }
    }

    private GridMap _grid;
    private Node3D _head;
    private Node3D _arrow;
    private Node3D _ring;
    private Transform3D _arrowRest;

    // The head glides over the ground; the arrow floats above it, and alone carries the flourishes, so the camera,
    // which follows the head, stays steady.
    private Godot.Vector3 _headGoal;
    private Godot.Vector3 _headVelocity;
    private float _yawGoal;
    private float _yaw;
    private float _yawVelocity;
    private float _wobble;
    private float _wobbleVelocity;
    private float _celebratedAt = float.NaN;
    private Passives _passives;

    private Stream<Citizen, Flow, Activation, Startle, Matrix4X3Custom> _cubes;
    private Stream<Activation> _activations;

    public override void _Ready()
    {
        base._Ready();

        _grid = GetParent<GridMap>();
        _head = GetNode<Node3D>("Head");
        _arrow = _head.GetNode<Node3D>("Arrow");
        _ring = _head.GetNode<Node3D>("Ring");
        _arrowRest = _arrow.Transform;
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
        _yawGoal = _yaw = _head.Rotation.Y;
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
        // Always turn the short way round.
        if (facing != Vector3I.Zero) _yawGoal = _yaw + Mathf.Wrap(MathF.Atan2(facing.X, facing.Z) - _yaw, -MathF.PI, MathF.PI);
    }

    /// <summary>
    ///     Flings the head towards a blocked cell and squashes the arrow; both spring back on their own.
    /// </summary>
    public void Bump(Vector3I direction)
    {
        _headVelocity += new Godot.Vector3(direction.X, 0, direction.Z) * BumpSpeed;
        _wobbleVelocity -= BumpWobble;
    }

    /// <summary>
    ///     Sends a wave of light along the trail, from its start to its head.
    /// </summary>
    public void Celebrate() => _celebratedAt = Now;

    public override void _Process(double delta)
    {
        base._Process(delta);
        HandBack();

        var dt = (float) delta;
        var position = _head.Position;
        Spring(ref position, ref _headVelocity, _headGoal, GlideFrequency, GlideDamping, dt);
        Spring(ref _yaw, ref _yawVelocity, _yawGoal, TurnFrequency, TurnDamping, dt);
        Spring(ref _wobble, ref _wobbleVelocity, 0, WobbleFrequency, WobbleDamping, dt);
        _head.Position = position;
        _head.Rotation = _head.Rotation with { Y = _yaw };

        AnimateArrow(Now);
    }

    // Hovers and sways the arrow, hops and stretches it along its motion, banks it into turns, and squashes it on bumps.
    // The ring on the ground shrinks as the arrow rises.
    private void AnimateArrow(float time)
    {
        var speed = _headVelocity.Length();
        var hop = MathF.Min(speed * HopPerSpeed, MaxHop);
        var lift = HoverHeight + BobHeight * MathF.Sin(time * 2.4f) + hop;

        var bank = Math.Clamp(-_yawVelocity * BankPerTurnRate, -MaxBank, MaxBank);
        var roll = new Basis(Godot.Vector3.Back, Sway * MathF.Sin(time * 1.7f) + bank);

        var motion = _headVelocity.Rotated(Godot.Vector3.Up, -_yaw);
        var stretch = Stretch(motion, MathF.Min(speed * StretchPerSpeed, MaxStretch))
                      * Stretch(Godot.Vector3.Back, MathF.Max(_wobble, -0.5f));

        _arrow.Transform = new Transform3D(stretch * roll * _arrowRest.Basis, _arrowRest.Origin + Godot.Vector3.Up * lift);
        _ring.Scale = Godot.Vector3.One * (1 - hop);
    }

    protected override void Animate(float time, float delta)
    {
        var celebration = float.IsNaN(_celebratedAt) ? -1 : time - _celebratedAt;

        // Must be for for web builds, who can't do Job.
        _cubes.For((time, delta, celebration), static ((float time, float delta, float celebration) uniform,
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

    // Damped spring, substepped so it stays stable at low frame rates.
    private static void Spring(ref Godot.Vector3 value, ref Godot.Vector3 velocity, Godot.Vector3 goal, float frequency, float damping, float delta)
    {
        var steps = Math.Max(1, Mathf.CeilToInt(delta / SpringStep));
        var h = delta / steps;
        for (var i = 0; i < steps; i++)
        {
            velocity += (frequency * frequency * (goal - value) - 2 * damping * frequency * velocity) * h;
            value += velocity * h;
        }
    }

    private static void Spring(ref float value, ref float velocity, float goal, float frequency, float damping, float delta)
    {
        var steps = Math.Max(1, Mathf.CeilToInt(delta / SpringStep));
        var h = delta / steps;
        for (var i = 0; i < steps; i++)
        {
            velocity += (frequency * frequency * (goal - value) - 2 * damping * frequency * velocity) * h;
            value += velocity * h;
        }
    }

    // Volume-preserving scale: stretches by 1 + amount along the axis, and squashes evenly across it.
    private static Basis Stretch(Godot.Vector3 axis, float amount)
    {
        if (axis.LengthSquared() < 1e-8f) return Basis.Identity;

        var d = axis.Normalized();
        var along = 1 + amount;
        var across = 1 / MathF.Sqrt(along);
        return new Basis(
            across * Godot.Vector3.Right + (along - across) * d.X * d,
            across * Godot.Vector3.Up + (along - across) * d.Y * d,
            across * Godot.Vector3.Back + (along - across) * d.Z * d);
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
