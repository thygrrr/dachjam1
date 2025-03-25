extends CanvasLayer

func _get(property: StringName) -> Variant:
	return get_node(NodePath(property))
