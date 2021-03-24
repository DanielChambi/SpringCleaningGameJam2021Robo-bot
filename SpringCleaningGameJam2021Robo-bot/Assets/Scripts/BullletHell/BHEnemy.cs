using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemy : MonoBehaviour
{
    /*Base class for all enemy in Bullet Hell*/

    float hp = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();    
    }

    void ManageMovement()
    {
        /*Default movemetn path for enemies*/
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * 5);
        transform.position = pos;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Collision with projectiles tagged "PlayerProjectile"*/
        if(collision.transform.tag == "PlayerProjectile")
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
            EnemyDestroy();
        }
    }

    void EnemyDestroy()
    {
        Destroy(gameObject);
    }
}
