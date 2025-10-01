using Godot;
using Godot.Collections;

namespace jam;

public partial class Level : GridMap
{
    private Protesters _protesters;

    private Dictionary<Vector3I, Protesters> _occupation = new();
    
    public override void _Ready()
    {
        base._Ready();
        _protesters = GetNode<Protesters>("Protesters");
        
        var cells = GetUsedCells();

        _occupation[_protesters.Head] = _protesters;
    }

    public override void _Input(InputEvent ev)
    {
        if (!ev.IsActionType()) return;
        
        var head = _protesters.Head;
        Vector3I direction;
        
        if (ev.IsActionPressed("ui_up"))
        {
            direction = Vector3I.Right;
        }
        else if (ev.IsActionPressed("ui_down"))
        {
            direction = Vector3I.Left;
        }
        else if (ev.IsActionPressed("ui_left"))
        {
            direction = Vector3I.Forward;
        }
        else if (ev.IsActionPressed("ui_right"))
        {
            direction = Vector3I.Back;
        }
        else return;

        var destination = head + direction;
        if (Occupied(destination)) return;

        _protesters.ClaimTile(destination);
        _occupation[destination] = _protesters;
    }

    private bool Occupied(Vector3I destination)
    {
        return _occupation.TryGetValue(destination, out _) || GetCellItem(destination) != -1;
    }
}