using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOfInterest : MonoBehaviour {

    public string oname;

    [TextArea(3, 10)]
    public string scanDescription;
    [SerializeField]
    bool scanable, useable;

    public KeyCode key;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (GameManager.state == 1 && scanable) {
            key = KeyCode.E;
            GameManager.addActionToList(this);
        }
        if (GameManager.state == 2 && useable) {
            key = KeyCode.E;
            GameManager.addActionToList(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        GameManager.removeAction(this);
    }

    internal void scan() {
        GameObject.FindGameObjectWithTag("Heinz").GetComponent<TalkingHeinz>().readInfo(scanDescription);
    }

    internal void use() {
        gameObject.SetActive(false);
    }
}