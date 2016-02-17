using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormControllerScript : MonoBehaviour, IColourConfigCallback, IBuilderConfigCallback {

	private List<Trunk> currentScene = new List<Trunk>();
	private Trunk brush = null;

	private List<IFormConfiguration> allConfigs = new List<IFormConfiguration>();
	private IFormConfiguration currentConfig;

	private ColourConfiguration colourConfig = new ColourConfiguration();
	private ColourPalette colourPalette;
	private bool displayColourPalette = false;

	private FormBuilderPalette formBuilderPalette;
	private bool displayFormBuilderPalette;

	private HelpPalette helpPalette;
	private bool displayHelp = false;

	//damp keyboard input on toggles
	private const int keyDelayLength = 10;
	private int keyDelayCount = 0;
	private bool keyDelayOn = false;

	private static float largestBoundsDistance;

	private Material dawnMaterial;
	private Material eerieMaterial;
	private Material nightMaterial;
	private Material noMaterial;

	private Camera mainCamera;
	private Camera backgroundCamera;

	private bool autoPreset = false;
	private bool autoMutate1 = false;
	private bool autoMutate2 = false;
	private float autoChangeCount = 0;

//	private bool waitForJuliaSetToRender = false;

	// Use this for initialization
	void Start () {

		IFormConfiguration formConfig1 = new FormConfig1 ();
		IFormConfiguration formConfig2 = new FormConfig2 ();
		IFormConfiguration formConfig3 = new FormConfig3 ();
		IFormConfiguration formConfig4 = new FormConfig4 ();
		IFormConfiguration formConfig5 = new FormConfig5 ();
		IFormConfiguration formConfig6 = new FormConfig6 ();
		IFormConfiguration formConfig7 = new FormConfig7 ();
		IFormConfiguration formConfig8 = new FormConfig8 ();
		IFormConfiguration formConfig9 = new FormConfig9 ();

		allConfigs.Add (formConfig1);
		allConfigs.Add (formConfig2);
		allConfigs.Add (formConfig3);
		allConfigs.Add (formConfig4);
		allConfigs.Add (formConfig5);
		allConfigs.Add (formConfig6);
		allConfigs.Add (formConfig7);
		allConfigs.Add (formConfig8);
		allConfigs.Add (formConfig9);

		currentConfig = formConfig1;

		dawnMaterial = Resources.Load("DawnDusk Skybox") as Material;
		eerieMaterial = Resources.Load("Eerie Skybox") as Material;
		nightMaterial = Resources.Load("StarryNight Skybox") as Material;

		mainCamera = Camera.main;
		mainCamera.clearFlags = CameraClearFlags.Skybox;
		RenderSettings.skybox = dawnMaterial;
		backgroundCamera = GameObject.Find ("Background Camera").GetComponent<Camera>();
		backgroundCamera.enabled = false;

		formBuilderPalette = new FormBuilderPalette (this);
		colourPalette = new ColourPalette (this);
		helpPalette = new HelpPalette ();

		//initialiase with something

		createNewBrush(); //slot 1	

		//make it colourful
		colourConfig.setCycle (true);
		colourConfig.setPulse (true);
		colourConfig.setFadeColour (true);

		autoPreset = true;
		autoMutate1 = true;
	}

	//configuration and help screens
	void OnGUI () {

		if (displayHelp) {
			helpPalette.displayHelp();
		}

		if (displayFormBuilderPalette) {
			formBuilderPalette.displayFormBuilderPalette(currentConfig);
		}

		if (displayColourPalette) {
			colourPalette.displayColourPalette(colourConfig);
		}

	}

	// Update is called once per frame
	void Update () {

		/* Check for key input */

		if (Input.GetKey("1")) {
			//config 1
			currentConfig = allConfigs[0];
		}

		if (Input.GetKey("2")) {
			//config 2
			currentConfig = allConfigs[1];
		}

		if (Input.GetKey("3")) {
			//config 3
			currentConfig = allConfigs[2];
		}

		if (Input.GetKey("4")) {
			//config 4
			currentConfig = allConfigs[3];
		}

		if (Input.GetKey("5")) {
			//config 5
			currentConfig = allConfigs[4];
		}

		if (Input.GetKey("6")) {
			//config 6
			currentConfig = allConfigs[5];
		}

		if (Input.GetKey("7")) {
			//config 7
			currentConfig = allConfigs[6];
		}

		if (Input.GetKey("8")) {
			//config 8
			currentConfig = allConfigs[7];
		}

		if (Input.GetKey("9")) {
			//config 9
			currentConfig = allConfigs[8];
		}

		if (Input.GetKey("n")) {
			//new form
			createNewBrush();
			return;
		}

		if (Input.GetKey ("b")) {
			//clear brush
			clearBrush();
		}

		if (Input.GetKey("s")) {
			//save current form to scene
			saveForm();
			return;
		}

		if (Input.GetKey("c")) {
			//clear scene
			clearScene();
			return;
		}

		if (!keyDelayOn) {
			if (Input.GetKey("v")) {
				//branch scaling
				currentConfig.setScaleBranch(!currentConfig.getScaleBranch());
				//switch on key delay to stop retrigger
				switchOnKeyDelay();
			}
		}

		if (Input.GetKey ("r")) {
			//symetrical random mutation
			currentConfig.randomiseValuesWithSymetry();
		}

		if (Input.GetKey ("t")) {
			//non symetrical random mutation
			currentConfig.randomiseValuesWithoutSymetry();
		}

		if (!keyDelayOn) {
			if (Input.GetKey ("p")) {
				//toggle colour palette
				displayColourPalette = !displayColourPalette;
				if (displayColourPalette) {
					displayHelp = false;
				}
				//switch on key delay to stop retrigger
				switchOnKeyDelay();
			}
		}

		if (!keyDelayOn) {
			if (Input.GetKey ("f")) {
				//toggle builder palette
				displayFormBuilderPalette = !displayFormBuilderPalette;
				if (displayFormBuilderPalette) {
					displayHelp = false;
				}
				//switch on key delay to stop retrigger
				switchOnKeyDelay();
			}
		}

		if (!keyDelayOn) {
			if (Input.GetKey("j")) {
				autoPreset = !autoPreset;
				if (autoPreset) {
					int random = Random.Range(0, 9);
					currentConfig = allConfigs[random];
					autoMutate1 = false;
					autoMutate2 = false;
					autoChangeCount = 0;
				}
				switchOnKeyDelay();
			}
		}

		if (!keyDelayOn) {
			if (Input.GetKey("k")) {
				autoMutate1 = !autoMutate1;
				if (autoMutate1) {
					currentConfig.randomiseValuesWithSymetry();
					autoPreset = false;
					autoMutate2 = false;
					autoChangeCount = 0;
				}
				switchOnKeyDelay();
			}
		}

		if (!keyDelayOn) {
			if (Input.GetKey("l")) {
				autoMutate2 = !autoMutate2;
				if (autoMutate2) {
					currentConfig.randomiseValuesWithoutSymetry();
					autoPreset = false;
					autoMutate1 = false;
					autoChangeCount = 0;
				}
				switchOnKeyDelay();
			}
		}

		//help key
		if (!keyDelayOn) {
			if (Input.GetKey("h")) {
				displayHelp = !displayHelp;
				if (displayHelp) {
					//switch off any other open forms
					displayColourPalette = false;
					displayFormBuilderPalette = false;
				}
				switchOnKeyDelay();
			}
		}

		/* key delay timer */
		if (keyDelayOn) {
			if (keyDelayCount < keyDelayLength) {
				keyDelayCount++;
			} else {
				//finished delay
				keyDelayOn = false;
			}
		}

		/* Automated Stuff */
		//random go through presets
		if (autoPreset) {
			if (autoChangeCount < 15) {
				autoChangeCount += Time.deltaTime;
			} else {
				autoChangeCount = 0;
				int random = Random.Range(0, 9);
				currentConfig = allConfigs[random];
			}
		}

		//auto mutate1
		if (autoMutate1) {
			if (autoChangeCount < 35) {
				autoChangeCount += Time.deltaTime;
			} else {
				autoChangeCount = 0;
				currentConfig.randomiseValuesWithSymetry();
			}
		}
		//auto mutate 2
		if (autoMutate2) {
			if (autoChangeCount < 25) {
				autoChangeCount += Time.deltaTime;
			} else {
				autoChangeCount = 0;
				currentConfig.randomiseValuesWithoutSymetry();
			}
		}

		//default
		renderScene ();
	}

	/* Interface Methods */

	public static float getLargestBoundsDistance() {
		return largestBoundsDistance;
	}

	public void updateColourConfig(ColourConfiguration colourConfig) {
		this.colourConfig = colourConfig;
	}

	public void setBackground(ColourConfiguration colourConfig) {

		this.colourConfig = colourConfig;

		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Dawn) {
			backgroundCamera.enabled = false;
			mainCamera.clearFlags = CameraClearFlags.Skybox;
			RenderSettings.skybox = dawnMaterial;
		}
		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Eerie) {
			backgroundCamera.enabled = false;
			mainCamera.clearFlags = CameraClearFlags.Skybox;
			RenderSettings.skybox = eerieMaterial;
		}
		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Night) {
			backgroundCamera.enabled = false;
			mainCamera.clearFlags = CameraClearFlags.Skybox;
			RenderSettings.skybox = nightMaterial;
		}
