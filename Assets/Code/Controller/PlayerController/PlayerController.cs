using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider colider;
    [SerializeField]
    private Rigidbody rigidbody;

    private MovementBehavior movementBehavior;

    private void Awake()
    {
        movementBehavior = new MovementBehavior(rigidbody);
    }

    public void UpdateMovementBehavior(MovementInformation movementInfo)
    {
        movementBehavior.ApplyMovement(movementInfo);
    }
}
