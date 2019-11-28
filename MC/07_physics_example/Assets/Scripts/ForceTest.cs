using UnityEngine;
using System.Collections;

public class ForceTest : MonoBehaviour
{

    public float moveForce = 10f;
    public float jumpForce = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // FixedUpdate is called 60 times per second
    void FixedUpdate()
    {
        var body = GetComponent<Rigidbody>();
        
        body.AddForce(new Vector3(
            Input.GetAxis("Horizontal") * moveForce, //x axis = left/right
            0,
            Input.GetAxis("Vertical") * moveForce)); //z axis = forward/backward

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //simulate impulse of force for 1s
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


}
