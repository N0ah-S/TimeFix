using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KIBehavior : MonoBehaviour {


    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField]
    public GameObject target;

    public float speed = 1f;

    public void Update() {
        float dist = target.transform.position.x - this.transform.position.x;
        if (Mathf.Abs(dist) > 0.3) {
            gameObject.transform.Translate(Time.deltaTime * Mathf.Sign(dist) * speed, 0, 0);
            spriteRenderer.flipX = dist < 0;
        } else {
            ObjectOfInterest interest = target.GetComponent<ObjectOfInterest>();
            if(interest != null) {
                interest.enabled = false;
                interest.interact(this);
                collision(interest);
            }
        }
    }

    public void collision(ObjectOfInterest entity) {
        if (entity.typeOfDeadly == TypeOfDeadly.EXPLOSIVE || entity.typeOfDeadly == TypeOfDeadly.POISONOUS) {
            gameObject.SetActive(false);
        }else if(entity.typeOfDeadly == TypeOfDeadly.REDIRECT) {
            target = entity.redirect;
        }
    }
}