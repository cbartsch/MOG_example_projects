using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{

    public float maxSpeed = 1;
    public float acceleration = 1;
    public float drag = 0.1f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float speed = 0;

    void Update()
    {
        //get horizontal input (arrow button, joystick direction)
        float xInput = Input.GetAxis("Horizontal");

        //increase or decrease speed
        speed += xInput * acceleration * Time.deltaTime;

        //simulate friction
        speed *= (1 - drag);

        //keep speed in bounds
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        //pass speed to animator
        animator.SetFloat("Speed", Mathf.Abs(speed));

        if (speed < -0.01)
        {
            //flip if moving to the left
            spriteRenderer.flipX = true;
        }
        else if (speed > 0.01)
        {
            //unflip if moving to the right
            spriteRenderer.flipX = false;
        }

        //move object in scene
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
}
