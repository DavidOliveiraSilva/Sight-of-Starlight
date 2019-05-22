using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBulletItem : MonoBehaviour
{
    public GameObject specialBullet;
    public int ammo;
    public GameObject release;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<Player>().AddSpecialBullet(specialBullet, ammo);
            AutoDestroy();
        }
    }
    void AutoDestroy() {
        GameObject rl = Instantiate(release);
        rl.transform.position = transform.position;
        Destroy(gameObject);
    }
}
