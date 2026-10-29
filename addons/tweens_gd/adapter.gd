# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name TweensGdAdapter
extends TweensGdBinding
## Reads, writes, and interpolates values in custom storage.
##
## Override [method read] and [method write], and optionally [method interpolate].
## Each playback receives its own [method copy]. Hooks that return [String] use an empty
## string for success and a message for failure. Constructors must accept no arguments.
## C# equivalent: the protected operations of [code]TweenDefinition[/code].

## Copies adapter configuration with its concrete script type; captured references remain shared.
func copy() -> TweensGdAdapter:
	return super.copy() as TweensGdAdapter

var _captured_type: int = TYPE_NIL
var _interpolating_offset := false
## Working color coordinates: OKLab=0, sRGB=1, linear RGB=2. Copied from the definition at activation.
var color_space := 0
## Premultiplied=0, straight=1. Copied from the definition at activation.
var alpha_mode := 0
## Target API RGB encoding: sRGB=0, linear=1. Copied from the definition at activation.
var color_encoding := 0

## Interpolates endpoints using eased [param weight], which may overshoot [code][0, 1][/code].
func interpolate(from: Variant, to: Variant, weight: float) -> Variant:
	if typeof(from) == TYPE_COLOR:
		var a: Color = from
		var b: Color = to
		if _interpolating_offset: return a.lerp(b, weight)
		return TweensGdInterpolation.interpolate_color(a, b, weight, color_space, alpha_mode, color_encoding)
	return TweensGdInterpolation.interpolate(from, to, weight, _captured_type if _captured_type != TYPE_NIL else typeof(from))

## Relative offsets and factors use Godot components by default and dispatch custom [method interpolate] overrides.
func interpolate_offset(from: Variant, to: Variant, weight: float) -> Variant:
	var previous := _interpolating_offset
	_interpolating_offset = true
	var result: Variant = interpolate(from, to, weight)
	_interpolating_offset = previous
	return result
