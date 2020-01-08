#version 330
in vec2 vTexture;

uniform sampler2D uTexture;
uniform vec4 uColor;

out vec4 color;
out vec4 bloom;

void main(){
	vec4 tex = texture(uTexture, vTexture);
	color = tex * uColor;

	bloom = tex;
}