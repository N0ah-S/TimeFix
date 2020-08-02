using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static GameManager singelton;

    [SerializeField]
    public static int state;

    public Text help_text;
    List<ObjectOfInterest> list;

    void Start() {
        singelton = this;
        list = new List<ObjectOfInterest>();
        state = 1;
    }

    void Update() {
        if(state == 1 && list.Count != 0 && Input.GetKeyDown(list[0].key)) {
            list[0].scan();
            help_text.gameObject.SetActive(false);
            list.RemoveAt(0);
        } else if (state == 2 && list.Count != 0 && Input.GetKeyDown(list[0].key)) {
            list[0].use();
            help_text.gameObject.SetActive(false);
            list.RemoveAt(0);
        }
    }

    public static void addActionToList(ObjectOfInterest objectOfInterest) {
        if (singelton.list.Count == 0) {
            if(state == 1) setText("Press " + objectOfInterest.key.ToString() + " to scan " + objectOfInterest.oname);
            else if(state == 2) setText("Press " + objectOfInterest.key.ToString() + " to take " + objectOfInterest.oname);
        }

        singelton.list.Add(objectOfInterest);
    }

    public static void removeAction(ObjectOfInterest objectOfInterest) {
        int index = singelton.list.IndexOf(objectOfInterest);
        if (index == 0) {
            singelton.help_text.gameObject.SetActive(false);
        }
        if(index != -1) singelton.list.RemoveAt(index);
    }

    public static void setText(string text) {
        singelton.help_text.gameObject.SetActive(true);
        singelton.help_text.text = text;
    }
}
