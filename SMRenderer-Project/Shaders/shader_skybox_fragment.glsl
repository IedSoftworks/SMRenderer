#version 330
in vec2 vTexture;
in vec4 gl_FragCoord;
in vec4 vPosition;

uniform sampler2D uTexture;
uniform sampler2D uForm;
uniform vec2 uTexSize;
uniform vec4 uColor;

out vec4 color;
out vec4 bloom;

void main(){

	vec4 texColor = texture(uTexture, vTexture);
	texColor.w *= texture(uForm, vTexture).w;

	color = texColor * uColor;
	bloom = vec4(0,0,0,0);
}