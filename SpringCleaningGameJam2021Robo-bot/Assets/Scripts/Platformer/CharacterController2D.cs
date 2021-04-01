using UnityEngine;




public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public Transform[] groundChecks;

    public float max_x_speed = 10;
    public float max_y_speed = 10;

    public float x_acceleration = 100;

    public float jump_speed = 5;

    float x_axis;
    float y_axis;

    bool isGrounded = false;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        x_axis = Input.GetAxis("Horizontal");
        y_axis = Input.GetAxis("Vertical");

        isGrounded = GroundCheck();


        if (x_axis != 0)
        {
            float x_speed = rigidbody.velocity.x;
            x_speed += x_acceleration * x_axis;

            rigidbody.velocity = new Vector2(Mathf.Clamp(x_speed, -max_x_speed, max_x_speed), rigidbody.velocity.y);
        } else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_speed);
        }

    }

    bool GroundCheck()
    {
        bool grounded = false;
        for (int i = 0; i < groundChecks.Length; i++)
        {
            if(Physics2D.Linecast(transform.position, groundChecks[i].position, 1 << LayerMask.NameToLayer("Obstacle")))
            {
                grounded = true;
            }
        }
        return grounded;
    }
}