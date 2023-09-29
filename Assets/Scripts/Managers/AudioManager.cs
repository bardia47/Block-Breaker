using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClip
{
    PADDLEHIT,
    BRICKHIT,
    LIFELOST
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;

    public AudioClip paddleHit;
    public AudioClip brickHit;
    public AudioClip lifeLost;

    public Dictionary<SoundClip, AudioClip> soundDictionary
        = new Dictionary<SoundClip, AudioClip>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        soundDictionary.Add(SoundClip.BRICKHIT, brickHit);
        soundDictionary.Add(SoundClip.PADDLEHIT, paddleHit);
        soundDictionary.Add(SoundClip.LIFELOST, lifeLost);
    }

    public static void PlaySound(SoundClip s, float volume = 1f,
        float pitch = 1f, bool loop = false)
    {
        instance.audioSource.clip = instance.soundDictionary[s];
        instance.audioSource.volume = volume;
        instance.audioSource.pitch = pitch;
        instance.audioSource.loop = loop;
        instance.audioSource.Play();
    }
}
