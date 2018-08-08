using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class float_obj : MonoBehaviour {

    Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position[1] <= .05)
        {
            newPos[0] = transform.position[0];
            newPos[2] = transform.position[2];
            newPos[1] = 0.0f;
            transform.position = newPos;
        }
	}
}
