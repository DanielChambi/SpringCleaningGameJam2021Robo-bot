using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPGOverWorldController : MonoBehaviour
{
    int playerNumSteps = 0;

    float randomEncounterChance = 0.01f;

    public GameObject player;

    //Information of last loaded scene: player position and scene path
    public static Vector3 playerPos;
    public static string overworldScene = "Scenes/RPG/RPGOverworld";

    string randomEncounterScene = "Scenes/RPG/RPGBattle";

    void Start()
    {
        if (playerPos != Vector3.zero)
        {
            player.GetComponent<RPGPlayer>().SetStartPosition(playerPos);
        }    
    }

    public int PlayerSteps()
    {
        playerNumSteps++;

        if(Random.value < randomEncounterChance)
        {
            playerPos = player.transform.position;
            overworldScene = SceneManager.GetActiveScene().path;

            StartCoroutine(StartRandomEncounter());
        }
        
        return playerNumSteps;
    }

    IEnumerator StartRandomEncounter()
    {
        AsyncOperation Asyncload = SceneManager.LoadSceneAsync(randomEncounterScene, LoadSceneMode.Single);

        while (!Asyncload.isDone)
        {
            yield return null;
        }
    }
}
