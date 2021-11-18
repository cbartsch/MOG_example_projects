 using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {

    private Animator animator;
    
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        float diff = Input.GetAxis("Vertical") * Time.deltaTime * 0.5f;

        var current = animator.GetFloat("Forward");
        animator.SetFloat("Forward", Mathf.Clamp01(current + diff));
        
        animator.SetBool("OnGround", !Input.GetKey(KeyCode.Space));
	}
}
