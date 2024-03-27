using Mediapipe.Unity.Sample.HandTracking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMenuActions : MonoBehaviour
{
    [SerializeField] private HandTracking _handTracking;
    [SerializeField] private LandmarkUIActivation _landmarkUIActivation;
    [SerializeField] private HandGestureTracker _handGestureTracker;
    [SerializeField] private LayerMask _buttonLayer;
    [SerializeField] private Image _progressBar;

    [SerializeField] private float _raycastLength = 20;
    [SerializeField] private float _timeBeforeButtonActivates = 1;

    private Transform _fingerRayOrigin;
    float _timer;

    private void Start()
    {
        _fingerRayOrigin = _handTracking.HandParent.IndexFingerRayOrigin;
        _progressBar.fillAmount = 0;
    }

    private void Update()
    {
        if (_handTracking.HandLandmarks == null)
            return;

        if (_handGestureTracker.GetHandGestures() == HandGestures.HG_Point)
        {      
            Vector3 origin = _fingerRayOrigin.position;
            origin += -_fingerRayOrigin.forward * _fingerRayOrigin.localScale.x * 0.5f;
            Vector3 direction = -_fingerRayOrigin.forward;

            _landmarkUIActivation.CheckForButton(_progressBar, origin, direction, _raycastLength, _buttonLayer, _timeBeforeButtonActivates);
        }
        else
        {
            _landmarkUIActivation.Deactivate(_progressBar);
        }
    }
}
