# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name BlendType
extends RefCounted
## How different In and Out curves meet inside the centered blend window, as in
## [code]definition.blend_type = BlendType.HERMITE[/code].

## Two cubic segments preserve the midpoint; its velocity uses modified Akima (makima) weights.
const MAKIMA := 0
## Two cubic Hermite segments preserve the midpoint and match acceleration there.
const HERMITE := 1
## Crossfade the families' paired profiles with a smooth weight.
const SMOOTH_STEP := 2
## Crossfade with a linear weight; velocity may jump at the window edges.
const LINEAR := 3
