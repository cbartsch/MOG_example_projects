using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    public float moveSpeed = 20;
    public float rotateSpeed = 40;
    public float jumpSpeed = 10;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        var body = GetComponentInChildren<Rigidbody>();
        var particles = GetComponentInChildren<ParticleSystem>();
        var particlesMain = particles.main;

        if (transform.position.y < -3) {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        body.AddForce(body.transform.forward * Input.GetAxis("Vertical") * moveSpeed);
        body.AddTorque(body.transform.up * Input.GetAxis("Horizontal") * rotateSpeed);

        // body.velocity is in global coordinates.
        // check local z-velocity to get current forward speed
        var localVelocity = transform.InverseTransformDirection(body.velocity);
        var forwardSpeed = localVelocity.z;
        
        // scale particles emission with speed
        particlesMain.startLifetime = forwardSpeed / 10;
        particlesMain.startSpeed = forwardSpeed / 2;

        if (Input.GetButtonDown("Jump")) {
            body.AddForce(body.transform.up * jumpSpeed, ForceMode.Impulse);

            particles.Emit(100);
        }
    }
}