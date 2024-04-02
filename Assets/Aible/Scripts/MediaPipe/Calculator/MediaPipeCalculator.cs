using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
    public class MediaPipeCalculator : MonoBehaviour
    {
        public static Vector3 ConvertToVector(NormalizedLandmark landmark) => new Vector3(landmark.X, landmark.Y, landmark.Z);
        public static Vector3 ConvertToVector(Landmark landmark) => new Vector3(landmark.X, landmark.Y, landmark.Z);

        public static float CalculateXAngle(Vector4 a, Vector4 b, Vector4 c)
        {
            float radians = Mathf.Atan2(c.y - b.y, c.x - b.x) - Mathf.Atan2(a.y - b.y, a.x - b.x);
            float angle = Mathf.Abs(radians * 180.0f / Mathf.PI);

            if (angle > 180.0f)
            {
                angle = 360 - angle;
            }

            return angle;
        }

        public static float CalculateYAngle(Vector4 a, Vector4 b, Vector4 c)
        {
            float radians = Mathf.Atan2(c.x - b.x, c.y - b.y) - Mathf.Atan2(a.x - b.x, a.y - b.y);
            float angle = Mathf.Abs(radians * 180.0f / Mathf.PI);

            if (angle > 180.0f)
            {
                angle = 360 - angle;
            }

            return angle;
        }

        public static float CalculateAngle3D(Vector3 a, Vector3 b, Vector3 c)
        {
            // Calculate the dot product
            float dotProduct = Vector3.Dot(a - b, c - b);

            // Calculate magnitudes of the vectors
            float magnitudeA = Vector3.Magnitude(a - b);
            float magnitudeC = Vector3.Magnitude(c - b);

            // Handle potential division by zero (vectors with zero magnitude)
            if (magnitudeA == 0.0f || magnitudeC == 0.0f)
            {
                return 0.0f; // Or throw an exception depending on your error handling strategy
            }

            // Calculate the angle in radians using acos (arccosine)
            float radians = Mathf.Acos(dotProduct / (magnitudeA * magnitudeC));

            // Convert to degrees
            float angle = radians * 180.0f / Mathf.PI;

            return angle;
        }

        public static void RotateGameObjectWithAngle(GameObject gameObject, Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            float angle = CalculateAngle3D(pointA, pointB, pointC);

            // Calculate rotation axis (pointing from B to center of A and C)
            Vector3 rotationAxis = (pointA + pointC) / 2.0f - pointB;
            rotationAxis = Vector3.Normalize(rotationAxis); // Ensure normalized axis

            // Apply rotation to the GameObject
            gameObject.transform.Rotate(rotationAxis, angle);
        }
    }
}

