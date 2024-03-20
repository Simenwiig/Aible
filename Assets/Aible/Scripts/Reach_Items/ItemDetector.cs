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
    [SerializeField] private Basket _basket;
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

        // Right arm detector
        _rightOrigin = _bodyPoints.Points[16].transform.position;       
        Collider[] rightCollider = Physics.OverlapSphere(_rightOrigin, _radius, _reachItemLayer, QueryTriggerInteraction.Collide);
        if (rightCollider.Length > 0)
        {
            ItemDetected(rightCollider[0]);
        }

        // Left arm detector
        _leftOrigin = _bodyPoints.Points[15].transform.position;
        Collider[] leftCollider = Physics.OverlapSphere(_leftOrigin, _radius, _reachItemLayer, QueryTriggerInteraction.Collide);
        if (leftCollider.Length > 0)
        {
            ItemDetected(leftCollider[0]);
        }
    }

    private void ItemDetected(Collider collider)
    {
        ReachItem item = collider.gameObject.GetComponent<ReachItem>();
        item.ItemReached();
        Destroy(item.gameObject);
        _basket.AddApple();
    }
    
    private void OnDrawGizmos()
    {
        if (_bodyPoints == null || _drawPoseLandmark.IsBodyRemovedFromCamera)
            return;

        Gizmos.color = Color.red;  
        Gizmos.DrawWireSphere(_rightOrigin, _radius);
        Gizmos.DrawWireSphere(_leftOrigin, _radius);
    }
}
