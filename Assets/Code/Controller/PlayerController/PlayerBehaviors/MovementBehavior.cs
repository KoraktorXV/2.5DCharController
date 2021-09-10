using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior
{
    private Rigidbody rigidbody;
    private MovementAttributes attributes;
    private PlayerController ownController;

    private JumpingBehavior jumpingBehavior;

    private MovementInformation moveInfos;

    public MovementBehavior(Rigidbody iniRigidbody, MovementAttributes iniAttributes, PlayerController iniOwnController)
    {
        rigidbody = iniRigidbody;
        attributes = iniAttributes;
        ownController = iniOwnController;
        jumpingBehavior = new JumpingBehavior(iniRigidbody, iniAttributes, iniOwnController);
        moveInfos = new MovementInformation();
    }

    public void UpdateMovementInfo(MovementInformation newMoveInfos)
    {
        moveInfos = newMoveInfos;

        jumpingBehavior.UpdateJumpingBehavior(newMoveInfos);
    }

    public void ApplyMovement()
    {
        jumpingBehavior.ApplyJumpingBehavior();
        ApplyHorizontalForces();
        ApplyHoverForces();
        ApplyVertictalForces();
    }

    private void ApplyHorizontalForces()
    {
        Vector3 horizontalVelocity = new Vector3(rigidbody.velocity.x, 0, 0);
        bool isMoveDirInVelocetyDir = Vector3.Dot(horizontalVelocity.normalized, moveInfos.inputMovementDir) == 1;
        bool isHorizontalVelocityToBig = horizontalVelocity.magnitude > attributes.maxVelocety;

        if (moveInfos.inputMovementDir.magnitude > 0)
        {
            if (!isHorizontalVelocityToBig || (isHorizontalVelocityToBig && !isMoveDirInVelocetyDir))
            {
                rigidbody.AddForce(moveInfos.inputMovementDir.normalized * attributes.movementForce);
            }
        }
        else
        {
            float dragFactor = 0.5f * (rigidbody.velocity.magnitude / attributes.maxVelocety) * attributes.dragConstand;
            Vector3 dragForce = -new Vector3(rigidbody.velocity.x, 0, 0).normalized * dragFactor;
            rigidbody.AddForce(dragForce);
        }
    }

    private void ApplyHoverForces()
    {
        if (!jumpingBehavior.IsJumpInQueue() && ownController.GetGrundHitPoint() != Vector3.zero)
        {
            Vector3 currentVelocety = rigidbody.velocity;
            float currentDistanceDelta = ownController.GetDistaceToGrund() - attributes.hoverDistancToGrund;
            float springForce = -attributes.springConstant * currentDistanceDelta;

            rigidbody.AddForce(Vector3.up * (springForce - rigidbody.velocity.y * attributes.hoverSpringDampener));
        }
    }

    private void ApplyVertictalForces()
    {
        if (ownController.IsWallsliding() && !jumpingBehavior.IsJumpInQueue())
        {
            rigidbody.velocity *= attributes.wallSlipinis;
        }
    }

}


