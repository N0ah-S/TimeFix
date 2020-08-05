using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static GameManager singelton;

    [SerializeField]
    public static int state;

    public Text help_text, state_indicator;
    List<ObjectOfInterest> list;
    PlayerScript player;

    void Start() {
        singelton = this;
        list = new List<ObjectOfInterest>();
        state = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) activateFixMode();

        if (list.Count < 1) return;
        if(state == 1 && list.Count != 0 && Input.GetKeyDown(list[0].key)) {
            list[0].scan();
            removeText();
            list.RemoveAt(0);
        } else if (Input.GetKeyDown(list[0].key)) {
            Item switchItem = player.getItem();
            player.setItem(list[0].item);
            list[0].item = switchItem;
            if(list[0].item == null) {
                list[0].deactivate();
            } else {
                list[0].activate();   
            }
            
            removeText();
            list.RemoveAt(0);
        } else if (state == 2 && list.Count != 0 && Input.GetKeyDown(list[0].key)) {
            list[0].use();
            if(list.Count != 0) list.RemoveAt(0); ///Possibly creating bugs
            removeText();
        }

    }

    [ContextMenu("Go into Fix Mode")]
    public void activateFixMode() {
        state_indicator.text = "Fix Mode";
        state = 2;
    }

    public static void addActionToList(ObjectOfInterest objectOfInterest) {
        if (singelton.list.Count == 0) {
            if (singelton.player.getItem() != null && !objectOfInterest.isActive()) setText("Press " + objectOfInterest.key.ToString() + " to place " + singelton.player.getItem().name);
            else if (state == 1) setText("Press " + objectOfInterest.key.ToString() + " to scan " + objectOfInterest.getItemName());
            //else if (objectOfInterest.item == null) setText("Press " + objectOfInterest.key.ToString() + " to take " + objectOfInterest.item.name);
            else if (state == 2) setText("Press " + objectOfInterest.key.ToString() + " to take " + objectOfInterest.getItemName());
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

    public static void removeText() {
        singelton.help_text.gameObject.SetActive(false);
    }


}
