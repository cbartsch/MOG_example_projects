using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceExample : MonoBehaviour {

    public float moveSpeed = 10;
    public float jumpForce = 100;
    private Rigidbody body;

    // Use this for initialization
	void Start() 
	{
		body = GetComponent<Rigidbody>();
	}
	
	// FixedUpdate is called once per physics update step
	void FixedUpdate()
	{
		float xInput = Input.GetAxis("Horizontal");
	    float yInput = Input.GetAxis("Vertical");

	    // Move: add horizontal force
	    var moveVector = xInput * Vector3.right + 
	                     yInput * Vector3.forward;

	    body.AddForce(moveVector * moveSpeed);
	}

	// Update is called once per frame
	private void Update() 
	{
		// Jump: add vertical impulse
		if (Input.GetButtonDown("Jump"))
		{
			body.AddForce(Vector3.up * jumpForce, 
				ForceMode.Impulse);
		}
	}
}
