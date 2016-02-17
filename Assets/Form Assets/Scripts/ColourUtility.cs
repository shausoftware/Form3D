using UnityEngine;
using System.Collections;

public class ColourUtility {

	public struct HSBColour {
		
		public float hue;
		public float saturation;
		public float brightness;
		
		public HSBColour(float hue, float saturation, float brightness) {
			this.hue = hue;
			this.saturation = saturation;
			this.brightness = brightness;
		}
	};
	
	public struct RGBColour {
		
		public float red;
		public float green;
		public float blue;
		
		public RGBColour(float red, float green, float blue) {
			this.red = red;
			this.green = green;
			this.blue = blue;
		}
	};

	public static Color fadeModelColour(Color modelColour, int iterations, int iteration) {
		
		float red = 1 - ((1 - modelColour.r) * ((1.0f / iterations) * iteration));
		float green = 1 - ((1 - modelColour.g) * ((1.0f / iterations) * iteration));
		float blue =  1 - ((1 - modelColour.b) * ((1.0f / iterations) * iteration));
		
		if (red > 1) {
			red = 1;
		}
		if (green > 1) {
			green = 1;
		}
		if (blue > 1) {
			blue = 1;
		}
		
		return new Color (red, green, blue, 1);
	}

	//get rgb equivalent
	public static RGBColour rgbFromHSB(float hue, float saturation, float brightness, float alpha = 1) {
		
		float r = brightness;
		float g = brightness;
		float b = brightness;
		
		if (saturation != 0) {
			
			float max = brightness;
			float dif = brightness * saturation;
			float min = brightness - dif;
			
			float h = hue * 360f;
			
			if (h < 60f) {
				r = max;
				g = h * dif / 60f + min;
				b = min;
			} else if (h < 120f) {
				r = -(h - 120f) * dif / 60f + min;
				g = max;
				b = min;
			} else if (h < 180f) {
				r = min;
				g = max;
				b = (h - 120f) * dif / 60f + min;
			} else if (h < 240f) {
				r = min;
				g = -(h - 240f) * dif / 60f + min;
				b = max;
			} else if (h < 300f) {
				r = (h - 240f) * dif / 60f + min;
				g = min;
				b = max;
			} else if (h <= 360f) {
				r = max;
				g = min;
				b = -(h - 360f) * dif / 60 + min;
			} else {
				r = 0;
				g = 0;
				b = 0;
			}
		}
		
		return new RGBColour(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b));
	}
}
