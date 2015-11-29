using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BowelMovementController : MonoBehaviour {

    public List<GameObject> pistons = new List<GameObject>();
    public float[] pistonSpeeds;
    public float maxPistonReach = 0.4f;
    public GameObject[] sliderPegs = new GameObject[3];
    private float[] pegStartPos;
    private float maxSpeed = 0.75f;

	// Use this for initialization
	void Start () {

        pistonSpeeds = new float[3];
        pistonSpeeds[0] = 0.0f;
        pistonSpeeds[1] = 0.0f;
        pistonSpeeds[2] = 0.0f;

        pegStartPos = new float[3];
        pegStartPos[0] = sliderPegs[0].transform.position.y;
        pegStartPos[1] = sliderPegs[1].transform.position.y;
        pegStartPos[2] = sliderPegs[2].transform.position.y;

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

            } else if(!p.forward)
            {
                // move backwards by speed
                p.trajectory -= moveAmount;
                p.transform.Translate(Vector3.up * -moveAmount);
            }
        }

        // Keyboard controls
        if(Input.GetKey(KeyCode.Q) && pistonSpeeds[0] < maxSpeed)
        {
            sliderPegs[0].transform.position = new Vector2(sliderPegs[0].transform.position.x, pegStartPos[0] + pistonSpeeds[0]);
            pistonSpeeds[0] += Time.deltaTime;
            Mathf.Clamp(pistonSpeeds[0], -maxSpeed, maxSpeed);
        }
        if (Input.GetKey(KeyCode.A) && pistonSpeeds[0] > -maxSpeed)
        {
            sliderPegs[0].transform.position = new Vector2(sliderPegs[0].transform.position.x, pegStartPos[0] + pistonSpeeds[0]);
            pistonSpeeds[0] -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W) && pistonSpeeds[1] < maxSpeed)
        {
            sliderPegs[1].transform.position = new Vector2(sliderPegs[1].transform.position.x, pegStartPos[1] + pistonSpeeds[1]);
            pistonSpeeds[1] += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && pistonSpeeds[1] > -maxSpeed)
        {
            sliderPegs[1].transform.position = new Vector2(sliderPegs[1].transform.position.x, pegStartPos[1] + pistonSpeeds[1]);
            pistonSpeeds[1] -= Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.E) && pistonSpeeds[2] < maxSpeed)
        {
            sliderPegs[2].transform.position = new Vector2(sliderPegs[2].transform.position.x, pegStartPos[2] + pistonSpeeds[2]);
            pistonSpeeds[2] += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && pistonSpeeds[2] > -maxSpeed)
        {
            sliderPegs[2].transform.position = new Vector2(sliderPegs[2].transform.position.x, pegStartPos[2] + pistonSpeeds[2]);
            pistonSpeeds[2] -= Time.deltaTime;
        }
        Mathf.Clamp(pistonSpeeds[0], -maxSpeed, maxSpeed);
        Mathf.Clamp(pistonSpeeds[1], -maxSpeed, maxSpeed);
        Mathf.Clamp(pistonSpeeds[2], -maxSpeed, maxSpeed);
	}
}
