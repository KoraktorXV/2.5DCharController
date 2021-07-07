using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Events/SoundEvents")]
public class SoundEventSystem : ScriptableObject
{
    public event Action<int, AudioSource> playSoundEvent;
    public void PlaySound(int soundID, AudioSource audioSource)
    {
        if (playSoundEvent != null)
        {
            playSoundEvent(soundID, audioSource);
        }
    }

    public event Action<int> stopSoundEvent;
    public void StopSound(int soundID)
    {
        if (playSoundEvent != null)
        {
            stopSoundEvent(soundID);
        }
    }

    public Func<int, bool> isSoundPlaying;
    public bool IsSoundPlaying(int soundID)
    {
        if (isSoundPlaying != null)
        {
            return isSoundPlaying(soundID);
        }

        return false;
    }
}
