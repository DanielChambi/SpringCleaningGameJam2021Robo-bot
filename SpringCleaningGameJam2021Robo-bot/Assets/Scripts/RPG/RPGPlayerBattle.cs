using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerBattle : RPGUnit
{
    bool blocking = false;

    protected override void Start()
    {
        base.Start();

        attackMoveSet = new Attack[] { 
            new Attack("Bonk", 5),
            new Attack("Magic Bonk", 10, 2),
            new Attack("Magicer Bonk", 20, 6)
        };

        hp = 30;
        mp = 10;
    }

    public void PlayerBlock()
    {
        blocking = true;
    }

    protected override void ReceiveDamage(float damage)
    {
        if (blocking)
        {
            damage = damage / 2;
        }

        hp -= damage;

        if (hp <= 0)
        {
            UnitKnockedOut();
        }
    }

    public string[] AttackMenuListing()
    {
        string[] list = new string[attackMoveSet.Length];

        for(int i = 0; i < attackMoveSet.Length; i++)
        {
            list[i] = attackMoveSet[i].name;

            string mpCost;
            if(attackMoveSet[i].mpCost <= 0)
            {
                mpCost = "-";
            }
            else
            {
                mpCost = attackMoveSet[i].mpCost.ToString();
            }

            list[i] = list[i] + " " + mpCost;
        }

        return list;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
