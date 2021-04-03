using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectileEnemyCircle : BHProjectileEnemy
{
    float speed = 1;

    /*Movement for all projectiles spawned by Circle bullet pattern*/
    protected override void ProjectileMovement()
    {
        
        Vector3 velocity = transform.rotation * Vector3.right;


        transform.Translate(velocity * speed * Time.deltaTime, Space.World);
    }
}
