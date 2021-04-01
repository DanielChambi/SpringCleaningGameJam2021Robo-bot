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
    
    //movement vector applied on update
    Vector2 velocity;

    //input directions
    float x_axis;
    float y_axis;

    //reference to current projectile to spawn when attacking
    public GameObject currentProjectile;

    void Start()
    {
        velocity = Vector2.zero;
        shootTimer = 0;
    }

    void Update()
    {
        ManageMovement();
        ManageShooting();
    }

    /*Handle behaviour for movement
     * 
     */
    void ManageMovement()
    {
        //update input axis
        x_axis = Input.GetAxis("Horizontal");
        y_axis = Input.GetAxis("Vertical");

        //updtae velocity based on input
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


        //update velocity vector liimting to max speed
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

    /*Manage behaviour for attacking
     * 
     */
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
        //detect collision with enemy projectiles and recive damage it deals
        if(collision.transform.tag == "EnemyProjectile")
        {
            BHProjectile projectile = collision.transform.GetComponent<BHProjectile>();
            ReceiveDamage(projectile.Damage());
            Destroy(collision.transform.gameObject);
        }
    }

    /*Behaviour when player receives damage
     * 
     */
    void ReceiveDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            PlayerDestroy();
        }
    }

    /*Behaviour when player is destroyed
     * 
     */
    void PlayerDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path, LoadSceneMode.Single);
    }

}

