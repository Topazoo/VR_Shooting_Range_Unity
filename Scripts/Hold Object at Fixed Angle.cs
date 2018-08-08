using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !This script may need to be renamed snap_to.cs!

// This script will make sure an item is at the same fixed position every
// time it is picked up via being parented by the controller. Attach this script
// to an object with a collider.

//Update to poll for inputs and set flags

public class snap_to : MonoBehaviour
{

    Quaternion parent_rot; //The rotation of the parent
    public Vector3 rot_angle; //rotation offset angle
    public Vector3 offset; // offsets for position

    void Update() //Called every frame
    {
        if (this.transform.parent) //If an object parents it
        {
            Debug.Log("An object senses it's parented");
            parent_rot = transform.parent.rotation; 
            transform.rotation = parent_rot; //Get parent rotation
            transform.Rotate(rot_angle); 
            Debug.Log("Rotation currently " + transform.rotation);
            transform.localPosition = offset; //apply position offset
        }

    }
       
        

}
