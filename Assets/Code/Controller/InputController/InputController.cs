using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController 
{
    private MovementInformation currentMovementInfo = new MovementInformation();

    PlayerInputaktions inputAktions = new PlayerInputaktions();

    public InputController()
    {
        inputAktions.Player.Enable();
    }

    public void UpdateInput()
    {
        UpdateMovementDirection();
        UpdateJumpingInfo();
    }

    private void UpdateMovementDirection()
    {
        currentMovementInfo.inputMovementDir = inputAktions.Player.Movement.ReadValue<Vector2>();

        currentMovementInfo.inputMovementDir.Normalize();
    }

    private void UpdateJumpingInfo()
    {               
        if (inputAktions.Player.Jumping.ReadValue<float>() == 1)
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
