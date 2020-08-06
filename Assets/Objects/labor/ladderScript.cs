using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderScript : MonoBehaviour
{

    List<GameObject> climbing;
    List<int> direction;

    public void Start()
    {
        climbing = new List<GameObject>();
        direction = new List<int>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (climbing.IndexOf(collision.gameObject) == -1)
        {
            if (Input.GetKey(KeyCode.W) && collision.gameObject.CompareTag("Player"))
            {
                climbing.Add(collision.gameObject);
                direction.Add((int)Mathf.Sign(this.transform.position.y - collision.gameObject.transform.position.y));
                collision.gameObject.GetComponent<PlayerScript>().Onladder(true);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < climbing.Count; i++)
        {
            climbing[i].transform.Translate(0, direction[i]*3 * Time.deltaTime, 0);
            GameObject gameObject = climbing[i];
            if ((gameObject.transform.position.y - this.transform.position.y > GetComponent<BoxCollider2D>().bounds.size.y / 1.5 && direction[i] > 0) ||
                (gameObject.transform.position.y - this.transform.position.y < GetComponent<BoxCollider2D>().bounds.size.y / -4 && direction[i] < 0))
            {
                int pos = i;
                climbing.RemoveAt(pos);
                direction.RemoveAt(pos);
                if (gameObject.CompareTag("Player"))
                {
                    UnityEngine.Debug.Log("leftLadder");
                    gameObject.GetComponent<PlayerScript>().Onladder(false);
                }
            }
        }
    }
}
