#version 330

in vec3 vPosition;
in vec3 vNormal;

out vec4 color;

uniform vec4 uLightPositions[4];
uniform vec4 uLightColors[4];
uniform int uLightCount;
uniform vec4 uAmbientLight;

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