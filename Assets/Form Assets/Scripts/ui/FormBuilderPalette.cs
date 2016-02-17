using UnityEngine;
using System.Collections;

public class FormBuilderPalette : MonoBehaviour {

	private string[] configurationGridStrings = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
	private int selectedConfiguration = 0;

	//private string[] shapeGridStrings = {"Sphere *", "Box *", "Complex 1 *", "Complex 2 *", "Simple Torus *"};
	private string[] shapeGridStrings = {"Sphere *", "Box *", "Complex 1 *", "Complex 2 *"};
	private string[] formProcessorStrings = {"Starfish", "Lexodrome", "Rings"};

	private int selectedShape = 0;
	private int selectedFormProcessor = 0;

	private IBuilderConfigCallback callback;

	//start position
	private float startPositionX = 0;
	private float startPositionY = 0;
	private float startPositionZ = 0;

	//start rotation
	private float startRotationX = 0;
	private float startRotationY = 0;
	private float startRotationZ = 0;

	//trunk position delta
	private float trunkPositionDeltaX = 0;
	private float trunkPositionDeltaY = 0;
	private float trunkPositionDeltaZ = 0;

	//trunk rotation delta
	private float trunkRotationDeltaX = 0;
	private float trunkRotationDeltaY = 0;
	private float trunkRotationDeltaZ = 0;
	
	//branch position delta
	private float branchPositionDeltaX = 0;
	private float branchPositionDeltaY = 0;
	private float branchPositionDeltaZ = 0;
	
	//branch rotation delta
	private float branchRotationDeltaX = 0;
	private float branchRotationDeltaY = 0;
	private float branchRotationDeltaZ = 0;

	private float trunkIterations = 0;
	private float branchIterations = 0;

	//stack start rotation
	private float stackStartRotationX = 0;
	private float stackStartRotationY = 0;
	private float stackStartRotationZ = 0;

	//stack rotation delta
	private float stackRotationDeltaX = 0;
	private float stackRotationDeltaY = 0;
	private float stackRotationDeltaZ = 0;

	private float mutationStrength = 1;
	private float scaling = 1;

	public FormBuilderPalette(IBuilderConfigCallback callback) {
		this.callback = callback;
	}

