using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrafiksController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform footTarget;


    private float currentRunningSpeed = 0;

    public void SetAnimation(PlayerAnimationEnum aniEnum)
    {

    }

    public void SetRunningSpeed(float newRunningSpeed)
    {
        currentRunningSpeed = newRunningSpeed;
    }

    public void SetFloorPoint(Vector3 newTarget)
    {
        if (newTarget == Vector3.zero)
        {
            footTarget.position = transform.position + Vector3.down * 0.4f;
        }
        else
        {
            footTarget.position = newTarget;
        }
        
    }
}
