using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed = 100, rotateSpeed = 400;
    
    private Rigidbody body;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        body.AddForce(CustomInputManager.GetAxis("Vertical") * 
                      moveSpeed * transform.forward);

        body.AddTorque(CustomInputManager.GetAxis("Horizontal") * 
                       rotateSpeed * transform.up);

        //reset player if fallen down
        if (transform.position.y < -10) {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }
}