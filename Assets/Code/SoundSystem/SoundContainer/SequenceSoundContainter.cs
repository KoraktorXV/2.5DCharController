using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SequenceSoundContainer", menuName = "ScriptableObjects/Sound/SequenceSoundContainer", order = 4)]
[System.Serializable]
public class SequenceSoundContainter : BaseSoundContainer
{
    private int currentIndex = 0;
    private bool soundBreak = false;
    private IEnumerator currentEnumerator = null;
    private BaseSoundContainer currentContainer;
    private AudioSource aktiveSource;
    private static System.Timers.Timer timer;

    public List<BaseSoundContainer> soundSequence = new List<BaseSoundContainer>();
    

    public override bool IsPlaying()
    {
        if (soundSequence[currentIndex] != null)
        {
            return soundSequence[currentIndex].IsPlaying();
        }
        else
        {
            return false;
        }        
    }

    public override void PlaySound(AudioSource currentSource)
    {
        currentEnumerator = PlaySoundInSquens(currentSource);
        SoundController.Instance.SoundCoroutineHandler(currentEnumerator);
    }

    private IEnumerator PlaySoundInSquens(AudioSource currentSource)
    {
        for (int i = 0; i < soundSequence.Count; i++)
        {
            if (!soundBreak)
            {
                soundSequence[i].PlaySound(currentSource);
                currentIndex = i;
                while (soundSequence[i].IsPlaying())
                {
                    yield return null;
                }                
            }            
        }
    }

    public override void StopSound()
    {
        foreach (var sound in soundSequence)
        {
            if (sound != null)
            {
                sound.StopSound();
            }
        }
    }
}
