#version 330

in vec2 vTexture;

uniform sampler2D uTexture;
uniform sampler2D uForm;
uniform vec4 uColor;

out vec4 color;

vec4 texColor;

void Texturing() {
	texColor = texture(uTexture, vTexture);
	color = texColor * uColor;
}
void TexWithForm() {
	texColor = texture(uTexture, vTexture);
	texColor.w *= texture(uForm, vTexture).w;

	color = texColor * uColor;
}