using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMenuDefault : RPGMenu
{
    
    public override ActionCode SelectOption()
    {
        switch (selectedOption)
        {
            case (int)DefaultOption.attack:
                return new ActionCode((int)ActionCode.Action.MenuSelectPlayerAttack);
                break;

            case (int)DefaultOption.block:
                return new ActionCode((int)ActionCode.Action.PlayerBlock);
                break;
            case (int)DefaultOption.run:
                return new ActionCode((int)ActionCode.Action.MenuRunAway);

            default:
                return new ActionCode((int)ActionCode.Action.Null);
                break;
        }
    }

    enum DefaultOption
    {
        attack = 0,
        block = 1,
        run = 2
    }
}
