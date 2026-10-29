// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using Godot;

namespace tweens.gd;

public static partial class Tweens
{
    /// <summary>A reusable parallel animation. Each play owns independent ordinary channel tweens.</summary>
    public sealed partial class Keyframes
    {
        private readonly AnimationTrack[] tracks;
        /// <summary>Shared timing, easing and color policy, captured when this definition is created.</summary>
        public TweenOptions Options { get; }
        /// <summary>Default interpolation for segments without an arriving-key override.</summary>
        public Interpolation Interpolation { get; }
        /// <summary>The number of animated channels.</summary>
        public int Count => tracks.Length;

        internal Keyframes(Dictionary<string, List<AnimationKey>> channels, Duration? duration, EaseType? ease,
            Interpolation interpolation, TweenOptions? options)
        {
            Options = Configure(options, duration, ease);
            Interpolation = interpolation;
            tracks = Build(channels, Options, interpolation);
        }

        internal static void AddValues<T>(Dictionary<string, List<AnimationKey>> channels, string path,
            ReadOnlySpan<T> values, Func<T, Variant> convert, bool uniform = false)
        {
            if (values.IsEmpty) return;
            if (values.Length < 2) throw new ArgumentException($"Channel '{path}' needs at least two values.");
            var channel = new List<AnimationKey>(values.Length);
            for (var i = 0; i < values.Length; i++) channel.Add(new(100.0 * i / (values.Length - 1), convert(values[i]), null, uniform));
            channels[path] = channel;
        }

        /// <summary>Creates a definition from sparse percentage keys; copies and sorts the supplied data.</summary>
        public Keyframes(ReadOnlySpan<Keyframe> keys, Duration? duration = null, EaseType? ease = null,
            Interpolation interpolation = default, TweenOptions? options = null)
        {
            Options = Configure(options, duration, ease);
            Interpolation = interpolation;
            var channels = new Dictionary<string, List<AnimationKey>>(StringComparer.Ordinal);
            foreach (var key in keys)
            {
                void Add(string path, Variant value, bool uniform = false)
                {
                    if (key.Stop == 0 && key.Interpolation is not null) throw new ArgumentException("A zero-percent key has no arriving segment.");
                    if (!channels.TryGetValue(path, out var channel)) channels[path] = channel = [];
                    channel.Add(new(key.Stop, value, key.Interpolation, uniform));
                }
                if (key.X is { } x) Add("position:x", x);
                if (key.Y is { } y) Add("position:y", y);
                if (key.Z is { } z) Add("position:z", z);
                if (key.Position is { } position) Add("position", position.ToVariant());
                if (key.Rotation is { } rotation) Add("rotation", rotation.ToVariant());
                if (key.RotationDegrees is { } degrees) Add("rotation_degrees", degrees.ToVariant());
                if (key.Scale is { } scale) Add("scale", scale.ToVariant(), true);
                if (key.Quaternion is { } quaternion) Add("quaternion", quaternion);
                if (key.Skew is { } skew) Add("skew", skew);
                if (key.Modulate is { } modulate) Add("modulate", modulate);
                if (key.SelfModulate is { } selfModulate) Add("self_modulate", selfModulate);
                if (key.Alpha is { } alpha) Add("modulate:a", alpha);
                if (key.Transparency is { } transparency) Add("transparency", transparency);
                if (key.Paths is { } paths) foreach (var pair in paths) Add(pair.Path, pair.Value);
            }
            tracks = Build(channels, Options, interpolation);
        }

