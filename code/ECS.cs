using Godot;
using System;
using fennecs;
public partial class ECS : Node3D
{
    private World _world;

    public override void _EnterTree()
    {
        _world = new World();
    }
    public override void _ExitTree()
    {
        base._ExitTree();
        _world.Dispose();
        _world = null;
    }
}
