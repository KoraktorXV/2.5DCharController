using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInformation
{
    public Vector3 inputMovementDir = new Vector3();
    public bool isJumpingInput = false;
    public MovementInformation()
    {
    }

    public MovementInformation(MovementInformation iniInfos)
    {
        inputMovementDir = new Vector3( iniInfos.inputMovementDir.x, 
                                        iniInfos.inputMovementDir.y, 
                                        iniInfos.inputMovementDir.z);
        isJumpingInput = iniInfos.isJumpingInput;
    }
}
