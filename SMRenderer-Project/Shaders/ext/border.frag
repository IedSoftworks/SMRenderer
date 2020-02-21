#version 330

in vec2 vTexture;

uniform vec2 uObjectSize;
uniform sampler2D uTexture;
uniform int uBorderUsage;
uniform vec4 uBorderColor;
uniform int uBorderWidth;
uniform int uBorderLength;

out vec4 color;

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