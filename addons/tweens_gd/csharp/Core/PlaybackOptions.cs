// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss
#nullable enable
namespace tweens.gd;

/// <summary>The clock and pause policy of one playback root, independently of its motion definitions.</summary>
public readonly record struct PlaybackOptions
{
    public TweenProcessMode ProcessMode { get; init; }
    public TweenPauseMode PauseMode { get; init; }
    public bool UseUnscaledTime { get; init; }

    internal void Validate()
    {
        if (!System.Enum.IsDefined(ProcessMode) || !System.Enum.IsDefined(PauseMode))
            throw new System.ArgumentException("Invalid playback mode.");
    }
}
