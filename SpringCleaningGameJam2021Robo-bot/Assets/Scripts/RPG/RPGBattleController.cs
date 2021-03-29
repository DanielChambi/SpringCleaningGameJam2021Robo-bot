using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBattleController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;

    public RPGMenu currentMenu;

    float inputDelay = 0.1f;
    float inputTimer;

    void Start()
    {
        inputTimer = inputDelay;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetButtonDown("Vertical"))
        {
            float y_axis = Input.GetAxis("Vertical");
            if (y_axis < 0 || y_axis > 0)
            {
                currentMenu.MoveSelection( -(int)Mathf.Sign(y_axis) );
            }
        }
            

       

        if (Input.GetButtonDown("Submit"))
        {
            ParseActionCode(currentMenu.SelectOption());
        }

        if (Input.GetButtonDown("Cancel"))
        {
            ParseActionCode(new ActionCode(ActionCode.Action.MenuGoBack));
        }
    }

    void ParseActionCode(ActionCode action)
    {
        switch (action.action){
            
            case ActionCode.Action.MenuSelectPlayerAttack:
                OpenPlayerAttackMenu(action);
                break;
            case ActionCode.Action.MenuRunAway:
                RunAway();
                break;
            case ActionCode.Action.PlayerAttack:
                PlayerAttack(action);
                break;
            case ActionCode.Action.PlayerBlock:
                PlayerBlock();
                break;
            case ActionCode.Action.EnemyAttack:
                EnemyAttack(action);
                break;
            case ActionCode.Action.MenuGoBack:
                MenuGoBack();
                break;
            default:
                Debug.Log("UntreatedAction");
                break;

        }
    }


    void OpenPlayerAttackMenu(ActionCode action)
    {
        Debug.Log("Opening player attack");
        action.target.SetActive(true);
        action.target.GetComponent<RPGMenu>().prevMenu = currentMenu;
        currentMenu = action.target.GetComponent<RPGMenu>();
    }

    void MenuGoBack()
    {
        if(!(currentMenu is RPGMenuDefault))
        {
            RPGMenu menu = currentMenu.prevMenu;
            currentMenu.gameObject.SetActive(false);
            currentMenu = menu;
        }
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
