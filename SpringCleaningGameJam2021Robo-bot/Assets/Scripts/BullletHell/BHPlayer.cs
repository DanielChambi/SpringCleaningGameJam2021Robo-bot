using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BHPlayer : MonoBehaviour
{
    public float max_speed = 5;
    public float acceleration = 1;

    public float hp = 20;

    public float shootDelay = 10f; //delay between shots in seconds
    float shootTimer;
    
    Vector2 velocity;

    float x_axis;
    float y_axis;

    public GameObject currentProjectile;

    void Start()
    {
        velocity = Vector2.zero;
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
        ManageShooting();
    }

    void ManageMovement()
    {
        //update velocity
        x_axis = Input.GetAxis("Horizontal");
        y_axis = Input.GetAxis("Vertical");

        if (x_axis != 0)
        {
            velocity.x = velocity.x + acceleration * x_axis;
        }
        else
        {
            velocity.x = 0;
        }

        if (y_axis != 0)
        {
            velocity.y = velocity.y + acceleration * y_axis;
        }
        else
        {
            velocity.y = 0;
        }


        //apply velocity
        velocity.x = Mathf.Clamp(velocity.x, -max_speed, max_speed);
        velocity.y = Mathf.Clamp(velocity.y, -max_speed, max_speed);

        transform.Translate(velocity * Time.deltaTime, Space.World);

        //limit movements to play space bounds
        Bounds bounds = BHPlaySpaceBounds.Bounds();
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, bounds.min.x, bounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.min.y, bounds.max.y);
        transform.position = newPosition;
    }

    void ManageShooting()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer < 0) shootTimer = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            if(shootTimer == 0)
            {
                GameObject.Instantiate(currentProjectile, transform.position, Quaternion.identity);
                shootTimer = shootDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "EnemyProjectile")
        {
            BHProjectile projectile = collision.transform.GetComponent<BHProjectile>();
            ReceiveDamage(projectile.Damage());
            Destroy(collision.transform.gameObject);
        }
    }

    void ReceiveDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            PlayerDestroy();
        }
    }

    void PlayerDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path, LoadSceneMode.Single);
    }
}

