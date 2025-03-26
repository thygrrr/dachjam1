using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using fennecs;
using Godot;
using Vector3 = System.Numerics.Vector3;

namespace jam;

public partial class Cubes : MultiMeshInstance3D
{
    private static World World => ECS.World;
    private Stream<Matrix4X3, Index, Tile, Cubes> _cubes;

    private List<Tile> _tiles = [];
    
    private float _timeAccumulator;
    
    
    private FastNoiseLite _noiseLite = new()
    {
        Frequency = 0.025f,
    };
    
    private const int Count = 1000;
    
    private readonly Random _random = new();
    public override void _Ready()
    {
        base._Ready();
        Multimesh.InstanceCount = Count;
        Multimesh.VisibleInstanceCount = Count;
        _cubes = World.Query<Matrix4X3, Index, Tile, Cubes>(default, default, default, Link.With(this)).Has<Cube>().Stream();

        using var spawner = World.Entity()
            .Add<Cube>()
            .Add<Matrix4X3>()
            .Add<Index>()
            .Add<Tile>()
            .Add(Link.With(this))
            .Spawn(Count);
        
        var i = 0;
        _cubes.For((ref Matrix4X3 matrix, ref Index index, ref Tile tile) =>
        {
            matrix = new(new(_random.NextSingle() * 5, 0, _random.NextSingle() * 5), 1);
            index = new(i++);
            var j = i/20;
            tile = new(j%20, j/20);
        });
    }
    
    public override void _Process(double delta)
    {
        var dt = (float) delta;
        
        base._Process(delta);
        _timeAccumulator += dt;
        
        _cubes.For((_timeAccumulator, _noiseLite), ((float time, FastNoiseLite noise) uniform, ref Matrix4X3 matrix, ref Index index, ref Tile tile) =>
        {
            float iv = (float) index.Value * 20f;
            float jv = (float) index.Value * 30f;
            var brownianX = uniform.noise.GetNoise3D(tile.X, uniform.time, tile.Z + jv); 
            var brownianZ = uniform.noise.GetNoise3D(tile.X, tile.Z + iv, uniform.time); 
            var pos = new Vector3(tile.X , 0, tile.Z) + new Vector3(brownianX, 0, brownianZ);
            matrix = new(pos);
        });
        
        _cubes.Raw(
            uniform: (Multimesh.GetRid(), Count * Matrix4X3.SizeInFloats),
            action:  static ((Rid mesh, int count) uniform, Memory<Matrix4X3> transforms) =>
            {
                var floatSpan = MemoryMarshal.Cast<Matrix4X3, float>(transforms.Span);
                RenderingServer.MultimeshSetBuffer(uniform.mesh, floatSpan[..uniform.count]);
            });

    }
    
    private record struct Cube;

    private record struct Index(int Value);
    
    private record struct Tile(int X, int Z)
    {
    }
}

