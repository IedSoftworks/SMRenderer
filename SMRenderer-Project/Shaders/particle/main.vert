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

uniform vec3[2048] uParticleMovements;
uniform float uParticleTime;
uniform vec2 uSize;

mat4 zRotation(float angle) {
	float s = sin(angle);
	float c = cos(angle);
	return mat4(c, -s, 0, 0,
				s, c, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1);
}

void main() {
	vec3 Mot = uParticleMovements[gl_InstanceID];

	vec4 Position = vec4(aPosition.x, aPosition.y, aPosition.z, 1);
	//Position = zRotation(Mot.z) * Position;
	
	vec2 Motion = vec2(Mot.x * uParticleTime, Mot.y * uParticleTime) / uSize;
	Position = vec4(Position.x + Motion.x, Position.y + Motion.y, Position.z, Position.w);

	vTexture = aTexture;
	vPosition = (uM * Position).xyz;
	vNormal = normalize((uN * vec4(aNormal, 1)).xyz);
	gl_Position = uMVP * Position;
}