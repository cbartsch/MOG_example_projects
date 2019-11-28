using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Rotator : MonoBehaviour {

    //default = up: rotate horizontally
    public Vector3 axis = Vector3.up;

    //in degrees / second
    public float speed = 360;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(axis, Time.deltaTime * speed, Space.World);
	}
}
