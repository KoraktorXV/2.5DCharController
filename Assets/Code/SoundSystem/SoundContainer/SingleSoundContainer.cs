using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleSoundContainer", menuName = "ScriptableObjects/Sound/SingleSoundContainer", order = 3)]
public class SingleSoundContainer : BaseSoundContainer
{
    public Sound sound;
    public override void PlaySound(AudioSource currentSource)
    {
        if (sound != null)
        {
            sound.SetupSource(currentSource);
            sound.source.Play();
        }
    }

    public override bool IsPlaying()
    {
        if (sound.source != null)
        {
            return sound.source.isPlaying;
        }
        else
        {
            return false;
        }
    }

    public override void StopSound()
    {
        if (sound.source != null)
        {
            sound.source.Stop();
        }
    }
}
