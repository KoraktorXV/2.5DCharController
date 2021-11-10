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
    private PlayerGrafiksController grafiksController;
    [SerializeField]
    private SoundEventSystem soundEvents;

    private MovementBehavior movementBehavior;
    private bool isInAir = false;
    private bool isOutsideHoverDetecRange = false;
    private float distanceToGrund = 0.0f;
    private Vector3 grundHitPoint = new Vector3();
    private Vector3 rigthWallHitpoint = new Vector3();
    private Vector3 leftWallHitpoint = new Vector3();

    private void Awake()
    {
        movementBehavior = new MovementBehavior(rigidbody, attributes, this);
    }

    public void UpdateMovementInfo(MovementInformation movementInfo)
    {
        movementBehavior.UpdateMovementInfo(movementInfo);
    }

    public void UpdatePlayerController()
    {
        UpdateController();
        movementBehavior.ApplyMovement();
        UpdateGraficsController();
    }

    private void UpdateController()
    {
        UpdateRaycastInfos();
        //Debug.Log("isOutsideDetectionRange: " + isOutsideDetectionRange + "\ndistanceToGrund: " + distanceToGrund + "\nisInAir: " + isInAir);
    }

    private void UpdateGraficsController()
    {
        grafiksController.SetFloorPoint(GetGrundHitPoint());
        grafiksController.SetRunningSpeed(1/attributes.maxVelocety * rigidbody.velocity.magnitude);
        grafiksController.SetTrundirection(rigidbody.velocity);

    }

    public void UpdateRaycastInfos()
    {
        UpdateIsInAir();
        UpdateIsOnWall();
    }
    
    private void UpdateIsInAir()
    {
        RaycastHit hit;
        Ray inAirRay = new Ray(physiksUtilityObj.position, Vector3.down);

        if (Physics.Raycast(inAirRay, out hit, attributes.inAirRayCastLenght))
        {
            isOutsideHoverDetecRange = false;
            grundHitPoint = hit.point;
            distanceToGrund = (grundHitPoint - physiksUtilityObj.position).magnitude;
            isInAir = distanceToGrund < attributes.hoverDistancToGrund + attributes.hoverDistancDelta ? false : true;
        }
        else
        {
            isInAir = true;
            grundHitPoint = Vector3.zero;
            distanceToGrund = float.MinValue;
            isOutsideHoverDetecRange = true;
        }
    }

    private void UpdateIsOnWall()
    {
        RaycastHit rigthHit;
        Ray rigthRay = new Ray(transform.position + Vector3.up * attributes.wallDetectionSorceOffset, Vector3.right);

        RaycastHit leftHit;
        Ray leftRay = new Ray(transform.position + Vector3.up * attributes.wallDetectionSorceOffset, Vector3.left);

        if (Physics.Raycast(rigthRay, out rigthHit, attributes.wallDetectionLenght))
        {
            rigthWallHitpoint = rigthHit.point;
        }
        else
        {
            rigthWallHitpoint = Vector3.zero;
        }

        if (Physics.Raycast(leftRay, out leftHit, attributes.wallDetectionLenght))
        {
            leftWallHitpoint = leftHit.point;
        }
        else
        {
            leftWallHitpoint = Vector3.zero;
        }
    }

    public Vector3 GetClosesWallDir()
    {
        if (rigthWallHitpoint.magnitude == 0 && leftWallHitpoint.magnitude == 0)
        {
            return new Vector3();
        }
        else
        {
            if (rigthWallHitpoint.magnitude > leftWallHitpoint.magnitude)
            {
                return rigthWallHitpoint;
            }
            else
            {
                return leftWallHitpoint;
            }
        }
        
    }

    public bool IsInAir()
    {
        return isInAir;
    }

    public bool IsFalling()
    {
        if (IsInAir() &&
            rigidbody.velocity.y < 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsWallsliding()
    {
        if (IsInAir() && GetClosesWallDir().magnitude > 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIsOutsideDetectionRange()
    {
        return isOutsideHoverDetecRange;
    }

    public float GetDistaceToGrund()
    {
        return distanceToGrund;
    }

    public Vector3 GetGrundHitPoint()
    {
        return grundHitPoint;
    }

    public SoundEventSystem GetPlayerSoundEvents()
    {
        return soundEvents;
    }

    private void OnDrawGizmos()
    {
        if (rigidbody && attributes)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + rigidbody.velocity * Time.fixedDeltaTime * 5);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * attributes.inAirRayCastLenght);

            Gizmos.color = Color.blue;
            Vector3 sorce = transform.position + Vector3.up * attributes.wallDetectionSorceOffset;
            Gizmos.DrawLine(sorce, sorce + Vector3.right * attributes.wallDetectionLenght);
            Gizmos.DrawLine(sorce, sorce + Vector3.left * attributes.wallDetectionLenght);
        }
    }
}
