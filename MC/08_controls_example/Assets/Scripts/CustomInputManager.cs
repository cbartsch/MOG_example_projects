using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class CustomInputManager : MonoBehaviour
{
    private static CustomInputManager instance;

    public enum InputMode
    {
        Default, Gyro, Touch, CrossPlatformInput
    }

    public InputMode Mode = InputMode.Default;

    //only used for InputMode.Gyro
    public float maxGyroAngle = 90;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        Input.gyro.enabled = Mode == InputMode.Gyro;
    }

    public static float GetAxis(string axisName)
    {
        switch (instance.Mode)
        {
            case InputMode.Gyro: return instance.GetGyroAxis(axisName);
            case InputMode.Touch: return instance.GetTouchAxis(axisName);
            case InputMode.CrossPlatformInput:
                return CrossPlatformInputManager.GetAxis(axisName);
            default: return Input.GetAxis(axisName);
        }
    }

    public static void SetInputMode(InputMode mode)
    {
        instance.Mode = mode;
    }

    private float GetTouchAxis(string axisName)
    {
        if (!Input.GetMouseButton(0)) return 0;

        //get mouse position in range [0, 1]
        var position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var rawValue = axisName == "Horizontal" ? position.x :
                       axisName == "Vertical" ? position.y : 0;

        //convert to range [-1, 1]
        return rawValue * 2 - 1;
    }

    private float GetGyroAxis(string axisName)
    {
        var yawPitchRoll = Input.gyro.attitude.eulerAngles;

        //read angle in range [0°, 360°]
        var rawValue = axisName == "Horizontal" ? yawPitchRoll.x :
                       axisName == "Vertical" ? yawPitchRoll.y : 0;

        //convert to range [-180, 180]
        if (rawValue > 180) rawValue -= 360;
        if (rawValue < -180) rawValue += 360;

        //scale angle to [-1, 1]
        return Mathf.Clamp(rawValue / maxGyroAngle, -1, 1);
    }
}
