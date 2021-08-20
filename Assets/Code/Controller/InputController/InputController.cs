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
        currentMovementInfo.inputMovementDir = Vector3.zero;


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            currentMovementInfo.inputMovementDir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currentMovementInfo.inputMovementDir += Vector3.right;
        }
        else
        {
            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                currentMovementInfo.inputMovementDir += Vector3.left;
            }
            else if (Input.GetAxis("Horizontal") > 0.1f)
            {
                currentMovementInfo.inputMovementDir += Vector3.right;
            }
        }

        currentMovementInfo.inputMovementDir.Normalize();
    }

    private void UpdateJumpingInfo()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButton("Jump"))
        {
            currentMovementInfo.isJumpingInput = true;
        }
        else
        {
            currentMovementInfo.isJumpingInput = false;
        }
    }

    public MovementInformation GetMovementInfo()
    {
        return currentMovementInfo;
    }
}
