extends Control
class_name Cutscene

func _ready():
	visible = false
	for child in get_children():
		if child is Cutscene:
			child.visible = false
			child.scale = Vector2.ZERO


func play() -> void:
	visible = true
	if get_parent() is Cutscene:
		scale = Vector2.ZERO
		var tween = create_tween()
		tween.set_ease(Tween.EASE_OUT).set_trans(Tween.TRANS_BACK)
		tween.tween_property(self, "scale", Vector2.ONE, 0.5)
		await tween.finished

	for child in get_children():
		if child is Cutscene:
			await child.play()

	if get_parent() is Cutscene:
		await seconds(2)

	if get_parent() is Cutscene:
		var tween = create_tween()
		tween.set_ease(Tween.EASE_IN).set_trans(Tween.TRANS_BACK)
		tween.tween_property(self, "scale", Vector2.ZERO, 0.5).set_delay(2)



func seconds(amount: float):
	await get_tree().create_timer(amount).timeout
