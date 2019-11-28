using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceExample : MonoBehaviour {

    public float moveSpeed = 10;
    public float jumpForce = 100;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    var body = GetComponent<Rigidbody>();

	    float xInput = Input.GetAxis("Horizontal");
	    float yInput = Input.GetAxis("Vertical");

	    var moveVector = xInput * Vector3.right + 
	                     yInput * Vector3.forward;

        body.AddForce(moveVector * moveSpeed);

	    if (Input.GetButtonDown("Fire1")) {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	    }
	}
}
