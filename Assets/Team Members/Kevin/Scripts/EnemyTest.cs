using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float hp;
    public Rigidbody2D rb;
    public float knockBack;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GotHit(float dmg)
    {
        hp -= dmg;
        rb.AddForce(transform.right * knockBack);
        if (hp <= 0)
        {
            EnemyKilled();
        }
    }

    private void EnemyKilled()
    {
        Destroy(this.GameObject());
    }
    
}
