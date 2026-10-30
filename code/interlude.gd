class_name Interlude
extends Control
## A scene the campaign shows between levels; it waits until the interlude frees itself.
## By default it types its labels one after another, holds and fades out while drifting up. Override [method play] to animate it
## differently, awaiting each part with [method step]. The skip action (Space) stops the sequence and fades it out.

@export var fade := 0.75
@export var hold := 3.0
## Pixels the interlude rises while it fades out.
@export var drift := 12.0
## Seconds before each typed character, cycling for longer texts.
@export var typing: PackedFloat32Array = [0.18, 0.12, 0.2, 0.14]
## Seconds the fade out takes after a skip.
@export var skip_fade := 0.25

var skipped := false
var _playback: Variant


func _ready() -> void:
	await play()
	if not is_inside_tree() or is_queued_for_deletion():
		return
	if skipped:
		await Tweens.play(self, Tweens.modulate_alpha(0.0, skip_fade, Out.CUBIC)).end
	finish()


func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("skip") and not is_queued_for_deletion():
		get_viewport().set_input_as_handled()
		skip()


## Cancels the running [method step]; [method play] then returns and the interlude fades out.
func skip() -> void:
	if skipped:
		return
	skipped = true
	if _playback:
		_playback.cancel()


## Called once the interlude has played or was skipped. Frees it by default.
func finish() -> void:
	queue_free()


func play() -> void:
	var labels := find_children("*", "Label")
	blank(labels)
	for label: Label in labels:
		if not await step(type_in(label)):
			return
	await step(Tweens.play_all(self, [
		Tweens.modulate_alpha(0.0, fade, In.SINE, hold),
		Tweens.control_offset_transform_position_y(-drift, fade, In.CUBIC, hold),
	]))


## Hides the text of [param labels] until [method type_in] types it.
func blank(labels: Array) -> void:
	for label: Label in labels:
		label.visible_characters_behavior = TextServer.VC_CHARS_AFTER_SHAPING
		label.visible_characters = 0


## Types [param label]'s text from character [param from] on, at the [member typing] cadence, waiting [param delay]
## seconds longer before the first character. The text is laid out in full, so it never shifts while it is typed.
func type_in(label: Label, delay := 0.0, from := 0) -> TweensGdChain:
	label.visible_characters_behavior = TextServer.VC_CHARS_AFTER_SHAPING
	label.visible_characters = from
	var keys: Array[TweensGdDefinition] = []
	for count in range(from + 1, label.text.length() + 1):
		var wait := typing[keys.size() % typing.size()]
		if keys.is_empty():
			wait += delay
		keys.append(Tweens.label_visible_characters(count, 0.0, Tweens.Ease.LINEAR, wait))
	# A chain needs a key even when there is nothing left to type.
	if keys.is_empty():
		keys.append(Tweens.label_visible_characters(from, 0.0, Tweens.Ease.LINEAR, delay))
	return Tweens.chain(label, keys)


## Waits for a playback handle, group or chain to end. Returns false unless it completed: when it was skipped, or the
## interlude left the tree meanwhile.
func step(playback: Variant) -> bool:
	_playback = playback
	var reason: int = await playback.end
	_playback = null
	return reason == Tweens.Reason.COMPLETED
