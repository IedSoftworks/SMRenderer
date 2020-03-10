#version 330

in vec2 vTexture;

uniform sampler2D uTexture;
uniform vec2 uTexSize;
uniform vec2 uTexPosition;

//uniform int uActiveForm;
uniform sampler2D uForm;
uniform vec4 uColor;

out vec4 color;

vec4 texColor;
vec2 texPos;

vec4 textureProcess(bool form) {
	vec2 orgTexSize = textureSize(uTexture,0);
	texPos = vTexture;

	texPos *= ((uTexSize == vec2(0,0)) ? 1 : uTexSize / orgTexSize) + (uTexPosition / orgTexSize);

	vec4 tex = texture(uTexture, texPos);

	return tex;
}
void Texturing() {
	texColor = textureProcess(false);
	color = texColor * uColor;
}