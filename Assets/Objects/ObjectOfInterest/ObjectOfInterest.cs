using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOfInterest : MonoBehaviour {

    [SerializeField]
    bool scanable, useable, needsToBeTarget;
    public bool unlocked, active;

    [SerializeField]
    public Item item;

    [SerializeField]
    public KeyCode key;

    private void Start() {
        item.firstLinked = this.gameObject;
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (active) {
            if (collision.gameObject.CompareTag("Player")) {
                if (GameManager.state == 1 && scanable) {
                    key = KeyCode.E;
                    GameManager.addActionToList(this);
                }
                if (GameManager.state == 2 && useable) {
                    key = KeyCode.E;
                    GameManager.addActionToList(this);
                }
            } else if (collision.gameObject.CompareTag("NPC") && (unlocked || !needsToBeTarget)) {
                collision.gameObject.GetComponent<KIBehavior>().collision(this);
                if (item.typeOfDeadly == TypeOfDeadly.EXPLOSIVE || item.typeOfDeadly == TypeOfDeadly.EVENT) item.activate(this.gameObject);
            }
        } else {
            if (GameManager.state == 2 && useable) {
                key = KeyCode.E;
                GameManager.addActionToList(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GameManager.removeAction(this);
        }
    }

    internal void scan() {
        GameObject.FindGameObjectWithTag("Heinz").GetComponent<TalkingHeinz>().readInfo(item.description);
    }

    internal void use() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().setItem(item);
        GetComponent<SpriteRenderer>().enabled = false;
        item = null;
    }

    internal string getItemName() {
        return item == null ? "NOITEM" : item.name;
    }

    public void interact(KIBehavior KI) {
        if (active) {
            unlocked = true;
            if (item.typeOfDeadly == TypeOfDeadly.EXPLOSIVE || item.typeOfDeadly == TypeOfDeadly.EVENT) item.activate(this.gameObject);
            if (item.typeOfDeadly == TypeOfDeadly.POISONOUS) KI.collision(this);
        }
    }
    public void activate() {
        active = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
    public void deactivate() {
        active = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool isActive() {
        return active;
    }

}