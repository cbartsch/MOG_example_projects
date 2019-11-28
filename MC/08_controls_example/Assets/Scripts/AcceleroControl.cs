using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleroControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    Input.gyro.enabled = true;

	    var movement = Input.gyro.userAcceleration;

        Debug.Log("acceleration: " + movement);

	    var body = GetComponent<Rigidbody>();

        body.AddForce(movement * 50);
	}
}
