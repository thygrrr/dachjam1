using System.Numerics;

namespace jam;

/// <summary>
///     Identity of a citizen cube: the center of its home cell, its index within that cell, and a seed in [0, 1).
/// </summary>
public readonly record struct Citizen(Vector3 Home, int Ordinal, float Seed);

/// <summary>
///     The path through a citizen's cell, as offsets from the cell center to the previous and the next cell.
///     A zero offset means the path starts or ends in this cell. <see cref="Out"/> eases towards <see cref="OutGoal"/>.
/// </summary>
public record struct Flow(Vector3 In, Vector3 Out, Vector3 OutGoal);

/// <summary>
///     How much a citizen is part of a stream, from 0 (idle) to 1 (active). <see cref="Value"/> moves to <see cref="Goal"/>.
/// </summary>
public record struct Activation(float Value, float Goal);
