using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !This script may need to be renamed pickup_touchpad.cs!

// This script allows objects to be picked up when a user touches the
// thumbpad, it feels a bit more natural to me and frees up the trigger
// attach this script to either or both controllers.

//Update to poll for inputs and set flags

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class pickup_touchpad : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller
    bool heldFlag = false; //Is an object held

    // First event function
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
    }

    // Called on a physics step - FixedUpdate timestep changed to 90fps (1/90) Change to update flags
    void FixedUpdate()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index); //Create Controller variable and use it to store inputs
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
        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && col.gameObject.transform.parent == null && heldFlag == false) //If the touchpad is pressed and nothing is held
        {
            Debug.Log("You have collided with " + col.name + " with the touchpad down");

            heldFlag = true;
            col.attachedRigidbody.isKinematic = true; //Make Kinematic (no pyhsics)
            col.gameObject.transform.SetParent(gameObject.transform); //Give the collider gameobject the transform of this controller's gameobject

        }

        if (controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) && heldFlag == true) //If the touchpad is pressed while holding something
        {
            Debug.Log("You pressed the touchpad releasing " + col.name);
            col.gameObject.transform.SetParent(null); //Gameworld becomes parent 
            col.attachedRigidbody.isKinematic = false; //Physics can take effect

            heldFlag = false;

            if (col.attachedRigidbody)
                tossObject(col.attachedRigidbody);
        }
    }

}