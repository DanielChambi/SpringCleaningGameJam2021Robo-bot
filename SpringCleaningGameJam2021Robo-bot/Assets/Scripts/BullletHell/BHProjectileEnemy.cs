using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectileEnemy : BHProjectile
{
    /*Inherits from base projectile. Base class for all enemy projectiles*/
    protected override void ProjectileMovement()
    {
        transform.Translate(Vector3.down * default_y_speed * Time.deltaTime, Space.World);
    }
}
