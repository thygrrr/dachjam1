# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name InOut
extends RefCounted
## Matching half-duration legs. Back/Elastic/Jump overshoot and Bounce first-rebound depth use the full tween
## range.

## Constant velocity.
const LINEAR := In.LINEAR | Out.LINEAR
## Sinusoidal easing.
const SINE := In.SINE | Out.SINE
## Quadratic easing.
const QUAD := In.QUAD | Out.QUAD
## Cubic easing.
const CUBIC := In.CUBIC | Out.CUBIC
## Quartic easing.
const QUART := In.QUART | Out.QUART
## Quintic easing.
const QUINT := In.QUINT | Out.QUINT
## Exponential easing.
const EXPO := In.EXPO | Out.EXPO
## Circular easing.
const CIRC := In.CIRC | Out.CIRC
## Alias for the default 30% Back curve.
const BACK := In.BACK30 | Out.BACK30
## Alias for the default 30% Elastic curve.
const ELASTIC := In.ELASTIC30 | Out.ELASTIC30
## Alias for the default 30% Bounce curve.
const BOUNCE := In.BOUNCE30 | Out.BOUNCE30
## Cubic smoothing with zero endpoint velocity.
const SMOOTH_STEP := In.SMOOTH_STEP | Out.SMOOTH_STEP
## Quintic smoothing with zero endpoint velocity and acceleration.
const SMOOTHER_STEP := In.SMOOTHER_STEP | Out.SMOOTHER_STEP
## Back with 10% overshoot of the full tween range.
const BACK10 := In.BACK10 | Out.BACK10
## Elastic with 10% overshoot of the full tween range.
const ELASTIC10 := In.ELASTIC10 | Out.ELASTIC10
## Back with 20% overshoot of the full tween range.
const BACK20 := In.BACK20 | Out.BACK20
## Back with 30% overshoot of the full tween range.
const BACK30 := In.BACK30 | Out.BACK30
## Back with 40% overshoot of the full tween range.
const BACK40 := In.BACK40 | Out.BACK40
## Back with 50% overshoot of the full tween range.
const BACK50 := In.BACK50 | Out.BACK50
## Elastic with 20% overshoot of the full tween range.
const ELASTIC20 := In.ELASTIC20 | Out.ELASTIC20
## Elastic with 30% overshoot of the full tween range.
const ELASTIC30 := In.ELASTIC30 | Out.ELASTIC30
## Elastic with 40% overshoot of the full tween range.
const ELASTIC40 := In.ELASTIC40 | Out.ELASTIC40
## Elastic with 50% overshoot of the full tween range.
const ELASTIC50 := In.ELASTIC50 | Out.ELASTIC50
## Bounce with a first rebound depth of 10% of the full tween range.
const BOUNCE10 := In.BOUNCE10 | Out.BOUNCE10
## Bounce with a first rebound depth of 20% of the full tween range.
const BOUNCE20 := In.BOUNCE20 | Out.BOUNCE20
## Bounce with a first rebound depth of 30% of the full tween range.
const BOUNCE30 := In.BOUNCE30 | Out.BOUNCE30
## Bounce with a first rebound depth of 40% of the full tween range.
const BOUNCE40 := In.BOUNCE40 | Out.BOUNCE40
## Bounce with a first rebound depth of 50% of the full tween range.
const BOUNCE50 := In.BOUNCE50 | Out.BOUNCE50
## Alias for the default 30% Jump curve.
const JUMP := In.JUMP30 | Out.JUMP30
## Jump with 10% overshoot of the full tween range.
const JUMP10 := In.JUMP10 | Out.JUMP10
## Jump with 20% overshoot of the full tween range.
const JUMP20 := In.JUMP20 | Out.JUMP20
## Jump with 30% overshoot of the full tween range.
const JUMP30 := In.JUMP30 | Out.JUMP30
## Jump with 40% overshoot of the full tween range.
const JUMP40 := In.JUMP40 | Out.JUMP40
## Jump with 50% overshoot of the full tween range.
const JUMP50 := In.JUMP50 | Out.JUMP50
