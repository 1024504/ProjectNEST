using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float hp;

    public void GotHit(float dmg)
    {
        hp -= dmg;
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
