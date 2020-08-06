using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneUI : MonoBehaviour {

    public string scene;

    public void load() {
        SceneManager.LoadSceneAsync(scene);
    }

}