using Godot;
using jam;
using twodog.Testing;
using twodog.Testing.Xunit;

namespace dachjam.Tests;

[Collection<HeadlessCollection>]
public class LevelTests(HeadlessFixture godot)
{
    private static PackedScene[] CampaignLevels()
    {
        using var scene = GD.Load<PackedScene>((string) ProjectSettings.GetSetting("application/run/main_scene"));
        var root = scene.Instantiate();
        try { return root.GetNode<Campaign>("Campaign").Levels; }
        finally { root.Free(); }
    }

    [Fact]
    public void Campaign_HasLevels()
    {
        Assert.NotEmpty(CampaignLevels());
    }

    [Fact]
    public void EveryCampaignLevel_IsSolvable()
    {
        foreach (var scene in CampaignLevels())
        {
            var level = scene.Instantiate<Level>();
            try
            {
                godot.Tree.Root.AddChild(level);
                var puzzle = new Puzzle(level.Puzzle.Board,
                    level.Puzzle.Trails.Select(trail => new TrailSpec(trail.Start, trail.End)));

                Assert.True(Solver.CountSolutions(puzzle) > 0, $"{scene.ResourcePath} has no solution.");
            }
            finally { level.Free(); }
        }
    }
}