        /// <summary>Creates evenly spaced channel curves. Empty spans omit channels; supplied channels need two values.</summary>
        public Keyframes(ReadOnlySpan<Real> x = default, ReadOnlySpan<Real> y = default, ReadOnlySpan<Real> z = default,
            ReadOnlySpan<Vec> position = default, ReadOnlySpan<Vec> rotation = default, ReadOnlySpan<Vec> rotationDegrees = default,
            ReadOnlySpan<Vec> scale = default, ReadOnlySpan<Godot.Quaternion> quaternion = default, ReadOnlySpan<Real> skew = default,
            ReadOnlySpan<Godot.Color> modulate = default, ReadOnlySpan<Real> alpha = default, ReadOnlySpan<Godot.Color> selfModulate = default,
            ReadOnlySpan<Real> transparency = default, Duration? duration = null, EaseType? ease = null,
            Interpolation interpolation = default, TweenOptions? options = null)
        {
            Options = Configure(options, duration, ease);
            Interpolation = interpolation;
            var channels = new Dictionary<string, List<AnimationKey>>(StringComparer.Ordinal);
            void Add<T>(string path, ReadOnlySpan<T> values, Func<T, Variant> convert, bool uniform = false)
                => AddValues(channels, path, values, convert, uniform);
            Add("position:x", x, static v => v.Value); Add("position:y", y, static v => v.Value); Add("position:z", z, static v => v.Value);
            Add("position", position, static v => v.ToVariant()); Add("rotation", rotation, static v => v.ToVariant());
            Add("rotation_degrees", rotationDegrees, static v => v.ToVariant()); Add("scale", scale, static v => v.ToVariant(), true);
            Add("quaternion", quaternion, static v => v); Add("skew", skew, static v => v.Value);
            Add("modulate", modulate, static v => v); Add("modulate:a", alpha, static v => v.Value);
            Add("self_modulate", selfModulate, static v => v); Add("transparency", transparency, static v => v.Value);
            tracks = Build(channels, Options, interpolation);
        }

        private static TweenOptions Configure(TweenOptions? options, Duration? duration, EaseType? ease)
        {
            var result = options ?? new TweenOptions { Duration = 1 };
            if (duration is { } seconds) result = result with { Duration = seconds };
            if (ease is { } easing) result = result with { Ease = easing };
            _ = new Playback(result);
            _ = Easing.GetFunction(result.Ease);
            return result;
        }

        private static AnimationTrack[] Build(Dictionary<string, List<AnimationKey>> channels, TweenOptions options, Interpolation interpolation)
        {
            if (channels.Count == 0) throw new ArgumentException("An animation needs at least one channel.");
            var result = new AnimationTrack[channels.Count];
            var index = 0;
            foreach (var pair in channels)
            {
                foreach (var other in channels.Keys)
                {
                    if (pair.Key == other) continue;
                    if (pair.Key.StartsWith(other + ":", StringComparison.Ordinal) || other.StartsWith(pair.Key + ":", StringComparison.Ordinal)
                        || IsRotation(pair.Key) && IsRotation(other) && pair.Key.Split(':')[0] != other.Split(':')[0])
                        throw new ArgumentException($"Conflicting channels '{pair.Key}' and '{other}'.");
                }
                pair.Value.Sort(static (a, b) => a.At.CompareTo(b.At));
                result[index++] = new(pair.Key, pair.Value.ToArray(), options, interpolation);
            }
            return result;
        }

        private static bool IsRotation(string path) => path.Split(':')[0] is "rotation" or "rotation_degrees" or "quaternion";

        /// <summary>Validates all channel bindings, then starts independent playback owned by the target node.</summary>
        public Group Play(Node target, PlaybackOptions options = default)
            => Play(TweenRuntime.GetRunner(target).Scheduler, target, options);

        /// <summary>Plays through a manually driven scheduler. The node owns playback and must be in the tree.</summary>
        public Group Play(TweenScheduler scheduler, Node target, PlaybackOptions options = default)
        {
            ArgumentNullException.ThrowIfNull(scheduler);
            scheduler.ValidateStart(target, target, null, options);
            var definitions = new ITweenDefinition<Node>[tracks.Length];
            for (var i = 0; i < tracks.Length; i++) definitions[i] = tracks[i].Bind(target);
            return scheduler.AddAll(target, definitions, options: options);
        }
    }
}

internal readonly record struct AnimationKey(double At, Variant Value, Interpolation? Interpolation, bool Uniform);

