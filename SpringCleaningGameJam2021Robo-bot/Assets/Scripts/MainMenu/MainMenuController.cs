using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public Button startButton;

    string firstGameScene = "Scenes/Introvideo";

    void Start()
    {
        startButton.onClick.AddListener(StartGame);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        StartCoroutine(LoadFirstGameScene());   
    }

    IEnumerator LoadFirstGameScene()
    {

        AsyncOperation Asyncload = SceneManager.LoadSceneAsync(firstGameScene, LoadSceneMode.Single);

        while( !Asyncload.isDone)
        {
            yield return null;
        }
        
    }

    
}
