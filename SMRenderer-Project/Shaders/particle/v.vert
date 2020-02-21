#version 330

in vec3 aPosition;
in vec2 aTexture;
in int gl_InstanceID;

out vec3 vPosition;
out vec2 vTexture;

uniform mat4 uMVP;
uniform vec2[255] uParticleMovements;
uniform float uParticleTime;
uniform vec2 uSize;

void main() {
	
	vec2 Motion = vec2((gl_InstanceID+1) * 10 * uParticleTime, uParticleMovements[gl_InstanceID].y * uParticleTime) / uSize;

	vTexture = aTexture;
	vPosition = vec3(aPosition.x + Motion.x, aPosition.y + Motion.y, aPosition.z);
	gl_Position = uMVP * vec4(vPosition, 1);
}