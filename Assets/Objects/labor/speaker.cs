using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speaker : MonoBehaviour
{
    bool triggered = false;
    [SerializeField]
    public bool neuTriggerbar;
    [TextArea(3, 10)]
    public string text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered) {
            GameObject.FindGameObjectWithTag("Heinz").GetComponent<TalkingHeinz>().readInfo(text);
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (neuTriggerbar && collision.gameObject.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
