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

uniform vec4 uLightPositions[4];
uniform vec4 uLightColors[4];
uniform int uLightCount;
uniform vec4 uAmbientLight;

out vec4 color;
out vec4 bloom;

vec2 pos;
vec4 texColor;

void Border();

void Bloom();

void Lighting() {
	for(int i = 0; i < uLightCount; i++) {
		vec4 pos = uLightPositions[i];
		vec4 col = uLightColors[i];
		
		vec3 pointToLight = pos.xyz - vPosition;
		float distanceSqut = dot(pointToLight, pointToLight);
		pointToLight = normalize(pointToLight);

		float lightIntensity = max(dot(pointToLight, vNormal), 0.0) * (pos.w * 100) / distanceSqut;
		vec3 finalIll = uAmbientLight.rgb * uAmbientLight.w + (lightIntensity * 100) * col.w * col.rgb;
		
		color = color * vec4(finalIll, 1);
	}

}

void main() {
	pos = vPosition.xy * uObjectSize + vec2(uObjectSize.x / 2, uObjectSize.y /2);

	texColor = texture(uTexture, vTexture);
	texColor.w *= texture(uForm, vTexture).w;

	color = texColor * uColor;
	
	//Lighting();
	Border();
	Bloom();
}

