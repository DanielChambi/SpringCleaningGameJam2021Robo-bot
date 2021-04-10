using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHSoundManager : MonoBehaviour
{
    public AudioClip PlayerShoot;
    public AudioClip PlayerHit;

    public AudioClip BossShoot;
    public AudioClip BossAttackStart;
    public AudioClip BossDefeat;

    public AudioClip ProjectileBagExplode;

    

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayClip(SoundClip clip)
    {
        switch (clip)
        {
            case SoundClip.PlayerShoot:
                source.PlayOneShot(PlayerShoot);
                break;
            case SoundClip.PlayerHit:
                source.PlayOneShot(PlayerHit);
                break;
            case SoundClip.BossShoot:
                source.PlayOneShot(BossShoot);
                break;
            case SoundClip.BossAttackStart:
                source.PlayOneShot(BossAttackStart);
                break;
            case SoundClip.BossDefeat:
                source.PlayOneShot(BossDefeat);
                break;
            case SoundClip.ProjectileBagExplode:
                source.PlayOneShot(ProjectileBagExplode);
                break;
            default:
                break;
        }
    }

    public enum SoundClip
    {
        Null,
        PlayerShoot,
        PlayerHit,
        BossShoot,
        BossAttackStart,
        BossDefeat,
        ProjectileBagExplode
    }
}
