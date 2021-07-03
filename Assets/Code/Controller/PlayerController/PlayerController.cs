using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider colider;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private MovementAttributes attributes;
    private MovementBehavior movementBehavior;

    private void Awake()
    {
        movementBehavior = new MovementBehavior(rigidbody, attributes);
    }

    public void UpdateMovementBehavior(MovementInformation movementInfo)
    {
        movementBehavior.ApplyMovement(movementInfo);
    }
}
