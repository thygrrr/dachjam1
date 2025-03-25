extends Node3D

@export var trauma : float = 0.0
@export var amplitude : Vector2 = Vector2(0.15, 0.3)
@export var frequency : float = 15

@onready var noise : FastNoiseLite = FastNoiseLite.new()


var time : float
var velocity : Array[Vector3] = [Vector3.ZERO]

func _ready() -> void:
	noise.noise_type = FastNoiseLite.TYPE_PERLIN
	noise.frequency = frequency

func _process(delta: float) -> void:
	time += delta
	var goal := trauma * trauma * (amplitude.x * global_basis.x * noise.get_noise_2d(1, time) + amplitude.y * global_basis.y * noise.get_noise_2d(4, time))
	rotation = Smooth.Eulers(rotation, goal, 0.05, delta, velocity)

	var exponent = delta * 120.0
	var k = pow(0.98, exponent)
	trauma *= k
