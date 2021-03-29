using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGUnit : MonoBehaviour
{
    float hp;
    int mp;

    protected UnitState state;

    protected Attack[] attackMoveSet;

    private void Start()
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

    public void ReceiveDamage(float damage)
    {
        switch (state)
        {
            case UnitState.Ready:
                hp -= damage;
                break;
            case UnitState.Blocking:
                hp -= damage / 2;
                break;
        }

        if (hp <= 0)
        {
            UnitKnockedOut();
        }
    }

    protected virtual void UnitKnockedOut()
    {

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
        Blocking,
        Out
    }
}
