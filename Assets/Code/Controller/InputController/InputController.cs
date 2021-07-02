using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController 
{
    private MovementInformation currentMovementInfo = new MovementInformation();
    public void UpdateInput()
    {
        UpdateMovementDirection();
        UpdateJumpingInfo();
    }

    private void UpdateMovementDirection()
    {
        currentMovementInfo.movementDir = Vector3.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            currentMovementInfo.movementDir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currentMovementInfo.movementDir += Vector3.right;
        }

        currentMovementInfo.movementDir.Normalize();
    }

    private void UpdateJumpingInfo()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMovementInfo.isJumping = true;
        }
    }

    public MovementInformation GetMovementInfo()
    {
        return currentMovementInfo;
    }
}
