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
    private float timeSinceFall = 0.0f;

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
            Debug.Log("JumpingAkion was Added at: " + Time.realtimeSinceStartup);
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
                /*
                if (!ownController.IsInAir())
                {
                    if (IsJumpBuffer() && jumpBuffer.PeekDeeper() != null)
                    {
                        jumpBuffer.PeekDeeper().isJumping = true;
                    }
                }
                */
            }
            else
            {
                if (ownController.IsInAir())
                {
                    if (IsCoyoteTime())
                    {
                        TriggerJump();
                    }
                }
                else
                {
                    TriggerJump();
                }
            }
        }

        TrackCoyoteTime();
    }

    private void TrackCoyoteTime()
    {
        if (ownController.IsFalling() && ownController.IsInAir() && !jumpBuffer.IsAJumpInQueue() && timeSinceFall == 0.0f)
        {
            timeSinceFall = Time.realtimeSinceStartup;
        }
        else
        {
            timeSinceFall = 0.0f;
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

    private bool IsCoyoteTime()
    {
        if (jumpBuffer.IsAJumpInQueue() && ownController.IsFalling())
        {
            if (!jumpBuffer.Peek().isJumping)
            {
                if (Time.realtimeSinceStartup - timeSinceFall < attributes.coyoteTime)
                {
                    Debug.Log("DeltaTime for CoyoteTime was: " + (Time.realtimeSinceStartup - timeSinceFall));
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
        else
        {
            return false;
        }

    }
}
