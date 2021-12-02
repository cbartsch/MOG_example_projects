using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;
using Gyroscope = UnityEngine.Gyroscope;

public class CustomInputManager : MonoBehaviour {
    const string horizontalAxisName = "Horizontal";
    const string verticalAxisName = "Vertical";

    private static CustomInputManager instance;

    public enum InputMode {
        UnityInputAxes,
        UnityInputGyro,
        UnityInputTouch,
        CrossPlatformInput,
        InputSystemDevice,
        InputSystemAction
    }

    public InputMode Mode = InputMode.UnityInputAxes;

    //only used for InputMode.Gyro
    public float maxGyroAngle = 90;

    public InputActionReference analogAction;

    void Start() {
        instance = this;
    }

    void Update() {
        Input.gyro.enabled = Mode == InputMode.UnityInputGyro;
    }

    public static float GetAxis(string axisName) {
        return instance.Mode switch {
            InputMode.UnityInputGyro =>    instance.GetGyroAxis(axisName),
            InputMode.UnityInputTouch =>   instance.GetTouchAxis(axisName),
            InputMode.InputSystemDevice => instance.GetInputDeviceAxis(axisName),
            InputMode.InputSystemAction => instance.GetInputActionAxis(axisName),
            InputMode.CrossPlatformInput => CrossPlatformInputManager.GetAxis(axisName),
            _ => Input.GetAxis(axisName)
        };
    }

    public static void SetInputMode(InputMode mode) {
        instance.Mode = mode;
    }

    // get input from touch / mouse position (old input system)
    private float GetTouchAxis(string axisName) {
        if (!Input.GetMouseButton(0)) return 0;

        //get mouse position in range [0, 1]
        var position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var rawValue =
            axisName == horizontalAxisName ? position.x :
            axisName == verticalAxisName ? position.y : 0;

        //convert to range [-1, 1]
        return rawValue * 2 - 1;
    }

    // get input from gyroscope (old input system)
    private float GetGyroAxis(string axisName) {
        var yawPitchRoll = Input.gyro.attitude.eulerAngles;

        //read angle in range [0°, 360°]
        var rawValue = axisName == horizontalAxisName ? yawPitchRoll.x :
            axisName == verticalAxisName ? yawPitchRoll.y : 0;

        //convert to range [-180, 180]
        if (rawValue > 180) rawValue -= 360;
        if (rawValue < -180) rawValue += 360;

        //scale angle to [-1, 1]
        return Mathf.Clamp(rawValue / maxGyroAngle, -1, 1);
    }

    // get input from a specific device manually:
    private float GetInputDeviceAxis(string axisName) {
        var device = Keyboard.current;

        if (axisName == horizontalAxisName) {
            if (device.leftArrowKey.isPressed) return -1;
            if (device.rightArrowKey.isPressed) return 1;
        }
        else if (axisName == verticalAxisName) {
            if (device.downArrowKey.isPressed) return -1;
            if (device.upArrowKey.isPressed) return 1;
        }

        return 0;
    }

    // get input via flexible InputAction:
    private float GetInputActionAxis(string axisName) {
        // read current analog input
        var analogInput = analogAction.action.ReadValue<Vector2>();

        // return axis component
        return
            axisName == horizontalAxisName ? analogInput.x :
            axisName == verticalAxisName ? analogInput.y : 0;
    }
}