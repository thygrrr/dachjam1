using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using fennecs;
using Godot;
using Vector3 = System.Numerics.Vector3;

namespace jam;

public partial class Protesters : MultiMeshInstance3D
{
    private static World World => ECS.World;
    private Stream<Matrix4X3Custom, Index, Tile, Protesters> _cubes;

    private List<Tile> _tiles = [];
    
    private float _timeAccumulator;

    public Vector3I Head => _grid.LocalToMap(_head.Position);
    
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
    
    private Node3D _head;
    private Vector3I _home;

    public override void _Ready()
    {
        base._Ready();

        _grid = GetParent<GridMap>();

        Multimesh.InstanceCount = 0;
        Multimesh.VisibleInstanceCount = 0;
        _cubes = World.Query<Matrix4X3Custom, Index, Tile, Protesters>(default, default, default, Link.With(this)).Has<Cube>().Stream();
        _indexer = World.Query<Index>().Has<Cube>().Has(Link.With(this)).Stream();

        _head = GetNode<Node3D>("Head");
        _home = Head;
        _spawner = World.Entity()
            .Add<Cube>()
            .Add<Matrix4X3Custom>()
            .Add<Index>()
            .Add(Link.With(this));
        
        _tiles.Add(new(_home));
    }


    public void ClaimTile(Vector3I point)
    {
        for (var i = 0; i < _tiles.Count-2; i++)
        {
            _tiles[i+1].Move(_tiles[i]);
        }

        _tiles[0].Move(point);

        _tiles.Add(new(_home));
        _spawner.Add(_tiles[^1]);
        _spawner.Spawn(PerTileCount);
        
        _count += PerTileCount;
        Multimesh.InstanceCount = _count;
        Multimesh.VisibleInstanceCount = _count;
        
        _head.Position = _grid.MapToLocal(point);
        
        BuildIndex();
    }

    private void BuildIndex()
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
                Emission = Mathf.Sin(uniform.time + index.Value) + 1.0f,
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
    
    private class Tile(Vector3I point)
    {
        public int X { get; private set; } = point.X;
        public int Z { get; private set; } = point.Z;

        public void Move(Vector3I other)
        {
            X = other.X;
            Z = other.Z;
        }

        public void Move(Tile other)
        {
            X = other.X;
            Z = other.Z;
        }
    }
}

