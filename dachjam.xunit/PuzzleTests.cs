using Godot;
using jam;

namespace dachjam.Tests;

public class PuzzleTests
{
    // Rows are Z, columns are X. '#' is a wall; uppercase letters start trails, matching lowercase letters end them.
    private static Puzzle Parse(params string[] rows)
    {
        var open = new List<Vector3I>();
        var starts = new SortedDictionary<char, Vector3I>();
        var ends = new Dictionary<char, Vector3I>();

        for (var z = 0; z < rows.Length; z++)
        for (var x = 0; x < rows[z].Length; x++)
        {
            var symbol = rows[z][x];
            if (symbol == '#') continue;

            var cell = new Vector3I(x, 0, z);
            open.Add(cell);
            if (char.IsUpper(symbol)) starts[symbol] = cell;
            if (char.IsLower(symbol)) ends[char.ToUpper(symbol)] = cell;
        }

        return new Puzzle(new Board(open), starts.Select(start =>
            new TrailSpec(start.Value, ends.TryGetValue(start.Key, out var end) ? end : null)));
    }

    // Level0: a ring around a 3x3 block, with the end right next to the start.
    private static Puzzle Ring() => Parse(
        "#######",
        "#aA...#",
        "#.###.#",
        "#.###.#",
        "#.###.#",
        "#.....#",
        "#######");

    private static readonly Vector3I[] LongWayRound =
    [
        ..Enumerable.Repeat(Vector3I.Right, 3),
        ..Enumerable.Repeat(Vector3I.Back, 4),
        ..Enumerable.Repeat(Vector3I.Left, 4),
        ..Enumerable.Repeat(Vector3I.Forward, 4),
    ];

    [Fact]
    public void Ring_LongWayRound_IsSolved()
    {
        var puzzle = Ring();

        foreach (var direction in LongWayRound)
        {
            Assert.False(puzzle.IsSolved);
            Assert.Equal(MoveKind.Extended, puzzle.TryMove(0, direction).Kind);
        }

        Assert.True(puzzle.IsSolved);
        Assert.Equal(16, puzzle.Trails[0].Cells.Count);
    }

    [Fact]
    public void Solver_FindsTheRingsOnlySolution()
    {
        Assert.Equal(1, Solver.CountSolutions(Ring(), cap: 10));
    }

    [Fact]
    public void Solver_ReportsUnsolvablePuzzles()
    {
        // Start and end share a checkerboard color, but a 4-cell path must end on the other color.
        var puzzle = Parse(
            "####",
            "#A.#",
            "#.a#",
            "####");

        Assert.Equal(0, Solver.CountSolutions(puzzle));
    }

    [Fact]
    public void Ring_ShortcutToEnd_IsNotSolved_AndCannotContinue()
    {
        var puzzle = Ring();

        Assert.Equal(MoveKind.Extended, puzzle.TryMove(0, Vector3I.Left).Kind);
        Assert.True(puzzle.Trails[0].Complete);
        Assert.False(puzzle.IsSolved);
        Assert.Equal(MoveKind.Blocked, puzzle.TryMove(0, Vector3I.Back).Kind);
    }

    [Fact]
    public void SteppingBack_Retracts()
    {
        var puzzle = Ring();
        puzzle.TryMove(0, Vector3I.Right);

        var move = puzzle.TryMove(0, Vector3I.Left);

        Assert.Equal(new Move(MoveKind.Retracted, new Vector3I(3, 0, 1)), move);
        Assert.Equal([new Vector3I(2, 0, 1)], puzzle.Trails[0].Cells);
        Assert.Equal(MoveKind.Extended, puzzle.TryMove(0, Vector3I.Right).Kind);
    }

    [Fact]
    public void Retract_NeverRemovesTheStart()
    {
        var puzzle = Ring();

        Assert.Equal(MoveKind.Blocked, puzzle.Retract(0).Kind);
        Assert.Single(puzzle.Trails[0].Cells);
    }

