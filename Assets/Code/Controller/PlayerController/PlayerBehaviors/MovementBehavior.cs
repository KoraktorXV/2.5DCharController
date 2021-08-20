using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior
{
    private Rigidbody rigidbody;
    private MovementAttributes attributes;
    private PlayerController ownController;

    private MovementInformation moveInfos;

    private JumpAktion OLDjumpBuffer;

    private JumpingBuffer jumpBuffer = new JumpingBuffer();

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

        if (newMoveInfos.isJumpingInput && OLDjumpBuffer == null)
        {
            OLDjumpBuffer = new JumpAktion();
        }
    }

    public void ApplyMovement()
    {
        JumpingBehavior(1);
        ApplyHorizontalForces();
        ApplyHoverForces();
    }

    private void JumpingBehavior()
    {
        if (jumpBuffer.IsAJumpInQueue())
        {
            if (!ownController.IsInAir() &&
                !IsJumpBuffer() &&
                !IsCoyoteTime())
            {
                jumpBuffer.Dequeue();
            }

            if (!jumpBuffer.Peek().isJumping &&
                !ownController.IsInAir())
            {
                jumpBuffer.Peek().isJumping = true;
                ownController.GetPlayerSoundEvents().PlaySound((int)SoundEnum.jump, Camera.main.GetComponent<AudioSource>());
            }

            if (jumpBuffer.Peek().isJumping)
            {
                if (jumpBuffer.Peek().jumpTime < attributes.jumpRaiseTime)
                {
                    float upVelocety = attributes.jumpRaiseCurve.Evaluate((jumpBuffer.Peek().jumpTime / attributes.jumpRaiseTime)) * attributes.maxJumpVelocety;
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, upVelocety, rigidbody.velocity.z);
                    jumpBuffer.Peek().jumpTime += Time.fixedDeltaTime;
                }
            }
        }        
    }

    private void JumpingBehavior(int x) 
     {
        if (OLDjumpBuffer != null && 
            OLDjumpBuffer.isJumping && 
            rigidbody.velocity.y < 0.0f && 
            !ownController.GetIsOutsideDetectionRange() &&
            !IsJumpBuffer(1))
        {
            OLDjumpBuffer = null;
        }

        if (OLDjumpBuffer != null && OLDjumpBuffer.isJumping == false)
        {
            //ApplyJumpingForces();
            OLDjumpBuffer.isJumping = true;
            ownController.GetPlayerSoundEvents().PlaySound((int)SoundEnum.jump, Camera.main.GetComponent<AudioSource>());
        }

        if (OLDjumpBuffer != null && OLDjumpBuffer.isJumping == true)
        {
            if (OLDjumpBuffer.jumpTime < attributes.jumpRaiseTime)
            {
                float upVelocety = attributes.jumpRaiseCurve.Evaluate((OLDjumpBuffer.jumpTime / attributes.jumpRaiseTime)) * attributes.maxJumpVelocety;
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, upVelocety, rigidbody.velocity.z);
                OLDjumpBuffer.jumpTime += Time.fixedDeltaTime;
            }
        }
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
        if (OLDjumpBuffer == null && ownController.GetGrundHitPoint() != Vector3.zero)
        {
            Vector3 currentVelocety = rigidbody.velocity;
            float currentDistanceDelta = ownController.GetDistaceToGrund() - attributes.hoverDistancToGrund;
            float springForce =  -attributes.springConstant * currentDistanceDelta;

            rigidbody.AddForce(Vector3.up * (springForce - rigidbody.velocity.y * attributes.hoverSpringDampener));
        }
    }

    private bool IsJumpBuffer()
    {
        if (jumpBuffer.IsAJumpInQueue() && ownController.IsInAir())
        {
            if (Time.realtimeSinceStartup - jumpBuffer.Peek().GetTimeStamp() < attributes.jumpBufferTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool IsJumpBuffer(int x)
    {
        if (OLDjumpBuffer != null && !ownController.IsInAir())
        {
            if (Time.realtimeSinceStartup - OLDjumpBuffer.GetTimeStamp() < attributes.jumpBufferTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool IsCoyoteTime()
    {
        if (!jumpBuffer.Peek().isJumping && ownController.IsInAir())
        {
            if (Time.realtimeSinceStartup - jumpBuffer.Peek().GetTimeStamp() < attributes.coyoteTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}


