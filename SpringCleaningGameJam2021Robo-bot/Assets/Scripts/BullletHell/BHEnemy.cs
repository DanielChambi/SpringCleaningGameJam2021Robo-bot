using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemy : MonoBehaviour
{
    /*Base class for all enemy in Bullet Hell*/

    float hp = 10;

    public GameObject currentProjectile;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementPath();    
    }

    /*Default movement and shooting behaivour for enemies. To be rewritten by child enemy classes*/
    protected virtual void MovementPath()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * 5) * 2;
        if(Mathf.Abs(pos.x) <= 0.1)
        {
            Shoot();
        }
        transform.position = pos;
    }

    protected virtual void Shoot()
    {
        GameObject.Instantiate(currentProjectile, transform.position, Quaternion.identity);
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