using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnections : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _destination;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, _origin.position);
        _lineRenderer.SetPosition(1, _destination.position);
    }
}
