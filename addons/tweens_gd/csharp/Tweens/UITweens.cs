// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Godot;

namespace tweens.gd;

/// <summary>Animates ColorRect.Color in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ColorRectColorTween() : PropertyTween<ColorRect, Color>(
    static n => n.Color, static (n, value) => n.Color = value, Interpolators.Color);

/// <summary>Animates ColorRect.Color.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ColorRectColorAlphaTween() : PropertyTween<ColorRect, float>(
    static n => n.Color.A, static (n, value) => { var current = n.Color; current.A = value; n.Color = current; }, Interpolators.Float);

/// <summary>Animates Label.VisibleRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class LabelVisibleRatioTween() : PropertyTween<Label, float>(
    static n => n.VisibleRatio, static (n, value) => n.VisibleRatio = value, Interpolators.Float);

/// <summary>Animates RichTextLabel.VisibleRatio in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class RichTextLabelVisibleRatioTween() : PropertyTween<RichTextLabel, float>(
    static n => n.VisibleRatio, static (n, value) => n.VisibleRatio = value, Interpolators.Float);

/// <summary>Animates TextureProgressBar.TintUnder in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTintUnderTween() : PropertyTween<TextureProgressBar, Color>(
    static n => n.TintUnder, static (n, value) => n.TintUnder = value, Interpolators.Color);

/// <summary>Animates TextureProgressBar.TintUnder.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTintUnderAlphaTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.TintUnder.A, static (n, value) => { var current = n.TintUnder; current.A = value; n.TintUnder = current; }, Interpolators.Float);

/// <summary>Animates TextureProgressBar.TintOver in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTintOverTween() : PropertyTween<TextureProgressBar, Color>(
    static n => n.TintOver, static (n, value) => n.TintOver = value, Interpolators.Color);

/// <summary>Animates TextureProgressBar.TintOver.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTintOverAlphaTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.TintOver.A, static (n, value) => { var current = n.TintOver; current.A = value; n.TintOver = current; }, Interpolators.Float);

/// <summary>Animates TextureProgressBar.TintProgress in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTintProgressTween() : PropertyTween<TextureProgressBar, Color>(
    static n => n.TintProgress, static (n, value) => n.TintProgress = value, Interpolators.Color);

/// <summary>Animates TextureProgressBar.TintProgress.A in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTintProgressAlphaTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.TintProgress.A, static (n, value) => { var current = n.TintProgress; current.A = value; n.TintProgress = current; }, Interpolators.Float);

/// <summary>Animates TextureProgressBar.RadialInitialAngle in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarRadialInitialAngleTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.RadialInitialAngle, static (n, value) => n.RadialInitialAngle = value, Interpolators.Float);

/// <summary>Animates TextureProgressBar.RadialFillDegrees in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarRadialFillDegreesTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.RadialFillDegrees, static (n, value) => n.RadialFillDegrees = value, Interpolators.Float);

/// <summary>Animates TextureProgressBar.RadialCenterOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarRadialCenterOffsetTween() : PropertyTween<TextureProgressBar, Vector2>(
    static n => n.RadialCenterOffset, static (n, value) => n.RadialCenterOffset = value, Interpolators.Vector2);

/// <summary>Animates TextureProgressBar.RadialCenterOffset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarRadialCenterOffsetXTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.RadialCenterOffset.X, static (n, value) => { var current = n.RadialCenterOffset; current.X = value; n.RadialCenterOffset = current; }, Interpolators.Float);

/// <summary>Animates TextureProgressBar.RadialCenterOffset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarRadialCenterOffsetYTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.RadialCenterOffset.Y, static (n, value) => { var current = n.RadialCenterOffset; current.Y = value; n.RadialCenterOffset = current; }, Interpolators.Float);

/// <summary>Animates TextureProgressBar.TextureProgressOffset in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTextureProgressOffsetTween() : PropertyTween<TextureProgressBar, Vector2>(
    static n => n.TextureProgressOffset, static (n, value) => n.TextureProgressOffset = value, Interpolators.Vector2);

/// <summary>Animates TextureProgressBar.TextureProgressOffset.X in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTextureProgressOffsetXTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.TextureProgressOffset.X, static (n, value) => { var current = n.TextureProgressOffset; current.X = value; n.TextureProgressOffset = current; }, Interpolators.Float);

/// <summary>Animates TextureProgressBar.TextureProgressOffset.Y in Godot property units.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class TextureProgressBarTextureProgressOffsetYTween() : PropertyTween<TextureProgressBar, float>(
    static n => n.TextureProgressOffset.Y, static (n, value) => { var current = n.TextureProgressOffset; current.Y = value; n.TextureProgressOffset = current; }, Interpolators.Float);
