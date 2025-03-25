extends Node3D
class_name SmoothFollow

@export var target : Node3D;
@export var motion_smooth_time : float = 0.1
@export var angular_smooth_time : float = 0.15

@export var follow_position : bool = true
@export var follow_rotation : bool = false

var linear_velocity : Array[Vector3] = [Vector3.ZERO]
var angular_velocity : Array[Vector3] = [Vector3.ZERO]

func _process(delta: float) -> void:
	if not target:
		return
	_follow_rotation(delta)
	_follow_position(delta)

func _follow_rotation(delta: float) -> void:
	var current_eulers = global_rotation
	var goal_eulers := target.global_rotation
	var smooth_eulers = Smooth.Eulers(current_eulers, goal_eulers, angular_smooth_time, delta, angular_velocity)
	quaternion = Quaternion.from_euler(smooth_eulers)

func _follow_position(delta: float) -> void:
	position = Smooth.Pos(position, target.global_position, motion_smooth_time, delta, linear_velocity)
