using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior
{
    private Rigidbody rigidbody;
    private MovementAttributes attributes;
    private PlayerController ownController;

    private bool isJumping = false;

    public MovementBehavior(Rigidbody iniRigidbody, MovementAttributes iniAttributes, PlayerController iniOwnController)
    {
        rigidbody = iniRigidbody;
        attributes = iniAttributes;
        ownController = iniOwnController;
    }

    public void ApplyMovement(MovementInformation moveInfo)
    {
        JumpingBehavior(moveInfo);
        ApplyHorizontalForces(moveInfo);
        ApplyHoverForces();
    }

    private void JumpingBehavior(MovementInformation moveInfo) 
    {
        if (moveInfo.isJumpingInput && !isJumping)
        {
            ApplyJumpingForces();
            isJumping = true;
        }

        if (isJumping && rigidbody.velocity.y < 0.0f && !ownController.GetIsOutsideDetectionRange())
        {
            isJumping = false;
        }
        
    }

    private void ApplyJumpingForces()
    {
        rigidbody.AddForce(Vector3.up * attributes.jumpForce);        
    }

    private void ApplyHorizontalForces(MovementInformation moveInfo)
    {
        Vector3 horizontalVelocity = new Vector3(rigidbody.velocity.x, 0, 0);
        bool isMoveDirInVelocetyDir = Vector3.Dot(horizontalVelocity.normalized, moveInfo.inputMovementDir) == 1;
        bool isHorizontalVelocityToBig = horizontalVelocity.magnitude > attributes.maxVelocety;

        if (moveInfo.inputMovementDir.magnitude > 0)
        {
            if (!isHorizontalVelocityToBig || (isHorizontalVelocityToBig && !isMoveDirInVelocetyDir))
            {
                rigidbody.AddForce(moveInfo.inputMovementDir.normalized * attributes.movementForce);
            }
        }
    }

    private void ApplyHoverForces()
    {


    }
}
