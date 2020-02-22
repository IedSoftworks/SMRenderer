#version 330

in vec3 vPosition;
in vec4 gl_FragCoord;

out vec4 color;

void Bloom();
void Texturing();
void Lighting();

void main() {
	Texturing();
	Lighting();


	Bloom();
}