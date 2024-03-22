using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.Sample.PoseTracking;
using Mediapipe;
using Mediapipe.Unity;
using Color = UnityEngine.Color;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] private DrawPoseLandmark _drawPoseLandmark;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _reachItemLayer;

    private LandmarkPoints _bodyPoints;

    private Vector3 _rightOrigin;
    private Vector3 _leftOrigin;

    private void Update()
    {
        _bodyPoints = _drawPoseLandmark.BodyPoints;

        if (_bodyPoints == null || _drawPoseLandmark.IsBodyRemovedFromCamera)
            return;

        //camera
        Camera camera = Camera.main;

        // Right arm detector
        _rightOrigin = new Vector3(_bodyPoints.Points[16].transform.position.x, _bodyPoints.Points[16].transform.position.y, 0);       
        Collider[] rightCollider = Physics.OverlapCapsule(camera.transform.position, _rightOrigin, _radius, _reachItemLayer, QueryTriggerInteraction.Collide);
        if (rightCollider.Length > 0)
        {
            if (rightCollider[0].gameObject.tag == "ReachItem")
            {
                ItemDetected(rightCollider[0]);
            }
            else
            {
                MobDetected(rightCollider[0]);
            }
        }

        // Left arm detector
        _leftOrigin = _bodyPoints.Points[15].transform.position;
        Collider[] leftCollider = Physics.OverlapCapsule(camera.transform.position, _leftOrigin, _radius, _reachItemLayer, QueryTriggerInteraction.Collide);
        if (leftCollider.Length > 0)
        {
            if (leftCollider[0].gameObject.tag == "ReachItem")
            {
                ItemDetected(leftCollider[0]);
            }
            else
            {
                MobDetected(leftCollider[0]);
            }         
        }
    }

    private void ItemDetected(Collider collider)
    {
        ReachItem item = collider.gameObject.GetComponent<ReachItem>();
        item.ItemReached();
        Destroy(item.gameObject);
        Reach_Item_Actions.ItemReached?.Invoke();
    }

    private void MobDetected(Collider collider)
    {
        Insect insect = collider.gameObject.GetComponent<Insect>();
        StartCoroutine(insect.Die());
    }
    /*
    private void OnDrawGizmos()
    {
        if (_bodyPoints == null || _drawPoseLandmark.IsBodyRemovedFromCamera)
            return;

        // Set the color for the Gizmos
        Gizmos.color = Color.red;

        Camera camera = Camera.main;

        // Right arm capsule
        var rightBottom = camera.transform.position;
        var rightTop = _rightOrigin;
        Gizmos.DrawWireSphere(rightBottom, _radius); // Draw bottom sphere
        Gizmos.DrawWireSphere(rightTop, _radius);  // Draw top sphere
        Gizmos.DrawLine(rightBottom, rightTop);  // Draw connecting line
    }*/
}
