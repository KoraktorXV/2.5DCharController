using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAktion
{
    public bool isJumping = false;
    public float jumpTime = 0.0f;
    private float jumpAktionTimeStamp = 0.0f;

    public JumpAktion() 
    {
        jumpAktionTimeStamp = Time.realtimeSinceStartup;
    }

    public float GetTimeStamp()
    {
        return jumpAktionTimeStamp;
    }
}
