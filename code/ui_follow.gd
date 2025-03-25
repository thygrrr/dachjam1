extends Control

@export var target : Node3D

@onready var content : Control = $Content
@onready var camera : Camera3D = get_viewport().get_camera_3d()

var critter_view : Node3D


func _position():
	if camera and target:
		var screen_position := camera.unproject_position(target.global_position)
		global_position = global_position * 0.8 + 0.2 * screen_position


func _process(_delta: float) -> void:
	_position()