internal sealed class AnimationTrack
{
    private readonly string path;
    private readonly NodePath nativePath;
    private readonly AnimationKey[] keys;
    private readonly TweenOptions options;
    private readonly Interpolation interpolation;
    private readonly Dictionary<Variant.Type, ITweenDefinition<Node>> prepared = [];

    internal AnimationTrack(string path, AnimationKey[] keys, TweenOptions options, Interpolation interpolation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        this.path = path; nativePath = new NodePath(path); this.keys = keys; this.options = options; this.interpolation = interpolation;
        for (var i = 0; i < keys.Length; i++)
        {
            if (!double.IsFinite(keys[i].At) || keys[i].At < 0 || keys[i].At > 100 || i > 0 && keys[i].At == keys[i - 1].At)
                throw new ArgumentException($"Invalid or duplicate stop {keys[i].At} on '{path}'.");
            if (keys[i].At == 0 && keys[i].Interpolation is not null) throw new ArgumentException($"A zero-percent key on '{path}' has no arriving segment.");
            var mode = keys[i].Interpolation ?? interpolation;
            if (mode.Kind == 3) _ = Easing.GetFunction(mode.Ease);
            AnimationValues.Validate(keys[i].Value);
        }
    }

    internal ITweenDefinition<Node> Bind(Node target)
    {
        AnimationValues.ValidatePath(target, path);
        if (!GodotObject.IsInstanceValid(target) || target.IsQueuedForDeletion()) throw new ArgumentException("The animation target became invalid during validation.");
        using var current = target.GetIndexed(nativePath);
        if (!GodotObject.IsInstanceValid(target) || target.IsQueuedForDeletion()) throw new ArgumentException("The animation target became invalid during validation.");
        var type = current.VariantType;
        if (prepared.TryGetValue(type, out var cached)) return cached;
        ITweenDefinition<Node> result = type switch
        {
            Variant.Type.Float => Compile<double>(type), Variant.Type.Int => Compile<long>(type),
            Variant.Type.Vector2 => Compile<Vector2>(type), Variant.Type.Vector3 => Compile<Vector3>(type),
            Variant.Type.Vector4 => Compile<Vector4>(type), Variant.Type.Color => Compile<Color>(type),
            Variant.Type.Quaternion => Compile<Quaternion>(type), Variant.Type.Rect2 => Compile<Rect2>(type),
            _ => throw new ArgumentException($"Channel '{path}' has unsupported type {type}."),
        };
        prepared.Add(type, result);
        return result;
    }

    private ITweenDefinition<Node> Compile<T>(Variant.Type type) where T : struct
    {
        var typed = new CurveKey<T>[keys.Length];
        for (var i = 0; i < keys.Length; i++)
        {
            using var value = AnimationValues.Coerce(keys[i].Value, type, keys[i].Uniform);
            typed[i] = new(keys[i].At, AnimationValues.Read<T>(value), keys[i].Interpolation);
        }
        return new Tweens.Sampled<Node, T>(new PropertyPathBinding<T>(nativePath),
            new KeyframeCurve<T>(typed, interpolation, options.ColorSpace, options.AlphaMode, options.ColorEncoding), options.Timing);
    }
}

public static partial class TweenExtensions
{
    /// <summary>Starts a reusable keyframe definition on this node.</summary>
    public static Group Animate(this Node target, Tweens.Keyframes definition, PlaybackOptions options = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return definition.Play(target, options);
    }
    /// <summary>Starts a reusable keyframe definition on this node.</summary>
    public static Group Tween(this Node target, Tweens.Keyframes definition, PlaybackOptions options = default) => target.Animate(definition, options);
    /// <summary>Creates and plays sparse percentage keys on this node.</summary>
    public static Group Animate(this Node target, ReadOnlySpan<Tweens.Keyframe> keys, Duration? duration = null,
        EaseType? ease = null, Interpolation interpolation = default, TweenOptions? options = null)
        => target.Animate(new Tweens.Keyframes(keys, duration, ease, interpolation, options));
}
