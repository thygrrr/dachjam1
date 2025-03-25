extends Node

@onready var victory = $Victory
@onready var success = $Success

@onready var calm = $Calm
@onready var main = $Main

var mtween : Tween
var ctween : Tween
func fade_main() -> void:
	if mtween:
		mtween.stop()
	if ctween:
		ctween.stop()
	mtween = create_tween()
	mtween.tween_property(main, "volume_db", 0, 1)
	ctween = create_tween()
	ctween.tween_property(calm, "volume_db", -80, 5)

func fade_calm() -> void:
	if mtween:
		mtween.stop()
	if ctween:
		ctween.stop()
	ctween = create_tween()
	ctween.tween_property(calm, "volume_db", 0, 1)
	mtween = create_tween()
	mtween.tween_property(main, "volume_db", -80, 5)
