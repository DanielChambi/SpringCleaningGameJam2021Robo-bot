using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGUnit : MonoBehaviour
{
    protected float hp;
    protected int mp;

    protected UnitState state;

    protected Attack[] attackMoveSet;

    protected virtual void Start()
    {
        hp = 10;
        mp = 5;
        state = UnitState.Ready;
    }

    /*Set up unit for it's new turn*/
    public virtual void UnitSetUp()
    {
        state = UnitState.Ready;
    }

    public bool AttackTarget(int attackIndex, RPGUnit target)
    {
        bool attackPerformed = false;

        if (attackIndex >= 0 || attackIndex < attackMoveSet.Length)
        {
            Attack attack = attackMoveSet[attackIndex];

            float damage = attack.damage;


            if (mp >= attack.mpCost)
            {
                mp -= attack.mpCost;
                if (mp < 0) mp = 0;

                Debug.Log(gameObject.name + " used: " + attack.name + "!");

                target.ReceiveDamage(damage);

                attackPerformed = true;
            } else
            {
                Debug.Log("Not enough mp!");
                attackPerformed = false;
            }        
        }
        else
        {
            Debug.Log("Attack index: " + attackIndex + " out of bounds for unit: " + gameObject.name);
            attackPerformed = false;
        }

        return attackPerformed;
    }

    protected virtual void ReceiveDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            UnitKnockedOut();
        }
    }

    protected virtual void UnitKnockedOut()
    {
        state = UnitState.Out;
        Debug.Log("Unit " + gameObject.name + " knocked out");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public UnitState State()
    {
        return state;
    }

    protected struct Attack
    {
        public string name { get; }
        public float damage { get; }
        public int mpCost { get; }

        public Attack(string name, float damage)
        {
            this.name = name;
            this.damage = damage;
            mpCost = 0;
        }
        public Attack(string name, float damage, int mpCost): this(name, damage)
        {
            this.mpCost = mpCost;
        }

    }

    public enum UnitState
    {
        Null,
        Ready,
        Out
    }
}
