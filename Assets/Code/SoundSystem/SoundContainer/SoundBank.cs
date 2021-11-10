using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "ScriptableObjects/Sound/SoundBank", order = 2)]
public class SoundBank : ScriptableObject
{
    public List<BaseSoundContainer> soundList = new List<BaseSoundContainer>();

    private Dictionary<int, BaseSoundContainer> soundBank = new Dictionary<int, BaseSoundContainer>();

    public void SetUpSoundBank()
    {
        if (soundList.Count > 0)
        {
            foreach (BaseSoundContainer soundCobtainer in soundList)
            {
                if (!soundBank.ContainsKey((int)soundCobtainer.ObjectID))
                {
                    soundBank.Add((int)soundCobtainer.ObjectID, soundCobtainer);
                }
            }
        }        
    }

    public bool IsPlaying(int soundID)
    {
        BaseSoundContainer currentSound;

        if (soundBank.TryGetValue(soundID, out currentSound))
        {
            return currentSound.IsPlaying();
        }
        else
        {
            return false;
        }
    }

    public void PlaySound(int soundID, AudioSource currentSource)
    {
        BaseSoundContainer currentSound;

        if (soundBank.TryGetValue(soundID, out currentSound))
        {
            currentSound.PlaySound(currentSource);
        }
    }

    public void StopSound(int soundID)
    {
        BaseSoundContainer currentSound;

        if (soundBank.TryGetValue(soundID, out currentSound))
        {
            currentSound.StopSound();
        }
    }
}
