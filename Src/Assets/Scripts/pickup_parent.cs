using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Update to poll for inputs and set flags

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class pickup_parent : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller

    // First event function
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
	}
	
	// Called on a physics step - FixedUpdate timestep changed to 90fps (1/90) Change to update flags
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

    void tossObject(Rigidbody rigidbody) //Throw objects with controller's velocity
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent; //Origin is tracked object's origin

        if (origin) //Make sure it's not null
        {
            rigidbody.velocity = origin.TransformVector(controller.velocity); //Transform to world space not local
            rigidbody.angularVelocity = origin.TransformVector(controller.angularVelocity);
        }

        else
        {
            rigidbody.velocity = controller.velocity; //Else throw in local space
            rigidbody.angularVelocity = controller.angularVelocity;
        }
    }

    void OnTriggerStay(Collider col) //OnTriggerStay Collision (pick up an object)
    {
        Debug.Log("Collided with " + col.name + " OnTriggerStay");

        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && col.gameObject.transform.parent == null) //If the trigger is being pressed
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

            if (col.attachedRigidbody)
                tossObject(col.attachedRigidbody);
        }
    }

}
