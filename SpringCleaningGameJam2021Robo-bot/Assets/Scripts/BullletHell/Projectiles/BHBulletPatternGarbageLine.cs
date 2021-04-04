using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHBulletPatternGarbageLine : BHBulletPattern
{
    public GameObject projectile;
    float spacing = 1;
    int numProjectiles = 6;
    protected override void SpawnPattern()
    {
        for(int i = 0; i < numProjectiles; i++)
        {
            float x_pos = transform.position.x - (spacing * (numProjectiles - 1)) / 2 + spacing * i;
            Instantiate(projectile, new Vector3(x_pos, transform.position.y, 0), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
