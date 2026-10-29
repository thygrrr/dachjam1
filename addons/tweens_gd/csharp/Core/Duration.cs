// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;

namespace tweens.gd;

/// <summary>A duration in seconds, accepting numeric seconds or a <see cref="TimeSpan"/>.</summary>
/// <remarks>Values are validated when playback starts, allowing signed timing adjustments.</remarks>
/// <param name="Seconds">Duration in seconds. GDScript timing fields use float seconds directly.</param>
public readonly record struct Duration(double Seconds)
{
    /// <summary>Converts a TimeSpan to seconds.</summary>
    public static implicit operator Duration(TimeSpan value) => new(value.TotalSeconds);
    /// <summary>Wraps numeric seconds.</summary>
    public static implicit operator Duration(float seconds) => new(seconds);
    /// <summary>Wraps numeric seconds.</summary>
    public static implicit operator Duration(double seconds) => new(seconds);
    /// <summary>Reads the duration in seconds.</summary>
    public static implicit operator double(Duration duration) => duration.Seconds;
}
