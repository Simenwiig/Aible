using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    Rigidbody rb;

    public float multiplier = 2.4f;
    public float moveForce = 100f;
    public float turnTorque = 10;

    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    [SerializeField] LayerMask ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < anchors.Length; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }

        rb.AddForce(moveForce * transform.forward);
        rb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);
    }

    void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit, ground))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            rb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
        }
    }
}
