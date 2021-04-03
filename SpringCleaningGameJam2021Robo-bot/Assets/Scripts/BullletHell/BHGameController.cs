using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BHGameController : MonoBehaviour
{
    public GameObject player;
    public Text playerHp;
    bool winCondition { get; }

    private void OnGUI()
    {
        playerHp.text = player.GetComponent<BHPlayer>().HpToString();
    }
}
