extends Interlude
## The title sequence: the quote and its author are typed, everything but its "grau" fades out again, and ".exe" is typed
## behind it. Run on its own, it changes to [member next_scene] when done; as an interlude it frees itself.

## Scene to change to when the intro ran as the current scene.
@export_file("*.tscn") var next_scene := "res://campaign.tscn"
## Seconds between the typed quote and its author.
@export var author_delay := 0.6
## Seconds "grau" stands alone before the typing begins.
@export var linger := 1.0
## Seconds the finished title holds before it fades out.
@export var title_hold := 2.5

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


# Labels fade with self_modulate, so quote-full fades without quote-exe.
func play() -> void:
	var shown := word.length()
	blank([quote_full, quote_author])
	quote_exe.self_modulate.a = 0
	quote_exe.visible_characters = shown

	if not await step(type_in(quote_full)):
		return
	if not await step(type_in(quote_author, author_delay)):
		return

	# quote-exe's word appears the moment the rest begins to fade, so it never fades with it.
	if not await step(Tweens.group([
		Tweens.play(quote_exe, Tweens.self_modulate_alpha(1.0, 0.0, Tweens.Ease.LINEAR, hold)),
		Tweens.play(quote_full, Tweens.self_modulate_alpha(0.0, fade * 2, In.SINE | Out.CUBIC, hold)),
		Tweens.play(quote_author, Tweens.self_modulate_alpha(0.0, fade, In.SINE, hold)),
	])):
		return

	if not await step(type_in(quote_exe, linger, shown)):
		return

	await step(Tweens.play(self, Tweens.modulate_alpha(0.0, fade * 2, In.SINE, title_hold)))
