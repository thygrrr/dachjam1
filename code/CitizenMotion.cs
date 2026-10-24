using System;
using System.Numerics;

namespace jam;

/// <summary>
///     Stateless, thread-safe motion of citizen cubes, evaluated every frame from their components.
/// </summary>
public static class CitizenMotion
{
    public const int PerCell = 25;

    private const float WanderRadius = 0.38f;
    private const float FlowSpeed = 0.6f;
    private const float SwirlRadius = 0.1f;
    private const float VortexRadius = 0.25f;
    private const float Spin = 2.5f;
    private const int Strands = 3;
    private const float StartleRate = 2.5f;
    private const float FlinchDistance = 0.2f;
    private const float FlinchHop = 0.12f;
    private const float WaveSpeed = 6f;

    // Helix turns per cell. Must be whole, so the helix continues seamlessly into the next cell.
    private const int Twist = 1;

    /// <summary>
    ///     Aimless, bounded drift around the home cell center.
    /// </summary>
    public static Vector3 Wander(in Citizen citizen, float time)
    {
        var phase = citizen.Seed * 1000f;
        var rate = 0.3f + 0.3f * citizen.Seed;
        var x = 0.55f * MathF.Sin(time * rate + phase)
                + 0.3f * MathF.Sin(time * 2.3f * rate + phase * 1.7f)
                + 0.15f * MathF.Sin(time * 5.1f * rate + phase * 0.3f);
        var z = 0.55f * MathF.Sin(time * 1.3f * rate + phase * 2.9f)
                + 0.3f * MathF.Sin(time * 2.9f * rate + phase * 0.7f)
                + 0.15f * MathF.Sin(time * 4.7f * rate + phase * 1.3f);
        return citizen.Home + new Vector3(x, 0, z) * WanderRadius;
    }

    /// <summary>
    ///     Position on a helix that flows along the path through the cell, or on a vortex ring if the path neither
    ///     enters nor leaves it. Every cell runs the same phases, so neighboring cells join without seams.
    /// </summary>
    public static Vector3 Swirl(in Citizen citizen, in Flow flow, float time, out float scale)
    {
        var u = Fract(time * FlowSpeed + (float) citizen.Ordinal / PerCell);
        var angle = MathF.Tau * (Twist * u + (float) (citizen.Ordinal % Strands) / Strands) + time * Spin;

        // Quadratic Bézier from the entry edge through the cell center to the exit edge.
        var a = Vector3.Lerp(citizen.Home + flow.In * 0.5f, citizen.Home, u);
        var b = Vector3.Lerp(citizen.Home, citizen.Home + flow.Out * 0.5f, u);
        var tangent = b - a;
        var side = tangent.LengthSquared() > 1e-8f
            ? Vector3.Normalize(new Vector3(-tangent.Z, 0, tangent.X))
            : Vector3.UnitX;
        var helix = Vector3.Lerp(a, b, u)
                    + SwirlRadius * (MathF.Cos(angle) * side + (1 + MathF.Sin(angle)) * Vector3.UnitY);

        // Cubes grow out of an open start and shrink into an open end.
        var openStart = 1 - MathF.Min(1, flow.In.Length());
        var openEnd = 1 - MathF.Min(1, flow.Out.Length());
        var helixScale = (1 - openStart * (1 - SmoothStep(0, 0.25f, u))) * (1 - openEnd * SmoothStep(0.75f, 1, u));

        var ring = MathF.Tau * citizen.Ordinal / PerCell + time * Spin;
        var vortex = citizen.Home + new Vector3(
            MathF.Cos(ring) * VortexRadius,
            SwirlRadius * (1 + MathF.Sin(ring * Strands)),
            MathF.Sin(ring) * VortexRadius);

        var lone = openStart * openEnd;
        scale = float.Lerp(helixScale, 1, lone);
        return Vector3.Lerp(helix, vortex, lone);
    }

    /// <summary>
    ///     Fades the startle and returns the flinch offset: away and up, peaking halfway through.
    /// </summary>
    public static Vector3 Flinch(in Citizen citizen, ref Startle startle, float delta)
    {
        startle.Amount = MathF.Max(0, startle.Amount - delta * StartleRate);
        var envelope = 4 * startle.Amount * (1 - startle.Amount) * (0.5f + citizen.Seed);
        return envelope * (startle.Direction * FlinchDistance + Vector3.UnitY * FlinchHop);
    }

    /// <summary>
    ///     A pulse running along a solved trail from its start, <paramref name="since"/> seconds after solving.
    ///     Cells it passed keep an <paramref name="afterglow"/>.
    /// </summary>
    public static float Wave(int slot, float since, out float afterglow)
    {
        var distance = since * WaveSpeed - slot;
        afterglow = SmoothStep(0, 1, distance);
        return MathF.Exp(-distance * distance);
    }

    public static float Glow(in Citizen citizen, float time) => 1 + 0.5f * MathF.Sin(time * 3f + MathF.Tau * citizen.Seed);

    public static float MoveTowards(float value, float goal, float step) =>
        MathF.Abs(goal - value) <= step ? goal : value + MathF.Sign(goal - value) * step;

    public static float SmoothStep(float x) => SmoothStep(0, 1, x);

    private static float SmoothStep(float from, float to, float x)
    {
        var t = Math.Clamp((x - from) / (to - from), 0, 1);
        return t * t * (3 - 2 * t);
    }

    private static float Fract(float x) => x - MathF.Floor(x);
}
