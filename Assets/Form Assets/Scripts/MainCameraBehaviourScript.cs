using UnityEngine;
using System.Collections;

public class MainCameraBehaviourScript : MonoBehaviour {

	public GameObject target = null;
	private int speed = 50;

	//auto simulate key presses
	private bool auto = false;
	private bool keyQ = false;
	private bool keyW = false;
	private bool keyLeft = false;
	private bool keyRight = false;
	private bool keyUp = false;
	private bool keyDown = false;
	private bool keyComma = false;
	private bool keyPeriod = false;

	private float autoWaitTime = 0;

	//damp keyboard input on toggles
	private const int keyDelayLength = 30;
	private int keyDelayCount = 0;
	private bool keyDelayOn = false;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("FormController");
		if (target == null) {
			Debug.Log("camera target not found"); 
		} else {
			transform.LookAt(target.transform);
			//initialiase with some movement
			auto = true;
			keyQ = true;
			keyUp = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (target != null) {

			if (auto) {
				if (autoWaitTime < 15) {
					autoWaitTime += Time.deltaTime;
				} else {
					changeCameraMovement();
					autoWaitTime = 0;
				}
			}

			if (!keyDelayOn) {
				if (Input.GetKey("a")) {
					keyDelayOn = true;
					keyDelayCount = 0;
					auto = !auto;
					if (auto) {
						changeCameraMovement();
						autoWaitTime = 0;
					} else {
						resetCameraMovement();
					}
				}
			}

			//ensure camera looks at target at origin
			//transform.LookAt(target.transform);

			//zoom by keys
			bool zoomByKeys = false;
			if (Input.GetKey ("z")) {
				//zoom in
				//camera.fieldOfView--; 
				transform.Translate(Vector3.forward * Time.deltaTime * 5);
				zoomByKeys = true;
			}
			if (Input.GetKey ("x")) {
				//zoom out
				//camera.fieldOfView++;
				transform.Translate(Vector3.back * Time.deltaTime * 5);
				zoomByKeys = true;
			}

			if (!zoomByKeys) {

				float largestBoundsDistance = FormControllerScript.getLargestBoundsDistance ();
				float cameraTargetDistance = Vector3.Distance(target.transform.position, transform.position);
				float multiplier = 1.5f;
				float deltaLimit = 0.2f;
				if (largestBoundsDistance < 15) {
					multiplier = 2.5f;
				}
				float delta = cameraTargetDistance - largestBoundsDistance * multiplier;

				//avoid camera judder
				if (delta > deltaLimit) {
					transform.Translate(Vector3.forward * Time.deltaTime * 2);
				}
				if (delta < -deltaLimit) {
					transform.Translate(Vector3.back * Time.deltaTime * 2);
				}
			}

			//react to orbit rotation keys and auto
			if (Input.GetKey("up") || keyUp) {
				transform.RotateAround(target.transform.position, Vector3.right, Time.deltaTime * speed);
			} 
			if (Input.GetKey("down") || keyDown) {
				transform.RotateAround(target.transform.position, Vector3.left, Time.deltaTime * speed);
			}
			if (Input.GetKey("left") || keyLeft) {
				transform.RotateAround(target.transform.position, Vector3.down, Time.deltaTime * speed);
			}
			if (Input.GetKey("right") || keyRight) {
				transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * speed);
			}
			if (Input.GetKey("q") || keyQ) {
				transform.RotateAround(target.transform.position, Vector3.forward, Time.deltaTime * speed);
			}
			if (Input.GetKey("w") || keyW) {
				transform.RotateAround(target.transform.position, Vector3.back, Time.deltaTime * speed);
			}
			if (Input.GetKey(",") || keyComma) {
				transform.RotateAround(transform.position, transform.forward, Time.deltaTime * 90f);
			}
			if (Input.GetKey(".") || keyPeriod) {
				transform.RotateAround(transform.position, transform.forward, Time.deltaTime * -90f);
			}
		}

		//key delay
		if (keyDelayOn) {
			if (keyDelayCount < keyDelayLength) {
				keyDelayCount++;
			} else {
				//finished delay
				keyDelayOn = false;
			}
		}
	}

	private void resetCameraMovement() {
		keyQ = false;
		keyW = false;
		keyLeft = false;
		keyRight = false;
		keyUp = false;
		keyDown = false;
		keyComma = false;
		keyPeriod = false;
	}

	private void changeCameraMovement() {

		resetCameraMovement ();

		float random = Random.Range (0, 8);

		if (random < 1) {
			keyQ = true;
			keyUp = true;
		} else if (random < 2) {
			keyLeft = true;
			keyComma = true;
		} else if (random < 3) {
			keyUp = true;
			keyQ = true;
		} else if (random < 4) {
			keyComma = true;
			keyLeft = true;
		} else if (random < 5) {
			keyW = true;
			keyDown = true;
		} else if (random < 6) {
			keyRight = true;
			keyPeriod = true;
		} else if (random < 7) {
			keyDown = true;
			keyW = true;
		} else if (random < 8) {
			keyPeriod = true;
			keyRight = true;
		}
	}
}
