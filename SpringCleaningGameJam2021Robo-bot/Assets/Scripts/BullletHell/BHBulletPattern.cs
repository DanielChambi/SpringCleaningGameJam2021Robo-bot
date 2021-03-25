using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHBulletPattern : MonoBehaviour
{
    /*Base class for bullet pattern spawning*/
    
    void Start()
    {
        SpawnPattern();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void SpawnPattern()
    {
        //cant make it abstract so here's a flower @-º-|]
    }

}
