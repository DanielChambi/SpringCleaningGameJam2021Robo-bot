using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BHGameController : MonoBehaviour
{
    public GameObject player;
    public Text playerHp;

    public AudioSource backgroundSource;
    //text for provisional end screen
    public Text youWonText;
    

    bool winCondition = false;
    
    public void WinConditionMet()
    {
        winCondition = true;
        //provisional indicator
        backgroundSource.GetComponent<BHBackgroundMusic>().PlayVictoryMusic();
        youWonText.gameObject.SetActive(true);

    }

    private void OnGUI()
    {
        playerHp.text = player.GetComponent<BHPlayer>().HpToString();
    }
}
