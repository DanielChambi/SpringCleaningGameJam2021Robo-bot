using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectile : MonoBehaviour
{
    /*Base class for all projectiles in Bullet Hell*/
    public float default_y_speed = 7;
    public float damage = 1;    //damage points the projectile does on contact

    float y_bounds_buffer = 1.5f;
    float x_bounds_buffer = 1.5f;
    void Start()
    {
        
    }


    void Update()
    {
        ProjectileMovement();

        //destroy if out of bounds
        Bounds bounds = BHPlaySpaceBounds.Bounds();
        float max_y = bounds.center.y + bounds.extents.y * y_bounds_buffer;
        float min_y = bounds.center.y - bounds.extents.y * y_bounds_buffer;
        float max_x = bounds.center.x + bounds.extents.x * x_bounds_buffer;
        float min_x = bounds.center.x - bounds.extents.x * x_bounds_buffer;
        if (transform.position.y > max_y || transform.position.y < min_y || transform.position.x > max_x || transform.position.x < min_x)
        {
            Destroy(gameObject);
        }

    }

    /*Default movement pattern for projectile. Function to be rewritten in child projectile clases.*/
    protected virtual void ProjectileMovement()
    {
        transform.Translate(Vector3.up * default_y_speed * Time.deltaTime, Space.World);
    }

    /*Get damage points this projectile deals*/
    public float Damage()
    {
        return damage;
    }
}
