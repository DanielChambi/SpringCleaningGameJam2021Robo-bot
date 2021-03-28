using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActionCode
{
    public GameObject target { get; }
    public int actionIndex { get;  }
    public int actionSubindex { get; } 


    public ActionCode(int actionIndex)
    {
        this.actionIndex = actionIndex;
        this.target = null;
        actionSubindex = -1;
    }

    public ActionCode(int actionIndex, int actionSubindex): this(actionIndex)
    {
        this.actionSubindex = actionSubindex;
    }

    public ActionCode(int actionIndex, GameObject target): this(actionIndex)
    {
        this.target = target;
    }

    public ActionCode(int actionIndex, int actionSubindex, GameObject target): this(actionIndex, actionSubindex)
    {
        this.target = target;
    }



    public enum Action
    {
        Null,
        MenuSelectPlayerAttack,
        MenuGoBack,
        MenuRunAway,
        PlayerBlock,
        PlayerAttack,
        EnemyAttack
    }
}


