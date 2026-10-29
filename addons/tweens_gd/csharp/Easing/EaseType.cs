// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss
// SPDX-FileCopyrightText: 2020 Jeffrey Lanters

#nullable enable
namespace tweens.gd {
  /// <summary>Easing identifiers. Combine In and Out flags, or select a legacy named curve.</summary>
  /// <remarks>Legacy names retain their numeric values and shapes; do not combine them with |.</remarks>
  [System.Flags]
  public enum EaseType : long {
    /// <summary>Legacy Linear curve. Use In, Out, or InOut for composable easing.</summary>
    Linear = 0,
    /// <summary>Legacy SineIn curve. Use In, Out, or InOut for composable easing.</summary>
    SineIn = 10,
    /// <summary>Legacy SineOut curve. Use In, Out, or InOut for composable easing.</summary>
    SineOut = 11,
    /// <summary>Legacy SineInOut curve. Use In, Out, or InOut for composable easing.</summary>
    SineInOut = 12,
    /// <summary>Legacy QuadIn curve. Use In, Out, or InOut for composable easing.</summary>
    QuadIn = 20,
    /// <summary>Legacy QuadOut curve. Use In, Out, or InOut for composable easing.</summary>
    QuadOut = 21,
    /// <summary>Legacy QuadInOut curve. Use In, Out, or InOut for composable easing.</summary>
    QuadInOut = 22,
    /// <summary>Legacy CubicIn curve. Use In, Out, or InOut for composable easing.</summary>
    CubicIn = 30,
    /// <summary>Legacy CubicOut curve. Use In, Out, or InOut for composable easing.</summary>
    CubicOut = 31,
    /// <summary>Legacy CubicInOut curve. Use In, Out, or InOut for composable easing.</summary>
    CubicInOut = 32,
    /// <summary>Legacy QuartIn curve. Use In, Out, or InOut for composable easing.</summary>
    QuartIn = 40,
    /// <summary>Legacy QuartOut curve. Use In, Out, or InOut for composable easing.</summary>
    QuartOut = 41,
    /// <summary>Legacy QuartInOut curve. Use In, Out, or InOut for composable easing.</summary>
    QuartInOut = 42,
    /// <summary>Legacy QuintIn curve. Use In, Out, or InOut for composable easing.</summary>
    QuintIn = 50,
    /// <summary>Legacy QuintOut curve. Use In, Out, or InOut for composable easing.</summary>
    QuintOut = 51,
    /// <summary>Legacy QuintInOut curve. Use In, Out, or InOut for composable easing.</summary>
    QuintInOut = 52,
    /// <summary>Legacy ExpoIn curve. Use In, Out, or InOut for composable easing.</summary>
    ExpoIn = 60,
    /// <summary>Legacy ExpoOut curve. Use In, Out, or InOut for composable easing.</summary>
    ExpoOut = 61,
    /// <summary>Legacy ExpoInOut curve. Use In, Out, or InOut for composable easing.</summary>
    ExpoInOut = 62,
    /// <summary>Legacy CircIn curve. Use In, Out, or InOut for composable easing.</summary>
    CircIn = 70,
    /// <summary>Legacy CircOut curve. Use In, Out, or InOut for composable easing.</summary>
    CircOut = 71,
    /// <summary>Legacy CircInOut curve. Use In, Out, or InOut for composable easing.</summary>
    CircInOut = 72,
    /// <summary>Legacy BackIn curve. Use In, Out, or InOut for composable easing.</summary>
    BackIn = 80,
    /// <summary>Legacy BackOut curve. Use In, Out, or InOut for composable easing.</summary>
    BackOut = 81,
    /// <summary>Legacy BackInOut curve. Use In, Out, or InOut for composable easing.</summary>
    BackInOut = 82,
    /// <summary>Legacy ElasticIn curve. Use In, Out, or InOut for composable easing.</summary>
    ElasticIn = 90,
    /// <summary>Legacy ElasticOut curve. Use In, Out, or InOut for composable easing.</summary>
    ElasticOut = 91,
    /// <summary>Legacy ElasticInOut curve. Use In, Out, or InOut for composable easing.</summary>
    ElasticInOut = 92,
    /// <summary>Legacy BounceIn curve. Use In, Out, or InOut for composable easing.</summary>
    BounceIn = 100,
    /// <summary>Legacy BounceOut curve. Use In, Out, or InOut for composable easing.</summary>
    BounceOut = 101,
    /// <summary>Legacy BounceInOut curve. Use In, Out, or InOut for composable easing.</summary>
    BounceInOut = 102,
    /// <summary>Cubic ease-in-out with zero velocity at both endpoints.</summary>
    SmoothStep = 110,
    /// <summary>Quintic ease-in-out with zero velocity and acceleration at both endpoints.</summary>
    SmootherStep = 120,
  }
}
