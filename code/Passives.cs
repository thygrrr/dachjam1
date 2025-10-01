using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using fennecs;
using Godot;
using Vector3 = System.Numerics.Vector3;

namespace jam;

public partial class Passives : MultiMeshInstance3D
{
    private static World World => ECS.World;
    private Stream<Matrix4X3Custom, Index, Tile, Passives> _cubes;

    private List<Tile> _tiles = [];
    
    private float _timeAccumulator;
    
    
    private FastNoiseLite _noiseLite = new()
    {
        Frequency = 0.05f,
    };

    private int _count;
    private const int PerTileCount = 25;
    
    private readonly Random _random = new();
    private EntitySpawner _spawner;
    private Stream<Index> _indexer;
    private GridMap _grid;
    
    public override void _Ready()
    {
        base._Ready();

        _grid = GetParent<GridMap>();

        Multimesh.InstanceCount = 0;
        Multimesh.VisibleInstanceCount = 0;
        _cubes = World.Query<Matrix4X3Custom, Index, Tile, Passives>(default, default, default, Link.With(this)).Has<Cube>().Stream();
        _indexer = World.Query<Index>().Has<Cube>().Has(Link.With(this)).Stream();

        _spawner = World.Entity()
            .Add<Cube>()
            .Add<Matrix4X3Custom>()
            .Add<Tile>()
            .Add<Index>()
            .Add(Link.With(this));
    }

    public void PopulateTile(Vector3I tile)
    {
        _spawner.Add(new Tile(tile.X, tile.Z));
        _spawner.Spawn(PerTileCount);
        
        _count += PerTileCount;
        Multimesh.InstanceCount = _count;
        Multimesh.VisibleInstanceCount = _count;
    }
    
    public void BuildIndex()
    {
        var i = 0;
        _indexer.For((ref Index index) => index.Value = i++);
    }
    
    public override void _Process(double delta)
    {
        base._Process(delta);

        var dt = (float) delta;
        _timeAccumulator += dt;
        
        _cubes.Job(
            uniform: (_timeAccumulator, _noiseLite), 
            action: ((float time, FastNoiseLite noise) uniform, ref Matrix4X3Custom matrix, ref Index index, ref Tile tile) =>
        {
            var iv = index.Value * 20f;
            var jv = index.Value * 30f;
            
            var brownianX = uniform.noise.GetNoise3D(tile.X, uniform.time, tile.Z + jv) * 0.8f; 
            var brownianY = uniform.noise.GetNoise3D(tile.X + iv, tile.Z, uniform.time) * 0.01f; 
            var brownianZ = uniform.noise.GetNoise3D(tile.X, tile.Z + iv, uniform.time) * 0.8f; 
            var pos = new Vector3(tile.X , 0, tile.Z) + new Vector3(brownianX, brownianY, brownianZ);
            matrix = new(pos)
            {
                Emission = 0, //Mathf.Sin(uniform.time + index.Value) + 1.0f,
            };
        });
        
        _cubes.Raw(
            uniform: (Multimesh.GetRid(), _count * Matrix4X3Custom.SizeInFloats),
            action: static ((Rid mesh, int count) uniform, Memory<Matrix4X3Custom> transforms) =>
            {
                var floatSpan = MemoryMarshal.Cast<Matrix4X3Custom, float>(transforms.Span);
                RenderingServer.MultimeshSetBuffer(uniform.mesh, floatSpan[..uniform.count]);
            });
    }
    
    private record struct Cube;

    private record struct Index(int Value);
    
    private record struct Tile(int X, int Z)
    {
        
    }
}

