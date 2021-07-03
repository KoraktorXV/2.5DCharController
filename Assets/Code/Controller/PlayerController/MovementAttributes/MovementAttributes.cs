using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementAttributes", menuName = "ScriptableObjects/MovementAttributes", order = 1)]
public class MovementAttributes : ScriptableObject
{
    public float maxVelocety = 5.0f;
    public float movementForce = 100.0f;
    public float jumpForce = 2000.0f;
}
