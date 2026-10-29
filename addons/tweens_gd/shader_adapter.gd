# SPDX-License-Identifier: MIT
# SPDX-FileCopyrightText: 2026 Moritz Voss
extends "adapter.gd"
## Binds a material or instance shader uniform for one playback.
##
## Prefer [method Tweens.shader_parameter] or [method Tweens.instance_shader_parameter].
## Captures metadata and override state at start; non-retaining completion restores
## the initial override or removes it to reveal the shader default.

## Name of the shader uniform. Resolved and validated when playback starts.
var parameter: StringName
## Use an instance uniform on a CanvasItem or GeometryInstance3D instead of a ShaderMaterial uniform.
var instance_uniform := false
var _uniform_type := TYPE_NIL
var _had_override := false
var _read := false
var _changed := false
var _mesh: Mesh
var _slots: Array = []
var _bindings: Array[Dictionary] = []
var _shaders: Array[Shader] = []

func copy() -> TweensGdAdapter:
	var script: GDScript = get_script()
	var result: TweensGdAdapter = script.new()
	result.set(&"parameter", parameter)
	result.set(&"instance_uniform", instance_uniform)
	return result

func prepare(target: Object) -> String:
	if String(parameter).strip_edges().is_empty(): return "A shader parameter name is required."
	if instance_uniform:
		if not target is CanvasItem and not target is GeometryInstance3D:
			return "Instance shader tweens require a CanvasItem or GeometryInstance3D."
		_mesh = _get_mesh(target)
		_slots = _get_slots(target)
	else:
		if not target is ShaderMaterial: return "Shader parameter tweens require a ShaderMaterial."
		_slots = [target]
	for slot: Material in _slots:
		var material := slot
		var visited: Dictionary = {}
		while material != null:
			if not is_instance_valid(material): return "A live material is required."
			if visited.has(material.get_instance_id()): return "Material next passes must not form a cycle."
			visited[material.get_instance_id()] = true
			var shader_material := material as ShaderMaterial
			var shader: Shader = shader_material.shader if shader_material != null else null
			if shader_material != null:
				if not is_instance_valid(shader): return "A live shader is required."
				if not _shaders.has(shader):
					_shaders.append(shader)
					if shader.changed.connect(_shader_changed) != OK: return "The shader cannot be watched for changes."
			_bindings.append({"material": material, "next": material.next_pass, "shader": shader})
			material = material.next_pass if instance_uniform else null
	var metadata: Array = target.get_property_list() if instance_uniform else (target as ShaderMaterial).shader.get_shader_uniform_list()
	var name := "instance_shader_parameters/" + String(parameter) if instance_uniform else String(parameter)
	for entry: Dictionary in metadata:
		if entry.name != name: continue
		_uniform_type = entry.type
		if not _uniform_type in [TYPE_INT, TYPE_FLOAT, TYPE_VECTOR2, TYPE_VECTOR3, TYPE_VECTOR4, TYPE_COLOR]:
			return "The shader uniform type is unsupported."
		var usage: int = entry.usage
		if instance_uniform: _had_override = (usage & PROPERTY_USAGE_STORAGE) != 0
		return ""
	return "No %s shader uniform named '%s' is available; check its declaration and renderer." % ["instance" if instance_uniform else "material", parameter]

func read(target: Object) -> Variant:
	if instance_uniform:
		var canvas := target as CanvasItem
		if canvas != null:
			if _had_override: return canvas.get_instance_shader_parameter(parameter)
			return RenderingServer.canvas_item_get_instance_shader_parameter_default_value(canvas.get_canvas_item(), parameter)
		var geometry := target as GeometryInstance3D
		if _had_override: return geometry.get_instance_shader_parameter(parameter)
		return RenderingServer.instance_geometry_get_shader_parameter_default_value(geometry.get_instance(), parameter)
	var material := target as ShaderMaterial
	var value: Variant = material.get_shader_parameter(parameter)
	# Only the first read captures whether an override existed; later reads see this tween's writes.
	if not _read: _had_override = value != null
	_read = true
	return value if value != null else RenderingServer.shader_get_parameter_default(material.shader.get_rid(), parameter)

func validate_value(value: Variant) -> String:
	if typeof(value) != _uniform_type:
		return "Shader values must match the declared uniform type; defaults require a working renderer."
	if typeof(value) == TYPE_INT and (value < -2147483648 or value > 2147483647):
		return "Shader integers must fit in 32 bits."
	return super.validate_value(value)

func interpolate(from: Variant, to: Variant, weight: float) -> Variant:
	var value: Variant = super.interpolate(from, to, weight)
	if _uniform_type != TYPE_INT: return value
	var integer: int = value
	return clampi(integer, -2147483648, 2147483647)

func write(target: Object, value: Variant) -> String:
	var error := _validate_binding(target)
	if not error.is_empty(): return error
	_set_parameter(target, value)
	return ""

func restore(target: Object, initial: Variant) -> String:
	var error := _validate_binding(target)
	if not error.is_empty(): return error
	_set_parameter(target, initial if _had_override else null)
	return ""

func _set_parameter(target: Object, value: Variant) -> void:
	if not instance_uniform: (target as ShaderMaterial).set_shader_parameter(parameter, value)
	elif target is CanvasItem: (target as CanvasItem).set_instance_shader_parameter(parameter, value)
	else: (target as GeometryInstance3D).set_instance_shader_parameter(parameter, value)

func _shader_changed() -> void:
	_changed = true

func _validate_binding(target: Object) -> String:
	if _changed: return "The shader changed during playback. Start a new tween for the new binding."
	if instance_uniform:
		if not is_instance_valid(_mesh) and _mesh != null: return "The mesh was freed during playback."
		if _get_mesh(target) != _mesh or _get_slots(target) != _slots:
			return "The mesh or material binding changed during shader playback."
	for binding in _bindings:
		var material: Material = binding.material
		if not is_instance_valid(material): return "A shader material was freed during playback."
		if instance_uniform and material.next_pass != binding.next: return "The material pass binding changed during playback."
		var shader_material := material as ShaderMaterial
		if shader_material != null and (not is_instance_valid(binding.shader) or shader_material.shader != binding.shader):
			return "The shader binding changed during playback. Start a new tween for the new shader."
	return ""

func _get_mesh(target: Object) -> Mesh:
	var mesh_instance := target as MeshInstance3D
	if mesh_instance != null: return mesh_instance.mesh
	var multimesh_instance := target as MultiMeshInstance3D
	if multimesh_instance != null and multimesh_instance.multimesh != null: return multimesh_instance.multimesh.mesh
	return null

func _get_slots(target: Object) -> Array:
	if target is CanvasItem:
		var canvas: CanvasItem = target
		while canvas.use_parent_material and canvas.get_parent() is CanvasItem: canvas = canvas.get_parent()
		return [canvas.material]
	var geometry := target as GeometryInstance3D
	var result: Array = [geometry.material_override, geometry.material_overlay]
	var mesh := _get_mesh(target)
	if mesh != null:
		var mesh_instance := target as MeshInstance3D
		for surface in range(mesh.get_surface_count()):
			result.append(mesh_instance.get_active_material(surface) if mesh_instance != null else mesh.surface_get_material(surface))
	return result

func release() -> String:
	for shader in _shaders:
		if is_instance_valid(shader) and shader.changed.is_connected(_shader_changed): shader.changed.disconnect(_shader_changed)
	_shaders.clear()
	_bindings.clear()
	_slots.clear()
	_mesh = null
	return ""
