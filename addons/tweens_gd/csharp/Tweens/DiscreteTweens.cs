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

/// <summary>Animates Label.VisibleCharacters in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class LabelVisibleCharactersTween() : PropertyTween<Label, int>(
    static n => n.VisibleCharacters, static (n, value) => n.VisibleCharacters = value, Interpolators.Int);

/// <summary>Animates RichTextLabel.VisibleCharacters in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class RichTextLabelVisibleCharactersTween() : PropertyTween<RichTextLabel, int>(
    static n => n.VisibleCharacters, static (n, value) => n.VisibleCharacters = value, Interpolators.Int);

/// <summary>Animates ScrollContainer.ScrollHorizontal in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ScrollContainerScrollHorizontalTween() : PropertyTween<ScrollContainer, int>(
    static n => n.ScrollHorizontal, static (n, value) => n.ScrollHorizontal = value, Interpolators.Int);

/// <summary>Animates ScrollContainer.ScrollVertical in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class ScrollContainerScrollVerticalTween() : PropertyTween<ScrollContainer, int>(
    static n => n.ScrollVertical, static (n, value) => n.ScrollVertical = value, Interpolators.Int);

/// <summary>Animates Sprite2D.Frame in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class Sprite2DFrameTween() : PropertyTween<Sprite2D, int>(
    static n => n.Frame, static (n, value) => n.Frame = value, Interpolators.Int);

/// <summary>Animates AnimatedSprite2D.Frame in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AnimatedSprite2DFrameTween() : PropertyTween<AnimatedSprite2D, int>(
    static n => n.Frame, static (n, value) => n.Frame = value, Interpolators.Int);

/// <summary>Animates AnimatedSprite3D.Frame in Godot property units. Integer samples round to nearest, with ties away from zero.</summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class AnimatedSprite3DFrameTween() : PropertyTween<AnimatedSprite3D, int>(
    static n => n.Frame, static (n, value) => n.Frame = value, Interpolators.Int);
