using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHBackgroundMusic : MonoBehaviour
{
    public AudioClip victoryMusic;
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayVictoryMusic()
    {
        source.Stop();
        source.loop = false;
        source.PlayOneShot(victoryMusic);
    }
}
