# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
extends "adapter.gd"
## Custom storage configured with Callables. Prefer [method Tweens.custom] to create one.

## Receives the target and returns its current value.
var getter: Callable
## Receives the target and value. A String return reports failure; an empty String or other return means success.
var setter: Callable
## Optional Callable receiving from, to, and eased weight. Weight may overshoot [code][0, 1][/code].
var interpolator: Callable
## Optional Callable receiving a value and returning an error String, empty on success.
var validator: Callable

func prepare(_target: Object) -> String:
	if not getter.is_valid() or not setter.is_valid(): return "Custom tweens need valid getter and setter Callables."
	for callback: Callable in [interpolator, validator]:
		if not callback.is_null() and not callback.is_valid(): return "A custom adapter Callable is invalid."
	return ""

func read(target: Object) -> Variant:
	return getter.call(target)

func write(target: Object, value: Variant) -> String:
	if not setter.is_valid(): return "The custom setter Callable is no longer valid."
	var result: Variant = setter.call(target, value)
	return result if result is String else ""

func interpolate(from: Variant, to: Variant, weight: float) -> Variant:
	if interpolator.is_null(): return super.interpolate(from, to, weight)
	if not interpolator.is_valid(): return null
	return interpolator.call(from, to, weight)

func interpolate_offset(from: Variant, to: Variant, weight: float) -> Variant:
	if not interpolator.is_null(): return interpolate(from, to, weight)
	return super.interpolate_offset(from, to, weight)

func validate_value(value: Variant) -> String:
	if validator.is_null(): return super.validate_value(value)
	if not validator.is_valid(): return "The custom validator Callable is no longer valid."
	var result: Variant = validator.call(value)
	return result if result is String else "The custom validator must return an error string (empty on success)."
