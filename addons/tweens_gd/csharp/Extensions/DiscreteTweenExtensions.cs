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

public static partial class TweenExtensions
{
    /// <summary>Starts a LabelVisibleCharactersTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Label, int> TweenVisibleCharacters(this Label target,
        int to, Duration duration, Action<LabelVisibleCharactersTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new LabelVisibleCharactersTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a LabelVisibleCharactersTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<Label, int> TweenVisibleCharacters(this Label target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new LabelVisibleCharactersTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a RichTextLabelVisibleCharactersTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<RichTextLabel, int> TweenVisibleCharacters(this RichTextLabel target,
        int to, Duration duration, Action<RichTextLabelVisibleCharactersTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new RichTextLabelVisibleCharactersTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a RichTextLabelVisibleCharactersTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<RichTextLabel, int> TweenVisibleCharacters(this RichTextLabel target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new RichTextLabelVisibleCharactersTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ScrollContainerScrollHorizontalTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<ScrollContainer, int> TweenScrollHorizontal(this ScrollContainer target,
        int to, Duration duration, Action<ScrollContainerScrollHorizontalTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ScrollContainerScrollHorizontalTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ScrollContainerScrollHorizontalTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<ScrollContainer, int> TweenScrollHorizontal(this ScrollContainer target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ScrollContainerScrollHorizontalTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a ScrollContainerScrollVerticalTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<ScrollContainer, int> TweenScrollVertical(this ScrollContainer target,
        int to, Duration duration, Action<ScrollContainerScrollVerticalTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new ScrollContainerScrollVerticalTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a ScrollContainerScrollVerticalTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<ScrollContainer, int> TweenScrollVertical(this ScrollContainer target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new ScrollContainerScrollVerticalTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a Sprite2DFrameTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<Sprite2D, int> TweenFrame(this Sprite2D target,
        int to, Duration duration, Action<Sprite2DFrameTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new Sprite2DFrameTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a Sprite2DFrameTween and returns its playback handle.</summary>
    /// <remarks>Copies the options; the explicit duration takes precedence.</remarks>
    public static TweenInstance<Sprite2D, int> TweenFrame(this Sprite2D target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new Sprite2DFrameTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a AnimatedSprite2DFrameTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AnimatedSprite2D, int> TweenFrame(this AnimatedSprite2D target,
        int to, Duration duration, Action<AnimatedSprite2DFrameTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AnimatedSprite2DFrameTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a AnimatedSprite2DFrameTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AnimatedSprite2D, int> TweenFrame(this AnimatedSprite2D target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AnimatedSprite2DFrameTween { To = to, Duration = duration }, options), playback);

    /// <summary>Starts a AnimatedSprite3DFrameTween and returns its playback handle.</summary>
    /// <remarks>Configure runs before snapshotting and can override any definition option, including duration.</remarks>
    public static TweenInstance<AnimatedSprite3D, int> TweenFrame(this AnimatedSprite3D target,
        int to, Duration duration, Action<AnimatedSprite3DFrameTween>? configure = null, PlaybackOptions playback = default)
        => target.Tween(ConfigureDefinition(new AnimatedSprite3DFrameTween { To = to, Duration = duration }, configure), playback);

    /// <summary>Starts a AnimatedSprite3DFrameTween.
    /// Copies the options; the explicit duration takes precedence.</summary>
    public static TweenInstance<AnimatedSprite3D, int> TweenFrame(this AnimatedSprite3D target,
        int to, Duration duration, TweenOptions options, PlaybackOptions playback = default)
        => target.Tween(ApplyOptions(new AnimatedSprite3DFrameTween { To = to, Duration = duration }, options), playback);
}