	public void displayFormBuilderPalette(IFormConfiguration formConfig) {

		// Make a background box
		GUI.Box(new Rect(10, 10, 700, 590), "Builder Palette");

		bool displayStarfishControls = false;
		bool displayLoxodromeControls = false;
		bool displayRingControls = false;
		if (formConfig.getFormProcessor ().GetType () == typeof(StarfishForm)) {
			displayStarfishControls = true;
		} else if (formConfig.getFormProcessor ().GetType () == typeof(LoxodromeForm)) {
			displayLoxodromeControls = true;
		} else if (formConfig.getFormProcessor().GetType() == typeof(RingForm)) {
			displayRingControls = true;
		}

		/* TRUNK */

		//start position 
		Vector3 startPosition = formConfig.getStartPosition ();
		startPositionX = startPosition.x;
		startPositionY = startPosition.y;
		startPositionZ = startPosition.z;

		GUI.Label (new Rect (20, 40, 150, 20), "Trunk Start Position X");
		startPositionX = GUI.HorizontalSlider (new Rect (180, 45, 180, 20), startPositionX, -5, 5);

		GUI.Label (new Rect (20, 70, 150, 20), "Trunk Start Position Y");
		startPositionY = GUI.HorizontalSlider (new Rect (180, 75, 180, 20), startPositionY, -5, 5);

		GUI.Label (new Rect (20, 100, 150, 20), "Trunk Start Position Z");
		startPositionZ = GUI.HorizontalSlider (new Rect (180, 105, 180, 20), startPositionZ, -5, 5);

		startPosition.x = startPositionX;
		startPosition.y = startPositionY;
		startPosition.z = startPositionZ;
		formConfig.setStartPosition (startPosition);

		// start rotation
		Vector3 startRotation = formConfig.getStartRotation ();
		startRotationX = startRotation.x;
		startRotationY = startRotation.y;
		startRotationZ = startRotation.z;

		if (displayLoxodromeControls) {
			GUI.Label (new Rect (370, 40, 150, 20), "Rhumb Angle");
			//startRotationX = GUI.HorizontalSlider (new Rect (520, 45, 180, 20), startRotationX, 0.1f, 2f);
			startRotationX = GUI.HorizontalSlider (new Rect (520, 45, 180, 20), startRotationX, 0, 2f);
		}		
		if (displayStarfishControls) {
			GUI.Label (new Rect (370, 40, 150, 20), "Trunk Start Rotation X");
			startRotationX = GUI.HorizontalSlider (new Rect (520, 45, 180, 20), startRotationX, 0, 360);
		}			
		if (displayStarfishControls || displayRingControls) {
			GUI.Label (new Rect (370, 70, 150, 20), "Trunk Start Rotation Y");
			startRotationY = GUI.HorizontalSlider (new Rect (520, 75, 180, 20), startRotationY, 0, 360);
			
			GUI.Label (new Rect (370, 100, 150, 20), "Trunk Start Rotation Z");
			startRotationZ = GUI.HorizontalSlider (new Rect (520, 105, 180, 20), startRotationZ, 0, 360);
		}

		startRotation.x = startRotationX;
		startRotation.y = startRotationY;
		startRotation.z = startRotationZ;
		formConfig.setStartRotation (startRotation);

		//trunk position delta
		Vector3 trunkPositionDelta = formConfig.getTrunkPositionDelta ();
		trunkPositionDeltaX = trunkPositionDelta.x;
		trunkPositionDeltaY = trunkPositionDelta.y;
		trunkPositionDeltaZ = trunkPositionDelta.z;

		if (displayStarfishControls || displayRingControls) {
			GUI.Label (new Rect (20, 130, 150, 20), "Trunk Position Delta X");
			trunkPositionDeltaX = GUI.HorizontalSlider (new Rect (180, 135, 180, 20), trunkPositionDeltaX, -1, 1);

			GUI.Label (new Rect (20, 160, 150, 20), "Trunk Position Delta Y");
			trunkPositionDeltaY = GUI.HorizontalSlider (new Rect (180, 165, 180, 20), trunkPositionDeltaY, -1, 1);

			GUI.Label (new Rect (20, 190, 150, 20), "Trunk Position Delta Z");
			trunkPositionDeltaZ = GUI.HorizontalSlider (new Rect (180, 195, 180, 20), trunkPositionDeltaZ, -1, 1);
		}

		trunkPositionDelta.x = trunkPositionDeltaX;
		trunkPositionDelta.y = trunkPositionDeltaY;
		trunkPositionDelta.z = trunkPositionDeltaZ;
		formConfig.setTrunkPositionDelta (trunkPositionDelta);

		//trunk rotation delta
		Vector3 trunkRotatiomnDelta = formConfig.getTrunkRotationDelta ();
		trunkRotationDeltaX = trunkRotatiomnDelta.x;
		trunkRotationDeltaY = trunkRotatiomnDelta.y;
		trunkRotationDeltaZ = trunkRotatiomnDelta.z;

		if (displayStarfishControls) {
			GUI.Label (new Rect (20, 220, 150, 20), "Trunk Rotation Delta X");
			trunkRotationDeltaX = GUI.HorizontalSlider (new Rect (180, 225, 180, 20), trunkRotationDeltaX, -60, 60);
		}			
		if (displayStarfishControls || displayRingControls) {
			GUI.Label (new Rect (20, 250, 150, 20), "Trunk Rotation Delta Y");
			trunkRotationDeltaY = GUI.HorizontalSlider (new Rect (180, 255, 180, 20), trunkRotationDeltaY, -60, 60);
			
			GUI.Label (new Rect (20, 280, 150, 20), "Trunk Rotation Delta Z");
			trunkRotationDeltaZ = GUI.HorizontalSlider (new Rect (180, 285, 180, 20), trunkRotationDeltaZ, -60, 60);
		}

		trunkRotatiomnDelta.x = trunkRotationDeltaX;
		trunkRotatiomnDelta.y = trunkRotationDeltaY;
		trunkRotatiomnDelta.z = trunkRotationDeltaZ;
		formConfig.setTrunkRotationDelta (trunkRotatiomnDelta);

		//trunk iterations
		trunkIterations = formConfig.getTrunkIterations ();
		
		GUI.Label (new Rect (20, 310, 150, 20), "Trunk Iterations *");
		trunkIterations = GUI.HorizontalSlider (new Rect (180, 315, 180, 20), trunkIterations, 1, 24);
		
		formConfig.setTrunkIterations ((int)trunkIterations);

		/* BRANCH */

		//branch position delta
		Vector3 branchPositionDelta = formConfig.getBranchPositionDelta ();
		branchPositionDeltaX = branchPositionDelta.x;
		branchPositionDeltaY = branchPositionDelta.y;
		branchPositionDeltaZ = branchPositionDelta.z;

		if (displayRingControls || displayLoxodromeControls) {
			GUI.Label (new Rect (370, 130, 150, 20), "Radius");
			branchPositionDeltaX = GUI.HorizontalSlider (new Rect (520, 135, 180, 20), branchPositionDeltaX, 0.2f, 1);
		}
		if (displayStarfishControls) {
			GUI.Label (new Rect (370, 130, 150, 20), "Branch Position Delta X");
			branchPositionDeltaX = GUI.HorizontalSlider (new Rect (520, 135, 180, 20), branchPositionDeltaX, -1, 1);
			
			GUI.Label (new Rect (370, 160, 150, 20), "Branch Position Delta Y");
			branchPositionDeltaY = GUI.HorizontalSlider (new Rect (520, 165, 180, 20), branchPositionDeltaY, -1, 1);
			
			GUI.Label (new Rect (370, 190, 150, 20), "Branch Position Delta Z");
			branchPositionDeltaZ = GUI.HorizontalSlider (new Rect (520, 195, 180, 20), branchPositionDeltaZ, -1, 1);
		}

		branchPositionDelta.x = branchPositionDeltaX;
		branchPositionDelta.y = branchPositionDeltaY;
		branchPositionDelta.z = branchPositionDeltaZ;
		formConfig.setBranchPositionDelta (branchPositionDelta);

		//branch rotation delta
		Vector3 branchRotationDelta = formConfig.getBranchTwistDelta ();
		branchRotationDeltaX = branchRotationDelta.x;
		branchRotationDeltaY = branchRotationDelta.y;
		branchRotationDeltaZ = branchRotationDelta.z;

		if (displayStarfishControls) {
			GUI.Label (new Rect (370, 220, 150, 20), "Branch Rotation Delta X");
			branchRotationDeltaX = GUI.HorizontalSlider (new Rect (520, 225, 180, 20), branchRotationDeltaX, -30, 30);
			
			GUI.Label (new Rect (370, 250, 150, 20), "Branch Rotation Delta Y");
			branchRotationDeltaY = GUI.HorizontalSlider (new Rect (520, 255, 180, 20), branchRotationDeltaY, -30, 30);
			
			GUI.Label (new Rect (370, 280, 150, 20), "Branch Rotation Delta Z");
			branchRotationDeltaZ = GUI.HorizontalSlider (new Rect (520, 285, 180, 20), branchRotationDeltaZ, -30, 30);
		}

		branchRotationDelta.x = branchRotationDeltaX;
		branchRotationDelta.y = branchRotationDeltaY;
		branchRotationDelta.z = branchRotationDeltaZ;
		formConfig.setBranchTwistDelta (branchRotationDelta);

		//branch iterations
		branchIterations = formConfig.getStackIterations ();

		GUI.Label (new Rect (370, 310, 150, 20), "Branch Iterations *");
		branchIterations = GUI.HorizontalSlider (new Rect (520, 315, 180, 20), branchIterations, 1, 60);

		formConfig.setStackIterations ((int)branchIterations);

		/* STACK */

		//stack start rotation
		Vector3 stackStartRotation = formConfig.getStackStartTwist ();
		stackStartRotationX = stackStartRotation.x;
		stackStartRotationY = stackStartRotation.y;
		stackStartRotationZ = stackStartRotation.z;

		GUI.Label (new Rect (20, 340, 150, 20), "Stack Start Rotation X");
		stackStartRotationX = GUI.HorizontalSlider (new Rect (180, 345, 180, 20), stackStartRotationX, 0, 360);

		GUI.Label (new Rect (20, 370, 150, 20), "Stack Start Rotation Y");
		stackStartRotationY = GUI.HorizontalSlider (new Rect (180, 375, 180, 20), stackStartRotationY, 0, 360);

		GUI.Label (new Rect (20, 400, 150, 20), "Stack Start Rotation Z");
		stackStartRotationZ = GUI.HorizontalSlider (new Rect (180, 405, 180, 20), stackStartRotationZ, 0, 360);

		stackStartRotation.x = stackStartRotationX;
		stackStartRotation.y = stackStartRotationY;
		stackStartRotation.z = stackStartRotationZ;
		formConfig.setStackStartTwist (stackStartRotation);

		//stack rotation delta
		Vector3 stackRotationDelta = formConfig.getStackTwistDelta ();
		stackRotationDeltaX = stackRotationDelta.x;
		stackRotationDeltaY = stackRotationDelta.y;
		stackRotationDeltaZ = stackRotationDelta.z;

		GUI.Label (new Rect (370, 340, 150, 20), "Stack Rotation Delta X");
		stackRotationDeltaX = GUI.HorizontalSlider (new Rect (520, 345, 180, 20), stackRotationDeltaX, -30, 30);
		
		GUI.Label (new Rect (370, 370, 150, 20), "Stack Rotation Delta Y");
		stackRotationDeltaY = GUI.HorizontalSlider (new Rect (520, 375, 180, 20), stackRotationDeltaY, -30, 30);
		
		GUI.Label (new Rect (370, 400, 150, 20), "Stack Rotation Delta Z");
		stackRotationDeltaZ = GUI.HorizontalSlider (new Rect (520, 405, 180, 20), stackRotationDeltaZ, -30, 30);

		stackRotationDelta.x = stackRotationDeltaX;
		stackRotationDelta.y = stackRotationDeltaY;
		stackRotationDelta.z = stackRotationDeltaZ;
		formConfig.setStackTwistDelta (stackRotationDelta);

		/* MUTATION STRENGTH */
		mutationStrength = formConfig.getMutationStrength ();

		GUI.Label (new Rect (20, 430, 150, 20), "Mutation Strength");
		mutationStrength = GUI.HorizontalSlider (new Rect (180, 435, 180, 20), mutationStrength, 0.5f, 3.5f);

		formConfig.setMutationStrength (mutationStrength);

		/* SCALING */
		scaling = formConfig.getScaleDelta ();

		GUI.Label (new Rect (370, 430, 150, 20), "Scaling *");
		scaling = GUI.HorizontalSlider (new Rect (520, 435, 180, 20), scaling, 0.9f, 1.1f);

		formConfig.setScaleDelta (scaling);

		/* STACK SHAPE */

		selectedShape = formConfig.getStackShapeIndex ();
		selectedShape = GUI.SelectionGrid (new Rect (20, 460, 200, 60), selectedShape, shapeGridStrings, 2);
		formConfig.setStackShape (selectedShape);

		/* Form Processor */
		if (formConfig.getFormProcessor().GetType() == typeof(StarfishForm)) {
			selectedFormProcessor = 0;
		} else if (formConfig.getFormProcessor().GetType() == typeof(LoxodromeForm)) {
			selectedFormProcessor = 1;
		} else if (formConfig.getFormProcessor().GetType() == typeof(RingForm)) {
			selectedFormProcessor = 2;
		} 
		selectedFormProcessor = GUI.SelectionGrid (new Rect (20, 525, 200, 60), selectedFormProcessor, formProcessorStrings, 2);
		if (selectedFormProcessor == 0) {
			formConfig.setFormProcessor(new StarfishForm());
		} else if (selectedFormProcessor == 1) {
			formConfig.setFormProcessor(new LoxodromeForm());
		} else if (selectedFormProcessor == 2) {
			formConfig.setFormProcessor(new RingForm());
		}

		/* Configurations */

		selectedConfiguration = formConfig.getIndex ();
		selectedConfiguration = GUI.SelectionGrid (new Rect (230, 460, 300, 90), selectedConfiguration, configurationGridStrings, 3);
		if (selectedConfiguration != formConfig.getIndex ()) {
			//selected configuration changed
			callback.changeSelectedFormConfig(selectedConfiguration);
		}

		/* Control buttons */

		if (GUI.Button(new Rect(540, 460, 160, 26), "Reset Config")) {
			formConfig.reset();
		}
		if (GUI.Button(new Rect(540, 490, 160, 26), "Create Form")) {
			callback.createNewBrush();
		}
		if (GUI.Button(new Rect(540, 520, 160, 26), "Clear Brush")) {
			callback.clearBrush();
		}

		/* Load and Save buttons */
		if (GUI.Button(new Rect(230, 560, 147, 26), "Load")) {
			ConfigXML.loadConfiguration(formConfig);
		}
		if (GUI.Button(new Rect(383, 560, 147, 26), "Save")) {
			ConfigXML.saveConfiguration(formConfig);
		}

	}
}
