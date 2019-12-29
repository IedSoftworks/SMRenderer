#version 330
in vec2 vTex;

uniform sampler2D uTextureScene;
uniform sampler2D uTextureBloom;
uniform int uMerge;
uniform int uHorizontal;
uniform vec2 uResolution;

out vec4 color;

float weight[9] = float[] (.4, .2,.1,.075,.05,.01,.0075,.005,.0001);
vec2 tex_offset = vec2(1,1);

float bloomSizeFactor = 0.00075;
float multiplier = 1;

void main() {
	vec3 result = texture(uTextureBloom, vTex).rgb * weight[0];

	if (uHorizontal == 1) {
		for(int i = 1; i < 9; i++) {
			result += texture(uTextureBloom, vTex + vec2(tex_offset.x * (float(i) * (bloomSizeFactor * float(i))), 0.0)).rgb * (weight[i] * multiplier);
			result += texture(uTextureBloom, vTex - vec2(tex_offset.x * (float(i) * (bloomSizeFactor * float(i))), 0.0)).rgb * (weight[i] * multiplier);
		}
		color.x = result.x;
		color.y = result.y;
		color.z = result.z;
		color.w = 1;
	}
	else {
		for(int i = 1; i < 9; i++) {
			result += texture(uTextureBloom, vTex + vec2(0.0, tex_offset.y * (float(i) * (bloomSizeFactor * float(i))))).rgb * (weight[i] * multiplier);
			result += texture(uTextureBloom, vTex - vec2(0.0, tex_offset.y * (float(i) * (bloomSizeFactor * float(i))))).rgb * (weight[i] * multiplier);
		}
		if(uMerge > 0) {
			vec3 oriColor = texture(uTextureScene, vTex).rgb;
			result += oriColor;
		}
		color.x = result.x;
		color.y = result.y;
		color.z = result.z;
		color.w = 1;
	}
}