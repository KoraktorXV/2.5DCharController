using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController SoundControllerInstance;
    private static readonly object padlock = new object();
    public static SoundController Instance
    {
        get
        {
            lock (padlock)
            {
                if (SoundControllerInstance == null)
                {
                    SoundControllerInstance = (SoundController)FindObjectOfType(typeof(SoundController));

                    if (SoundControllerInstance == null)
                    {
                        var singletonObject = new GameObject();
                        SoundControllerInstance = singletonObject.AddComponent<SoundController>();
                        singletonObject.name = typeof(SoundController).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return SoundControllerInstance;
            }
        }
    }

    private SoundController() { }

    public SoundEventSystem soundEvents;
    public SoundBank soundBank;

    private void Awake()
    {
        soundBank.SetUpSoundBank();
        soundEvents.playSoundEvent += PlaySound;
        soundEvents.stopSoundEvent += StopSound;
        soundEvents.isSoundPlaying += IsSoundPlaying;
    }

    private void OnDestroy()
    {
        soundEvents.playSoundEvent -= PlaySound;
        soundEvents.stopSoundEvent -= StopSound;
        soundEvents.isSoundPlaying -= IsSoundPlaying;
    }

    private void PlaySound(int SoundID, AudioSource audioSource)
    {
        if (soundBank)
        {
            soundBank.PlaySound(SoundID, audioSource);
        }
    }

    private void StopSound(int SoundID)
    {
        if (soundBank)
            soundBank.StopSound(SoundID);
    }

    private bool IsSoundPlaying(int SoundID)
    {
        if (soundBank)
        {
            return soundBank.IsPlaying(SoundID);
        }            
        else
        {
            return false;
        }
    }

    public void SoundCoroutineHandler(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
