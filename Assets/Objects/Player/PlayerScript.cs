using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 2f;

    void Start() {
            
    }

    void Update() {
        if(Input.GetKey(KeyCode.D)) {
            gameObject.transform.Translate(Time.deltaTime * speed, 0, 0);
        } else if (Input.GetKey(KeyCode.A)) {
            gameObject.transform.Translate(-Time.deltaTime * speed, 0, 0);
        }
    }

}