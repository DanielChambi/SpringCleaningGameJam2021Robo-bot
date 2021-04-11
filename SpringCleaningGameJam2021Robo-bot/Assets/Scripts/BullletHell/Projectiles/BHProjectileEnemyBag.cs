using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHProjectileEnemyBag : BHProjectileEnemy
{
    float explodeTime;
    float explodeTimeMin = 1f;
    float explodeTimeMax = 1.75f;
    float explodeTimer = 0;

    float speed = 5;

    public GameObject circlePattern;

    BHSoundManager soundManager;

    private void Start()
    {
        explodeTime = Random.Range(explodeTimeMin, explodeTimeMax);

        soundManager = GameObject.Find("SoundManager").GetComponent<BHSoundManager>();
    }
    protected override void ProjectileMovement()
    {
        explodeTimer += Time.deltaTime;
        if(explodeTimer >= explodeTime)
        {
            Instantiate(circlePattern, transform.position, Quaternion.identity);
            soundManager.PlayClip(BHSoundManager.SoundClip.ProjectileBagExplode);
            Destroy(gameObject);
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
    }
}
