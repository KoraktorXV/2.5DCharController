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
    public float jumpBufferTime = 0.66f;
    public float coyoteTime = 0.66f;
    [Header("Hovering")]
    public float inAirRayCastLenght = 0.5f;
    public float springConstant = 100.0f;
    public float hoverDistancToGrund = 0.5f;
    public float hoverDistancDelta = 0.25f;
    public float hoverSpringDampener = 15.0f;
    [Header("Wall Slide")]
    public float wallDetectionSorceOffset = 0.2f;
    public float wallDetectionLenght = 0.5f;
    public float wallSlipinis = 0.2f;
}
