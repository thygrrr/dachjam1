extends Interlude
## The title sequence: the full quote fades in, everything but its "grau" fades out again, and ".exe" is typed
## behind it. Run on its own, it changes to [member next_scene] when done; as an interlude it frees itself.

## Scene to change to when the intro ran as the current scene.
@export_file("*.tscn") var next_scene := "res://campaign.tscn"
## Seconds the author's name trails the quote.
@export var author_delay := 0.6
## Seconds "grau" stands alone before the typing begins.
@export var linger := 1.0
## Seconds before each typed character; the last one repeats for any further characters.
@export var typing: PackedFloat32Array = [0.18, 0.12, 0.2, 0.14]
## Seconds the finished title holds before it fades out.
@export var title_hold := 2.5

@onready var quote: Control = $quote
@onready var quote_full: Label = $"quote/quote-full"
@onready var quote_author: Label = $"quote/quote-author"
@onready var quote_exe: Label = $"quote/quote-full/quote-exe"

## The word quote-exe starts with, found in quote-full.
var word: String:
	get:
		return quote_exe.text.substr(0, quote_exe.text.find("."))


func _ready() -> void:
	quote_full.resized.connect(align)
	align.call_deferred()
	super()


## Moves quote-exe onto the word in quote-full, wherever quote-full wraps it.
func align() -> void:
	var at := quote_full.text.find(word)
	quote_exe.position = quote_full.get_character_bounds(at).position - quote_exe.get_character_bounds(0).position


func finish() -> void:
	if self == get_tree().current_scene and next_scene:
		get_tree().change_scene_to_file(next_scene)
	else:
		queue_free()


# Labels fade with self_modulate, so quote-full fades without quote-exe; offset transforms on the quote column move
# everything in it together.
func play() -> void:
	var shown := word.length()
	for label: Label in [quote_full, quote_author, quote_exe]:
		label.self_modulate.a = 0
	quote.offset_transform_position.y = drift
	quote_author.offset_transform_position.x = drift
	quote_exe.visible_characters = shown

	if not await step(Tweens.group([
		Tweens.play(quote_full, Tweens.self_modulate_alpha(1.0, fade, Out.SINE)),
		Tweens.play(quote, Tweens.control_offset_transform_position_y(0.0, fade * 2, Out.CUBIC)),
		Tweens.play(quote_author, Tweens.self_modulate_alpha(1.0, fade, Out.SINE, author_delay)),
		Tweens.play(quote_author, Tweens.control_offset_transform_position_x(0.0, fade * 2, Out.CUBIC, author_delay)),
	])):
		return

	# quote-exe's word appears the moment the rest begins to fade, so it never fades with it.
	if not await step(Tweens.group([
		Tweens.play(quote_exe, Tweens.self_modulate_alpha(1.0, 0.0, Tweens.Ease.LINEAR, hold)),
		Tweens.play(quote_full, Tweens.self_modulate_alpha(0.0, fade * 2, In.SINE | Out.CUBIC, hold)),
		Tweens.play(quote_author, Tweens.self_modulate_alpha(0.0, fade, In.SINE, hold)),
	])):
		return

	var keys: Array[TweensGdDefinition] = []
	for count in range(shown + 1, quote_exe.text.length() + 1):
		var delay := typing[mini(keys.size(), typing.size() - 1)]
		if keys.is_empty():
			delay += linger
		keys.append(Tweens.label_visible_characters(count, 0.0, Tweens.Ease.LINEAR, delay))
	if not await step(Tweens.chain(quote_exe, keys)):
		return

	await step(Tweens.play(self, Tweens.modulate_alpha(0.0, fade * 2, In.SINE, title_hold)))
