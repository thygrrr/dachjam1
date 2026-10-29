# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name TweensGdBinding
extends RefCounted
## Reads and writes storage independently of timing and value sampling.
## Each playback copies configuration before preparation. Constructors must accept no arguments.

## Copies configuration for one playback. Captured Objects and Callables remain shared.
## Override to copy custom mutable data or omit transient playback state.
## Allocate playback bindings in [method prepare]. An unprepared copy can be discarded
## without calling [method release]; its RefCounted references are freed normally.
func copy() -> TweensGdBinding:
	var script: GDScript = get_script()
	var result: TweensGdBinding = script.new()
	# Match definition snapshots: scalar configuration is copied; captured objects remain shared.
	for field in get_property_list():
		var usage: int = field["usage"]
		var field_name: StringName = field["name"]
		if usage & PROPERTY_USAGE_SCRIPT_VARIABLE: result.set(field_name, get(field_name))
	return result

## Prepares bindings before the initial read. Return an error string, empty on success.
func prepare(_target: Object) -> String:
	return ""

## Reads the current value. Called at start and during relative playback that follows the target.
func read(_target: Object) -> Variant:
	return null

## Writes an interpolated value. Return an error string, empty on success.
func write(_target: Object, _value: Variant) -> String:
	return "Override the adapter's write method."

## Restores captured state on natural completion without RETAIN_FINAL_VALUE. Defaults to [method write].
func restore(target: Object, initial: Variant) -> String:
	return write(target, initial)

## Validates endpoints and sampled values. Return an error string, empty on success.
func validate_value(value: Variant) -> String:
	if not TweensGdInterpolation.supported(value) or not TweensGdInterpolation.finite(value):
		return "Adapter values must be supported, finite values."
	if typeof(value) == TYPE_QUATERNION:
		var rotation: Quaternion = value
		if rotation.length_squared() == 0.0 or not is_finite(rotation.length_squared()):
			return "Quaternion endpoints must have finite, nonzero squared length."
	return ""

## Releases bindings owned by this playback copy once preparation was attempted, including
## after failed preparation. Copies discarded before preparation do not call this hook.
## Return an error string, empty on success. Captured shared Objects must remain usable.
func release() -> String:
	return ""
