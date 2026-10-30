class_name Interlude
extends Control
## A scene the campaign shows between levels; it waits until the interlude frees itself.
## By default it fades in while drifting up, holds and fades out again. Override [method play] to animate it
## differently, awaiting each part with [method step]. The skip action (Space) stops the sequence and fades it out.

@export var fade := 0.75
@export var hold := 3.0
## Pixels the interlude rises while it fades in.
@export var drift := 24.0
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
	modulate.a = 0
	offset_transform_position.y = drift
	if not await step(Tweens.play_all(self, [
		Tweens.modulate_alpha(1.0, fade, Out.SINE),
		Tweens.control_offset_transform_position_y(0.0, fade, Out.CUBIC),
	])):
		return
	await step(Tweens.play_all(self, [
		Tweens.modulate_alpha(0.0, fade, In.SINE, hold),
		Tweens.control_offset_transform_position_y(-drift / 2, fade, In.CUBIC, hold),
	]))


## Waits for a playback handle, group or chain to end. Returns false unless it completed: when it was skipped, or the
## interlude left the tree meanwhile.
func step(playback: Variant) -> bool:
	_playback = playback
	var reason: int = await playback.end
	_playback = null
	return reason == Tweens.Reason.COMPLETED
