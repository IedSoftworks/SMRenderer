#version 330
in vec3 aPosition;
in vec3 aNormal;
in vec2 aTexture;
in int gl_InstanceID;

out vec2 vTexture;
out vec4 vPosition;

uniform mat4 uMVP;
uniform vec2 uMotion;
uniform vec2 uRange;
uniform float uTime;
uniform float uRand;

float rand(vec2 value) {
	return fract(sin(dot(value.xy, vec2(12.9898,78.233))) * 43758.5453);
}

void main() {
	vTexture = aTexture;
	
	float addX = (gl_InstanceID+1) * uRand *(uMotion.x*uTime);
	float addY = (gl_InstanceID+1) * uRand *(uMotion.x*uTime);

	vec2 pos = vec2(aPosition.x + addX, aPosition.y+addY);
	vPosition = vec4(pos, aPosition.z, 1.0);

	gl_Position = uMVP * vPosition;
}