using Mediapipe;
using Mediapipe.Unity.Sample.PoseTracking;
using UnityEngine;

public class DrawPoseLandmark : MonoBehaviour
{
    [SerializeField] private PoseLandmarkSolution _poseLandmarkSolution;
    [SerializeField] private GameObject _body;
    [SerializeField] private float _bodySlerpSpeeed = 20f;
    [SerializeField][Range(0, 1)] private float visibilty = 0.9f;

    [HideInInspector] public LandmarkPoints BodyPoints;
    [HideInInspector] public bool IsBodyRemovedFromCamera;

    private float _timer;

    private void Awake()
    {
        GameObject handClone = GameObject.Instantiate(_body) as GameObject;
        handClone.name = "Body";
        BodyPoints = handClone.GetComponent<LandmarkPoints>();
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
    }
}
