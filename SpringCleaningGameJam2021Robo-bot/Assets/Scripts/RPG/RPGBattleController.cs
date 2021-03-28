using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBattleController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;

    public RPGMenu currentMenu;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMenu.MoveSelection(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMenu.MoveSelection(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ParseActionCode(currentMenu.SelectOption());
        }
    }

    void ParseActionCode(ActionCode action)
    {
        switch (action.actionIndex){
            
            case (int)ActionCode.Action.MenuSelectPlayerAttack:
                OpenPlayerAttackMenu();
                break;
            case (int)ActionCode.Action.MenuRunAway:
                RunAway();
                break;
            case (int)ActionCode.Action.PlayerAttack:
                PlayerAttack(action);
                break;
            case (int)ActionCode.Action.PlayerBlock:
                PlayerBlock();
                break;
            case (int)ActionCode.Action.EnemyAttack:
                EnemyAttack(action);
                break;
            default:
                Debug.Log("UntreatedAction");
                break;

        }
    }


    void OpenPlayerAttackMenu()
    {
        Debug.Log("Opening player attack");
    }

    void RunAway()
    {
        Debug.Log("Running away");
    }

    void PlayerAttack(ActionCode action)
    {
        Debug.Log("Player using attack: " + action.actionSubindex + " against enemy:" + action.target.name);
    }

    void PlayerBlock()
    {
        Debug.Log("Player blocking");
    }

    void EnemyAttack(ActionCode action)
    {
        Debug.Log("Enemy using attack: " + action.actionSubindex + " against player:" + action.target.name);
    }
}
