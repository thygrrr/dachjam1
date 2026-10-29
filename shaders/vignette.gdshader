shader_type canvas_item;

// Radii are normalized so 1.0 lies on the screen edges; at outer_radius (and
// beyond) the vignette reaches its full color.
uniform float inner_radius = 0.5;
uniform float outer_radius = 1.0;
uniform float vignette_strength = 1.0;
uniform float dither_strength = 0.03;
// 2 is a circle (full color only on the edges, past them in the corners);
// higher values hug the edges like a rounded rectangle.
uniform float squareness: hint_range(2.0, 16.0) = 4.0;
uniform vec4 vignette_color: source_color;


void fragment() {
	vec2 offset = abs(UV - vec2(0.5)) * 2.0;
	float dist = pow(pow(offset.x, squareness) + pow(offset.y, squareness), 1.0 / squareness);

	float vignette = clamp(smoothstep(inner_radius, outer_radius, dist) * vignette_strength, 0.0, 1.0);
	float dither = fract(sin(dot(UV, vec2(12.9898, 78.233))) * 43758.5453123) * dither_strength;

	COLOR = vec4(vignette_color.rgb, vignette + dither);
}
