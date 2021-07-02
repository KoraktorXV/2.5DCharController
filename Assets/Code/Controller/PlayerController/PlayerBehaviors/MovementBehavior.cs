using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior
{
    private Rigidbody rigidbody;

    private float maxVelocety = 5.0f;
    private float movementForce = 100.0f;
    private float jumpForce = 2000.0f;

    public MovementBehavior(Rigidbody iniRigidbody)
    {
        rigidbody = iniRigidbody;
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
            rigidbody.AddForce(Vector3.up * jumpForce);

            moveInfo.isJumping = false;
        }

        if (moveInfo.movementDir.magnitude > 0)
        {
            rigidbody.AddForce(moveInfo.movementDir.normalized * movementForce);
        }
    }

    private void LimitVelocety()
    {
        if (rigidbody.velocity.magnitude > maxVelocety)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocety;
        }
    }
}
