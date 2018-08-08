using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Update to poll for inputs and set flags

[RequireComponent(typeof(SteamVR_TrackedObject))] // This script requires a SteamVR tracked object

public class reset_object : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller

    public Transform objectReferenceSlot1;  //A reference to an object

    // First event function
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
    }

    // Called on a physics step - FixedUpdate timestep changed to 90fps (1/90) Change to update flags
    void FixedUpdate()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index); //Create Controller variable and use it to store inputs

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) //On touchpad top press
        {
            Debug.Log("Touchpad top was clicked"); //Log action 
            objectReferenceSlot1.transform.position = new Vector3(0.0f, 0.5f, 0.0f); //recenter object
            objectReferenceSlot1.GetComponent<Rigidbody>().velocity = Vector3.zero; //Set velocity back to zero
            objectReferenceSlot1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //set angular velocity back to zero
        }

    }

}
