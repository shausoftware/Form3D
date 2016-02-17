using UnityEngine;
using System.Collections;

public class ColourPalette : MonoBehaviour {

	private IColourConfigCallback callback;

	Texture2D texture = new Texture2D(180, 20);

	public ColourPalette(IColourConfigCallback callback) {
		this.callback = callback;
	}

	public void displayColourPalette(ColourConfiguration colourConfig) {

		Color guiColour = GUI.color;

		// Make a background box
		GUI.Box(new Rect(Screen.width - 210, 10, 200, 450), "Colour Palette");

		// pulsate colour button
		if (colourConfig.getPulse()) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 40, 180, 20), "Pulsate Colour")) {
			colourConfig.setPulse(!colourConfig.getPulse());
			callback.updateColourConfig(colourConfig);
		}
		GUI.color = guiColour;

		//pulsate speed slider
		GUI.Label (new Rect (Screen.width - 146, 70, 180, 20), "Pulse Speed");

		colourConfig.setPulseSpeed (GUI.HorizontalSlider (new Rect (Screen.width - 200, 100, 180, 20), colourConfig.getPulseSpeed(), 10.0f, 0.0f));

		// colour cycle button
		if (colourConfig.getCycle()) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 130, 180, 20), "Cycle Colour")) {
			colourConfig.setCycle(!colourConfig.getCycle());
			callback.updateColourConfig(colourConfig);
		}
		GUI.color = guiColour;

		// Base colour button
		if (colourConfig.getFadeColour()) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 160, 180, 20), "Fade Colour")) {
			colourConfig.setFadeColour(!colourConfig.getFadeColour());
			callback.updateColourConfig(colourConfig);
		}
		GUI.color = guiColour;

		//rgb colour sliders
		GUI.Label (new Rect (Screen.width - 160, 190, 180, 20), "Red, Green, Blue");
		
		colourConfig.setBaseRed(GUI.HorizontalSlider (new Rect (Screen.width - 200, 220, 180, 20), colourConfig.getBaseRed(), 0.0f, 1.0f));

		colourConfig.setBaseGreen(GUI.HorizontalSlider (new Rect (Screen.width - 200, 250, 180, 20), colourConfig.getBaseGreen(), 0.0f, 1.0f));

		colourConfig.setBaseBlue(GUI.HorizontalSlider (new Rect (Screen.width - 200, 280, 180, 20), colourConfig.getBaseBlue(), 0.0f, 1.0f));

		for (int i = 0; i < 200; i++) {
			for (int j = 0; j < 20; j++) {
				Color currentColor = new Color (colourConfig.getBaseRed(), colourConfig.getBaseGreen(), colourConfig.getBaseBlue(), 1.0f);
				texture.SetPixel (i, j, currentColor);
			}
		}
		texture.Apply();
		GUI.Box(new Rect(Screen.width - 200, 310, 180, 20), texture);

		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Dawn) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 340, 180, 20), "Dawn Skybox")) {
			colourConfig.setBackgroundType(ColourConfiguration.BackgroundType.Dawn);
			callback.setBackground(colourConfig);
		}
		GUI.color = guiColour;

		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Eerie) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 370, 180, 20), "Eerie Skybox")) {
			colourConfig.setBackgroundType(ColourConfiguration.BackgroundType.Eerie);
			callback.setBackground(colourConfig);
		}
		GUI.color = guiColour;

		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Night) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 400, 180, 20), "Night Skybox")) {
			colourConfig.setBackgroundType(ColourConfiguration.BackgroundType.Night);
			callback.setBackground(colourConfig);
		}
		GUI.color = guiColour;

/*
//NOT IN USE
		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Julia) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 430, 180, 20), "Julia")) {
			colourConfig.setBackgroundType(ColourConfiguration.BackgroundType.Julia);
			callback.setBackground(colourConfig);
		}
		GUI.color = guiColour;
//*/
		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.None) {
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(Screen.width - 200, 430, 180, 20), "None")) {
			colourConfig.setBackgroundType(ColourConfiguration.BackgroundType.None);
			callback.setBackground(colourConfig);
		}
		GUI.color = guiColour;
	}
}
