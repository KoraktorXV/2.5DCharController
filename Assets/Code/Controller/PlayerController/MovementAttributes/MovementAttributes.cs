using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementAttributes", menuName = "ScriptableObjects/MovementAttributes", order = 1)]
public class MovementAttributes : ScriptableObject
{
    public float maxVelocety = 105.0f;
    public float movementForce = 20.0f;
    //public float accelerationTime = 0.5f;
    //public AnimationCurve accelerationCurve;
    //public float deccelerationTime = 0.5f;
    //public AnimationCurve deccelerationCurve;
    public float inAirRayCastLenght = 0.5f;
    public float jumpForce = 350.0f;
    public float springConstant = 20.0f;
    public float hoverDistancToGrund = 0.5f;
    public float hoverDistancDelta = 0.25f;
    public float hoverSpringDampener = 5.0f;
}
