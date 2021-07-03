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

        LimitVelocety();
    }

    private void ApplyForces(MovementInformation moveInfo)
    {
        if (moveInfo.isJumping)
        {
            rigidbody.AddForce(Vector3.up * attributes.jumpForce);

            moveInfo.isJumping = false;
        }

        if (moveInfo.movementDir.magnitude > 0)
        {
            rigidbody.AddForce(moveInfo.movementDir.normalized * attributes.movementForce);
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
