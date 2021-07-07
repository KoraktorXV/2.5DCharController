using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pich = 1;

    public bool randPich = false;
    [Range(3, 0)]
    public float minPichRandRange = 0;
    [Range(0, 3)]
    public float maxPichRandRange = 0;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void SetupSource(AudioSource currentSource)
    {
        source = currentSource;

        source.clip = clip;

        source.volume = volume;

        SetPich();        

        source.loop = loop;
    }

    private void SetPich() 
    {
        if (randPich)
        {
            source.pitch = GetRandPich();
        }
        else
        {
            source.pitch = pich;
        }
    }

    private float GetRandPich()
    {
        if (pich - minPichRandRange <= 0)
        {
            minPichRandRange = pich - 0.1f;
        }
        return Random.Range(pich - minPichRandRange, pich + maxPichRandRange);
    }
}
