using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float speed = 2f;

    void Start() {
            
    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                animator.Play("walk");
            gameObject.transform.Translate(Time.deltaTime * speed, 0, 0);
            spriteRenderer.flipX = false;
        } else if (Input.GetKey(KeyCode.A)) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                animator.Play("walk");
            gameObject.transform.Translate(-Time.deltaTime * speed, 0, 0);
            spriteRenderer.flipX = true;
        } else {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                animator.Play("idle");
        }
    }

}