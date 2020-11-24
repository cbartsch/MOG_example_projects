using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    public float moveSpeed = 100, rotateSpeed = 400;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        var body = GetComponent<Rigidbody>();

        //use CustomInputManager to control instead of default Input class
        body.AddForce(Input.GetAxis("Vertical") * moveSpeed * transform.forward);

        body.AddTorque(Input.GetAxis("Horizontal") * rotateSpeed * transform.up);

        //reset player if fallen down
        if (transform.position.y < -10) {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }
}