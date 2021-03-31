using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RPGBattleController : MonoBehaviour
{
    /*References to active units in battle
     * 
     */
    public GameObject enemy;
    public GameObject player;

    /*Reference to currently active menu
     * 
     */
    public RPGMenu currentMenu;

    /*Reference to text UI for player's stats
     * 
     */
    public Text playerName;
    public Text playerHpText;
    public Text playerMpText;

    /*Battles state, indicating side's turn and win/lose state 
     *States dictate behaivour on Update()
     */
    BattleState state;

    /*indicate if controller is waiting for a coroutine to finish
     * 
     */
    bool coroutineEnable;

    void Start()
    {
        coroutineEnable = true;
        state = BattleState.PlayerTurn;
    }

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

    private void OnGUI()
    {
        //Update UI player stats
        playerName.text = player.name;
        playerHpText.text = player.GetComponent<RPGUnit>().HpToString();
        playerMpText.text = player.GetComponent<RPGUnit>().MpToString();
    }

    /*Update() behavour on player turn
     * 
     */
    void UpdatePlayerTurn()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            float y_axis = Input.GetAxis("Vertical");
            if (y_axis < 0 || y_axis > 0)
            {
                //change selected option on current menu. Assumes option's index increases top to bottom.
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

    /*Update() behavour on enemy turn
    * 
    */
    void UpdateEnemyTurn()
    {
        bool attackPerformed = false;
        attackPerformed = enemy.GetComponent<RPGEnemy>().EnemyAttack();

        if (attackPerformed)
        {
            EnemyPassTurn();
        }
    }

    /*Update() behavour after battle is won
    * 
    */
    void UpdateBattleWon()
    {
        if (coroutineEnable)
        {
            coroutineEnable = false;
            StartCoroutine(ExitBattle());
        }
    }

    /*Update() behavour after battle is lost
    * 
    */
    void UpdateBattleLost()
    {
        if (coroutineEnable)
        {
            coroutineEnable = false;
            StartCoroutine(ExitBattle());
        }
    }

    /*Receive action code from current menu and call appropiate function
    * action:       action code struct indicating required behavour from menu option
    */
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

    /*Activate Player attack selection menu and update current menu reference
     *action:       action code, "target" contains GameObject reference to player attack menu
     */
    void OpenPlayerAttackMenu(ActionCode action)
    {
        //Debug.Log("Opening player attack");
        action.target.SetActive(true);
        action.target.GetComponent<RPGMenu>().prevMenu = currentMenu;
        currentMenu = action.target.GetComponent<RPGMenu>();
    }

    /*Close currently active menu and update reference to previous menu
     *Default menu is lowest possible level and cannot be closed 
     */
    void MenuGoBack()
    {
        if(!(currentMenu is RPGMenuDefault))
        {
            RPGMenu menu = currentMenu.prevMenu;
            currentMenu.gameObject.SetActive(false);
            currentMenu = menu;
        }
    }

    /*Run away from battle and reload overworld scene
     *
     */
    void RunAway()
    {
        Debug.Log("Running away");
        if (coroutineEnable)
        {
            coroutineEnable = false;
            StartCoroutine(ExitBattle());
        }
    }

    /*Issue directive to player to perfomed attack indicated in action code
     *action:       action code, "subindex" contains index of player's attack in moveset 
     */
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

    /*Issue directive to player to activate block state
     * 
     */
    void PlayerBlock()
    {
        //Debug.Log("Player blocking");

        player.GetComponent<RPGPlayerBattle>().PlayerBlock();

        PlayerPassTurn();
    }

    /*Issue directive to enemy to perfom an attack
     * 
     */
    void EnemyAttack(ActionCode action)
    {
        Debug.Log("Enemy using attack: " + action.actionSubindex + " against player:" + action.target.name);
    }


    /*Close player turn and update vattle state and units for enemy turn
     * 
     */
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

    /*Close enemy turn and update battle state and units for player turn
     * 
     */
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

    /*Checks win/loss condition and updates battle state
     *returns:      bool indicating whether the battle has reached an end state or not 
     */
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

    /*Checks for win condition of the battle
     * returns:     bool indicating whether the win condition has been met
     */
    bool CheckWinCondition()
    {
        bool win = false;

        if (enemy.GetComponent<RPGUnit>().State() == RPGUnit.UnitState.Out)
        {
            win = true;
        }

        return win;
    }

    /*Checks for lose condition of the battle
    * returns:     bool indicating whether the lose condition has been met
    */
    bool CheckLoseCondition() 
    {
        bool lose = false;
        if(player.GetComponent<RPGUnit>().State() == RPGUnit.UnitState.Out)
        {
            lose = true;
        }

        return lose;
    }

    /*Update battle state when the battle is won
     * 
     */
    void BattleWon()
    {
        Debug.Log("Battle won! :)");
        state = BattleState.BattleWon;
    }

    /*Update battle state when the battle is lost
     * 
     */
    void BattleLost()
    {
        Debug.Log("Battle Lost! :(");
        state = BattleState.BattleLost;
    }

    /*Asynchronous operation to exit battle scene and load last overworld scene
     * 
     */
    IEnumerator ExitBattle()
    {
        yield return new WaitForSeconds(5);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(RPGOverWorldController.overworldScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    /*Battle's possible states that determines its behavour
     *Null:         unhandled state
     *PlayerTurn:   state indicating it's the player's turn to carry actions
     *EnemyTurn:    state indicating it's the enemy's turn to carry actions
     *BattleWon:    state indicating the battle has been won
     *BattleLost:   state indicating the battle has been lost
     */
    public enum BattleState
    {
        Null,
        PlayerTurn,
        EnemyTurn,
        BattleWon,
        BattleLost
    }
}
