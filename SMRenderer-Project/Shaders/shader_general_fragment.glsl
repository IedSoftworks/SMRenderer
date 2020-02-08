﻿#version 330
in vec2 vTexture;
in vec4 gl_FragCoord;
in vec3 vPosition;
in vec3 vNormal;

uniform sampler2D uTexture;
uniform sampler2D uForm;
uniform vec2 uObjectSize;
uniform vec4 uColor;

uniform int uBloomUsage;
uniform vec4 uBloomColor;

uniform int uBorderUsage;
uniform vec4 uBorderColor;
uniform int uBorderWidth;
uniform int uBorderLength;

uniform vec3 uLightPosition;
uniform vec4 uLightColor;
uniform float uLightIntensity;
uniform vec4 uAmbientLight;

out vec4 color;
out vec4 bloom;

vec2 pos;
vec4 texColor;

void Border() {
	// Border Stuff
	if (uBorderUsage > 0) {
		if (uBorderUsage == 1 && texColor.w == 0) {// TextureBorder
			vec2 pxSize = 1.0 / vec2(textureSize(uTexture,0));
		
			vec4 check1 = texture(uTexture, vTexture + vec2(1,0) * pxSize);
			vec4 check2 = texture(uTexture, vTexture + vec2(-1,0) * pxSize);
			vec4 check3 = texture(uTexture, vTexture + vec2(0,1) * pxSize);
			vec4 check4 = texture(uTexture, vTexture + vec2(0,-1) * pxSize);

			if(check1.w > 0 && check1 != uBorderColor) color = uBorderColor;
			else if(check2.w > 0 && check2 != uBorderColor) color = uBorderColor;
			else if(check3.w > 0 && check3 != uBorderColor) color = uBorderColor;
			else if(check4.w > 0 && check4 != uBorderColor) color = uBorderColor;
		}
		if (uBorderUsage == 2) {
			if ((pos.x > 0 && pos.x < uBorderWidth) || (pos.x < uObjectSize.x && pos.x > uObjectSize.x - uBorderWidth) || (pos.y > 0 && pos.y < uBorderWidth) || (pos.y < uObjectSize.y && pos.y > uObjectSize.y - uBorderWidth) ) {
				color = uBorderColor;
			}
		}
		if (uBorderUsage == 3) { //QuadEdgeBorder
			float len = uBorderLength;
			if (uBorderLength < 0) {
				float aspect = (uObjectSize.x + uObjectSize.y) / 2;
				len = (aspect * .1);
			}
			bool check1 = (pos.y > 0 && pos.y < len && pos.x > 0 && pos.x < uBorderWidth) || (pos.x > 0 && pos.x < len && pos.y > 0 && pos.y < uBorderWidth); // upper left
			bool check2 = (pos.y < uObjectSize.y && pos.y > uObjectSize.y - len && pos.x > 0 && pos.x < uBorderWidth) || (pos.x > 0 && pos.x < len && pos.y < uObjectSize.y && pos.y > uObjectSize.y - uBorderWidth);
			bool check3 = (pos.x < uObjectSize.x && pos.x > uObjectSize.x - len && pos.y > 0 && pos.y < uBorderWidth) || (pos.y > 0 && pos.y < len && pos.x < uObjectSize.x && pos.x > uObjectSize.x - uBorderWidth);
			bool check4 = (pos.x < uObjectSize.x && pos.x > uObjectSize.x - len && pos.y < uObjectSize.y && pos.y > uObjectSize.y - uBorderWidth) || (pos.y < uObjectSize.y && pos.y > uObjectSize.y-len && pos.x < uObjectSize.x && pos.x > uObjectSize.x - uBorderWidth);
			if (check1 || check2 || check3 || check4 ) {
				color = uBorderColor;
			}
		}
	}
}

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

void Lighting() {
	vec3 pointToLight = uLightPosition - vPosition;
	float distanceSqut = dot(pointToLight, pointToLight);
	pointToLight = normalize(pointToLight);

	float lightIntensity = max(dot(pointToLight, vNormal), 0.0) * (uLightIntensity * 100) / distanceSqut;
	vec3 finalIll = uAmbientLight.rgb * uAmbientLight.w + (lightIntensity * 100) * uLightColor.w * uLightColor.rgb;
	color = color * vec4(finalIll, 1);
}

void main() {
	pos = vPosition.xy * uObjectSize + vec2(uObjectSize.x / 2, uObjectSize.y /2);

	texColor = texture(uTexture, vTexture);
	texColor.w *= texture(uForm, vTexture).w;

	color = texColor * uColor;
	
	Lighting();
	Border();
	Bloom();
}

