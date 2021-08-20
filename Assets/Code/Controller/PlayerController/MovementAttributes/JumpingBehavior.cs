using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBehavior
{
    private Rigidbody rigidbody;
    private MovementAttributes attributes;
    private PlayerController ownController;
    private MovementInformation moveInfos;

    private JumpingBuffer jumpBuffer = new JumpingBuffer();
    private JumpAktion lastJumpAtempt;
    private float lastTimeOnGrund = 0.0f;

    public JumpingBehavior(Rigidbody iniRigidbody, MovementAttributes iniAttributes, PlayerController iniOwnController)
    {
        rigidbody = iniRigidbody;
        attributes = iniAttributes;
        ownController = iniOwnController;
        moveInfos = new MovementInformation();
    }

    public bool IsJumpInQueue()
    {
        if (jumpBuffer.IsAJumpInQueue())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateJumpingBehavior(MovementInformation newMoveInfos)
    {
        if (newMoveInfos.isJumpingInput)
        {
            lastJumpAtempt = new JumpAktion();

            if (!ownController.IsInAir())
            {
                //Debug.Log("JumpingAkion was Added at: " + Time.realtimeSinceStartup);
                jumpBuffer.Queue(lastJumpAtempt);
            }
            else if (IsCoyoteTime())
            {
                jumpBuffer.Queue(lastJumpAtempt);
            }
        }
        if (IsJumpBuffer())
        {
            jumpBuffer.Queue(new JumpAktion());
        }
    }

    public void ApplyJumpingBehavior() 
    {
        if (jumpBuffer.IsAJumpInQueue())
        {
            if (jumpBuffer.Peek().isJumping)
            {
                ApplyJumping();

            }
            else
            {
                TriggerJump();
            }
        }

        TrackCoyoteTime();
    }

    private void TrackCoyoteTime()
    {
        if (!ownController.IsInAir())
        {
            lastTimeOnGrund = Time.realtimeSinceStartup;
        }
    }

    private void TriggerJump()
    {
        jumpBuffer.Peek().isJumping = true;
        ownController.GetPlayerSoundEvents().PlaySound((int)SoundEnum.jump, Camera.main.GetComponent<AudioSource>());
    }

    private void ApplyJumping()
    {
        if (jumpBuffer.Peek().jumpTime < attributes.jumpRaiseTime)
        {
            float upVelocety = attributes.jumpRaiseCurve.Evaluate((jumpBuffer.Peek().jumpTime / attributes.jumpRaiseTime)) * attributes.maxJumpVelocety;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, upVelocety, rigidbody.velocity.z);
            jumpBuffer.Peek().jumpTime += Time.fixedDeltaTime;
        }
        else
        {
            jumpBuffer.Dequeue();
        }
    }

    private bool IsJumpBuffer()
    {
        if (!ownController.IsInAir() && !jumpBuffer.IsAJumpInQueue() && lastJumpAtempt !=null)
        {
            if (Time.realtimeSinceStartup - lastJumpAtempt.GetTimeStamp() < attributes.jumpBufferTime)
            {
                Debug.Log("JumpBuffer was Added at: " + Time.realtimeSinceStartup);
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
        if (ownController.IsFalling())
        {
            if (Time.realtimeSinceStartup - lastTimeOnGrund < attributes.coyoteTime)
            {
                Debug.Log("CoyoteTime was Added at: " + Time.realtimeSinceStartup);
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
