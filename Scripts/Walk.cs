using UnityEngine;
using System.Collections;
using Valve.VR;

// Attach this script to one or both Vive controllers
// Attach the CameraRig and whatever object designates forward 
//(Usually the eyes or head camera) to the corresponding public variable
// The top of the touchpad will make the camera go forward and the bottom will make it
// reverse. The Y axis will never change.

public class Walk : MonoBehaviour
{
	
    SteamVR_TrackedObject trackedObj; //The tracked object
    SteamVR_Controller.Device controller; //The controller
    public GameObject cameraRig; //The Camera Rig
    public GameObject look; //The object considered "forward"
    public float speed = 2.0f;
   
    Vector2 touchpad; //Where the user's finger is on the touchpad
    Vector3 currentLocation; //The current location of the camera rig
    Vector3 nextLocation; //Where it should move to


    // First event function
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); //get and set required component
    }


    // Called on a physics step - FixedUpdate timestep changed to 90fps (1/90) Change to update flags
    void FixedUpdate()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index); //Create Controller variable and use it to store inputs
        currentLocation = cameraRig.transform.position; // Get position of rig

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) //On touchpad top press
        {
            Debug.Log("Touchpad top was clicked to move"); //Log action 
            touchpad = controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            if (touchpad.y > 0.15f)
            {
                Debug.Log("forward"); //Log action 
                nextLocation = currentLocation; //Next location begins at current
                nextLocation += look.transform.forward * speed * Time.deltaTime; // moves on all axes
                nextLocation[1] = 0; //revert Y
                cameraRig.transform.position = nextLocation; //move rig
            }

            else
            {
                Debug.Log("backward");
                nextLocation = currentLocation; //Next location begins at current
                nextLocation -= look.transform.forward * speed * Time.deltaTime; // moves on all axes
                nextLocation[1] = 0; //revert Y
                cameraRig.transform.position = nextLocation; //move rig
            }
        }

    }


}