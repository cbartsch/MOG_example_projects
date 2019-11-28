using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;

    public float jumpForce = 10;
    public float walkForce = 50;
    public float drag = 1;
    
    private bool groundContact = false;

    // Use this for initialization
    void Start()
    {

    }
    
    void FixedUpdate()
    {
        if (groundContact)
        {
            float force = walkForce * Input.GetAxis("Horizontal");
            spriteRenderer.flipX = force < 0;
            body.AddForce(Vector2.right * force);
        }

    }

    private void Update()
    {
        if (groundContact && Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        body.drag = groundContact ? drag : 0;

        animator.SetBool("Grounded", groundContact);
        animator.SetFloat("YVel", body.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            groundContact = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            groundContact = false;
        }
    }
}
