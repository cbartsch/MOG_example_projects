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

        var intensity = Vector3.Scale(body.velocity, new Vector3(1, 0, 1)).magnitude;
        particlesMain.startLifetime = intensity / 20;
        particlesMain.startSpeed = intensity;

        if (Input.GetButtonDown("Jump")) {
            body.AddForce(body.transform.up * jumpSpeed, ForceMode.Impulse);

            particles.Emit(100);
        }
    }
}