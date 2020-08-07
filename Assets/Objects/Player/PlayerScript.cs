using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float speed = 2f;
    public bool ladder = false;

    public Image inventoryDisplay;
    Item inventory;

    void Start() {
        inventoryDisplay.color = Color.clear; //So you can't see the reference Image ingame
    }

    void Update() {
        if (ladder) {
            this.GetComponent<Rigidbody2D>().Sleep();
        } else {
            if (Input.GetKey(KeyCode.D)) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    animator.Play("walk");
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);
                //gameObject.transform.Translate(Time.deltaTime * speed, 0, 0);
                spriteRenderer.flipX = false;
            } else if (Input.GetKey(KeyCode.A)) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    animator.Play("walk");
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0, 0);
                //gameObject.transform.Translate(-Time.deltaTime * speed, 0, 0);
                spriteRenderer.flipX = true;
            } else {
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                    animator.Play("idle");
            }
        }
    }

    public void setItem(Item item) {
        if (item == null) {
            removeItem();
        } else {
            inventoryDisplay.sprite = item.sprite;
            inventoryDisplay.color = Color.white;
            inventory = item;
        }
    }

    public Item getItem() {
        return inventory;
    }

    public void removeItem() {
        inventoryDisplay.color = Color.clear;
        inventory = null;
    }

    public void Onladder(bool yes) {
        this.ladder = yes;

        if (yes) {
            this.GetComponent<Rigidbody2D>().Sleep();
            animator.Play("climb");
        } else {
            this.GetComponent<Rigidbody2D>().IsAwake();
            animator.Play("idle");
        }

        this.GetComponent<CapsuleCollider2D>().enabled = !yes;
    }

}