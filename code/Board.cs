using System;
using System.Collections.Generic;
using Godot;

namespace jam;

/// <summary>
///     The walkable cells of a level. Cells are 4-connected on the XZ plane.
/// </summary>
public sealed class Board(IEnumerable<Vector3I> cells)
{
    public static readonly Vector3I[] Directions = [Vector3I.Right, Vector3I.Back, Vector3I.Left, Vector3I.Forward];

    private readonly HashSet<Vector3I> _cells = [..cells];

    public IReadOnlyCollection<Vector3I> Cells => _cells;
    public int Count => _cells.Count;

    public bool Contains(Vector3I cell) => _cells.Contains(cell);

    /// <summary>
    ///     Collects every open cell connected to one of the seeds. <paramref name="open"/> must be bounded.
    /// </summary>
    public static Board FloodFill(IEnumerable<Vector3I> seeds, Func<Vector3I, bool> open)
    {
        var cells = new HashSet<Vector3I>();
        var frontier = new Stack<Vector3I>();
        foreach (var seed in seeds)
        {
            if (open(seed)) frontier.Push(seed);
        }

        while (frontier.TryPop(out var cell))
        {
            if (!cells.Add(cell)) continue;
            foreach (var direction in Directions)
            {
                var neighbor = cell + direction;
                if (!cells.Contains(neighbor) && open(neighbor)) frontier.Push(neighbor);
            }
        }

        return new Board(cells);
    }
}
