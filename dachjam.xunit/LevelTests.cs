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
        using var scene = GD.Load<PackedScene>("res://campaign.tscn");
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

                Assert.True(Solver.CountSolutions(puzzle, cap: 1) > 0, $"{scene.ResourcePath} has no solution.");
            }
            finally { level.Free(); }
        }
    }

    [Fact]
    public void EveryPretext_HidesTheBoardBehindGrau()
    {
        var grau = ProjectSettings.GetSetting("rendering/environment/defaults/default_clear_color").AsColor();
        foreach (var scene in CampaignLevels())
        {
            var level = scene.Instantiate<Level>();
            var pretext = level.Pretext?.Instantiate<Control>();
            try
            {
                Assert.True(pretext is not null, $"{scene.ResourcePath} has no pretext.");
                var background = pretext.GetNode<ColorRect>("Background");
                Assert.Equal(grau, background.Color);
                Assert.Equal(new Vector4(0, 0, 1, 1),
                    new Vector4(background.AnchorLeft, background.AnchorTop, background.AnchorRight, background.AnchorBottom));
                Assert.True(background.GetIndex() < pretext.GetNode("Quote").GetIndex(), "The background covers the text.");
            }
            finally
            {
                pretext?.Free();
                level.Free();
            }
        }
    }
}
