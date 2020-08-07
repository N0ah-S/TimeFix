using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static GameManager singelton;

    [SerializeField]
    public static int state;

    [SerializeField]
    public List<GameObject> fixing, investigating;

    public static float agression;

    public Text help_text, state_indicator;
    List<ObjectOfInterest> list;
    PlayerScript player;

    void Start() {
        singelton = this;
        list = new List<ObjectOfInterest>();
        state = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        foreach (GameObject gameObject in fixing)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in investigating)
        {
            gameObject.SetActive(true);
        }
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
        foreach(GameObject gameObject in fixing)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in investigating)
        {
            gameObject.SetActive(false);
        }
    }

    public static void addActionToList(ObjectOfInterest objectOfInterest) {
        if (singelton.list.Count == 0) {
            setActionText(objectOfInterest);
        }

        singelton.list.Add(objectOfInterest);
    }

    public static void removeAction(ObjectOfInterest objectOfInterest) {
        if (singelton.list.Count > 0)
        {
            int index = singelton.list.IndexOf(objectOfInterest);
            if (index == 0)
            {
                singelton.help_text.gameObject.SetActive(false);
            }
            if (index != -1) singelton.list.RemoveAt(index);
            setActionText(singelton.list[0]);
        }
    }

    public static void setText(string text) {
        singelton.help_text.gameObject.SetActive(true);
        singelton.help_text.text = text;
    }

    public static void removeText() {
        singelton.help_text.gameObject.SetActive(false);
    }

    public static void changeAgression(float amount)
    {
        agression += amount;
        if(agression > 100)
        {
            agression = 100f;
        }
        GameObject.FindGameObjectWithTag("agression").transform.localScale = new Vector3(agression/100, 1, 0);
    }

    public static void setActionText(ObjectOfInterest objectOfInterest)
    {
        if (singelton.player.getItem() != null && !objectOfInterest.isActive()) setText("Press " + objectOfInterest.key.ToString() + " to place " + singelton.player.getItem().name);
        else if (state == 1) setText("Press " + objectOfInterest.key.ToString() + " to scan " + objectOfInterest.getItemName());
        //else if (objectOfInterest.item == null) setText("Press " + objectOfInterest.key.ToString() + " to take " + objectOfInterest.item.name);
        else if (state == 2) setText("Press " + objectOfInterest.key.ToString() + " to take " + objectOfInterest.getItemName());
    }


}
