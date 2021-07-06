using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior
{
    private Rigidbody rigidbody;
    private MovementAttributes attributes;

    public MovementBehavior(Rigidbody iniRigidbody, MovementAttributes iniAttributes)
    {
        rigidbody = iniRigidbody;
        attributes = iniAttributes;
    }

    public void ApplyMovement(MovementInformation moveInfo)
    {
        ApplyForces(moveInfo);
    }

    private void ApplyForces(MovementInformation moveInfo)
    {
        if (moveInfo.isJumping)
        {
             rigidbody.AddForce(Vector3.up * attributes.jumpForce);

            moveInfo.isJumping = false;
        }

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

    private void LimitVelocety()
    {
        if (rigidbody.velocity.magnitude > attributes.maxVelocety)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * attributes.maxVelocety;
        }
    }
}
