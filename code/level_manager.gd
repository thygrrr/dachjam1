extends Node

#@onready var levels : Array[PackedScene] = [
#	preload("res://levels/level_test.tscn"),
#	preload("res://levels/level_1.tscn"),
#	preload("res://levels/level_2.tscn"),
#	preload("res://levels/level_3.tscn")
#]

@onready var levels : Array[PackedScene] = [
	#preload("uid://bo6gr2w24mxr1"),
	#preload("uid://m7dutl3kt23p"),
	#preload("uid://2mcgggxjs382"),
	#preload("uid://dod7vdgcc64td")
]

var current : Node3D
var current_index : int

var loading: bool

func retry():
	#await AppUi.fade_overlay.fade_out()
	self.load(current_index)

func load(choice: int) -> void:
	if loading:
		return
	loading = true
	if (choice >= 0 and choice < levels.size()):
		#await AppUi.fade_overlay.fade_out()
		prints("Loading level", choice)
		current_index = choice
		if current:
			current.queue_free()
			current = null
		current = levels[choice].instantiate()
		get_tree().root.add_child(current)
		await get_tree().process_frame
		#AppUi.fade_overlay.fade_in()
	loading = false
