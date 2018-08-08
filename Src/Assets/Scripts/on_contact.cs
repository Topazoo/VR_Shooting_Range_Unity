using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Update to poll for inputs and set flags

public class on_contact : MonoBehaviour
{

    Quaternion parent_rot; //The rotation of the parent
    public Vector3 rot_angle; //rotation offset angle
    public Vector3 offset; // offsets for position

    SteamVR_Controller.Device controller; //The controller
    SteamVR_TrackedObject trackedObj; //The tracked object

    void Update() //Called every frame
    {
        if (this.transform.parent) //If an object parents it
        {
            Debug.Log("An object senses it's parented");
            parent_rot = transform.parent.rotation; 
            transform.rotation = parent_rot; //Get parent rotation
            transform.Rotate(rot_angle); 
            transform.localPosition = offset; //apply position offset   

        }

    }
       
        

}
