#version 330
in vec3 vPosition;
in vec3 vNormal;

uniform vec2 uObjectSize;
vec2 pos;

void Bloom();
void Lighting();
void Texturing();
void Depth();

void main() {
	pos = vPosition.xy * uObjectSize + vec2(uObjectSize.x / 2, uObjectSize.y /2);
	
	Texturing();
	Lighting();
	Depth();
	Bloom();
}

