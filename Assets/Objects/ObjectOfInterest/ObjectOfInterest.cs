using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOfInterest : MonoBehaviour {

    [SerializeField]
    bool scanable, useable, needsToBeTarget;
    bool unlocked, active;
    public TypeOfDeadly typeOfDeadly;

    [SerializeField]
    public Item item;

    [SerializeField]
    public GameObject redirect;

    public GameObject action;
    public KeyCode key;

    private void Start() {
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
                if (typeOfDeadly == TypeOfDeadly.EXPLOSIVE || typeOfDeadly == TypeOfDeadly.EVENT) action.SetActive(true);
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
            if (typeOfDeadly == TypeOfDeadly.EXPLOSIVE || typeOfDeadly == TypeOfDeadly.EVENT) action.SetActive(true);
            if (typeOfDeadly == TypeOfDeadly.POISONOUS) KI.collision(this);
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