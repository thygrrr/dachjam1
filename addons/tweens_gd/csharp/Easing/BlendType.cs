// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
namespace tweens.gd;

/// <summary>How different In/Out families meet inside the centered transition window.</summary>
public enum BlendType
{
    /// <summary>Two cubic segments preserve the midpoint; its velocity uses modified Akima (makima) weights.</summary>
    Makima,
    /// <summary>Two cubic Hermite segments preserve the midpoint and match acceleration there.</summary>
    Hermite,
    /// <summary>Crossfade the families' paired profiles with a smooth weight.</summary>
    SmoothStep,
    /// <summary>Crossfade with a linear weight; velocity may jump at the window edges.</summary>
    Linear,
}
