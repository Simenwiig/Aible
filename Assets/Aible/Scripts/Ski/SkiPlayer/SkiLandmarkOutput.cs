using Mediapipe;
using Mediapipe.Unity;
using Mediapipe.Unity.Sample.PoseTracking;
using UnityEngine;

public class SkiLandmarkOutput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SkiMovement _skiMovement;
    [SerializeField] PoseLandmarkSolution _poseLandmarkSolution;

    [Header("Landmark Parameters")]
    [SerializeField, Range(0, 1)] float _minShulderAngle = 0.3f;
    [SerializeField, Range(0, 0.2f)] float _minHipDifference = 0.02f;
    [SerializeField, Range(10, 30)] float _minKneeAngle = 13f;
    //[SerializeField, Range(0, 1)] float _humanExistThreshold = 0.7f;

    private void LateUpdate()
    {
        NormalizedLandmarkList landmarkList = _poseLandmarkSolution._LandmarkList;

        if (landmarkList != null)
        {
            MoveLeftArm(landmarkList);
            MoveRightArm(landmarkList);
            MoveSideways(landmarkList);
        }
    }

    private void MoveLeftArm(NormalizedLandmarkList landmarkList)
    {
        Vector3 lArmAngleA = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[23]);
        Vector3 lArmAngleB = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[11]);
        Vector3 lArmAngleC = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[13]);

        float lArmAngle = MediaPipeCalculator.CalculateAngles(lArmAngleA, lArmAngleB, lArmAngleC);
        float lNormalizedAngle = lArmAngle / 90f;

        if (lNormalizedAngle > _minShulderAngle && _skiMovement.leftArmAngle < (lNormalizedAngle - 0.05f))
        {
            _skiMovement.leftArmAngle += Time.deltaTime * 5;
        }
        else if (lNormalizedAngle > _minShulderAngle)
        {
            _skiMovement.leftArmAngle = lNormalizedAngle;
        }
        else if (_skiMovement.leftArmAngle >= 0)
        {
            _skiMovement.leftArmAngle -= Time.deltaTime * 2;
        }
    }

    private void MoveRightArm(NormalizedLandmarkList landmarkList)
    {
        Vector3 rArmAngleA = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[24]);
        Vector3 rArmAngleB = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[12]);
        Vector3 rArmAngleC = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[14]);

        float rArmAngle = MediaPipeCalculator.CalculateAngles(rArmAngleA, rArmAngleB, rArmAngleC);
        float rNormalizedAngle = rArmAngle / 90f;

        if (rNormalizedAngle > _minShulderAngle && _skiMovement.rightArmAngle < (rNormalizedAngle - 0.05f))
        {
            _skiMovement.rightArmAngle += Time.deltaTime * 5;
        }
        else if (rNormalizedAngle > _minShulderAngle)
        {
            _skiMovement.rightArmAngle = rNormalizedAngle;
        }
        else if (_skiMovement.rightArmAngle >= 0)
        {
            _skiMovement.rightArmAngle -= Time.deltaTime * 2;
        }
    }

    private void MoveSideways(NormalizedLandmarkList landmarkList)
    {
        //MOVE RIGHT AND LEFT

        //Right Hip
        float lHipPont = landmarkList.Landmark[24].Y;
        //Left Hip
        float rHipPont = landmarkList.Landmark[23].Y;

        //Get distance between each hip
        float hipDistance = lHipPont - rHipPont;


        //Right Knee Angle
        Vector3 rKneeAngleA = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[24]);
        Vector3 rKneeAngleB = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[26]);
        Vector3 rKneeAngleC = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[28]);

        float rKneeAngle = MediaPipeCalculator.CalculateAngles(rKneeAngleA, rKneeAngleB, rKneeAngleC);

        //Left Knee Angle
        Vector3 lKneeAngleA = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[23]);
        Vector3 lKneeAngleB = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[25]);
        Vector3 lKneeAngleC = MediaPipeCalculator.ConvertToVector(landmarkList.Landmark[27]);

        float lKneeAngle = MediaPipeCalculator.CalculateAngles(lKneeAngleA, lKneeAngleB, lKneeAngleC);

        //Get difference between each knee angle
        float kneeDiff = lKneeAngle - rKneeAngle;

        if ((hipDistance > _minHipDifference || (hipDistance > 0.001 && kneeDiff > _minKneeAngle)))
        {
            _skiMovement.moveRight = true;
            //skiMovement.turnTorque = Mathf.Abs(hipDiff + kneeDiff);
        }
        else
        {
            _skiMovement.moveRight = false;
        }

        if ((hipDistance < -_minHipDifference || (hipDistance < -0.001 && kneeDiff < -_minKneeAngle)))
        {
            _skiMovement.moveLeft = true;
            //skiMovement.turnTorque =  Mathf.Abs(hipDiff + kneeDiff);
        }
        else
        {
            _skiMovement.moveLeft = false;
        }
    }
}
