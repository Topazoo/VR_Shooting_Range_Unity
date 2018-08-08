using UnityEngine;
using System.Collections;

// !You may need to rename this script to "noclip.cs"!

// Attach this script to one or both Vive controllers
// Attach the CameraRig and whatever object designates forward 
//(Usually the eyes or head camera) to the corresponding public variable
// The touchpad will make the camera go forward.

public class noclip : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller
    public GameObject cameraRig; //The Camera Rig
    public GameObject look; //The object considered "forward"
    public float speed = 2.0f;


    // First event function
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
    }


    // Called on a physics step - FixedUpdate timestep changed to 90fps (1/90)
    void FixedUpdate()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index); //Create Controller variable and use it to store inputs

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) //On touchpad top press
        {
            Debug.Log("Touchpad top was clicked to move"); //Log action 
            cameraRig.transform.position += look.transform.forward * speed * Time.deltaTime; //move in direction
        }

    }


}