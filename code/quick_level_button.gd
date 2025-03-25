extends Button

@export var level = 0
func _ready() -> void:
	pressed.connect(_pressed)

func _pressed() -> void:
	#AppUi.pause_overlay.load(level)
	pass
