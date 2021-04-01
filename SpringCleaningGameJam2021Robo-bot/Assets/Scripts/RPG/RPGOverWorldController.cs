using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPGOverWorldController : MonoBehaviour
{
    int playerNumSteps = 0;

    public float randomEncounterChance = 0.01f;

    public GameObject player;

    //Information of last loaded scene: player position and scene path to reload scene
    public static Vector3 playerPos;                                        //position vector. zero vector stands for undefined player position
    public static string overworldScene = "Scenes/RPG/RPGOverworld";

    //Default battle scene to load on random encounters
    string randomEncounterScene = "Scenes/RPG/RPGBattle";

    void Start()
    {
        //Place player on previously loaded position
        if (playerPos != Vector3.zero)
        {
            player.GetComponent<RPGPlayer>().SetStartPosition(playerPos);
        }    
    }

    /*Behaviour every time a player takes a step: update step count chance for a random battle encounter
     * 
     */
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

    /*coroutine to load battle scene for random encounter
     * 
     */
    IEnumerator StartRandomEncounter()
    {
        AsyncOperation Asyncload = SceneManager.LoadSceneAsync(randomEncounterScene, LoadSceneMode.Single);

        while (!Asyncload.isDone)
        {
            yield return null;
        }
    }
}