    [Fact]
    public void Walls_Block()
    {
        var puzzle = Ring();

        Assert.Equal(new Move(MoveKind.Blocked, new Vector3I(2, 0, 0)), puzzle.TryMove(0, Vector3I.Forward));
        Assert.Equal(MoveKind.Blocked, puzzle.TryMove(0, Vector3I.Back).Kind);
    }

    [Fact]
    public void OwnTrail_Blocks_ExceptThePreviousCell()
    {
        var puzzle = Parse(
            "####",
            "#A.#",
            "#..#",
            "####");

        puzzle.TryMove(0, Vector3I.Right);
        puzzle.TryMove(0, Vector3I.Back);
        puzzle.TryMove(0, Vector3I.Left);

        Assert.Equal(MoveKind.Blocked, puzzle.TryMove(0, Vector3I.Forward).Kind);
        Assert.True(puzzle.IsSolved);
    }

    [Fact]
    public void Trails_BlockEachOther_AndEachOthersEnds()
    {
        var puzzle = Parse(
            "######",
            "#A..a#",
            "#B..b#",
            "######");

        Assert.Equal(MoveKind.Blocked, puzzle.TryMove(0, Vector3I.Back).Kind);
        puzzle.TryMove(1, Vector3I.Right);
        puzzle.TryMove(0, Vector3I.Right);
        Assert.Equal(MoveKind.Blocked, puzzle.TryMove(0, Vector3I.Back).Kind);

        puzzle.TryMove(0, Vector3I.Right);
        puzzle.TryMove(0, Vector3I.Back);
        Assert.Equal(MoveKind.Blocked, puzzle.TryMove(0, Vector3I.Right).Kind);
    }

    [Fact]
    public void Trails_TogetherSolve()
    {
        var puzzle = Parse(
            "######",
            "#A..a#",
            "#B..b#",
            "######");

        for (var i = 0; i < 3; i++)
        {
            puzzle.TryMove(0, Vector3I.Right);
            puzzle.TryMove(1, Vector3I.Right);
        }

        Assert.True(puzzle.IsSolved);
    }

    [Fact]
    public void TrailWithoutEnd_IsCompleteAnywhere()
    {
        var puzzle = Parse(
            "#####",
            "#A..#",
            "#####");

        puzzle.TryMove(0, Vector3I.Right);
        Assert.False(puzzle.IsSolved);
        puzzle.TryMove(0, Vector3I.Right);
        Assert.True(puzzle.IsSolved);
    }

    [Fact]
    public void FloodFill_StaysBetweenWalls()
    {
        var walls = new HashSet<Vector3I>();
        for (var i = 0; i <= 6; i++) walls.UnionWith([new(i, 0, 0), new(i, 0, 6), new(0, 0, i), new(6, 0, i)]);
        for (var x = 2; x <= 4; x++)
        for (var z = 2; z <= 4; z++) walls.Add(new Vector3I(x, 0, z));

        var board = Board.FloodFill([new Vector3I(2, 0, 1)], cell =>
            cell.X is >= 0 and <= 6 && cell.Z is >= 0 and <= 6 && !walls.Contains(cell));

        Assert.Equal(16, board.Count);
        Assert.DoesNotContain(new Vector3I(3, 0, 3), board.Cells);
    }

    [Fact]
    public void Constructor_RejectsStartsAndEndsOffTheBoard()
    {
        var board = new Board([Vector3I.Zero, Vector3I.Right]);

        Assert.Throws<ArgumentException>(() => new Puzzle(board, [new TrailSpec(Vector3I.Back)]));
        Assert.Throws<ArgumentException>(() => new Puzzle(board, [new TrailSpec(Vector3I.Zero, Vector3I.Back)]));
        Assert.Throws<ArgumentException>(() => new Puzzle(board, [new TrailSpec(Vector3I.Zero), new TrailSpec(Vector3I.Zero)]));
        Assert.Throws<ArgumentException>(() => new Puzzle(board,
            [new TrailSpec(Vector3I.Zero, Vector3I.Right), new TrailSpec(Vector3I.Right)]));
    }
}
