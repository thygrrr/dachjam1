using System.Diagnostics;
using System.Linq;
using Godot;

namespace jam.code;

public partial class Level : StaticBody3D
{
    private GridMap _grid;
    private Cubes _cubes;

    public override void _Ready()
    {
        base._Ready();
        _grid = GetNode<GridMap>("GridMap");
        _cubes = GetNode<Cubes>("Cubes");
        
        var cells = _grid.GetUsedCells();

        var min_x = cells.MinBy(t => t.X).X;
        var min_z = cells.MinBy(t => t.Z).Z;

        var max_x = cells.MaxBy(t => t.X).X;
        var max_z = cells.MaxBy(t => t.Z).Z;
        
        for (var x = min_x; x <= max_x; x++)
             for (var z = min_z; z <= max_z; z++)
             {
                 var cell = new Vector3I(x, 0, z);
                 var cellItem = _grid.GetCellItem(cell);
                 if (cellItem == -1)
                 {
                     _cubes.PopulateTile(cell);
                 }
             }
        
        _cubes.BuildIndex();
    }

    public override void _Input(InputEvent ev)
    {
        var camera = GetViewport().GetCamera3D();
        
        var (origin, direction) = camera.MouseRay();
        
        var plane = new Plane(Vector3.Up, Vector3.Zero);
        if (!plane.IntersectsRay(origin, direction, out var intersection)) return;
        
        var local = _grid.LocalToMap(intersection);
        GD.Print($"Local: {local} {_grid.GetCellItem(local)}");
            
        /*
            var cell = LocalToMap()
            GD.Print(cell);
            if (cell != -1)
            {
                var cellItem = GetCellItem(cell);
                GD.Print(cellItem);
                if (cellItem != -1)
                {
                    SetCellItem(cell, -1);
                }
                else
                {
                    SetCellItem(cell, 0);
                }
            }
            */

    }
}