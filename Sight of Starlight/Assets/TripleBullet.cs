using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBullet : MonoBehaviour
{
    private Bullet b1;
    private Bullet b2;
    private Bullet b3;
    public float maxAngle;
    public float minAngle;
    private float angleOpening;
    private bool dead;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        angleOpening = Random.Range(minAngle, maxAngle);
        b1 = transform.Find("Bullet1").GetComponent<Bullet>();
        b1.exitAngle = angleOpening;
        b2 = transform.Find("Bullet2").GetComponent<Bullet>();
        b2.exitAngle = 0;
        b3 = transform.Find("Bullet3").GetComponent<Bullet>();
        b3.exitAngle = -angleOpening;
    }

    // Update is called once per frame
    void Update()
    {
        if(b1.dead && b2.dead && b3.dead && !dead) {
            AutoDestroy();
        }
        
    }
    public void AutoDestroy() {
        float animationEndTime = b1.animationEndTime;
        Destroy(gameObject, animationEndTime);
        dead = true;
    }
}
