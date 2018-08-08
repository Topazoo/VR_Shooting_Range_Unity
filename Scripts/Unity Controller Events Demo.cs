using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be updated to poll for inputs and set flags

// !You may need to rename this script to "trigger_input.cs"!

// Attach this script to one or both Vive controllers
// Holding and releasing the trigger will write descriptions of
// The actions to the Debug log 

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class trigger_input : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller

    // First event function
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
	}
	
	// Called on a physics step - FixedUpdate timestep changed to 90fps (1/90)
	void FixedUpdate () {
        controller = SteamVR_Controller.Input((int) trackedObj.index); //Create Controller variable and use it to store inputs

        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //On trigger hold
        {
            Debug.Log("Trigger is being held"); //Log action 
        }

        if (controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) //On initial trigger pull
        {
            Debug.Log("Trigger was pulled"); //Log action 
        }

        if (controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) //On trigger release
        {
            Debug.Log("Trigger was released"); //Log action 
        }

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)) //On (a bit before) trigger click
        {
            Debug.Log("Trigger is being clicked"); //Log action 
        }

        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) //On full trigger pull
        {
            Debug.Log("Trigger was clicked"); //Log action 
        }

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) //On full trigger release
        {
            Debug.Log("Trigger was unclicked"); //Log action 
        }

    }
}