using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trunk {

	private List<Branch> branches = new List<Branch>();

	FormBounds formBounds;

	private IFormConfiguration localFormConfig;
	private ColourConfiguration localColourConfig;

	//create a new model
	public Trunk(IFormConfiguration formConfig, ColourConfiguration colourConfig) {

		localFormConfig = formConfig;
		localColourConfig = colourConfig;

		FormProcessor formProcessor = formConfig.getFormProcessor ();
		formBounds = new FormBounds (formConfig.getStartPosition());
		formProcessor.createTrunk (branches, formBounds, formConfig, colourConfig);
	}

	//mutate model
	public void mutate(IFormConfiguration formConfig, ColourConfiguration colourConfig) {

		//if we are receiving null configs then this is rendering on scene and not current brush
		//so use local configs
		if (formConfig == null) {
			formConfig = localFormConfig;
		}
		if (colourConfig == null) {
			colourConfig = localColourConfig;
		}

		//increment colour once for each trunk
		colourConfig.incrementDisplayColour();
		//increment pulse once for each trunk
		colourConfig.incrementPulseCount();

		FormProcessor formProcessor = formConfig.getFormProcessor ();
		formBounds = new FormBounds (formConfig.getStartPosition());
		formProcessor.mutateTrunk (branches, formBounds, formConfig, colourConfig);
	}

	//remove stacks from scene
	public void removeFromScene() {

		foreach (Branch branch in branches) {
			branch.removeFromScene();
		}

		branches = new List<Branch> ();
	}

	//get size
	public float getLargestBoundDistance() {
		return formBounds.getLargestBoundDistance ();
	}
}
