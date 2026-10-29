# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
extends "adapter.gd"
## Reads and writes compound properties for generated helpers.
##
## Ordinary animation uses named [Tweens] factories. This adapter combines scalar paths
## into Vector2 or Vector4 values, or converts global Euler rotation to a Quaternion.

## Ordered scalar component paths for a Vector2 or Vector4 value.
var paths: Array[NodePath] = []
## Animate global_rotation through normalized quaternion interpolation instead of [member paths].
var global_quaternion := false

func read(target: Object) -> Variant:
	if global_quaternion:
		var euler: Vector3 = target.get(&"global_rotation")
		return Quaternion.from_euler(euler)
	var x: float = target.get_indexed(paths[0])
	var y: float = target.get_indexed(paths[1])
	if paths.size() == 2: return Vector2(x, y)
	var z: float = target.get_indexed(paths[2])
	var w: float = target.get_indexed(paths[3])
	return Vector4(x, y, z, w)

func write(target: Object, value: Variant) -> String:
	if global_quaternion:
		var rotation: Quaternion = value
		target.set(&"global_rotation", rotation.get_euler())
	else:
		for index in range(paths.size()):
			if not is_instance_valid(target): return "The target was freed during a compound write."
			target.set_indexed(paths[index], value[index])
	return ""
