using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGEnemy : RPGUnit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackMoveSet = new Attack[]
        {
            new Attack("Plonk", 3),
            new Attack("Plonk+", 5),
            new Attack("MegaPlonk", 8)
        };

        hp = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
