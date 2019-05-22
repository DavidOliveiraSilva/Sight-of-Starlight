using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP;
    private int hp;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int amount) {
        hp -= amount;
        if(hp <= 0) {
            dead = true;
        }
    }
}
