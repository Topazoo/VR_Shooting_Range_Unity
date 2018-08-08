using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    SteamVR_Controller.Device controller; //The controller
    SteamVR_TrackedObject trackedObj; //The tracked object (Cameras and Controllers)
    public GameObject projectile1; //The projectile
    public GameObject spawn; //Where the projectile should spawn from
    public float projectile1Speed; //Projectile speed
    Transform position; //Projectile position
    public string child1;

    void FixedUpdate() //Called every frame
    {
        if (this.transform.parent) //If an object parents it
        {
            trackedObj = GetComponentInParent<SteamVR_TrackedObject>();

            if (trackedObj)
                controller = SteamVR_Controller.Input((int)trackedObj.index); //Initialize Controller variable and use it to store inputs

            if (controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && (transform.Find(child1).gameObject.GetComponent<Renderer>().enabled == true)) //Trigger and magazine
            {
                Debug.Log("Trigger pulled with object held");
                position = spawn.transform; //Get spawn position
                GameObject bullet = Instantiate(projectile1, position.position, position.rotation); //Create projectile
                bullet.GetComponent<Rigidbody>().velocity = position.TransformDirection(new Vector3(0,0,projectile1Speed)); //Give it velocity
                controller.TriggerHapticPulse(3000); //Haptic feedback
                AudioSource aud = GetComponent<AudioSource>(); //Get firing sound
                aud.Play(); //Play
                ParticleSystem sys = spawn.GetComponent<ParticleSystem>();//Get particle effect of spawner
                sys.Play(); //Play
                Destroy(bullet.gameObject, 5); //Destroy in 5 seconds

            }

            if (! (transform.Find(child1).gameObject) )
            {
                Debug.Log("NoMag!");
            }
        }

    }

}
