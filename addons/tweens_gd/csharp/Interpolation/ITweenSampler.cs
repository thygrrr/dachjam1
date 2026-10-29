// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
namespace tweens.gd;

// The runtime consumes storage and sampling independently. Existing definition subclasses
// provide both facets without allocating adapter objects or bypassing virtual overrides.
internal interface ITweenSampler<TValue> where TValue : struct
{
    void Prepare(TValue from, TValue to);
    TValue Sample(TValue from, TValue to, float weight);
    TValue SampleOffset(TValue from, TValue to, float weight);
}
