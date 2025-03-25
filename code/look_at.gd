extends Node3D

@export var target : Node3D
@export var aim_amplitude : Vector3 = Vector3.ONE
@export var pos_amplitude : Vector3 = Vector3.ONE
@export var frequency : float = 0.02

@onready var noise = FastNoiseLite.new()

var time : float = 0

func _ready() -> void:
	noise.noise_type = FastNoiseLite.TYPE_SIMPLEX_SMOOTH
	noise.frequency = frequency



# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	time += delta

	if not target:
		return

	var target_position = target.global_position

	var arm_pos = pos_amplitude * Vector3(noise.get_noise_2d(3, time), noise.get_noise_2d(13, time), noise.get_noise_2d(7, time))
	position = arm_pos

	var aim_pos = aim_amplitude * Vector3(noise.get_noise_2d(1, time), noise.get_noise_2d(5, time), noise.get_noise_2d(10, time))
	global_transform = global_transform.looking_at(target_position + aim_pos)
