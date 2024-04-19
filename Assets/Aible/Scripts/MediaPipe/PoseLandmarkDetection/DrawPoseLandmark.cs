using Mediapipe;
using Mediapipe.Unity.Sample.PoseTracking;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawPoseLandmark : MonoBehaviour
{
    [SerializeField] private PoseLandmarkSolution _poseLandmarkSolution;
    [SerializeField] private GameObject _body;
    [SerializeField] private float _bodySlerpSpeeed = 20f;
    [SerializeField][Range(0, 1)] private float visibilty = 0.9f;

    [HideInInspector] public LandmarkPoints BodyPoints;
    [HideInInspector] public bool IsBodyRemovedFromCamera = true;

    private float _timer;

    private void Awake()
    {
        GameObject bodyClone = GameObject.Instantiate(_body) as GameObject;
        bodyClone.name = "Body";
        BodyPoints = bodyClone.GetComponent<LandmarkPoints>();
        bodyClone.gameObject.SetActive(false);
        IsBodyRemovedFromCamera = true;
    }

    void Update()
    {
        LandmarkList landmarkWordList = _poseLandmarkSolution._LandmarkWordList;

        if (landmarkWordList == null || BodyPoints == null 
            || landmarkWordList.Landmark[0].Visibility < visibilty 
            || landmarkWordList.Landmark[23].Visibility < visibilty 
            || landmarkWordList.Landmark[24].Visibility < visibilty)
        {
            if (_timer > 0.1f)
            {
                BodyPoints.gameObject.SetActive(false);
                IsBodyRemovedFromCamera = true;
            }
            else
            {
                _timer += Time.deltaTime;
            }
            return;
        }
           
        if (IsBodyRemovedFromCamera)
        {
            BodyPoints.gameObject.SetActive(true);
        }

        if (_timer > 0)
        {
            _timer = 0;
            IsBodyRemovedFromCamera = false;
        }

        for (int i = 0; i < landmarkWordList.Landmark.Count; i++)
        {
            float x = landmarkWordList.Landmark[i].X * 6;
            float y = -landmarkWordList.Landmark[i].Y * 6;
            float z = 0;

            BodyPoints.Points[i].transform.localPosition = Vector3.Slerp(BodyPoints.Points[i].transform.localPosition, 
                new Vector3(x, y, z), _bodySlerpSpeeed * Time.deltaTime);
        }

        RotateHands();
    }


    private void RotateHands()
    {
        Transform leftHand = BodyPoints.Points[15].transform;
        Transform leftTarget = BodyPoints.Points[13].transform;

        leftHand.LookAt(leftTarget.position, Vector3.up);
        //leftHand.eulerAngles = new Vector3(leftHand.eulerAngles.x, 90, leftHand.eulerAngles.z);
        
        Transform rightHand = BodyPoints.Points[16].transform;
        Transform rightTarget = BodyPoints.Points[14].transform;

        rightHand.LookAt(rightTarget.position, Vector3.up);
        //rightHand.eulerAngles = new Vector3(rightHand.eulerAngles.x, 90, rightHand.eulerAngles.z);
    }
}
