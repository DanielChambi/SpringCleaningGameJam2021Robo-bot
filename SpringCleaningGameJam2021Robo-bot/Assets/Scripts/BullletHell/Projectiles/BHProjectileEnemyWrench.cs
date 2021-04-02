using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectileEnemyWrench : BHProjectileEnemy
{
    float y_speed = 5;
    float x_speed = 3;
    float rotate_speed = 5;

    float path_width = 3;

    float y_axis;

    float time = 0;


    private void Start()
    {
        y_axis = transform.position.y;
    }

    protected override void ProjectileMovement()
    {
        time += Time.deltaTime;

        float x_pos = y_axis + Mathf.Sin(time * x_speed) * path_width;

        transform.position = new Vector3(x_pos, transform.position.y, transform.position.z);


        transform.Translate(Vector2.down * y_speed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.forward, rotate_speed);
    }
}
