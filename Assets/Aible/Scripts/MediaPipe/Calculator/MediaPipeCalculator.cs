using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
    public class MediaPipeCalculator : MonoBehaviour
    {
        public static Vector3 ConvertToVector(NormalizedLandmark landmark) => new Vector3(landmark.X, landmark.Y, landmark.Z);
        public static Vector3 ConvertToVector(Landmark landmark) => new Vector3(landmark.X, landmark.Y, landmark.Z);

        public static float CalculateAngles(Vector4 a, Vector4 b, Vector4 c)
        {
            float radians = Mathf.Atan2(c.y - b.y, c.x - b.x) - Mathf.Atan2(a.y - b.y, a.x - b.x);
            float angle = Mathf.Abs(radians * 180.0f / Mathf.PI);

            if (angle > 180.0f)
            {
                angle = 360 - angle;
            }

            return angle;
        }
    }
}

