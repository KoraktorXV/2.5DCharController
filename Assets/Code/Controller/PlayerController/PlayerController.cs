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

    private MovementBehavior movementBehavior;
    private MovementInformation lastMovementInformation;
    private bool isInAir = false;
    private bool isOutsideDetectionRange = false;
    private float distanceToGrund = 0.0f;
    private Vector3 grundHitPoint = new Vector3(); 

    private void Awake()
    {
        movementBehavior = new MovementBehavior(rigidbody, attributes, this);
    }

    public void UpdatePlayerController(MovementInformation movementInfo)
    {
        UpdateController(movementInfo);
        movementBehavior.ApplyMovement(movementInfo);
    }

    private void UpdateController(MovementInformation movementInfo)
    {
        UpdateRaycastInfos();
        lastMovementInformation = new MovementInformation(movementInfo);
        Debug.Log("isOutsideDetectionRange: " + isOutsideDetectionRange + "\ndistanceToGrund: " + distanceToGrund + "\nisInAir: " + isInAir);
    }


    public void UpdateRaycastInfos()
    {
        RaycastHit hit;
        Ray inAirRay = new Ray(physiksUtilityObj.position, Vector3.down);

        if (Physics.Raycast(inAirRay, out hit, attributes.inAirRayCastLenght))
        {
            isOutsideDetectionRange = false;
            grundHitPoint = hit.point;
            distanceToGrund = (grundHitPoint - physiksUtilityObj.position).magnitude;
            isInAir = distanceToGrund < attributes.hoverDistancToGrund + attributes.hoverDistancDelta ? false : true;
        }
        else
        {
            isInAir = true;
            grundHitPoint = Vector3.zero;
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

    public Vector3 GetGrundHitPoint()
    {
        return grundHitPoint;
    }

    private void OnDrawGizmos()
    {
        if (rigidbody && attributes)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + rigidbody.velocity * Time.fixedDeltaTime * 5);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * attributes.inAirRayCastLenght);

        }
    }
}
