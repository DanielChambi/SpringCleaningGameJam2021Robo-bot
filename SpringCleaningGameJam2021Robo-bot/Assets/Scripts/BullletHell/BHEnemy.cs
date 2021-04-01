using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemy : MonoBehaviour
{
    float hp = 10;

    //default movement speed
    protected float default_speed = 5;

    //reference to default projectile object to spawn
    public GameObject currentProjectile;

    //reference to enemy controller to report when it's destroyed
    BHEnemyController enemyController;

    void Start()
    {
        enemyController = GameObject.Find("BHEnemyController").GetComponent<BHEnemyController>();
        SetUp();
    }

    void Update()
    {
        MovementPath();    
    }

    /*Behaviour to carry on Start() by child classes.
     * I think that I later figured that you can call the base Start() function from child classes
     * but I didn't know how to do it here and just added this
     */
    protected virtual void SetUp()
    {
        // wizard ->  <|:)
    }

    /*Default movement and shooting behaviour for enemies. To be rewritten by child enemy classes
     * 
     */
    protected virtual void MovementPath()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * default_speed) * 2;
        if(Mathf.Abs(pos.x) <= 0.1)
        {
            Shoot();
        }
        transform.position = pos;
    }

    /*Behaviour when shooting projectile
     * 
     */
    protected virtual void Shoot()
    {
        GameObject.Instantiate(currentProjectile, transform.position, Quaternion.identity);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision with projectiles tagged "PlayerProjectile"
        if(collision.transform.tag == "PlayerProjectile")
        {
            BHProjectile projectile = collision.transform.GetComponent<BHProjectile>();
            ReceiveDamage(projectile.Damage());
            Destroy(collision.transform.gameObject);
        }
    }

    /*Behaviour when enemy receives damage
    * 
    */
    void ReceiveDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            EnemyDestroy();
        }
    }

    /*Behaviour when enemy receives damage
    * 
    */
    protected virtual void EnemyDestroy()
    {
        enemyController.EnemyDestroyed();
        Destroy(gameObject);
    }
}
