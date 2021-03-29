using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGEnemy : RPGUnit
{
    public RPGBattleController controller;

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

    public virtual bool EnemyAttack()
    {
        bool attackPerformed = false;

        int attackIndex = Random.Range(0, attackMoveSet.Length);

        attackPerformed = AttackTarget(attackIndex, controller.player.GetComponent<RPGUnit>());

        return attackPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
