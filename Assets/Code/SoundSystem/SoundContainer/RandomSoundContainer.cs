using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSoundContainer", menuName = "ScriptableObjects/Sound/RandomSoundContainer", order = 5)]
public class RandomSoundContainer : BaseSoundContainer
{
    private int lastPlayedIndex = 0;

    public List<BaseSoundContainer> soundRandomSequence = new List<BaseSoundContainer>();

    public override bool IsPlaying()
    {
        if (soundRandomSequence[lastPlayedIndex] != null)
        {
            return soundRandomSequence[lastPlayedIndex].IsPlaying();
        }
        else
        {
            return false;
        }
    }

    public override void PlaySound(AudioSource currentSource)
    {
        lastPlayedIndex = Random.Range(0, soundRandomSequence.Count);
        if (soundRandomSequence[lastPlayedIndex] != null)
        {
            soundRandomSequence[lastPlayedIndex].PlaySound(currentSource);
        }        
    }

    public override void StopSound()
    {
        foreach (var sound in soundRandomSequence)
        {
            if (sound != null)
            {
                sound.StopSound();
            }            
        }
    }
}
