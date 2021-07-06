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
    [SerializeField]
    private Transform physiksUtilityObj;
    [SerializeField]
    private float maxInAirDistance = 0.5f;
    [SerializeField]
    private float maxHoverDistance = 0.25f;

    private MovementBehavior movementBehavior;
    private bool isInAir = false;
    private bool isOutsideDetectionRange = false;
    private float distanceToGrund = 0.0f;

    private void Awake()
    {
        movementBehavior = new MovementBehavior(rigidbody, attributes, this);
    }

    public void UpdatePlayerController(MovementInformation movementInfo)
    {
        UpdateController();
        movementBehavior.ApplyMovement(movementInfo);
    }

    private void UpdateController()
    {
        UpdateRaycastInfos();
        Debug.Log("isOutsideDetectionRange: " + isOutsideDetectionRange + "\ndistanceToGrund: " + distanceToGrund + "\nisInAir: " + isInAir);
    }


    public void UpdateRaycastInfos()
    {
        RaycastHit hit;
        Ray inAirRay = new Ray(physiksUtilityObj.position, Vector3.down);

        if (Physics.Raycast(inAirRay, out hit, maxInAirDistance))
        {
            isOutsideDetectionRange = false;
            distanceToGrund = (hit.point - physiksUtilityObj.position).magnitude;
            isInAir = distanceToGrund > maxHoverDistance ? true : false;
        }
        else
        {
            isInAir = true;
            distanceToGrund = float.MinValue;
            isOutsideDetectionRange = true;
        }
    }


    public bool GetIsInAir()
    {
        return isInAir;
    }

    public bool GetIsOutsideDetectionRange()
    {
        return isOutsideDetectionRange;
    }

    public float GetDistaceToGrund()
    {
        return distanceToGrund;
    }

    private void OnDrawGizmos()
    {
        if (rigidbody)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + rigidbody.velocity * Time.fixedDeltaTime * 5);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * maxInAirDistance);

        }
    }
}
