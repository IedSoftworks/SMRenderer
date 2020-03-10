#version 330

in vec3 vPosition;
in vec3 vNormal;

out vec4 color;
out vec4 bloom;

uniform vec4 uLightPositions[4];
uniform vec4 uLightColors[4];
uniform vec3 uLightDirections[4];
uniform int uLightCount;

uniform vec4 uAmbientLight;

void Lighting() {
	for(int i = 0; i < uLightCount; i++) {
		vec4 pos = uLightPositions[i];
		vec4 col = uLightColors[i];
		vec3 dir = vec3(uLightDirections[i].x, uLightDirections[i].y, 0 );
		
		vec3 pointToLight = pos.xyz - vPosition;
		float distanceSqut = dot(pointToLight, pointToLight);
		pointToLight = normalize(pointToLight);

		float lightIntensity = max(dot(pointToLight, vNormal), 0.0) * (pos.w * 100) / distanceSqut;
		vec3 finalIll = uAmbientLight.rgb + (lightIntensity * 100) * (col.w * col.rgb);
		
		if (uLightDirections[i].z == 1) {
			float direction = max(dot(dir, -1 * pointToLight), 0);
			finalIll *= direction;
		}

		color = color * vec4(finalIll, 1);
	}

}