using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BHPlayer : MonoBehaviour
{
    public float max_speed = 5;
    public float acceleration = 1;

    public float hpMax = 10; 
    public float hp;

    public float shootDelay = 10f; //delay between shots in seconds
    float shootTimer;

    //Time player is invincible after hit
    float invinDuration = 0.3f;
    //Time player is uncapable of shooting after hit
    float staggerduration = 0.3f;

    bool invincible = false;
    bool canShoot = true;

    //movement vector applied on update
    Vector2 velocity;

    //input directions
    float x_axis;
    float y_axis;

    //reference to current projectile to spawn when attacking
    public GameObject currentProjectile;

    public BHSoundManager soundManager;

    public BHGameController controller;

    void Start()
    {
        hp = hpMax;
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

        if (Input.GetButton("Fire1") && canShoot)
        {
            if(shootTimer == 0)
            {
                GameObject.Instantiate(currentProjectile, transform.position, Quaternion.identity);
                soundManager.PlayClip(BHSoundManager.SoundClip.PlayerShoot);
                shootTimer = shootDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //detect collision with enemy projectiles and recive damage it deals
        if(!invincible)
        {
            if(collision.transform.tag == "EnemyProjectile")
            {
                BHProjectile projectile = collision.transform.GetComponent<BHProjectile>();
                ReceiveDamage(projectile.Damage());
                Destroy(collision.transform.gameObject);

            } else if(collision.transform.tag == "Enemy")
            {
                ReceiveDamage(2);
            }
        }
    }

    /*Behaviour when player receives damage
     * 
     */
    void ReceiveDamage(float damage)
    {
        hp -= damage;
        soundManager.PlayClip(BHSoundManager.SoundClip.PlayerHit);
        if(hp <= 0)
        {
            PlayerDestroy();
        }

        invincible = true;
        StartCoroutine(HitInvinBlinking());
        canShoot = false;
        StartCoroutine(Stagger());
    }

    /*Coroutine to cause player blinking animation after hit
     * 
     */
    IEnumerator HitInvinBlinking()
    {
        float timer = 0;
        WaitForSeconds blinkDuration = new WaitForSeconds(0.1f);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        while (timer <= invinDuration )
        {
            timer += Time.deltaTime;
            yield return blinkDuration;
            sprite.enabled = !sprite.enabled;
        }
        sprite.enabled = true;
        
        invincible = false;
    }

    /*Coroutine to control stagger after hit
     * 
     */
    IEnumerator Stagger()
    {
        yield return new WaitForSeconds(staggerduration);
        canShoot = true;
    }

    /*Behaviour when player is destroyed
     * 
     */
    void PlayerDestroy()
    {
        controller.ReloadScene();
        Destroy(gameObject);
    }

    public string HpToString()
    {
        return hp + " / " + hpMax;
    }

}

