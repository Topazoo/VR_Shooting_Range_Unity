using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be updated to poll for inputs and set flags

// !You may need to rename this script to "controller_pickup.cs"!

// Attach this script to one or both Vive controllers
// Holding the trigger will allow you to hold and drop
// any object with a collider that is not Kinetmatic 

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class controller_pickup : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller

    // First event function
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
	}    
	
	void OnTriggerStay(Collider col) //OnTriggerStay Collision (pick up an object)
    {
        Debug.Log("Collided with " + col.name + " OnTriggerStay");

        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //If the trigger is being pressed
        {
            Debug.Log("You have collided with " + col.name + " with the trigger down");

            col.attachedRigidbody.isKinematic = true; //Make Kinematic (no pyhsics)
            col.gameObject.transform.SetParent(gameObject.transform); //Give the collider gameobject the transform of this controller's gameobject

        }

        if (controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) //If the trigger is released
        {
            Debug.Log("You released the trigger holding " + col.name);
            col.gameObject.transform.SetParent(null); //Gameworld becomes parent 
            col.attachedRigidbody.isKinematic = false; //Physics can take effect
        }
    }
	
}