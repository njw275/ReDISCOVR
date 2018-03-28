using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	//This script should be attached to each controller (Controller Left or Controller Right)

	// Getting a reference to the controller GameObject
	private SteamVR_TrackedObject trackedObj;
	// Getting a reference to the controller Interface
	private SteamVR_Controller.Device Controller;

	private bool startSwipe;
	private bool checkDirection;
	private float[] changingXvals = new float[3];
	int swipeCounter;
	int swipeCountPrev;
	public Animator anim;
	public Text textMode;
	public bool displayMenus;
	public GameObject lc;
	public GameObject rc;
	public GameObject manager;


	void Awake()
	{
		// initialize the trackedObj to the component of the controller to which the script is attached
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		startSwipe = true;
		checkDirection = false;
		swipeCounter = 1;
		swipeCountPrev = 1;
		displayMenus = true;
	}

	// Update is called once per frame
	void Update () {

		Controller = SteamVR_Controller.Input((int)trackedObj.index);


//		Controller.TriggerHapticPulse (700);
//		if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)){
//
//			Debug.Log("Yeah boiiiiiiii");
//		}

		// Getting the Touchpad Axis
		if (gameObject.name == "Controller (left)") {
			checkSwipe ();
			showMode ();
		}

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
			//anim.StopPlayback();
			//anim.enabled = false;
			//anim.isActiveAndEnabled = false;
			//anim.
			//anim.stop
		}

		// Getting the Grip Release
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log(gameObject.name + " Grip Release");


			lc.GetComponentInChildren<Canvas> ().enabled = !lc.GetComponentInChildren<Canvas> ().enabled;
			rc.GetComponentInChildren<Canvas> ().enabled = !rc.GetComponentInChildren<Canvas> ().enabled;

			displayMenus = lc.GetComponentInChildren<Canvas> ().enabled;

			if(!displayMenus){
				swipeCountPrev = swipeCounter;
				swipeCounter = 100;
			}else{
				swipeCounter = swipeCountPrev;
			}

			manager.GetComponent<prefabDisplay>().showPrefabs = displayMenus;
		}
		//Debug.Log ("SC: " + swipeCounter);
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
					swipeCounter++;
					if (swipeCounter == 4) {
						swipeCounter = 1;
					}
				} else {
					Debug.Log ("going to left");
					swipeCounter--;
					if (swipeCounter == 0) {
						swipeCounter = 3;
					}
				}
			}
			checkDirection = false;
		}
	}


	void showMode(){
	
		GameObject movementCube = GameObject.Find ("MovementCube");
		GameObject scaleCube = GameObject.Find ("ScaleCube");
		GameObject rotationCube = GameObject.Find ("RotationCube");

		if (swipeCounter == 1) {
			
			
			movementCube.GetComponent<Renderer> ().enabled = true;
			scaleCube.GetComponent<Renderer> ().enabled = false;
			rotationCube.GetComponent<Renderer> ().enabled = false;
			textMode.text = "Movement Mode";

		} else if (swipeCounter == 2) {
		
			movementCube.GetComponent<Renderer> ().enabled = false;
			scaleCube.GetComponent<Renderer> ().enabled = true;
			rotationCube.GetComponent<Renderer> ().enabled = false;
			textMode.text = "Scale Mode";
		
		} else if (swipeCounter == 3) {

			movementCube.GetComponent<Renderer> ().enabled = false;
			scaleCube.GetComponent<Renderer> ().enabled = false;
			rotationCube.GetComponent<Renderer> ().enabled = true;
			textMode.text = "Rotation Mode";
		
		} 
		else {
		
			movementCube.GetComponent<Renderer> ().enabled = false;
			scaleCube.GetComponent<Renderer> ().enabled = false;
			rotationCube.GetComponent<Renderer> ().enabled = false;

		}
	
	}


}