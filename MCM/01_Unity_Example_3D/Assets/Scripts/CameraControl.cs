using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotateSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        transform.position += transform.forward * Time.deltaTime * yInput * moveSpeed;
        transform.Rotate(Vector3.up, xInput * Time.deltaTime * 360 * rotateSpeed, Space.World);
    }
}
