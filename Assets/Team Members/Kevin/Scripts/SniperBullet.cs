using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : BulletBase
{
    public override void OnHit(Collision2D col)
    {
        EnemyBody enemy = col.gameObject.GetComponent<EnemyBody>();
        if (enemy != null)
        {
            enemy.health.HealthLevel -= bulletDmg;
        }
    }
}
