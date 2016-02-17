using UnityEngine;
using System.Collections;
using System.Threading;

/**
 * Used for generating Julia sets as background image
 * NOT IN USE
 **/ 

public class BackgroundImageControllerScript : MonoBehaviour {
	
	private static bool setGenerated = false;
	private static GUITexture backgroundImage;
	private static JuliaSetJob juliaSetJob;
//	private static ColourUtility.HSBColour[,] hsbPixelMap; //needed for colour cycles
	private static bool cycleColours = false;

	// Use this for initialization
	void Start () {
		//get a reference to background image from scene
		backgroundImage = GameObject.Find("Background Image").GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {

		if (juliaSetJob != null) {
			if (juliaSetJob.Update()) {
				//Julia set ready
				//hsb map needed for cycling
				//hsbPixelMap = juliaSetJob.hsbPixelMap;
				//render rgb
				renderTexture(juliaSetJob.rgbPixelMap);
				//reset job state
				juliaSetJob = null;
				setGenerated = true;
			}
		}
	}

	/* Interface methods */

	public static void startJuliaSet() {

		setGenerated = false;

		float zoom = Random.Range (1, 50);

		//start julia set generation job
		juliaSetJob = new JuliaSetJob ();
		juliaSetJob.zoom = zoom;
		juliaSetJob.width = Screen.width;
		juliaSetJob.height = Screen.height;

		juliaSetJob.Start ();
	}

	public static bool isSetGenerated() {
		return setGenerated;
	}

	/* Internal methods */

	private void renderTexture(ColourUtility.RGBColour[,] rgbPixelMap) {
		
		int width = rgbPixelMap.GetLength (0);
		int height = rgbPixelMap.GetLength (1);
		
		Texture2D texture = new Texture2D (width, height);
		
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				ColourUtility.RGBColour pixelColour = rgbPixelMap[x, y]; 
				texture.SetPixel(x, y, new Color(pixelColour.red, pixelColour.green, pixelColour.blue, 1));
			}
		}
		
		Rect backgroundSize = backgroundImage.pixelInset;
		backgroundSize.width = width;
		backgroundSize.height = height;
		backgroundImage.pixelInset = backgroundSize;
		//apply the texture to the background
		texture.Apply();
		backgroundImage.texture = texture;
	}
}
