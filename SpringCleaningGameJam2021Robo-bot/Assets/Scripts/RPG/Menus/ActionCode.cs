using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*unmutable data strcuture to comunicate actions from RPGMenu class to controller.
 *All Action Codes have at least the action parameter defined.
 *action:           action code indicating the action to carry by controller
 *target:           GameObject reference to object to be targeted by the action indicated
 *actionSubindex:   specific action subindex for broad actions that need more specificity
 */
public struct ActionCode
{
    public Action action { get; }
    public GameObject target { get; }
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


    /*All possible actions that can be chosen from a menu
     *Null:                         Unhanded action
     *MenuSelectPlayerAttack:       Menu action. Open menu to select player action. Target: reference to playyer attack menu
     *MenuGoBack:                   Menu action. Close active menu and activate previous menu
     *MenuRunAway:                  Menu action. Run away from battle to overworld
     *PlayerBlock:                  Player action: Player enters blocking state for the turn
     *PlayerAttack:                 Player action. Player performs an attack. Subindex: specific attack specified by index in unit's moveset array Target: enemy unit target of attack
     *EnemyAttack:                  I'm really not sure if this goes here but I added it for symmetry with player attack
     */
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


