extends Node3D
class_name SmoothRemoteTransform3D

@export var target : Node3D;
@export var motion_smooth_time : float = 0.1
@export var angular_smooth_time : float = 0.15

@export var scaling : Vector3 = Vector3.ONE

@export var follow_position : bool = true
@export var follow_rotation : bool = false

var linear_velocity : Array[Vector3] = [Vector3.ZERO]
var angular_velocity : Array[Vector3] = [Vector3.ZERO]

func _process(delta: float) -> void:
	if target and follow_position:
		_follow_position(delta)
	if target and follow_rotation:
		_follow_rotation(delta)


func _follow_rotation(delta: float) -> void:
	var current_eulers = global_rotation
	var goal_eulers := target.global_rotation
	var smooth_eulers = Smooth.Eulers(current_eulers, goal_eulers, angular_smooth_time, delta, angular_velocity)
	target.quaternion = Quaternion.from_euler(smooth_eulers)

func _follow_position(delta: float) -> void:
	target.position = Smooth.Pos(target.position, global_position * scaling, motion_smooth_time, delta, linear_velocity)

func _look_rotation(from : Vector3, dir : Vector3, up : Vector3) -> Quaternion:
	if up.cross(dir).is_zero_approx():
		return Quaternion.IDENTITY

	if dir.is_zero_approx():
		return Quaternion.IDENTITY

	var angle = from.signed_angle_to(dir.normalized(), up)
	return Quaternion(up, angle);

func warp() -> void:
	if follow_position:
		linear_velocity = [Vector3.ZERO]
		target.position = global_position * scaling
	if follow_rotation:
		angular_velocity = [Vector3.ZERO]
		target.rotation = global_rotation
