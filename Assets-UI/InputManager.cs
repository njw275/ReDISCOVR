using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	//This script should be attached to each controller (Controller Left or Controller Right)

	// Getting a reference to the controller GameObject
	private SteamVR_TrackedObject trackedObj;
	// Getting a reference to the controller Interface
	private SteamVR_Controller.Device Controller;

	private bool startSwipe;
	private bool checkDirection;
	private float[] changingXvals = new float[3];

	void Awake()
	{
		// initialize the trackedObj to the component of the controller to which the script is attached
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		startSwipe = true;
		checkDirection = false;
	}

	// Update is called once per frame
	void Update () {

		Controller = SteamVR_Controller.Input((int)trackedObj.index);

		// Getting the Touchpad Axis
		checkSwipe();

		// Getting the Trigger press
		if (Controller.GetHairTriggerDown())
		{
			Debug.Log(gameObject.name + " Trigger Press");
		}

		// Getting the Trigger Release
		if (Controller.GetHairTriggerUp())
		{
			Debug.Log(gameObject.name + " Trigger Release");
		}

		// Getting the Grip Press
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Press");
		}

		// Getting the Grip Release
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Release");
			GameObject lc = GameObject.Find("Controller (left)");
			GameObject rc = GameObject.Find("Controller (right)");

			lc.GetComponentInChildren<Canvas> ().enabled = !lc.GetComponentInChildren<Canvas> ().enabled;
			rc.GetComponentInChildren<Canvas> ().enabled = !rc.GetComponentInChildren<Canvas> ().enabled;
		}
	}


	void checkSwipe(){
	
		// Getting the Touchpad Axis
		if (Controller.GetAxis () != Vector2.zero) {
			//Get first x
			if (startSwipe == true) {
				for (int i = 0; i < 3; i++) {
					changingXvals [i] = Controller.GetAxis ().x;
				}
				startSwipe = false;
			} else {

				if(Controller.GetAxis().x != changingXvals[2]){
					changingXvals [1] = changingXvals [2];
					changingXvals [2] = Controller.GetAxis ().x;

				}

			}
			checkDirection = true;
			//Get second & third
			//if keep going change third to new and second to third
			//check last x
			//kleft or right?

		} else if (Controller.GetAxis () == Vector2.zero) {
			startSwipe = true;
			if (checkDirection) {
				if (changingXvals [0] < 0 && changingXvals [2] > 0) {
					Debug.Log ("going to right");
				} else {
					Debug.Log ("going to left");
				}
			}
			checkDirection = false;
		}
	
	}



}