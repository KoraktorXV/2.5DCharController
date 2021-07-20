using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementAttributes", menuName = "ScriptableObjects/MovementAttributes", order = 1)]
public class MovementAttributes : ScriptableObject
{
    [Header("Movement")]
    public float maxVelocety = 10.0f;
    public float movementForce = 50.0f;
    public float dragConstand = 30.0f;
    [Header("Jumping")]
    public float jumpForce = 350.0f;
    public float jumpRaiseTime = 0.25f;
    public float maxJumpVelocety = 10.0f;
    public AnimationCurve jumpRaiseCurve;
    [Header("Hovering")]
    public float inAirRayCastLenght = 0.5f;
    public float springConstant = 100.0f;
    public float hoverDistancToGrund = 0.5f;
    public float hoverDistancDelta = 0.25f;
    public float hoverSpringDampener = 15.0f;
}
