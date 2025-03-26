using Godot;

namespace jam.code;

public partial class Grid : Node3D
{
    [Export] private int _size = 20;

    private Node3D[][] _nodes;

    public override void _EnterTree()
    {
        base._EnterTree();
        _nodes = new Node3D[_size][];
        for (var i = 0; i < _nodes.Length; i++)
        {
            _nodes[i] = new Node3D[_size];
        }
    }
    
    private void _AddNode(Node3D node, int x, int y)
    {
        _nodes[x][y] = node;
        AddChild(node);
        //node.Translation = new Vector3(x, 0, y);
    }
}