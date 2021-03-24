using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectile : MonoBehaviour
{
    /*Base class for all projectiles in Bullet Hell*/
    public float default_y_speed = 7;

    float y_bounds_buffer = 1.5f;
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
        if (transform.position.y > max_y || transform.position.y < min_y)
        {
            Destroy(gameObject);
        }
    }

    /*Default movement pattern for projectile*/
    void ProjectileMovement()
    {
        transform.Translate(Vector3.up * default_y_speed * Time.deltaTime, Space.World);
    }
}
