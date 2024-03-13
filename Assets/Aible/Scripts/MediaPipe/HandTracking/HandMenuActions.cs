using Mediapipe.Unity.Sample.HandTracking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMenuActions : MonoBehaviour
{
    [SerializeField] private HandTracking _handTracking;
    [SerializeField] private HandGestureTracker _handGestureTracker;
    [SerializeField] private LayerMask _buttonLayer;

    [SerializeField] private TextMeshProUGUI testText;

    [SerializeField] private float raycastLength = 20;

    private Transform _fingerRayOrigin;
    [SerializeField] float _timer;

    private void Start()
    {
        _fingerRayOrigin = _handTracking.HandParent.IndexFingerRayOrigin;
    }

    private void Update()
    {
        testText.text = _timer.ToString("F2");

        if (_handTracking.HandLandmarks == null)
            return;

        if (_handGestureTracker.GetHandGestures() == HandGestures.HG_Point)
        {
            Vector3 origin = _fingerRayOrigin.position;
            origin += -_fingerRayOrigin.forward * _fingerRayOrigin.localScale.x * 0.5f;
            Vector3 direction = -_fingerRayOrigin.forward;
            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit, raycastLength, _buttonLayer))
            {
                Debug.DrawRay(origin, direction * raycastLength, Color.green);
                Button button = hit.transform.gameObject.GetComponent<Button>();
                _timer += Time.deltaTime % 60;
                if(_timer > 5)
                    ClickButton(button);
            }
            else
            {
                Debug.DrawRay(origin, direction * raycastLength, Color.red);
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }
    }

    private void ClickButton(Button button)
    {
        button.onClick.Invoke();
    }
}
