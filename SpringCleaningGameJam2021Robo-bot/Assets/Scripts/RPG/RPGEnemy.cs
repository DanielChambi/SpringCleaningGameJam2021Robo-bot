using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGEnemy : RPGUnit
{
    /*Reference to battle controller to obtain list of active players and choose target
     * 
     */
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

    /*Logic to select target player and attack used 
     * 
     *returns:  bool indicating whether the attack was able to be performed or not
     */
    public virtual bool EnemyAttack()
    {
        bool attackPerformed = false;

        int attackIndex = Random.Range(0, attackMoveSet.Length);

        attackPerformed = AttackTarget(attackIndex, controller.player.GetComponent<RPGUnit>());

        return attackPerformed;
    }

}
