using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BowelMovementController : MonoBehaviour {

    public List<GameObject> pistons = new List<GameObject>();
    public float[] pistonSpeeds;
    public float maxPistonReach = 0.5f;

	// Use this for initialization
	void Start () {

        pistonSpeeds = new float[3];
        pistonSpeeds[0] = 1;
        pistonSpeeds[1] = 0.5f;
        pistonSpeeds[2] = 0.1f;

	    foreach(GameObject piston in GameObject.FindGameObjectsWithTag("Piston"))
        {
            pistons.Add(piston);
        }


	}
	
	// Update is called once per frame
	void Update () {

        // Move pistons
        foreach(GameObject Piston in pistons)
        {
            PistonScript p = Piston.GetComponent<PistonScript>();
            float t = p.trajectory;
            // Reverse dir
            if (p.forward && t > maxPistonReach)
            {
                
                p.forward = false;

            } 

            if (!p.forward && t < -maxPistonReach)
            {
                p.forward = true;
            }
            float moveAmount = pistonSpeeds[p.group - 1] * Time.deltaTime;

            if(p.forward)
            {
                // move forward

                p.trajectory += moveAmount;
                p.transform.Translate(Vector3.up * moveAmount);

            } else
            {
                // move backwards by speed
                p.trajectory -= moveAmount;
                p.transform.Translate(Vector3.up * -moveAmount);

            }
       
        }

	
	}
}
