using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundContainer : ScriptableObject
{
    public SoundEnum ObjectID;
    

    public virtual void PlaySound(AudioSource currentSource)
    {

    }

    public virtual bool IsPlaying()
    {
        return false;
    }

    public virtual void StopSound()
    {

    }
}
