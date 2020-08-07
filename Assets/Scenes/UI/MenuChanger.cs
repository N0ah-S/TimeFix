using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChanger : MonoBehaviour {

    [SerializeField]
    GameObject hide, load;
    IEnumerator Fade() {
        for(int i = 0; i < 10; i++) {
            hide.transform.localScale *= 0.75f;
            yield return new WaitForSeconds(0.02f);
        }
        hide.SetActive(false);
        load.transform.localScale = hide.transform.localScale;
        load.SetActive(true);
        for (int i = 0; i < 10; i++) {
            load.transform.localScale /= 0.75f;
            yield return new WaitForSeconds(0.02f);
        }
    }



    public void loadMenu() {
        StartCoroutine(Fade());
    }

}