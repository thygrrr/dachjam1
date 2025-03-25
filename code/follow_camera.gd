extends Node3D

var target : Node3D
@export var scaling = Vector3(0.9, 0, 1.1)

func _ready() -> void:
	target = $Camera.target

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	position = scaling * target.global_position
