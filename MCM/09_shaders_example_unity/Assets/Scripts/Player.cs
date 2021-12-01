using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[ExecuteInEditMode]
public class Player : MonoBehaviour {
    public float moveSpeed = 1;
    private State state1;

    public Renderer lanternRenderer;

    // Start is called before the first frame update
    void Start() {
    }

    enum State {
        Idle,
        WalkHorizontal,
        WalkUp,
        WalkDown
    }

    private State state {
        get { return state1; }
        set {
            if (value != state) {
                var animator = GetComponent<Animator>();
                animator.ResetTrigger(state.ToString());
                animator.SetTrigger(value.ToString());
                state1 = value;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        var movement = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        lanternRenderer.sharedMaterial.SetFloat("_PosX", screenPos.x);
        lanternRenderer.sharedMaterial.SetFloat("_PosY", screenPos.y);

        var angle = Vector2.Angle(movement, Vector2.up);
        if (movement.sqrMagnitude < 0.01) {
            state = State.Idle;
            return;
        }
        else if (angle < 45) {
            state = State.WalkUp;
        }
        else if (angle < 135) {
            state = State.WalkHorizontal;
        }
        else {
            state = State.WalkDown;
        }

        var renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = movement.x < 0;
        lanternRenderer.sharedMaterial.SetFloat("_Angle", angle * Mathf.Sign(movement.x));
    }
}