using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class KIBehavior : MonoBehaviour {


    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField]
    public GameObject target;

    public float speed = 1f;
    public bool ladder = false;

    public void Update() {
        if (ladder){

        }else if(target != null){
            float dist = target.transform.position.x - this.transform.position.x;
            if (Mathf.Abs(dist) > 0.3){
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Sign(dist) * speed, 0, 0);
                spriteRenderer.flipX = dist < 0;
            }else{
                if (target.CompareTag("ladder"))
                {
                    target.GetComponent<ladderScript>().use(this.gameObject);
                }
                else
                {
                    ObjectOfInterest interest = target.GetComponent<ObjectOfInterest>();
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
                target = entity.GetComponent<ObjectOfInterest>().item.redirect;
            }
        }
        else
        {
            GameManager.changeAgression(10 * Time.deltaTime);
        }
    }

    public void Onladder(bool yes, ladderScript script)
    {
        ladder = yes;
        GetComponent<BoxCollider2D>().enabled = !yes;
        if (yes) GetComponent<Rigidbody2D>().Sleep();
        else
        {
            GetComponent<Rigidbody2D>().WakeUp();
            target = script.redirect;
        }
    }
}