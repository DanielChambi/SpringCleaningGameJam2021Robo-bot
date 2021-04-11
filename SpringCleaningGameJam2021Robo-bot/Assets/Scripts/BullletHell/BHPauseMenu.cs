using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BHPauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Image blackOutSquare;
    float blackOutFadeSpeed = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuActivate();
        }
    }


    public void PauseMenuActivate()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(!pausePanel.activeSelf);
        StartCoroutine(BlackOutFadeOut());
    }

    IEnumerator BlackOutFadeOut()
    {
        float imageAlpha = blackOutSquare.color.a;
        Color color = blackOutSquare.color;
        while (imageAlpha < 1)
        {
            imageAlpha += Time.deltaTime * blackOutFadeSpeed;
            blackOutSquare.color = new Color(color.r, color.g, color.b, imageAlpha);
            yield return null;
        }

        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
