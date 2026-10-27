using System;
using System.Linq;
using Godot;
using Vector3 = System.Numerics.Vector3;

namespace jam;

/// <summary>
///     A puzzle level. The empty GridMap cells reachable from the streams' starts form the board, and the streams must
///     cover all of it. Arrow keys steer the selected stream, Tab selects the next one, and R restarts.
///     Expects a <see cref="Passives"/> child and one or more <see cref="Protesters"/> children.
///     Its interludes are played around it by the <see cref="Campaign"/>.
/// </summary>
public partial class Level : GridMap
{
    [Signal]
    public delegate void SolvedEventHandler();

    public Puzzle Puzzle { get; private set; }

    /// <summary>
    ///     Global center of the board's bounding box.
    /// </summary>
    public Godot.Vector3 Center { get; private set; }

    public Node3D SelectedHead => _streams[_selected].Head;

    /// <summary>
    ///     A locked level ignores input.
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    ///     Shown in thick fog, before the fog clears.
    /// </summary>
    [ExportGroup("Interludes")]
    [Export] public PackedScene Pretext { get; set; }

    /// <summary>
    ///     Shown once the fog cleared, before play begins.
    /// </summary>
    [Export] public PackedScene Intro { get; set; }

    /// <summary>
    ///     Shown after the level is solved, before the fog returns.
    /// </summary>
    [Export] public PackedScene Outro { get; set; }

    private Passives _passives;
    private Protesters[] _streams;
    private int _selected;

    public override void _Ready()
    {
        base._Ready();

        _passives = GetNode<Passives>("Passives");
        _streams = GetChildren().OfType<Protesters>().ToArray();
        if (_streams.Length == 0) throw new InvalidOperationException($"Level {Name} has no Protesters.");

        var board = Board.FloodFill(_streams.Select(stream => stream.Start), IsOpen(out var bounds));
        Puzzle = new Puzzle(board, _streams.Select(stream => new TrailSpec(stream.Start, stream.End)));

        var min = new Vector3I(board.Cells.Min(cell => cell.X), 0, board.Cells.Min(cell => cell.Z));
        var max = new Vector3I(board.Cells.Max(cell => cell.X), 0, board.Cells.Max(cell => cell.Z));
        Center = ToGlobal((MapToLocal(min) + MapToLocal(max)) / 2);

        var capacity = _passives.Populate(board.Cells, cell => MapToLocal(cell).ToNumerics());
        foreach (var renderer in GetChildren().OfType<CitizenRenderer>()) renderer.Allocate(capacity, bounds);
        foreach (var stream in _streams) stream.Begin(_passives);

        Select(0);
    }

    public override void _UnhandledInput(InputEvent ev)
    {
        if (Locked || Puzzle.IsSolved) return;

        if (ev.IsActionPressed("ui_focus_next")) Select(_selected + 1);
        else if (ev.IsActionPressed("restart")) Restart();
        else if (Steering(ev) is { } direction) Step(_selected, direction);
        else return;

        GetViewport().SetInputAsHandled();
    }

    public MoveKind Step(int stream, Vector3I direction)
    {
        var head = Puzzle.Trails[stream].Head;
        var move = Puzzle.TryMove(stream, direction);
        Apply(stream, move);

        if (move.Kind == MoveKind.Blocked)
        {
            _streams[stream].Bump(direction);
            _passives.Startle(move.Cell, Offset(head, move.Cell));
        }
        else if (Puzzle.IsSolved)
        {
            foreach (var each in _streams) each.Celebrate();
            EmitSignal(SignalName.Solved);
        }

        return move.Kind;
    }

    public void Restart()
    {
        for (var i = 0; i < _streams.Length; i++)
        {
            while (Puzzle.Trails[i].Cells.Count > 1) Apply(i, Puzzle.Retract(i));
        }
    }

    private void Select(int index)
    {
        _selected = index % _streams.Length;
        for (var i = 0; i < _streams.Length; i++) _streams[i].Selected = i == _selected;
    }

    private void Apply(int index, Move move)
    {
        var stream = _streams[index];
        var trail = Puzzle.Trails[index];

        switch (move.Kind)
        {
            case MoveKind.Extended:
                var previous = trail.Cells[^2];
                stream.Steer(previous, Offset(previous, move.Cell));
                stream.Join(move.Cell, Offset(move.Cell, previous), trail.Cells.Count - 1);
                break;
            case MoveKind.Retracted:
                stream.Leave(move.Cell);
                stream.Steer(trail.Head, Vector3.Zero);
                break;
            default:
                return;
        }

        stream.MoveHead(trail.Head, trail.Cells.Count > 1 ? trail.Head - trail.Cells[^2] : Vector3I.Zero);
    }

    // Input is rotated to match the camera. Key-repeat echoes are ignored: one step per press.
    private static Vector3I? Steering(InputEvent ev)
    {
        if (ev.IsActionPressed("ui_up")) return Vector3I.Right;
        if (ev.IsActionPressed("ui_down")) return Vector3I.Left;
        if (ev.IsActionPressed("ui_left")) return Vector3I.Forward;
        if (ev.IsActionPressed("ui_right")) return Vector3I.Back;
        return null;
    }

    private Vector3 Offset(Vector3I from, Vector3I to) => (MapToLocal(to) - MapToLocal(from)).ToNumerics();

    // Open cells are empty cells within the bounding box of the level's blocks.
    private Func<Vector3I, bool> IsOpen(out Aabb bounds)
    {
        var used = GetUsedCells();
        var min = new Vector3I(used.Min(cell => cell.X), 0, used.Min(cell => cell.Z));
        var max = new Vector3I(used.Max(cell => cell.X), 0, used.Max(cell => cell.Z));

        var corner = MapToLocal(min) - CellSize;
        bounds = new Aabb(corner, MapToLocal(max) + 2 * CellSize - corner);

        return cell => cell.X >= min.X && cell.X <= max.X && cell.Z >= min.Z && cell.Z <= max.Z
                       && GetCellItem(cell) == InvalidCellItem;
    }
}
