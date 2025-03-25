extends Node

var camera : Camera3D:
	get:
		return %Camera3D

var follow : Node3D:
	get:
		return %CameraFollow.target
	set(value):
		%CameraFollow.target = value

#var look : Node3D:
#	get:
#		return %CameraLook.target
#	set(value):
#		%CameraLook.target = value

var shake : float:
	get:
		return %CameraShake.trauma
	set(value):
		%CameraShake.trauma = value

func warp():
	%CameraFollow.warp()
