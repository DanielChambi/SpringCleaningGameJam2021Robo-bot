using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectileEnemyGarbage : BHProjectileEnemy
{ 
    Vector2 direction;

    float speed = 5;

    void Start()
    {
        Bounds bounds = BHPlaySpaceBounds.Bounds();
        float y_target = bounds.min.y;
        float x_target = Random.Range( bounds.min.x, bounds.max.x );

        Vector3 targetPoint = new Vector3(x_target, y_target, 0);

        Vector3 movement = targetPoint - transform.position;

        direction = movement.normalized;
    }

    protected override void ProjectileMovement()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

}
