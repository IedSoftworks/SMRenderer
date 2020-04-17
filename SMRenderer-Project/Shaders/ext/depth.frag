#version 330

in vec3 vPosition;

out vec4 color;

uniform vec3 uDepthSettings; // x = minimum; y = maximum; z = factor
uniform float zPosition;

void Depth() {
	float zPos = vPosition.z;
	if (zPos > uDepthSettings.y) zPos = uDepthSettings.y;
	if (zPos < uDepthSettings.x) zPos = uDepthSettings.x;

	float factor = 1;
	factor += uDepthSettings.z * zPos;

	color *= vec4(factor);
}