using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHEnemyController: MonoBehaviour
{
    public GameObject baseEnemy;
    int enemiesDestroyed = 0;

    int prevEnemiesDestroyed = 0; //for provisional wave spawning

    private void Update()
    {
        /*Provisional wave spawning*/
        if(enemiesDestroyed > prevEnemiesDestroyed)
        {
            GameObject enemy = Instantiate(baseEnemy, this.transform);
            enemy.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(5, 10), 0);
            if(Random.value >= 0.5f)
            {
                enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        prevEnemiesDestroyed = enemiesDestroyed;
    }

    public int EnemyDestroyed()
    {
        enemiesDestroyed++;
        return enemiesDestroyed;
    }

    
}

