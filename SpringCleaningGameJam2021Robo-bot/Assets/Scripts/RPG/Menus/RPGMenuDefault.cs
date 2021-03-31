using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMenuDefault : RPGMenu
{

    public RPGMenu playerAttackMenu;

    public override ActionCode SelectOption()
    {
        switch (selectedOption)
        {
            case (int)DefaultOption.attack:
                return new ActionCode(ActionCode.Action.MenuSelectPlayerAttack, playerAttackMenu.gameObject);
                break;

            case (int)DefaultOption.block:
                return new ActionCode(ActionCode.Action.PlayerBlock);
                break;
            case (int)DefaultOption.run:
                return new ActionCode(ActionCode.Action.MenuRunAway);

            default:
                return new ActionCode(ActionCode.Action.Null);
                break;
        }
    }

    /*Named actions that can be selected in this menu
     */
    enum DefaultOption
    {
        attack,
        block,
        run
    }
}
