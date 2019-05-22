using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float dashDuration;
    public float dashSpeedMultiplier;
    private float dashing;
    public GameObject bullet;
    public GameObject specialBullet;
    public float bulletDelay;
    private float lastBullet;
    public float specialBulletDelay;
    private float lastSpecialBullet;
    public float dashDelay;
    private float lastDash;
    public int specialBulletAmmo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastBullet = Time.time;
        lastSpecialBullet = Time.time;
        lastDash = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        
        if((Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) && dashing <= 0 ) {
            float angle = Mathf.Atan2(ver, hor);
            rb.velocity = new Vector2(speed * Time.deltaTime * Mathf.Cos(angle)* Mathf.Abs(hor), 
                speed * Time.deltaTime * Mathf.Sin(angle)* Mathf.Abs(ver));
        } else {
            rb.velocity = new Vector2(0, 0);
        }
        if(dashing > 0) {
            dashing -= Time.deltaTime;
            rb.velocity = new Vector2(hor* dashSpeedMultiplier* speed * Time.deltaTime, ver * dashSpeedMultiplier * speed * Time.deltaTime);
        }
        if (Time.time - lastBullet > bulletDelay) {
            if (Input.GetButtonDown("Fire")) {
                Shot();
                lastBullet = Time.time;
            }
            
        }
        if (Time.time - lastSpecialBullet > specialBulletDelay) {
            if (specialBullet != null && Input.GetButtonDown("Fire2") && specialBulletAmmo > 0) {
                ShotSpecial();
                lastSpecialBullet = Time.time;
                specialBulletAmmo--;
            }
        }
        if (Time.time - lastDash > dashDelay) {
            if (Input.GetButtonDown("Dash")) {
                Dash();
                lastDash = Time.time;
            }
        }
        
        
        
    }
    void Dash() {
        dashing = dashDuration;
    }
    void Shot() {
        GameObject blt = Instantiate(bullet);
        blt.transform.position = transform.Find("BulletExitPoint").position;
    }
    void ShotSpecial() {
        GameObject blt = Instantiate(specialBullet);
        blt.transform.position = transform.Find("BulletExitPoint").position;
    }
    public void AddSpecialBullet(GameObject sb, int ammo) {
        specialBullet = sb;
        specialBulletDelay = sb.GetComponent<SpecialBullet>().delay;
        specialBulletAmmo = ammo;
    }
}
