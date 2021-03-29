using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPGBattleController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;

    public RPGMenu currentMenu;

    BattleState state;

    bool coroutineEnable;

    void Start()
    {
        coroutineEnable = true;
        state = BattleState.PlayerTurn;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case BattleState.PlayerTurn:
                UpdatePlayerTurn();
                break;
            case BattleState.EnemyTurn:
                UpdateEnemyTurn();
                break;
            case BattleState.BattleWon:
                UpdateBattleWon();
                break;
            case BattleState.BattleLost:
                UpdateBattleLost();
                break;
        }
    }

    void UpdatePlayerTurn()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            float y_axis = Input.GetAxis("Vertical");
            if (y_axis < 0 || y_axis > 0)
            {
                currentMenu.MoveSelection(-(int)Mathf.Sign(y_axis));
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

    void UpdateEnemyTurn()
    {
        bool attackPerformed = false;
        attackPerformed = enemy.GetComponent<RPGEnemy>().EnemyAttack();

        if (attackPerformed)
        {
            EnemyPassTurn();
        }
    }

    void UpdateBattleWon()
    {
        if (coroutineEnable)
        {
            coroutineEnable = false;
            StartCoroutine(ExitBattle());
        }
    }

    void UpdateBattleLost()
    {
        if (coroutineEnable)
        {
            coroutineEnable = false;
            StartCoroutine(ExitBattle());
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
        //Debug.Log("Opening player attack");
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
        if (coroutineEnable)
        {
            coroutineEnable = false;
            StartCoroutine(ExitBattle());
        }
    }

    void PlayerAttack(ActionCode action)
    {
        //Debug.Log("Player using attack: " + action.actionSubindex + " against enemy:" + action.target.name);

        bool attackPerformed = false;
        attackPerformed = player.GetComponent<RPGUnit>().AttackTarget(action.actionSubindex, enemy.GetComponent<RPGUnit>());

        if (attackPerformed)
        {
            PlayerPassTurn();
        }
    }

    void PlayerBlock()
    {
        //Debug.Log("Player blocking");

        player.GetComponent<RPGPlayerBattle>().PlayerBlock();

        PlayerPassTurn();
    }

    void EnemyAttack(ActionCode action)
    {
        Debug.Log("Enemy using attack: " + action.actionSubindex + " against player:" + action.target.name);
    }



    void PlayerPassTurn()
    {
        //collapse menus back to Default Menu
        while (!(currentMenu is RPGMenuDefault))
        {
            MenuGoBack();
        }

        BattleUpdateWinLoseState();

        //if battle state did not change to battle won/lost:
        if (state == BattleState.PlayerTurn)
        {
            state = BattleState.EnemyTurn;

            enemy.GetComponent<RPGUnit>().UnitSetUp();
        }
    }

    void EnemyPassTurn()
    {
        BattleUpdateWinLoseState();

        //if battle state did not change to battle won/lost:
        if (state == BattleState.EnemyTurn)
        {
            state = BattleState.PlayerTurn;
            player.GetComponent<RPGUnit>().UnitSetUp();
        }

    }

    /*Checks win/loss condition and update state*/
    bool BattleUpdateWinLoseState()
    {
        bool win = CheckWinCondition();
        bool lose = CheckLoseCondition();

        if (win)
        {
            BattleWon();
        }else if (lose)
        {
            BattleLost();
        }

        return win || lose;
    }

    bool CheckWinCondition()
    {
        bool win = false;

        if (enemy.GetComponent<RPGUnit>().State() == RPGUnit.UnitState.Out)
        {
            win = true;
        }

        return win;
    }

    bool CheckLoseCondition() 
    {
        bool lose = false;
        if(player.GetComponent<RPGUnit>().State() == RPGUnit.UnitState.Out)
        {
            lose = true;
        }

        return lose;
    }

    void BattleWon()
    {
        Debug.Log("Battle won! :)");
        state = BattleState.BattleWon;
    }

    void BattleLost()
    {
        Debug.Log("Battle Lost! :(");
        state = BattleState.BattleLost;
    }

    
    IEnumerator ExitBattle()
    {
        yield return new WaitForSeconds(5);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(RPGOverWorldController.overworldScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }



    public enum BattleState
    {
        Null,
        PlayerTurn,
        EnemyTurn,
        BattleWon,
        BattleLost
    }
}
