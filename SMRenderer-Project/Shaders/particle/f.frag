#version 330

in vec2 vTexture;
in vec3 vPosition;
in vec4 gl_FragCoord;

uniform sampler2D uTexture;
uniform vec4 uColor;

out vec4 color;

vec4 texColor;

void Bloom();

void main() {
	texColor = texture(uTexture, vTexture);

	color = texColor * uColor;

	Bloom();
}