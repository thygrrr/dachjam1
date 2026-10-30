using Godot;
using jam;

namespace dachjam.Tests;

/// <summary>
///     Depth-first search over a puzzle's own moves, so it checks levels against the game's actual rules.
/// </summary>
internal static class Solver
{
    public static int CountSolutions(Puzzle puzzle, int cap = 2)
    {
        var count = 0;
        Search(0);
        return count;

        void Search(int index)
        {
            if (count >= cap) return;
            if (index == puzzle.Trails.Count)
            {
                if (puzzle.IsSolved) count++;
                return;
            }

            // A trail without an end may stop anywhere; one with an end stops only there.
            var trail = puzzle.Trails[index];
            if (trail.Complete)
            {
                Search(index + 1);
                if (trail.End is not null) return;
            }

            if (!FreeCellsReachable(puzzle, index) || HasDeadEnd(puzzle, index)) return;

            foreach (var direction in Board.Directions)
            {
                if (trail.Cells.Count > 1 && trail.Head + direction == trail.Cells[^2]) continue;
                if (puzzle.TryMove(index, direction).Kind != MoveKind.Extended) continue;
                Search(index);
                puzzle.Retract(index);
            }
        }
    }

    // Every free cell must remain reachable from this trail's head or a later trail's start.
    private static bool FreeCellsReachable(Puzzle puzzle, int index)
    {
        var free = puzzle.Board.Cells.Except(puzzle.Trails.SelectMany(trail => trail.Cells)).ToHashSet();
        var reached = new HashSet<Vector3I>();
        var frontier = new Stack<Vector3I>(puzzle.Trails.Skip(index).Select(trail => trail.Head));

        while (frontier.TryPop(out var cell))
        {
            foreach (var direction in Board.Directions)
            {
                var neighbor = cell + direction;
                if (free.Contains(neighbor) && reached.Add(neighbor)) frontier.Push(neighbor);
            }
        }

        return reached.Count == free.Count;
    }

    // Every free cell joins a trail through two neighbors: free cells, or the heads of this and later trails, unless it
    // is a trail's end. Only as many cells as there are open-ended trails left can make do with one, by ending one.
    private static bool HasDeadEnd(Puzzle puzzle, int index)
    {
        var later = puzzle.Trails.Skip(index).ToList();
        var heads = later.ToDictionary(trail => trail.Head);
        var free = puzzle.Board.Cells.Except(puzzle.Trails.SelectMany(trail => trail.Cells)).ToHashSet();
        var openEnds = later.Count(trail => trail.End is null);
        var loose = 0;

        foreach (var cell in free)
        {
            var endOf = puzzle.Trails.FirstOrDefault(trail => trail.End == cell);
            var joins = Board.Directions.Select(direction => cell + direction).Count(neighbor =>
                free.Contains(neighbor)
                || heads.TryGetValue(neighbor, out var trail) && (endOf is null || endOf == trail));

            if (joins == 0) return true;
            if (joins == 1 && endOf is null && ++loose > openEnds) return true;
        }

        return false;
    }
}
