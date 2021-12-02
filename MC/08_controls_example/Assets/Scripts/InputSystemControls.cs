using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemControls : MonoBehaviour {
    
    public float moveSpeed = 100, rotateSpeed = 400;

    public Rigidbody body;
    
    private Vector2 input;
    
    public void ControlAnalog(InputAction.CallbackContext context) {
        input = context.action.ReadValue<Vector2>();
    }
    
    public void ControlFire(InputAction.CallbackContext context) {
        Debug.Log("Control fire: " + context.ReadValueAsButton(), this);
    }

    private void Update() {
        body.AddForce(input.y * moveSpeed * transform.forward);
        body.AddTorque(input.x * rotateSpeed * transform.up);
    }

}