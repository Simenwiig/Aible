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

        private HandGestures _handGestures;

        private NormalizedLandmarkList _landmarkList;

        private void Update()
        {
            if (_handPoints._handLandmarks == null)
                return;

            _landmarkList = _handPoints._handLandmarks[0];

            if (CheckIfPointFinger())
            {
                SetHandGesture(HandGestures.HG_Point);
            }
            else
            {
                SetHandGesture(HandGestures.HG_Default);
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

        private bool CheckIfClosedFinger(NormalizedLandmark landMarkA, NormalizedLandmark landMarkB, NormalizedLandmark landMarkC)
        {
            Vector3 angleA = MediaPipeCalculator.ConvertToVector(landMarkA);
            Vector3 angleB = MediaPipeCalculator.ConvertToVector(landMarkB);
            Vector3 angleC = MediaPipeCalculator.ConvertToVector(landMarkC);

            float angle = MediaPipeCalculator.CalculateAngles(angleA, angleB, angleC);

            return angle < 90;
        }
    }
}

