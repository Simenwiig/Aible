using UnityEngine;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public enum HandGestures
    {
        HG_Default,
        HG_Point,
        HG_TwoFingers
    }

    public class HandGestureTracker : MonoBehaviour
    {
        [SerializeField] private HandTracking _handPoints;

        [SerializeField] private Material _pointMaterial;
        private Material _defaultMaterial;

        private HandGestures _handGestures;

        private NormalizedLandmarkList _landmarkList;

        private void Start()
        {
            _defaultMaterial = HandPoints.GetMaterial(8);
        }

        private void Update()
        {
            if (_handPoints.HandLandmarks == null)
            {
                SetHandGesture(HandGestures.HG_Default);
                return;
            }         

            _landmarkList = _handPoints.HandLandmarks[_handPoints.HandIndex];

            if (CheckIfPointFinger())
            {
                if (GetHandGestures() == HandGestures.HG_Point)
                    return;
                SetHandGesture(HandGestures.HG_Point);
                HandPoints.ChangeMaterial(8, _pointMaterial);
            }
            else
            {
                if(GetHandGestures() == HandGestures.HG_Default)
                    return;
                SetHandGesture(HandGestures.HG_Default);
                HandPoints.ChangeMaterial(8, _defaultMaterial);;
            }
        }

        private bool CheckIfPointFinger()
        {
            bool idexFingerStraight = CheckIfStraightFinger(_landmarkList.Landmark[5],
                _landmarkList.Landmark[6], _landmarkList.Landmark[7]);

            bool middleFingerClosed = CheckIfStraightFinger(_landmarkList.Landmark[9],
                _landmarkList.Landmark[10], _landmarkList.Landmark[11]);

            bool ringFingerClosed = CheckIfStraightFinger(_landmarkList.Landmark[13],
                _landmarkList.Landmark[14], _landmarkList.Landmark[15]);

            bool pinkyFingerClosed = CheckIfStraightFinger(_landmarkList.Landmark[17],
                _landmarkList.Landmark[18], _landmarkList.Landmark[19]);

            return idexFingerStraight && !middleFingerClosed && !ringFingerClosed && !pinkyFingerClosed;
        }

        public void SetHandGesture(HandGestures handGestures)
        {
            if (GetHandGestures() == handGestures)
                return;
            _handGestures = handGestures;
        }

        public HandGestures GetHandGestures()
        {
            return _handGestures;
        }

        private bool CheckIfStraightFinger(NormalizedLandmark landMarkA, NormalizedLandmark landMarkB, NormalizedLandmark landMarkC)
        {
            Vector3 angleA = MediaPipeCalculator.ConvertToVector(landMarkA);
            Vector3 angleB = MediaPipeCalculator.ConvertToVector(landMarkB);
            Vector3 angleC = MediaPipeCalculator.ConvertToVector(landMarkC);

            float angle = MediaPipeCalculator.CalculateAngles(angleA, angleB, angleC);

            return angle > 170;
        }

        private void PrintFingerAngle(NormalizedLandmark landMarkA, NormalizedLandmark landMarkB, NormalizedLandmark landMarkC)
        {
            Vector3 angleA = MediaPipeCalculator.ConvertToVector(landMarkA);
            Vector3 angleB = MediaPipeCalculator.ConvertToVector(landMarkB);
            Vector3 angleC = MediaPipeCalculator.ConvertToVector(landMarkC);

            float angle = MediaPipeCalculator.CalculateAngles(angleA, angleB, angleC);

            Debug.Log(angle);
        }

        private bool CheckIfClosedFinger(NormalizedLandmark landMarkA, NormalizedLandmark landMarkB, NormalizedLandmark landMarkC)
        {
            Vector3 angleA = MediaPipeCalculator.ConvertToVector(landMarkA);
            Vector3 angleB = MediaPipeCalculator.ConvertToVector(landMarkB);
            Vector3 angleC = MediaPipeCalculator.ConvertToVector(landMarkC);

            float angle = MediaPipeCalculator.CalculateAngles(angleA, angleB, angleC);

            return angle < 160;
        }
    }
}

