using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemyRouted : BHEnemy
{
    //starting point index in route
    int startPoint = 0;
    //next point index in route
    int targetPoint = 1;

    //timer showing how far in to route section enemy is
    float routeTimer = 0;

    float shootDelay = 1;
    float shootTimer = 0;

    float speed = 2;

    //reference to Route game object to follow
    public Route route;

    protected override void MovementPath()
    {
        //movement behaviour
        FollowRoute();
        
        //shooting behaviour
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootDelay)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    /*Update position and point target within specified route
     * 
     */
    protected void FollowRoute()
    {
        routeTimer += Time.deltaTime * speed;

        if (transform.position != route.RoutePointIndex(targetPoint).position)
        {
            //place enemy in corresponding position
            transform.position = Vector3.Lerp(route.RoutePointIndex(startPoint).position, route.RoutePointIndex(targetPoint).position, routeTimer);
        }
        else
        {
            //update target and starting point indexes
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
        //destroy parent class containing enemy and route
        Transform parent = transform.parent;
        transform.parent = null;
        Destroy(parent.gameObject);

        base.EnemyDestroy();
    }
}
