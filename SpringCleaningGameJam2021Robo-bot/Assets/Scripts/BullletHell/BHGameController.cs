using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BHGameController : MonoBehaviour
{
    public GameObject player;
    public Text playerHp;
    public Image blackOutSquare;


    float blackOutFadeSpeed = 1;
    public AudioSource backgroundSource;
    //text for provisional end screen
    public Text youWonText;
    

    bool winCondition = false;

    private void Start()
    {
        StartCoroutine(BlackOutFadeIn());
    }

    public void WinConditionMet()
    {
        winCondition = true;
        //provisional indicator
        backgroundSource.GetComponent<BHBackgroundMusic>().PlayVictoryMusic();
        youWonText.gameObject.SetActive(true);
        Invoke("LoadCredits", 3);

    }

    public void ReloadScene()
    {
        backgroundSource.Stop();
        StartCoroutine(BlackOutFadeOut());
    }

    void LoadCredits()
    {
        StartCoroutine(BlackOutFadeOut("Scenes/CreditsScene"));
    }
    
    private void OnGUI()
    {
        if(player != null)
        {
            playerHp.text = player.GetComponent<BHPlayer>().HpToString();
        } else
        {
            playerHp.text = "0 / 0";
        }
        
    }

    IEnumerator BlackOutFadeIn()
    {
        float imageAlpha = blackOutSquare.color.a;
        Color color = blackOutSquare.color;
        while(imageAlpha > 0)
        {
            imageAlpha -= Time.deltaTime * blackOutFadeSpeed;
            blackOutSquare.color = new Color(color.r, color.g, color.b, imageAlpha);
            yield return null;
        }
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator BlackOutFadeOut(string scenePath)
    {
        float imageAlpha = blackOutSquare.color.a;
        Color color = blackOutSquare.color;
        while (imageAlpha < 1)
        {
            imageAlpha += Time.deltaTime * blackOutFadeSpeed;
            blackOutSquare.color = new Color(color.r, color.g, color.b, imageAlpha);
            yield return null;
        }

        SceneManager.LoadScene(scenePath);
    }
}
