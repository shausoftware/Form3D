using UnityEngine;
using System.Collections;

public class ColourConfiguration  {

	//colour and animation

	public enum BackgroundType {None, Dawn, Eerie, Night, Julia, Mandelbrot};

	private Color cycleColour = new Color (1, 0, 0, 1.0f);
	private int currentColour = 0; 
	private bool cycle = false;

	private int pulseCount = 0;
	private int pulseLength = 10;
	private bool pulse = false;
	private float pulseSpeed = 0.0f;
	private int pulseSpeedCount = 0;

	private bool fadeColour = false;
	private float baseRed = 1.0f;
	private float baseGreen = 1.0f;
	private float baseBlue = 1.0f;

	private BackgroundType backgroundType = BackgroundType.Dawn; 

	public ColourConfiguration() {
	}

	public ColourConfiguration(ColourConfiguration config) {
		this.cycleColour = config.getCycleColour ();
		//this.currentColour
		this.cycle = config.getCycle ();
		this.pulseCount = config.getPulseCount ();
		//this.pulseLength
		this.pulse = config.getPulse ();
		this.pulseSpeed = config.getPulseSpeed ();
		//this.pulseSpeedCount
		this.fadeColour = config.getFadeColour ();
		this.baseRed = config.getBaseRed ();
		this.baseGreen = config.getBaseGreen ();
		this.baseBlue = config.getBaseBlue ();
		this.backgroundType = config.getBackgroundType ();
	}

	public void setFadeColour(bool fadeColour) {
		this.fadeColour = fadeColour;
	}
	public bool getFadeColour() {
		return fadeColour;
	}

	public void setBaseRed(float baseRed) {
		this.baseRed = baseRed;
	}
	public float getBaseRed() {
		return baseRed;
	}

	public void setBaseGreen(float baseGreen) {
		this.baseGreen = baseGreen;
	}
	public float getBaseGreen() {
		return baseGreen;
	}

	public void setBaseBlue(float baseBlue) {
		this.baseBlue = baseBlue;
	}
	public float getBaseBlue() {
		return baseBlue;
	}

	public void setBackgroundType(BackgroundType backgroundType) {
		this.backgroundType = backgroundType;
	}
	public BackgroundType getBackgroundType() {
		return backgroundType;
	}

	public Color getCycleColour() {	
		return cycleColour;
	}

	public bool getCycle() {
		return cycle;
	}
	public void setCycle(bool cycle) {
		this.cycle = cycle;
	}

	public bool getPulse() {
		return pulse;
	}
	public void setPulse(bool pulse) {
		this.pulse = pulse;
	}

	public void setPulseSpeed(float pulseSpeed) {
		this.pulseSpeed = pulseSpeed;
	}
	public float getPulseSpeed() {
		return pulseSpeed;
	}

	public int getPulseCount() {
		return pulseCount;
	}
	
	public void incrementPulseCount() {

		//outer loop controls speed of pulse
		if (pulseSpeedCount < pulseSpeed) {
			pulseSpeedCount++;
		} else {
			pulseSpeedCount = 0;
			//innner loop controls position of pulse
			if (pulseCount < pulseLength) {
				pulseCount++;
			} else {
				pulseCount = 0;
			}
		}
	}

	public Color getPulseColourForStack(Color modelColor, int iteration) {

		Color segmentColour = modelColor;

		int currentOffset = (iteration - pulseCount) % pulseLength;

		if (currentOffset == 0) {
			//pale
			segmentColour = new Color (1 - cycleColour.r * 0.5f, 1 - cycleColour.g * 0.5f, 1 - cycleColour.b * 0.5f, 1);
		}
		if (currentOffset == 1) {
			//pale
			segmentColour = new Color (cycleColour.r, cycleColour.g, cycleColour.b, 1);
		}
		if (currentOffset == 2) {
			//pale
			segmentColour = new Color (1 - cycleColour.r * 0.5f, 1 - cycleColour.g * 0.5f, 1 - cycleColour.b * 0.5f, 1);
		}

		return segmentColour;
	}

	public void incrementDisplayColour() {
		
		if (cycle) {

			if (currentColour == 0) {
				//red
				if (cycleColour.r < 1) {
					//increment red 
					cycleColour.r += 0.01f;
					if (cycleColour.b > 0) {
						//decrement blue
						cycleColour.b -= 0.01f;
					}
				} else {
					//done with red next green
					cycleColour.b = 0;
					currentColour = 1;
				}
				
			} else if (currentColour == 1) {
				//green
				if (cycleColour.g < 1) {
					//increment green 
					cycleColour.g += 0.01f;
					if (cycleColour.r > 0) {
						//decrement red
						cycleColour.r -= 0.01f;
					}
				} else {
					//done with green next blue
					cycleColour.r = 0;
					currentColour = 2;
				}
				
			} else if (currentColour == 2) {
				//blue
				if (cycleColour.b < 1) {
					//increment blue
					cycleColour.b += 0.01f;
					if (cycleColour.g > 0) {
						//decrement green
						cycleColour.g -= 0.01f;
					}
				} else {
					//done with blue next red
					cycleColour.g = 0;
					currentColour = 0;
				}
			}
		}
	}
}
