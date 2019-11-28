using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotator : MonoBehaviour
{
    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            //directly apply device orientation to game object
            transform.rotation = Input.gyro.attitude;
        }
        else
        {
            Debug.Log("no gyro available");
        }
    }
}
