extends Node

func _input(event: InputEvent) -> void:
	if event is InputEventKey and event.is_pressed() and not event.is_echo():
		var key_offset := (event as InputEventKey).keycode - KEY_0
		#await LevelManager.load(key_offset)
