# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
class_name TweensGdKeyframes
extends RefCounted
## Reusable parallel keyframe definition. Prepared native curves are shared between independent plays.
## Supply a channel-to-array Dictionary, or percentage stops mapping to sparse channel Dictionaries.
## Sparse frames reserve "interpolation" for the arriving mode; use channel arrays for a property with that name.
## Configure [member options] before playback; each play snapshots those settings.

const Types = preload("types.gd")

## Shared timing, easing and color policy. Endpoints and callbacks belong to individual channel tweens.
var options := TweensGdDefinition.new()
var _tracks: Dictionary = {}
var _prepared: Dictionary = {}
var _error := ""
var _interpolation := int(Types.Interpolation.SMOOTH)

## Copies and validates keys. Use Tweens.Interpolation constants or an existing easing constant.
func _init(keys: Dictionary = {}, seconds: float = 1.0, easing: int = 0, interpolation: int = Types.Interpolation.SMOOTH) -> void:
	options.duration = seconds
	options.ease = easing
	_interpolation = interpolation
	if keys.is_empty():
		_error = "An animation needs at least one channel."
		return
	var percentages: bool = typeof(keys.keys()[0]) in [TYPE_INT, TYPE_FLOAT]
	for key: Variant in keys:
		if percentages != (typeof(key) in [TYPE_INT, TYPE_FLOAT]):
			_error = "Do not mix percentage stops and channel names."
			return
		if percentages:
			var stop: float = key
			if not keys[key] is Dictionary or not is_finite(stop) or stop < 0 or stop > 100:
				_error = "Percentage stops must be in [0, 100] and contain channel Dictionaries."
				return
			var frame: Dictionary = keys[key]
			for channel: Variant in frame:
				if channel == "interpolation": continue
				_add(channel, stop, frame[channel], frame.get("interpolation", -1))
		else:
			var values := _array(keys[key])
			if values.size() < 2:
				_error = "Channel '%s' needs at least two values." % key
				return
			for i in values.size():
				_add(key, 100.0 * i / (values.size() - 1), values[i], -1)
	for path: String in _tracks:
		var entries: Array = _tracks[path]
		entries.sort_custom(func(a: Dictionary, b: Dictionary) -> bool: return a.at < b.at)
		for i in range(1, entries.size()):
			if entries[i].at == entries[i - 1].at: _error = "Duplicate stop %s on '%s'." % [entries[i].at, path]
		for other: String in _tracks:
			if path == other: continue
			var root := path.get_slice(":", 0)
			var other_root := other.get_slice(":", 0)
			if path.begins_with(other + ":") or other.begins_with(path + ":") or (root != other_root and
					root in ["rotation", "rotation_degrees", "quaternion"] and other_root in ["rotation", "rotation_degrees", "quaternion"]):
				_error = "Conflicting channels '%s' and '%s'." % [path, other]
	if _tracks.is_empty(): _error = "An animation needs at least one channel."
	if not _valid_interpolation(_interpolation): _error = "Unknown curve interpolation or easing."

static func _array(value: Variant) -> Array:
	match typeof(value):
		TYPE_ARRAY:
			var data: Array = value
			return data
		TYPE_PACKED_BYTE_ARRAY:
			var data: PackedByteArray = value
			return Array(data)
		TYPE_PACKED_INT32_ARRAY:
			var data: PackedInt32Array = value
			return Array(data)
		TYPE_PACKED_INT64_ARRAY:
			var data: PackedInt64Array = value
			return Array(data)
		TYPE_PACKED_FLOAT32_ARRAY:
			var data: PackedFloat32Array = value
			return Array(data)
		TYPE_PACKED_FLOAT64_ARRAY:
			var data: PackedFloat64Array = value
			return Array(data)
		TYPE_PACKED_VECTOR2_ARRAY:
			var data: PackedVector2Array = value
			return Array(data)
		TYPE_PACKED_VECTOR3_ARRAY:
			var data: PackedVector3Array = value
			return Array(data)
		TYPE_PACKED_COLOR_ARRAY:
			var data: PackedColorArray = value
			return Array(data)
		TYPE_PACKED_VECTOR4_ARRAY:
			var data: PackedVector4Array = value
			return Array(data)
	return []

static func _valid_interpolation(value: int) -> bool:
	return value in [Types.Interpolation.SMOOTH, Types.Interpolation.LINEAR, Types.Interpolation.STEP] or not is_nan(TweensGdEasing.evaluate(value, 0.5))

static func _native_mode(value: int) -> int:
	match value:
		Types.Interpolation.SMOOTH: return 0
		Types.Interpolation.LINEAR: return 1
		Types.Interpolation.STEP: return 2
	return 3

