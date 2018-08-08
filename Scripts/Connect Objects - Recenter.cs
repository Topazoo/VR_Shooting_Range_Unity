using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be updated to poll for inputs and set flags

// !You may need to rename this script to "button_manipulate.cs"!

// Attach this script to one or both Vive controllers
// Attach the object you want to manipulate to "objectReferenceSlot1" 
// The touchpad will center the object in "objectReferenceSlot1"

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class button_manipulate : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller
	public Transform objectReferenceSlot1;  //A reference to an object

    // First event function
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
	}    
	
	void FixedUpdate () {
        controller = SteamVR_Controller.Input((int) trackedObj.index); //Create Controller variable and use it to store inputs
		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) //On touchpad top press
		{
			Debug.Log("Touchpad top was clicked"); //Log action 
			objectReferenceSlot1.transform.position = new Vector3(0.0f, 0.5f, 0.0f); //recenter object
			objectReferenceSlot1.GetComponent<Rigidbody>().velocity = Vector3.zero; //Set velocity back to zero
			objectReferenceSlot1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //set angular velocity back to zero
		}
	}

}