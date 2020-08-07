using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class KIBehavior : MonoBehaviour {


    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField]
    public List<GameObject> target;

    public float speed = 1f;
    public bool ladder = false;

    public void Update() {
        if (ladder){

        }else if(target.Count >0){
            float dist = target[0].transform.position.x - this.transform.position.x;
            if (Mathf.Abs(dist) > 0.3){
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Sign(dist) * speed, 0, 0);
                spriteRenderer.flipX = dist < 0;
            }else{
                if (target[0].CompareTag("ladder"))
                {
                    target[0].GetComponent<ladderScript>().use(this.gameObject);
                }
                else
                {
                    ObjectOfInterest interest = target[0].GetComponent<ObjectOfInterest>();
                    if (interest != null)
                    {
                        interest.enabled = false;
                        interest.interact(this);
                        collision(interest);
                    }
                }
            }
        }
        
    }

    public void collision(ObjectOfInterest entity) {
        if (entity.active && entity.item != null)
        {
            if (entity.item.typeOfDeadly == TypeOfDeadly.EXPLOSIVE || entity.item.typeOfDeadly == TypeOfDeadly.POISONOUS)
            {
                gameObject.SetActive(false);
            }
            else if (entity.item.typeOfDeadly == TypeOfDeadly.REDIRECT)
            {
                if(target.Count>0)target.RemoveAt(0);
            }
        }
        else
        {
            GameManager.changeAgression(10 * Time.deltaTime);
        }
    }

    public void Onladder(bool yes)
    {
        ladder = yes;
        GetComponent<BoxCollider2D>().enabled = !yes;
        if (yes) GetComponent<Rigidbody2D>().Sleep();
        else
        {
            GetComponent<Rigidbody2D>().WakeUp();
            if (target.Count > 0)
            {
                target.RemoveAt(0);
            }
        }
    }
}