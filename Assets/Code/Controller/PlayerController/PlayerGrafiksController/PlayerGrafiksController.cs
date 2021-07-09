using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrafiksController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private float currentRunningSpeed = 0;

    public void SetAnimation(PlayerAnimationEnum aniEnum)
    {

    }

    public void SetRunningSpeed(float newRunningSpeed)
    {
        currentRunningSpeed = newRunningSpeed;
    }
}
