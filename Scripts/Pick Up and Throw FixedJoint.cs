using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be updated to poll for inputs and set flags

// !You may need to rename this script to "controller_pickup.cs"!

// Attach this script to one or both Vive controllers
// Holding the trigger will allow you to hold or throw 
// any object with a collider that is not Kinetmatic 

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class pickup_rigidbody : MonoBehaviour
{

    public Rigidbody rigidBodyAttachPoint1; //To connect FixedJoint1 (controller)

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller

    public Transform objectReferenceSlot1;  //A reference to an object 

    FixedJoint fixedJoint1;

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

        if (fixedJoint1 == null && controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //If the trigger is being pressed and fixedjoint is null
        {
            Debug.Log("You have collided with " + col.name + " with the trigger down");

            fixedJoint1 = col.gameObject.AddComponent<FixedJoint>(); //Add a fixed joint to the object collided with
            fixedJoint1.connectedBody = rigidBodyAttachPoint1; //Connect to this rigidbody
        }

        else if (fixedJoint1 && controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) //If an object is held and the trigger is released
        {
            GameObject obj = fixedJoint1.gameObject; //Get the gameobject of FixedJoint1
            Rigidbody rigidbody = obj.GetComponent<Rigidbody>(); //Get attach point
            Object.Destroy(fixedJoint1); //Destroy the attach point
            fixedJoint1 = null; //Set to null

            tossObject(rigidbody); //physics
        }
    }
}