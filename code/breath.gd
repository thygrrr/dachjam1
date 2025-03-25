extends Node3D

@export var excitement : float = 3.0

@onready var mesh : Node3D = self
@onready var normal : Node3D = $Normal
@onready var blink : Node3D = $Blink

var noise : FastNoiseLite
var age : float
var blink_time : float

func _ready() -> void:
	noise = FastNoiseLite.new()
	age = randf() * 100.0
	excitement *= randf_range(0.5,2.0)
	if blink:
		blink.hide()

func _process(delta: float):
	age += delta * excitement
	var breath = (sin(age) + 1.0) * 0.03
	mesh.scale = Vector3.ONE + Vector3(noise.get_noise_2d(age, 1) * 0.1, breath, noise.get_noise_2d(age, 7) * 0.1)

	_blink(delta)

func _blink(delta: float):
	if not blink or not normal:
		return

	blink_time -= delta

	if blink_time <= 0:
		blink_time = randf_range(1, 4)
		normal.hide()
		blink.show()
		await get_tree().create_timer(0.1).timeout
		blink.hide()
		normal.show()
