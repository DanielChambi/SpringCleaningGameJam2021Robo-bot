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

    public void AttackTarget(int attackIndex, RPGUnit target)
    {
        if (attackIndex >= 0 || attackIndex < attackMoveSet.Length)
        {
            Attack attack = attackMoveSet[attackIndex];

            float damage = attack.damage;

            mp -= attack.mpCost;
            if (mp < 0) mp = 0;

            target.ReceiveDamage(damage);
        }
        else
        {
            Debug.Log("Attack index: " + attackIndex + " out of bounds for unit: " + gameObject.name);
        }
    }

    protected virtual void ReceiveDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            state = UnitState.Out;
        }
    }

    protected virtual void UnitKnockedOut()
    {
        state = UnitState.Out;
        Debug.Log("Unit " + gameObject.name + " knocked out");
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

    protected enum UnitState
    {
        Null,
        Ready,
        Out
    }
}
