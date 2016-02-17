using UnityEngine;
using System.Collections;

/**
 * Used for generating Julia sets as background image
 * NOT IN USE
 **/ 

public class JuliaSetJob : ThreadedJob {

	private const int maxIterations = 300; //after how much iterations the function should stop
	
	public float zoom;
	public int width;
	public int height;

	public ColourUtility.HSBColour[,] hsbPixelMap;
	public ColourUtility.RGBColour[,] rgbPixelMap;
	
	// Do threaded task. DON'T use the Unity API here
	protected override void ThreadFunction() {

		double moveX = 0;
		double moveY = 0;

		hsbPixelMap = new ColourUtility.HSBColour[width, height];
		rgbPixelMap = new ColourUtility.RGBColour[width, height];
		
		int x = 0;
		int y = 0;
		
		//each iteration, it calculates: new = old*old + c, where c is a constant and old starts at current pixel
		double cRe, cIm; //real and imaginary part of the constant c, determinate shape of the Julia Set
		double newRe, newIm, oldRe, oldIm; //real and imaginary parts of new and old
		
		//pick some values for the constant c, this determines the shape of the Julia Set
		cRe = -0.7;
		cIm = 0.27015;
		
		//loop through every pixel
		for (x = 0; x < width; x++) {
			for (y = 0; y < height; y++) {
				
				//calculate the initial real and imaginary part of z, based on the pixel location and zoom and position values
				newRe = 1.5 * (x - width / 2) / (0.5 * zoom * width) + moveX;
				newIm = (y - height / 2) / (0.5 * zoom * height) + moveY;
				//i will represent the number of iterations
				int i;
				//start the iteration process
				for(i = 0; i < maxIterations; i++)
				{
					//remember value of previous iteration
					oldRe = newRe;
					oldIm = newIm;
					//the actual iteration, the real and imaginary part are calculated
					newRe = oldRe * oldRe - oldIm * oldIm + cRe;
					newIm = 2 * oldRe * oldIm + cIm;
					//if the point is outside the circle with radius 2: stop
					if((newRe * newRe + newIm * newIm) > 4) break;
				}
				
				//colour black if max iterations exceeded
				ColourUtility.HSBColour hsbColour = new ColourUtility.HSBColour(0, 0, 0);
				if (i < maxIterations) {
					float hue = ((float) (i % 256)) / 256;
					hsbColour.hue = hue;
					hsbColour.saturation = 1;
					hsbColour.brightness = hue;
				}
				ColourUtility.RGBColour rgbColour = ColourUtility.rgbFromHSB(hsbColour.hue, 
				                                                             hsbColour.saturation, 
				                                                             hsbColour.brightness);
				
				//set pixel map for colour cycling
				hsbPixelMap[x, y] = hsbColour;
				rgbPixelMap[x, y] = rgbColour;
			}
		}
	}

	// This is executed by the Unity main thread when the job is finished
	protected override void OnFinished() {

	}
}
