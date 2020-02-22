#version 150

in vec3 aPosition;
in vec2 aTexture;
in vec3 aNormal;

out vec3 vPosition;
out vec3 vNormal;
out vec2 vTexture;

uniform mat4 uMVP;
uniform mat4 uM;
uniform mat4 uN;

uniform vec2[255] uParticleMovements;
uniform float uParticleTime;
uniform vec2 uSize;

void main() {
	
	vec2 Motion = vec2(uParticleMovements[gl_InstanceID].x * uParticleTime, uParticleMovements[gl_InstanceID].y * uParticleTime) / uSize;
	vec4 Position = vec4(aPosition.x + Motion.x, aPosition.y + Motion.y, aPosition.z, 1);

	vTexture = aTexture;
	vPosition = (uM * Position).xyz;
	vNormal = normalize((uN * vec4(aNormal, 1)).xyz);
	gl_Position = uMVP * Position;
}