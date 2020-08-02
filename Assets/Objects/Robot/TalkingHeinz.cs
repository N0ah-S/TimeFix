using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TalkingHeinz : MonoBehaviour {

    [SerializeField]
    Text textElement;
    float lastTime = 0;
    float waiting_time = 0.1f;

    public string textToTalk = "";


    private void Start() {
        textElement.text = "";
    }
    public void readInfo(string text) {
        Debug.Log(text);
        textToTalk += text;
    }

    void Update() {
        if(textToTalk.Length != 0 && lastTime + waiting_time < Time.time) {
            lastTime = Time.time;
            string character = textToTalk.Substring(0, 1);
            if (character.Equals("~")) {
                waiting_time = 0.25f;
            } else if(character.Equals("#")) {
                textElement.text = "";
            } else {
                textElement.text += character;
                waiting_time = 0.1f;
            }

            textToTalk = textToTalk.Remove(0, 1);
        }
    }

}


//Hallo wie geht's denn so?
