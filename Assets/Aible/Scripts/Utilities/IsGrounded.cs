using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] bool drawRay = true;

    [SerializeField] Transform startPos;
    [SerializeField] float boxCastDistance;
    [SerializeField] LayerMask groundLayer;

    public bool isGrounded()
    {
        return Physics.CheckSphere(startPos.position, boxCastDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (!drawRay)
            return;

        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(startPos.position, boxCastDistance);
    }
}
