using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemyBoss : BHEnemy
{
    [SerializeField]
    private Transform[] routes;

    [SerializeField]
    GameObject projectileGarbage;
    [SerializeField]
    GameObject projectileBag;
    [SerializeField]
    GameObject projectileWrench;
    [SerializeField]
    GameObject patternGarbageLine;

    //reference to game controller to report win condition met on destroy
    [SerializeField]
    GameObject gameController;

    [SerializeField]
    BHSoundManager soundManager;

    [SerializeField]
    AudioSource backgroundSource;

    Animator animator;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    private bool routeCoroutineAllowed;
    private bool waveCoroutineAllowed;

    public bool followRoute;

    BossPhase phase;

    //hp percentage at which the mid point of the fight starts
    float midPointPercentage;

    //Control time between attacking in shoot phase
    int shootAttackDelay;
    int shootAttackDelayDefault;      //default delay between attacks
    int shootAttackDelayOffset;       //max delay offset each itteration
    int shootAttackStepCount;

    bool shootAttacking;

    //Boss actions happen on steps
    //steps are variable units of time
    float stepTime;
    float stepTimer;
    float stepModifier;

    //parameter to define position where to spawn projectiles
    float projectileSpawnerWidth;           //width of spawning area
    float projectileSpawnerY;               //Y position in local space of spawning area
    Vector2 projectileSpawnerTopLeftPos;    //Top left corner of spawner area in local space

    // Start is called before the first frame update
    protected override void SetUp()
    {
        animator = GetComponent<Animator>();      

        hpMax = 1250;
        hp = hpMax;

        midPointPercentage = 0.4f;

        routeToGo = 0;
        tParam = 0f;
        speedModifier = 1.5f;
        routeCoroutineAllowed = true;

        waveCoroutineAllowed = true;

        followRoute = true;
        phase = BossPhase.ShootPhase;

        shootAttackDelayDefault = 15;
        shootAttackDelayOffset = 4;
        shootAttackDelay = shootAttackDelayDefault + Random.Range(-shootAttackDelayOffset, shootAttackDelayOffset);
        shootAttackStepCount = 0;

        stepTime = 1f;
        stepTimer = 0;
        stepModifier = 1;

        shootAttacking = false;

        projectileSpawnerWidth = 8;
        projectileSpawnerY = -4.5f;
        projectileSpawnerTopLeftPos = new Vector2(-projectileSpawnerWidth / 2, projectileSpawnerY);
    }

    void Update()
    {

        switch (phase)
        {
            case BossPhase.ShootPhase:
                UpdateShootPhase();
                break;
            case BossPhase.VacuumPhase:
                UpdateVacuumPhase();
                break;
        }


    }

    void UpdateShootPhase()
    {
        stepTimer += Time.deltaTime * stepModifier;
        if (stepTimer >= stepTime)
        {
            ShootStep();
            stepTimer = 0;
        }


        if (routeCoroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }

    }

    void ShootStep()
    {
        if (!shootAttacking)
        {
            shootAttackStepCount++;

            ShootRandomProjectile();

            if (shootAttackStepCount >= shootAttackDelay)
            {
                shootAttacking = true;
                animator.SetBool("ShootAttacking", true);
                followRoute = false;
                soundManager.PlayClip(BHSoundManager.SoundClip.BossAttackStart);

                shootAttackDelay = shootAttackDelayDefault + Random.Range(-shootAttackDelayOffset, shootAttackDelayOffset);
                shootAttackStepCount = 0;
            }
        }
        else
        {
            if (waveCoroutineAllowed)
            {
                StartRandomWave();
            }

        }
    }

    void ShootRandomProjectile()
    {
        float r = Random.value;
        GameObject projectile;

        if (r < 0.66f)
        {
            projectile = projectileGarbage;
        } else if (r < 0.90f)
        {
            projectile = projectileWrench;
        } else
        {
            projectile = projectileBag;
        }

        float x_offset = Random.Range(0, projectileSpawnerWidth);
        Vector3 spawnPosLocal = new Vector3(projectileSpawnerTopLeftPos.x + x_offset, projectileSpawnerTopLeftPos.y, 0);
        Instantiate(projectile, transform.position + spawnPosLocal, Quaternion.identity);
        soundManager.PlayClip(BHSoundManager.SoundClip.BossShoot);
    }

    void StartRandomWave()
    {
        float r = Random.value;
        if (r < 0.33f)
        {
            StartCoroutine(SpawnWaveGarbageLine());
        } else if (r < 0.66f)
        {
            StartCoroutine(SpawnWaveBagStair());
        } else
        {
            StartCoroutine(SpawnWaveWrenchPillars());
        }


    }

    void UpdateVacuumPhase()
    {

    }

    IEnumerator SpawnWaveGarbageLine()
    {
        waveCoroutineAllowed = false;

        WaitForSeconds delay = new WaitForSeconds(1);
        int iterations = 4;

        for (int i = 0; i < iterations; i++)
        {

            Instantiate(patternGarbageLine, new Vector3(transform.position.x, transform.position.y + projectileSpawnerTopLeftPos.y, 0), Quaternion.identity);
            soundManager.PlayClip(BHSoundManager.SoundClip.BossShoot);
            yield return delay;
        }

        waveCoroutineAllowed = true;
        shootAttacking = false;
        animator.SetBool("ShootAttacking", false);
        followRoute = true;
    }

    IEnumerator SpawnWaveBagStair()
    {
        waveCoroutineAllowed = false;

        WaitForSeconds delay = new WaitForSeconds(1f);
        int iterations = 2;
        int numProjectiles = 4;

        //ascending direction of stair
        bool directionRight = true;
        
        float spacing = 2;

        float x_origin = transform.position.x - (spacing * (numProjectiles - 1)) / 2;

        for (int i = 0; i < iterations; i++)
        {
            for (int j = 0; j < numProjectiles; j++)
            {
                float x_pos;
                if (directionRight)
                {
                    x_pos = x_origin + spacing * j;
                } else {
                    x_pos = x_origin + spacing * (numProjectiles - j);
                }

                Instantiate(projectileBag, new Vector3(x_pos, transform.position.y + projectileSpawnerTopLeftPos.y, 0), Quaternion.identity);
                soundManager.PlayClip(BHSoundManager.SoundClip.BossShoot);

                yield return delay;
            }
            directionRight = !directionRight;
            yield return delay;
        }



        waveCoroutineAllowed = true;
        shootAttacking = false;
        animator.SetBool("ShootAttacking", false);
        followRoute = true;
    }

    IEnumerator SpawnWaveWrenchPillars()
    {
        waveCoroutineAllowed = false;

        WaitForSeconds delay = new WaitForSeconds(1);

        int iterations = 3;
        int pillarHeight = 3;
        int numPillars = 5;

        bool spawnEvenPillars = true;

        float spacing = 2.2f;

        float x_origin = transform.position.x - (spacing * (numPillars - 1)) / 2;

        for(int i = 0; i < iterations; i++)
        {
            for(int h = 0; h < pillarHeight; h++)
            {
                for (int n = 0; n < numPillars; n++)
                {
                    bool isEven = n % 2 == 0;

                    if (isEven == spawnEvenPillars)
                    {
                        Instantiate(projectileWrench, new Vector3(x_origin + spacing * n, transform.position.y + projectileSpawnerTopLeftPos.y, 0), Quaternion.identity);
                        soundManager.PlayClip(BHSoundManager.SoundClip.BossShoot);
                    }
                }

                yield return delay;
            }

            spawnEvenPillars = !spawnEvenPillars;
        }


        waveCoroutineAllowed = true;
        shootAttacking = false;
        animator.SetBool("ShootAttacking", false);
        followRoute = true;
    }


    private IEnumerator GoByTheRoute(int routeNum)
    {
        routeCoroutineAllowed = false;

        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            if (followRoute)
            {
                tParam += Time.deltaTime * speedModifier;
            }

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        routeCoroutineAllowed = true;

    }

    protected override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        if(hp <= hpMax * midPointPercentage)
        {
            stepModifier = 1.25f;
            speedModifier = 2.5f;
            backgroundSource.pitch = 1.08f;
        }
    }

    protected override void EnemyDestroy()
    {
        soundManager.PlayClip(BHSoundManager.SoundClip.BossDefeat);
        backgroundSource.Stop();
        StopAllCoroutines();
        stepModifier = 0;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float fadeTime = 0.5f;
        float timer = fadeTime;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, (float)(timer / fadeTime));
            yield return new WaitForEndOfFrame();
        }

        gameController.GetComponent<BHGameController>().WinConditionMet();        
        base.EnemyDestroy();
    }
    enum BossPhase
    {
        Null,
        ShootPhase,
        VacuumPhase
    }
}
