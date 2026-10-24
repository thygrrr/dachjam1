using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace jam;

public readonly record struct TrailSpec(Vector3I Start, Vector3I? End = null);

public enum MoveKind
{
    Blocked,
    Extended,
    Retracted,
}

/// <summary>
///     Outcome of a move: the cell a trail entered, left, or was blocked from.
/// </summary>
public readonly record struct Move(MoveKind Kind, Vector3I Cell);

/// <summary>
///     One stream's path from its start. If it has an <see cref="End"/>, it must finish there.
/// </summary>
public sealed class Trail(Vector3I start, Vector3I? end)
{
    private readonly List<Vector3I> _cells = [start];

    public Vector3I? End { get; } = end;
    public IReadOnlyList<Vector3I> Cells => _cells;
    public Vector3I Start => _cells[0];
    public Vector3I Head => _cells[^1];
    public bool Complete => End is not { } goal || Head == goal;

    internal void Push(Vector3I cell) => _cells.Add(cell);
    internal void Pop() => _cells.RemoveAt(_cells.Count - 1);
}

/// <summary>
///     A Hamiltonian path puzzle: the trails together must cover every board cell exactly once.
/// </summary>
public sealed class Puzzle
{
    private readonly Trail[] _trails;
    private readonly Dictionary<Vector3I, Trail> _owners = new();

    public Board Board { get; }
    public IReadOnlyList<Trail> Trails => _trails;
    public bool IsSolved => _owners.Count == Board.Count && _trails.All(trail => trail.Complete);

    public Puzzle(Board board, IEnumerable<TrailSpec> trails)
    {
        Board = board;
        _trails = trails.Select(spec => new Trail(spec.Start, spec.End)).ToArray();

        foreach (var trail in _trails)
        {
            if (!board.Contains(trail.Start)) throw new ArgumentException($"Trail start {trail.Start} is not on the board.");
            if (!_owners.TryAdd(trail.Start, trail)) throw new ArgumentException($"Two trails start at {trail.Start}.");
        }

        foreach (var trail in _trails)
        {
            if (trail.End is not { } end) continue;
            if (!board.Contains(end)) throw new ArgumentException($"Trail end {end} is not on the board.");
            if (_owners.ContainsKey(end)) throw new ArgumentException($"Trail end {end} is another trail's start.");
        }
    }

    /// <summary>
    ///     Steps a trail's head one cell. Stepping back onto the previous cell retracts the trail instead.
    /// </summary>
    public Move TryMove(int trailIndex, Vector3I direction)
    {
        var trail = _trails[trailIndex];
        var target = trail.Head + direction;

        if (trail.Cells.Count > 1 && target == trail.Cells[^2]) return Retract(trailIndex);
        if (!CanEnter(trail, target)) return new Move(MoveKind.Blocked, target);

        trail.Push(target);
        _owners.Add(target, trail);
        return new Move(MoveKind.Extended, target);
    }

    /// <summary>
    ///     Removes a trail's head cell. A trail never retracts past its start.
    /// </summary>
    public Move Retract(int trailIndex)
    {
        var trail = _trails[trailIndex];
        var head = trail.Head;
        if (trail.Cells.Count == 1) return new Move(MoveKind.Blocked, head);

        trail.Pop();
        _owners.Remove(head);
        return new Move(MoveKind.Retracted, head);
    }

    private bool CanEnter(Trail trail, Vector3I cell) =>
        trail.Head != trail.End
        && Board.Contains(cell)
        && !_owners.ContainsKey(cell)
        && !_trails.Any(other => other != trail && other.End == cell);
}
