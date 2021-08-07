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
    private Vector3 lasteMoveDir = new Vector3();

    public void SetAnimation(PlayerJumpAniEnum aniEnum)
    {
        animator.SetInteger("jumpPhase", (int)aniEnum);
    }

    public void SetRunningSpeed(float newRunningSpeed)
    {
        currentRunningSpeed = newRunningSpeed;
        animator.SetFloat("speed", newRunningSpeed);
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

    public void SetTrundirection(Vector3 movementDir)
    {
        if (Vector3.Dot(movementDir.normalized, lasteMoveDir) != 1)
        {
            if (movementDir.x > 0)
            {
                SetAnimation(PlayerJumpAniEnum.turnRigth);
            }
            else
            {
                SetAnimation(PlayerJumpAniEnum.turnRigth);
            }
        }

        lasteMoveDir = movementDir.normalized;
    }
}
