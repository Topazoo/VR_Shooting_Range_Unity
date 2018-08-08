using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be updated to poll for inputs and set flags

// !You may need to rename this script to "controller_pickup.cs"!

// Attach this script to one or both Vive controllers
// Holding the trigger will allow you to hold or throw 
// any object with a collider that is not Kinetmatic 

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class controller_pickup : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller

    // First event function
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
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

            if (col.attachedRigidbody)
                tossObject(col.attachedRigidbody);
        }
    }
	
}