using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetToFollow;

    public void SetTargetToFollow(GameObject newTarget)
    {
        targetToFollow = newTarget;
        gameObject.transform.parent = targetToFollow.transform;
    }
}
