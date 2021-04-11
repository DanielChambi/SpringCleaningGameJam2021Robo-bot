using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHBulletPatternCircleBase : BHBulletPattern
{
    public GameObject projectile;
    public int num_bullet = 8;
    
    protected override void SpawnPattern()
    {
        for (int i = 0; i < num_bullet; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis( (360f/num_bullet)*i, Vector3.forward );
            GameObject.Instantiate(projectile, transform.position, rotation);
        }

        Destroy(gameObject);
    }


}
