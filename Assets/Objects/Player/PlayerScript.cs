using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float speed = 2f;

    public Image inventoryDisplay;
    Item inventory;

    void Start() {
        inventoryDisplay.color = Color.clear; //So you can't see the reference Image ingame
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

}