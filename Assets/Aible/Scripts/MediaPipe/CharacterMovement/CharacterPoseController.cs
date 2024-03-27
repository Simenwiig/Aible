using Mediapipe;
using Mediapipe.Unity;
using Mediapipe.Unity.Sample.PoseTracking;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class CharacterPoseController : MonoBehaviour
{
    [SerializeField] private PoseLandmarkSolution _poseLandmarkSolution;
    [SerializeField] private GameObject _arm;
    Vector3 _armStartRot;

    [SerializeField][Range(0, 1)] private float visibilty = 0.9f;


    private void Start()
    {
        _armStartRot = _arm.transform.localEulerAngles;
    }

    private void Update()
    {
        LandmarkList landmarkWordList = _poseLandmarkSolution._LandmarkWordList;
        NormalizedLandmarkList landmarkList = _poseLandmarkSolution._LandmarkList;

        if (landmarkWordList == null || landmarkList == null
          || landmarkWordList.Landmark[0].Visibility < visibilty
          || landmarkWordList.Landmark[23].Visibility < visibilty
          || landmarkWordList.Landmark[24].Visibility < visibilty)
        {
            return;
        }

        //print(landmarkWordList.Landmark[12]);

        Vector3 rightHip = MediaPipeCalculator.ConvertToVector(landmarkWordList.Landmark[24]);
        Vector3 rightShoulder = MediaPipeCalculator.ConvertToVector(landmarkWordList.Landmark[12]);
        Vector3 rightElbow = MediaPipeCalculator.ConvertToVector(landmarkWordList.Landmark[14]);

        float armY = MediaPipeCalculator.CalculateYAngle(rightHip, rightShoulder, rightElbow) - 90;
        float armZ = - MediaPipeCalculator.CalculateXAngle(rightHip, rightShoulder, rightElbow) + 90;

        MediaPipeCalculator.RotateGameObjectWithAngle(_arm, rightHip, rightShoulder, rightElbow);
    }
}
