using System;
using System.Collections.Generic;
using fennecs;
using Godot;
using Vector3 = System.Numerics.Vector3;

namespace jam;

/// <summary>
///     Spawns all citizens of a level and draws the idle ones, wandering around their home cells.
/// </summary>
public partial class Passives : CitizenRenderer
{
    private readonly Dictionary<Vector3I, Entity[]> _citizens = new();
    private readonly Random _random = new(1312);
    private Stream<Citizen, Matrix4X3Custom> _cubes;

    public override void _Ready()
    {
        base._Ready();
        _cubes = World.Query<Citizen, Matrix4X3Custom>().Has(Self).Stream();
    }

    /// <summary>
    ///     Spawns <see cref="CitizenMotion.PerCell"/> idle citizens per cell and returns how many there are in total.
    /// </summary>
    public int Populate(IEnumerable<Vector3I> cells, Func<Vector3I, Vector3> center)
    {
        using var template = World.Template()
            .Add(Self)
            .Add(new Matrix4X3Custom())
            .Add(new Flow())
            .Add(new Activation())
            .Needs<Citizen>();

        foreach (var cell in cells)
        {
            var home = center(cell);
            var citizens = new Entity[CitizenMotion.PerCell];
            template.Spawn(citizens, i => new Citizen(home, i, _random.NextSingle()));
            _citizens.Add(cell, citizens);
        }

        return _citizens.Count * CitizenMotion.PerCell;
    }

    /// <summary>
    ///     The citizens who live in a cell, wherever they are drawn right now.
    /// </summary>
    public ReadOnlySpan<Entity> CitizensAt(Vector3I cell) => _citizens[cell];

    protected override void Animate(float time, float delta)
    {
        _cubes.Job(time, static (float time, ref Citizen citizen, ref Matrix4X3Custom matrix) =>
        {
            matrix = new Matrix4X3Custom(CitizenMotion.Wander(citizen, time));
        });
    }
}
