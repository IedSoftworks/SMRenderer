#version 330
in vec3 aPosition;
in vec3 aNormal;
in vec2 aTexture;

out vec2 vTexture;
out vec3 vNormal;
out vec3 vPosition;

uniform mat4 uMVP;
uniform mat4 uM;
uniform mat4 uN;

//functions

void main() {
	vTexture = aTexture;
	vPosition = (uM * vec4(aPosition, 1.0)).xyz;
	vNormal = normalize((uN * vec4(aNormal, 0.0)).xyz);
	gl_Position = uMVP * vec4(vPosition, 1.0);
}