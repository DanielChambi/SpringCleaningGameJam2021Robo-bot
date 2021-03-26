using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemyRouted : BHEnemy
{
    int startPoint = 0;
    int targetPoint = 1;

    float routeTimer = 0;

    float shootDelay = 1;
    float shootTimer = 0;

    float speed = 2;

    public Route route;

    protected override void MovementPath()
    {
        FollowRoute();
        
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootDelay)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    protected void FollowRoute()
    {
        routeTimer += Time.deltaTime * speed;

        if (transform.position != route.RoutePointIndex(targetPoint).position)
        {
            transform.position = Vector3.Lerp(route.RoutePointIndex(startPoint).position, route.RoutePointIndex(targetPoint).position, routeTimer);
        }
        else
        {
            routeTimer = 0;
            targetPoint++;
            startPoint++;
            if (targetPoint == route.RouteLength())
            {
                targetPoint = 0;
            }
            if (startPoint == route.RouteLength())
            {
                startPoint = 0;
            }
        }
    }

    protected override void EnemyDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
