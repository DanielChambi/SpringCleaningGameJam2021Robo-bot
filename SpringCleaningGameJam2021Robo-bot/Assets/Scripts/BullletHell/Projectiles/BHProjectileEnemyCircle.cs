using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectileEnemyCircle : BHProjectileEnemy
{
    public new float default_y_speed = 1;

    /*Movement for all projectiles spawned by Circle bullet pattern*/
    protected override void ProjectileMovement()
    {
        
        Vector3 velocity = transform.rotation * Vector3.right;


        transform.Translate(velocity * default_y_speed * Time.deltaTime, Space.World);
    }
}
