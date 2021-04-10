using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CredtisController : MonoBehaviour
{
    public Image blackOutSquare;
    public VideoPlayer player;

    public string loadScenePath;

    float blackOutFadeSpeed = 1;
    void Start()
    {
        player.loopPointReached += VideoEnd;
        StartCoroutine(BlackOutFadeIn());
    }

    void VideoEnd(VideoPlayer vp)
    {
        StartCoroutine(BlackOutFadeOut());
    }

    IEnumerator BlackOutFadeIn()
    {
        float imageAlpha = blackOutSquare.color.a;
        Color color = blackOutSquare.color;
        while (imageAlpha > 0)
        {
            imageAlpha -= Time.deltaTime * blackOutFadeSpeed;
            blackOutSquare.color = new Color(color.r, color.g, color.b, imageAlpha);
            yield return null;
        }

        player.Play();
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

        SceneManager.LoadScene(loadScenePath);
    }
}
