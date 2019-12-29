#version 330
in vec3 aPosition;
in vec2 aTexture;

out vec2 vTex;

uniform mat4 uMVP;

void main() {
	vTex = aTexture;
	gl_Position = uMVP * vec4(aPosition, 1.0);
}