func _add(channel: Variant, stop: float, value: Variant, arriving: Variant) -> void:
	if not (channel is String or channel is StringName):
		_error = "Channel names must be Strings or StringNames."
		return
	if not arriving is int:
		_error = "Interpolation must be an integer constant."
		return
	var mode: int = arriving
	if (mode != -1 and not _valid_interpolation(mode)) or (stop == 0.0 and mode != -1):
		_error = "Invalid arriving interpolation on '%s' at %s." % [channel, stop]
		return
	if not TweensGdInterpolation.supported(value) or not TweensGdInterpolation.finite(value):
		_error = "Keyframe values must be finite supported values."
		return
	if value is Quaternion:
		var rotation: Quaternion = value
		if rotation.length_squared() == 0.0:
			_error = "Quaternion keys must be nonzero."
			return
	var aliases := {"x": "position:x", "y": "position:y", "z": "position:z", "alpha": "modulate:a"}
	var path: String = aliases.get(str(channel), str(channel))
	if path.is_empty():
		_error = "Channel paths must not be empty."
		return
	if not _tracks.has(path): _tracks[path] = []
	var entries: Array = _tracks[path]
	entries.append({"at": stop, "value": value, "interpolation": mode})

## Returns an error string for definition data; target bindings are validated before any tween starts.
func validate() -> String:
	if not _error.is_empty(): return _error
	if options.from_value != null or options.to_value != null or options.by_value != null or options.factor_from != 1.0 or options.factor_to != 1.0 or options.delta_from != null or options.delta_to != null:
		return "Keyframes define their own endpoints. Configure timing, easing and color policy through options."
	return options.validate()

func _compile(target: Object) -> Dictionary:
	var failure := validate()
	if not failure.is_empty(): return {"error": failure}
	if not is_instance_valid(target): return {"error": "An animation needs a valid target."}
	var definitions: Array[TweensGdDefinition] = []
	for path: String in _tracks:
		var current: Variant = TweensGdInterpolation.read_property(target, NodePath(path))
		if not is_instance_valid(target): return {"error": "The target became invalid during validation."}
		if not TweensGdInterpolation.supported(current): return {"error": "Channel '%s' does not resolve to a supported value." % path}
		var value_type := typeof(current)
		var cache_key := "%s:%s:%s:%s:%s" % [path, value_type, options.color_space, options.alpha_mode, options.color_encoding]
		var curve: TweensGdKeyframeCurve = _prepared.get(cache_key)
		if curve == null:
			var values: Array = []
			var stops := PackedFloat64Array()
			var modes := PackedInt64Array()
			var eases := PackedInt64Array()
			for entry: Dictionary in _tracks[path]:
				var value: Variant = entry.value
				if typeof(value) in [TYPE_INT, TYPE_FLOAT]:
					var number: float = value
					if value_type == TYPE_FLOAT: value = number
					elif value_type == TYPE_INT and typeof(value) == TYPE_FLOAT: value = roundi(number)
					elif path == "scale" and value_type == TYPE_VECTOR2: value = Vector2.ONE * value
					elif path == "scale" and value_type == TYPE_VECTOR3: value = Vector3.ONE * value
				if not TweensGdInterpolation.compatible(current, value): return {"error": "Key type does not match channel '%s'." % path}
				values.append(value)
				var stop: float = entry.at
				@warning_ignore("return_value_discarded")
				stops.append(stop)
				var arriving: int = entry.interpolation
				if arriving == -1 and entry.at != 0.0: arriving = _interpolation
				@warning_ignore("return_value_discarded")
				modes.append(-1 if arriving == -1 else _native_mode(arriving))
				@warning_ignore("return_value_discarded")
				eases.append(arriving if arriving != -1 and _native_mode(arriving) == 3 else 0)
			var curve_mode := _native_mode(_interpolation)
			curve = TweensGdKeyframeCurve.create(values, stops, curve_mode if curve_mode != 3 else 1,
				modes, eases, options.color_space, options.alpha_mode, options.color_encoding)
			if not curve.error.is_empty(): return {"error": "Channel '%s': %s" % [path, curve.error]}
			_prepared[cache_key] = curve
		var definition := options.copy()
		definition.property = NodePath(path)
		definition.value_type = value_type
		definition.keyframe_curve = curve
		definitions.append(definition)
	return {"definitions": definitions}

## Starts independent playback through the automatic runtime. Nodes own playback; other Objects need an owner.
func play(target: Object, owner: Variant = null, playback_options: TweensGdPlaybackOptions = null) -> TweensGdGroup:
	var compiled := _compile(target)
	if compiled.has("error"):
		push_error(compiled.error)
		var message: String = compiled.error
		return TweensGdGroup.rejected(message)
	return TweensGdRunner.play_all(target, compiled.definitions, owner, playback_options)

## Starts independent playback through a manually driven scheduler. Validation failures return a FAILED group.
func play_on(scheduler: TweensGdScheduler, target: Object, owner: Variant = null,
		playback_options: TweensGdPlaybackOptions = null) -> TweensGdGroup:
	if scheduler == null: return TweensGdGroup.rejected("An animation needs a scheduler.")
	var compiled := _compile(target)
	if compiled.has("error"):
		var message: String = compiled.error
		return TweensGdGroup.rejected(message)
	return scheduler.add_all(target, compiled.definitions, owner, playback_options)
