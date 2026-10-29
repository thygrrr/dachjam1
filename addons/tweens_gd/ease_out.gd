# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name Out
extends RefCounted
## Out curves. Combine one In and one Out with |, as in [code]In.SINE | Out.CUBIC[/code]; a single curve runs
## on its own.

## Omit this leg; the other curve runs over the entire duration.
const NONE := 0
## Constant velocity.
const LINEAR := 1 << 21
## Sinusoidal easing.
const SINE := 1 << 22
## Quadratic easing.
const QUAD := 1 << 23
## Cubic easing.
const CUBIC := 1 << 24
## Quartic easing.
const QUART := 1 << 25
## Quintic easing.
const QUINT := 1 << 26
## Exponential easing.
const EXPO := 1 << 27
## Circular easing.
const CIRC := 1 << 28
## Alias for the default 30% Back curve.
const BACK := 1 << 43
## Alias for the default 30% Elastic curve.
const ELASTIC := 1 << 47
## Alias for the default 30% Bounce curve.
const BOUNCE := 1 << 55
## Cubic smoothing with zero endpoint velocity.
const SMOOTH_STEP := 1 << 32
## Quintic smoothing with zero endpoint velocity and acceleration.
const SMOOTHER_STEP := 1 << 33
## Back with 10% overshoot of the full tween range.
const BACK10 := 1 << 29
## Elastic with 10% overshoot of the full tween range.
const ELASTIC10 := 1 << 30
## Back with 20% overshoot of the full tween range.
const BACK20 := 1 << 42
## Back with 30% overshoot of the full tween range.
const BACK30 := 1 << 43
## Back with 40% overshoot of the full tween range.
const BACK40 := 1 << 44
## Back with 50% overshoot of the full tween range.
const BACK50 := 1 << 45
## Elastic with 20% overshoot of the full tween range.
const ELASTIC20 := 1 << 46
## Elastic with 30% overshoot of the full tween range.
const ELASTIC30 := 1 << 47
## Elastic with 40% overshoot of the full tween range.
const ELASTIC40 := 1 << 48
## Elastic with 50% overshoot of the full tween range.
const ELASTIC50 := 1 << 49
## Bounce with a first rebound depth of 10% of the full tween range.
const BOUNCE10 := 1 << 31
## Bounce with a first rebound depth of 20% of the full tween range.
const BOUNCE20 := 1 << 54
## Bounce with a first rebound depth of 30% of the full tween range.
const BOUNCE30 := 1 << 55
## Bounce with a first rebound depth of 40% of the full tween range.
const BOUNCE40 := 1 << 56
## Bounce with a first rebound depth of 50% of the full tween range.
const BOUNCE50 := 1 << 57
## Alias for the default 30% Jump curve.
const JUMP := (1 << 63) | (1 << 6)
## Jump with 10% overshoot of the full tween range.
const JUMP10 := (1 << 62) | (1 << 63)
## Jump with 20% overshoot of the full tween range.
const JUMP20 := (1 << 62) | (1 << 6)
## Jump with 30% overshoot of the full tween range.
const JUMP30 := (1 << 63) | (1 << 6)
## Jump with 40% overshoot of the full tween range.
const JUMP40 := (1 << 62) | (1 << 7)
## Jump with 50% overshoot of the full tween range.
const JUMP50 := (1 << 63) | (1 << 7)
