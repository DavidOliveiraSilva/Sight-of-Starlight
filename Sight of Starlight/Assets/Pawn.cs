using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private EnemyHealth health;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    public float speed;
    
    public GameObject[] drops;
    [Range(0.0f, 1.0f)]
    public float chance;
    public float animationEndTime;

    public GameObject bullet;
    public float shotDelay;
    private float lastShot;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.dead) {
            AutoDestroy();
        }
        if(Time.time - lastShot > shotDelay) {
            Shot();
            lastShot = Time.time;
        }
    }
    public void AutoDestroy() {
        if (!dead) {
            if (Random.Range(0.0f, 1.0f) < chance && drops.Length > 0) {
                GameObject drop = Instantiate(drops[Random.Range(0, drops.Length - 1)]);
                drop.transform.position = transform.position;
            }
            Destroy(gameObject, animationEndTime);
            dead = true;
        }
    }
    void Shot() {
        GameObject blt = Instantiate(bullet);
        blt.transform.position = transform.Find("BulletExitPoint").transform.position;
    }
}
