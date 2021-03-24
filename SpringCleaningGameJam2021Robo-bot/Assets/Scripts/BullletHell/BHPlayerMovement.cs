using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHPlayerMovement : MonoBehaviour
{
    public float max_speed = 5;
    public float acceleration = 1;
    
    Vector2 velocity;

    float x_axis;
    float y_axis;

    void Start()
    {
        velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        x_axis = Input.GetAxis("Horizontal");
        y_axis = Input.GetAxis("Vertical");

        if(x_axis != 0)
        {
            velocity.x = velocity.x + acceleration * x_axis;
        } else
        {
            velocity.x = 0;
        }
        
        if(y_axis != 0)
        {
            velocity.y = velocity.y + acceleration * y_axis;
        } else
        {
            velocity.y = 0;
        }
        


        velocity.x = Mathf.Clamp(velocity.x, -max_speed, max_speed);
        velocity.y = Mathf.Clamp(velocity.y, -max_speed, max_speed);

        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
