using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior
{
    private Rigidbody rigidbody;
    private MovementAttributes attributes;
    private PlayerController ownController;

    private MovementInformation moveInfos;

    private JumpAktion jumpBuffer;

    public MovementBehavior(Rigidbody iniRigidbody, MovementAttributes iniAttributes, PlayerController iniOwnController)
    {
        rigidbody = iniRigidbody;
        attributes = iniAttributes;
        ownController = iniOwnController;
        moveInfos = new MovementInformation();
    }

    public void UpdateMovementInfo(MovementInformation newMoveInfos)
    {
        moveInfos = newMoveInfos;

        if (newMoveInfos.isJumpingInput && jumpBuffer == null)
        {
            jumpBuffer = new JumpAktion();
        }
    }

    public void ApplyMovement()
    {
        JumpingBehavior();
        ApplyHorizontalForces(moveInfos);
        ApplyHoverForces();
    }

    private void JumpingBehavior() 
     {
        if (jumpBuffer != null && jumpBuffer.isJumping && rigidbody.velocity.y < 0.0f && !ownController.GetIsOutsideDetectionRange())
        {
            jumpBuffer = null;
        }

        if (jumpBuffer != null && jumpBuffer.isJumping == false)
        {
            ApplyJumpingForces();
            jumpBuffer.isJumping = true;
            ownController.GetPlayerSoundEvents().PlaySound((int)SoundEnum.jump, Camera.main.GetComponent<AudioSource>());
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
        if (jumpBuffer == null && ownController.GetGrundHitPoint() != Vector3.zero)
        {
            Vector3 currentVelocety = rigidbody.velocity;
            float currentDistanceDelta = ownController.GetDistaceToGrund() - attributes.hoverDistancToGrund;
            float springForce =  -attributes.springConstant * currentDistanceDelta;

            rigidbody.AddForce(Vector3.up * (springForce - rigidbody.velocity.y * attributes.hoverSpringDampener));
        }
    }
}
