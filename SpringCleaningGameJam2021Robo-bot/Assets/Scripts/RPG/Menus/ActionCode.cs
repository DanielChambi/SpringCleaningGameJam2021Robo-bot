using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActionCode
{
    public GameObject target { get; }
    public Action action { get;  }
    public int actionSubindex { get; } 


    public ActionCode(ActionCode.Action action)
    {
        this.action = action;
        this.target = null;
        actionSubindex = -1;
    }

    public ActionCode(ActionCode.Action action, int actionSubindex): this(action)
    {
        this.actionSubindex = actionSubindex;
    }

    public ActionCode(ActionCode.Action action, GameObject target): this(action)
    {
        this.target = target;
    }

    public ActionCode(ActionCode.Action action, int actionSubindex, GameObject target): this(action, actionSubindex)
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


