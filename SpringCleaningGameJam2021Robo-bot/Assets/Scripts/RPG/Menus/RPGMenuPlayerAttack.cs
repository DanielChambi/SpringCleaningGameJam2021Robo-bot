using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPGMenuPlayerAttack : RPGMenu
{
    //Reference to controller in order to access player reference and obtain its attacks' name
    public RPGBattleController controller;

    protected override void Start()
    {
        base.Start();

        //obtain player attack names and update options manually referenced
        string[] attackList = controller.player.GetComponent<RPGPlayerBattle>().AttackMenuListing();

        for (int i = 0; i < options.Length && i < attackList.Length; i++)
        {
            options[i].GetComponent<Text>().text = attackList[i];
        }
    }

    public override ActionCode SelectOption()
    {
        return new ActionCode(ActionCode.Action.PlayerAttack, selectedOption, controller.enemy);
    }

}
