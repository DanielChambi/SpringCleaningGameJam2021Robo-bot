using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMenuPlayerAttack : RPGMenu
{
    public RPGBattleController controller;

    public override ActionCode SelectOption()
    {
        return new ActionCode(ActionCode.Action.PlayerAttack, selectedOption, controller.enemy);
    }

}
