using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnections : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _destination;

    private float _startWidth = 0.25f;
    private float _endtWidth = 0.25f;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = _startWidth;
        _lineRenderer.endWidth = _endtWidth;
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, _origin.position);
        _lineRenderer.SetPosition(1, _destination.position);
    }
}
