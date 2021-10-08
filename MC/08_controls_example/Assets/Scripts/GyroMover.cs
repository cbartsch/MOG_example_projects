using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMover : MonoBehaviour {

    public float moveSpeed = 50;

	void Update () {
        Input.gyro.enabled = true;
        var yawPitchRoll = Input.gyro.attitude.eulerAngles;

        //read angle in degrees
        var rawValueY = yawPitchRoll.y;

        //convert to range [-180, 180]
        if (rawValueY > 180) rawValueY -= 360;
        if (rawValueY < -180) rawValueY += 360;

        //scale angle to [-1, 1]
        rawValueY = Mathf.Clamp(rawValueY / 90, -1, 1);

        //move body
        GetComponent<Rigidbody>().AddForce(
            transform.forward * rawValueY * moveSpeed);
    }
}
