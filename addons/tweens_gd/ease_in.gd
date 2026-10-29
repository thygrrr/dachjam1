# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name In
extends RefCounted
## In curves. Combine one In and one Out with |, as in [code]In.SINE | Out.CUBIC[/code]; a single curve runs
## on its own.

## Omit this leg; the other curve runs over the entire duration.
const NONE := 0
## Constant velocity.
const LINEAR := 1 << 8
## Sinusoidal easing.
const SINE := 1 << 9
## Quadratic easing.
const QUAD := 1 << 10
## Cubic easing.
const CUBIC := 1 << 11
## Quartic easing.
const QUART := 1 << 12
## Quintic easing.
const QUINT := 1 << 13
## Exponential easing.
const EXPO := 1 << 14
## Circular easing.
const CIRC := 1 << 15
## Alias for the default 30% Back curve.
const BACK := 1 << 35
## Alias for the default 30% Elastic curve.
const ELASTIC := 1 << 39
## Alias for the default 30% Bounce curve.
const BOUNCE := 1 << 51
## Cubic smoothing with zero endpoint velocity.
const SMOOTH_STEP := 1 << 19
## Quintic smoothing with zero endpoint velocity and acceleration.
const SMOOTHER_STEP := 1 << 20
## Back with 10% overshoot of the full tween range.
const BACK10 := 1 << 16
## Elastic with 10% overshoot of the full tween range.
const ELASTIC10 := 1 << 17
## Back with 20% overshoot of the full tween range.
const BACK20 := 1 << 34
## Back with 30% overshoot of the full tween range.
const BACK30 := 1 << 35
## Back with 40% overshoot of the full tween range.
const BACK40 := 1 << 36
## Back with 50% overshoot of the full tween range.
const BACK50 := 1 << 37
## Elastic with 20% overshoot of the full tween range.
const ELASTIC20 := 1 << 38
## Elastic with 30% overshoot of the full tween range.
const ELASTIC30 := 1 << 39
## Elastic with 40% overshoot of the full tween range.
const ELASTIC40 := 1 << 40
## Elastic with 50% overshoot of the full tween range.
const ELASTIC50 := 1 << 41
## Bounce with a first rebound depth of 10% of the full tween range.
const BOUNCE10 := 1 << 18
## Bounce with a first rebound depth of 20% of the full tween range.
const BOUNCE20 := 1 << 50
## Bounce with a first rebound depth of 30% of the full tween range.
const BOUNCE30 := 1 << 51
## Bounce with a first rebound depth of 40% of the full tween range.
const BOUNCE40 := 1 << 52
## Bounce with a first rebound depth of 50% of the full tween range.
const BOUNCE50 := 1 << 53
## Alias for the default 30% Jump curve.
const JUMP := (1 << 59) | (1 << 60)
## Jump with 10% overshoot of the full tween range.
const JUMP10 := (1 << 58) | (1 << 59)
## Jump with 20% overshoot of the full tween range.
const JUMP20 := (1 << 58) | (1 << 60)
## Jump with 30% overshoot of the full tween range.
const JUMP30 := (1 << 59) | (1 << 60)
## Jump with 40% overshoot of the full tween range.
const JUMP40 := (1 << 58) | (1 << 61)
## Jump with 50% overshoot of the full tween range.
const JUMP50 := (1 << 59) | (1 << 61)
