class_name Interlude
extends Control
## A scene the campaign shows between levels; it waits until the interlude frees itself.
## By default it fades in, holds and fades out again. Override [method play] to animate it differently.

@export var fade := 0.75
@export var hold := 3.0


func _ready() -> void:
	await play()
	queue_free()


func play() -> void:
	modulate.a = 0
	var tween := create_tween()
	tween.tween_property(self, "modulate:a", 1.0, fade)
	tween.tween_interval(hold)
	tween.tween_property(self, "modulate:a", 0.0, fade)
	await tween.finished