/*
		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.Julia) {
			//julia set takes time to render so kick it off in thread
			//and wait until its generated before switching cameras
			//carry on doing whatever we are doing now until render ready
			BackgroundImageControllerScript.startJuliaSet();
			waitForJuliaSetToRender = true;
		}
//*/
		if (colourConfig.getBackgroundType () == ColourConfiguration.BackgroundType.None) {
			backgroundCamera.enabled = false;
			mainCamera.clearFlags = CameraClearFlags.Skybox;
			RenderSettings.skybox = noMaterial;
		}
	}

	public void changeSelectedFormConfig(int index) {
		currentConfig = allConfigs [index];
	}

	public void createNewBrush() {

		//clear current brush
		clearBrush ();
		//create new colour configuration for each form
		ColourConfiguration.BackgroundType background = ColourConfiguration.BackgroundType.None;
		if (colourConfig != null) {
			background = colourConfig.getBackgroundType();
		}
		colourConfig = new ColourConfiguration();
		colourConfig.setBackgroundType (background);
		//create new brush
		brush = new Trunk(currentConfig, colourConfig);
		largestBoundsDistance = brush.getLargestBoundDistance ();
	}

	public void clearBrush() {

		if (brush != null) {
			brush.removeFromScene ();
			brush = null;
		}
	}

	/* private methods */

	private void saveForm() {

		if (brush != null) {
			//save current form to scene
			IFormConfiguration sceneFormConfig = new IFormConfiguration(currentConfig);
			ColourConfiguration sceneColourConfig = new ColourConfiguration(colourConfig);
			currentScene.Add(new Trunk(sceneFormConfig, sceneColourConfig));
			clearBrush();
		}
	}

	private void renderScene() {

/*
		//Julia set takes time to render so we have to wait for it to complete
		if (waitForJuliaSetToRender) {
			if (BackgroundImageControllerScript.isSetGenerated()) {
				//julia set ready switch cameras
				mainCamera.enabled = false; //switch off first - bizarrre but it works
				backgroundCamera.enabled = true;
				mainCamera.enabled = true; //now switch on again
				RenderSettings.skybox = noMaterial;
				mainCamera.clearFlags = CameraClearFlags.Depth;
				waitForJuliaSetToRender = false;
			}
		}
*/
		//render brush
		if (brush != null) {
			//mutate current form
			brush.mutate(currentConfig, colourConfig);
			largestBoundsDistance = brush.getLargestBoundDistance();
		}

		//render saved forms
		foreach (Trunk model in currentScene) {
			model.mutate(null, null);
		}
	}

	private void clearScene() {

		//clear current brush
		clearBrush ();
		//clear scene
		foreach (Trunk form in currentScene) {
			form.removeFromScene();
		}
		currentScene = new List<Trunk>();
	}

	private void switchOnKeyDelay() {
		keyDelayOn = true;
		keyDelayCount = 0;
	}
}
