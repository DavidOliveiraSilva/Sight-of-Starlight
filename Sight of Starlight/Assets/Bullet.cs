using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    private Rigidbody2D rb;
    public float expireTime;
    public float animationEndTime;
    public GameObject explosion;
    private ParticleSystem ps;
    private ParticleSystem pstail;
    private CircleCollider2D cc;
    //radianos:
    public float exitAngle;
    //verificar se a variacao `dead` é true
    //se sim, esse Bullet não pode dar dano em ninguém
    public bool dead;
    public string ignoreTag;
    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed*Mathf.Cos(exitAngle), speed * Mathf.Sin(exitAngle));
        dead = false;
        ps = GetComponent<ParticleSystem>();
        pstail = transform.Find("Tail").GetComponent<ParticleSystem>();
        cc = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        expireTime -= Time.deltaTime;
        if(expireTime <= 0 && !dead) {
            AutoDestroy();
        }
    }
    public void AutoDestroy() {
        rb.velocity = new Vector2(0, 0);
        ps.Stop();
        pstail.Stop();
        cc.enabled = false;
        GameObject exp = Instantiate(explosion);
        exp.transform.position = transform.position;
        Destroy(gameObject, animationEndTime);
        dead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == ignoreTag) {
            return;
        }
        if (dead) {
            return;
        }
        if(collision.transform.tag == "Rock") {
            AutoDestroy();
        }
        if (collision.transform.tag == "Enemy") {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            AutoDestroy();
        }
    }
    public void Die() {

    }
}
