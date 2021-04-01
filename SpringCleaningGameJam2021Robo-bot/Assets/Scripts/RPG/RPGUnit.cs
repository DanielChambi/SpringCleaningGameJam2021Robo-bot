using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGUnit : MonoBehaviour
{
    /*Stats
     *hp:       Health points, reaching 0 health points knocks out unit
     *mp:       Magic point, resource used by units to perfom certain attacks
     */
    protected float hpMax;
    protected int mpMax;

    protected float hp;
    protected int mp;

    /*Unit's state during battle
     * 
     */
    protected UnitState state;

    /*Stores attacks available to the unit
     * 
     */
    protected Attack[] attackMoveSet;

    protected virtual void Start()
    {
        hpMax = 10;
        mpMax = 5;

        hp = hpMax;
        mp = mpMax;
        state = UnitState.Ready;
    }

    /*Set up unit for its new turn*/
    public virtual void UnitSetUp()
    {
        state = UnitState.Ready;
    }

    /*Performs attack on a target unit and calculates damage deat 
     *attackIndex:  the position of the attack in the attackMoveSet array
     *target:       RPGUnit component of the target unit
     *
     *return:       bool indicating whether the attack was able to be performed or not
     */
    public bool AttackTarget(int attackIndex, RPGUnit target)
    {
        bool attackPerformed = false;

        if (attackIndex >= 0 || attackIndex < attackMoveSet.Length)
        {
            Attack attack = attackMoveSet[attackIndex];

            float damage = attack.damage;

            //Attack will not be performed if the unit lacks enough mp
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

    /*calculate and take damage dealt by another RPGunit
     * damaga:  calculated damage being dealt
     */
    protected virtual void ReceiveDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            UnitKnockedOut();
        }
    }

    /*Change unit state to reflect knock out
     *
     */
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

    public string HpToString()
    {
        return hp + " / " + hpMax;
    }

    public string MpToString()
    {
        return mp + " / " + mpMax;
    }


    /*Unmutable struct to store attack data at runtime
     *name:     the attacks name
     *damage:   base damage dealt
     *mpCost:   cost in magic points
     */
    protected struct Attack
    {
        public string name { get; }
        public float damage { get; }
        public int mpCost { get; }

        //Attacks will always have a name and defined damage. Default MP cost is 0.
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

    /*Unit's possible states during battle
     *Null:     Unhandled value
     *Ready:    Active and capable of carrying actions
     *Out:      Uncapacitated and unable of carrying actions
     */
    public enum UnitState
    {
        Null,
        Ready,
        Out
    }
}
