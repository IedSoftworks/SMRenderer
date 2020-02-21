#version 330

uniform vec4 uColor;

uniform int uBloomUsage;
uniform vec4 uBloomColor;

out vec4 color;
out vec4 bloom;

vec4 texColor;

void Bloom() {
	if (uBloomUsage == 0) {
		bloom = vec4(0,0,0,0);
	} else if (uBloomUsage == 1) {
		bloom = vec4(uBloomColor.xyz, color.w * uBloomColor.w);
	} else if (uBloomUsage == 2) { 
		if (texColor.w > 0) bloom = uColor;
	} else if (uBloomUsage == 3) {
		bloom = texColor;
	} else if (uBloomUsage == 4) {
		bloom = texColor * uColor;
	} else if (uBloomUsage == 5) {
		bloom = color;
	}
}