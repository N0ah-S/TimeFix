using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TalkingHeinz : MonoBehaviour {

    [SerializeField]
    Image eyes, mouth, face, dots;

    [SerializeField]
    Text textElement;
    float lastTime = 0;
    float waiting_time = 0.08f;

    public string textToTalk = "";

    Vector3 rootEyePosition;
    Vector3 eyeOffset;

    private void Start() {
        textElement.text = "";
        rootEyePosition = eyes.rectTransform.position;
        eyeOffset = new Vector3();
    }
    public void readInfo(string text) {
        Debug.Log(text);
        textToTalk += text;
    }

    void Update() {
        eyeOffset.x = Mathf.Sin(Time.time) * 0.1f;
        eyes.rectTransform.SetPositionAndRotation(rootEyePosition + eyeOffset, eyes.rectTransform.rotation);
        if(textToTalk.Length != 0 && lastTime + waiting_time < Time.time) {
            lastTime = Time.time;
            string character = textToTalk.Substring(0, 1);
            if (character.Equals("~")) {
                waiting_time = 0.35f;
                mouth.CrossFadeColor(Color.grey, 0.08f, false, false);
                eyes.CrossFadeColor(Color.white, 0.08f, false, false);
            } else if(character.Equals("#")) {
                textElement.text = "";
                eyes.CrossFadeColor(Color.white, 0.08f, false, false);
            } else {
                textElement.text += character;
                waiting_time = 0.08f;
                if(character.Equals(" ")) {
                    mouth.CrossFadeColor(Color.grey, 0.08f, false, false);
                } else {
                    mouth.CrossFadeColor(Color.white, 0.08f, false, false);
                }
                if(!character.Equals(character.ToLower())) {
                    eyes.CrossFadeColor(Color.magenta, 0.8f, false, false);
                } else {
                    eyes.CrossFadeColor(Color.white, 0.08f, false, false);
                }
            }

            textToTalk = textToTalk.Remove(0, 1);
        }
    }

}


//Hallo wie geht's denn so?
