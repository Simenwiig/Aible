using Mediapipe.BlazePose;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseVisualizerSki : MonoBehaviour
{
    [SerializeField] WebCamInput webCamInput;
    [SerializeField] RawImage inputImageUI;
    [SerializeField] SkiMovement skiMovement;
    [SerializeField, Range(0, 1)] float minShulderAngle = 0.3f;
    [SerializeField, Range(0, 0.2f)] float minHipDifference = 0.02f;
    [SerializeField, Range(10, 30)] float minKneeAngle = 15f;
    public bool showShader;
    [SerializeField] Shader shader;
    [SerializeField, Range(0, 1)] float humanExistThreshold = 0.5f;

    Material material;
    BlazePoseDetecter detecter;
    bool threshold;

    const int BODY_LINE_NUM = 35;

    // Pairs of vertex indices of the lines that make up body's topology.
    // Defined by the figure in https://google.github.io/mediapipe/solutions/pose.
    readonly List<Vector4> linePair = new List<Vector4>
    {
        new Vector4(0, 1), new Vector4(1, 2), new Vector4(2, 3), new Vector4(3, 7), new Vector4(0, 4),
        new Vector4(4, 5), new Vector4(5, 6), new Vector4(6, 8), new Vector4(9, 10), new Vector4(11, 12),
        new Vector4(11, 13), new Vector4(13, 15), new Vector4(15, 17), new Vector4(17, 19), new Vector4(19, 15),
        new Vector4(15, 21), new Vector4(12, 14), new Vector4(14, 16), new Vector4(16, 18), new Vector4(18, 20),
        new Vector4(20, 16), new Vector4(16, 22), new Vector4(11, 23), new Vector4(12, 24), new Vector4(23, 24),
        new Vector4(23, 25), new Vector4(25, 27), new Vector4(27, 29), new Vector4(29, 31), new Vector4(31, 27),
        new Vector4(24, 26), new Vector4(26, 28), new Vector4(28, 30), new Vector4(30, 32), new Vector4(32, 28)
    };

    private void Start()
    {
        material = new Material(shader);
        detecter = new BlazePoseDetecter();
    }

    private void LateUpdate()
    {
        detecter.ProcessImage(webCamInput.inputImageTexture, BlazePoseModel.lite); 

        /*
        for (int i = 0; i < detecter.vertexCount; i++)
        {
            
            0~32 index datas are pose world landmark.
            Check below Mediapipe document about relation between index and landmark position.
            https://google.github.io/mediapipe/solutions/pose#pose-landmark-model-blazepose-ghum-3d
            Each data factors are
            x, y and z: Real-world 3D coordinates in meters with the origin at the center between hips.
            w: The score of whether the world landmark position is visible ([0, 1]).

            33 index data is the score whether human pose is visible ([0, 1]).
            This data is (score, 0, 0, 0).
               
        }*/


        threshold = detecter.GetPoseWorldLandmark(33).x > humanExistThreshold ? true : false;

        //Right Arm
        float rArmAngle = CalculateAngles(detecter.GetPoseWorldLandmark(24), detecter.GetPoseWorldLandmark(12), detecter.GetPoseWorldLandmark(14));

        float rNormalizedAngle = rArmAngle / 90f;

        if (rNormalizedAngle > minShulderAngle && skiMovement.rightArmAngle < (rNormalizedAngle - 0.05f) && threshold)
        {
            skiMovement.rightArmAngle += Time.deltaTime * 2;
        }     
        else if(rNormalizedAngle > minShulderAngle && threshold)
        {
            skiMovement.rightArmAngle = rNormalizedAngle;
        }
        else if(skiMovement.rightArmAngle >= 0)
        {
            skiMovement.rightArmAngle -= Time.deltaTime;
        }

        //Left Arm
        float lArmAngle = CalculateAngles(detecter.GetPoseWorldLandmark(23), detecter.GetPoseWorldLandmark(11), detecter.GetPoseWorldLandmark(13));

        float lNormalizedAngle = lArmAngle / 90f;

        if (lNormalizedAngle > minShulderAngle && skiMovement.leftArmAngle < (lNormalizedAngle - 0.05f) && threshold)
        {
            skiMovement.leftArmAngle += Time.deltaTime * 2;
        }
        else if (lNormalizedAngle > minShulderAngle && threshold)
        {
            skiMovement.leftArmAngle = lNormalizedAngle;
        }
        else if (skiMovement.leftArmAngle >= 0)
        {
            skiMovement.leftArmAngle -= Time.deltaTime;
        }

        //MOVE RIGHT AND LEFT

        //Right Hip
        float lHipPont = detecter.GetPoseWorldLandmark(23).y;
        //Left Hip
        float rHipPont = detecter.GetPoseWorldLandmark(24).y;

        //float hipMidPoint = CalculateEuclideanMidpoint(lHipPont, rHipPont);

        float hipDiff = lHipPont - rHipPont;


        //Right Knee Angle
        float rKneeAngle = CalculateAngles(detecter.GetPoseWorldLandmark(24),
            detecter.GetPoseWorldLandmark(26), detecter.GetPoseWorldLandmark(28));

        //Left Knee Angle
        float lKneeAngle = CalculateAngles(detecter.GetPoseWorldLandmark(23),
            detecter.GetPoseWorldLandmark(25), detecter.GetPoseWorldLandmark(27));

        float kneeDiff = lKneeAngle - rKneeAngle;

        if ((hipDiff > minHipDifference || (hipDiff > 0.001 && kneeDiff > minKneeAngle)) && threshold)
        {
            skiMovement.moveRight = true;
            //skiMovement.turnTorque = Mathf.Abs(hipDiff + kneeDiff);
        }
        else
        {
            skiMovement.moveRight = false;
        }

        if ((hipDiff < -minHipDifference || (hipDiff < -0.001 && kneeDiff < -minKneeAngle)) && threshold)
        {
            skiMovement.moveLeft = true;
            //skiMovement.turnTorque =  Mathf.Abs(hipDiff + kneeDiff);
        }
        else
        {
            skiMovement.moveLeft = false;
        }
    }

    float CalculateAngles(Vector4 a, Vector4 b, Vector4 c)
    {
        float radians = Mathf.Atan2(c.y - b.y, c.x - b.x) - Mathf.Atan2(a.y - b.y, a.x - b.x);
        float angle = Mathf.Abs(radians * 180.0f / Mathf.PI);

        if (angle > 180.0f)
        {
            angle = 360 - angle;
        }

        return angle;
    }

    private void OnRenderObject()
    {
        if (!showShader) return;

        var w = inputImageUI.rectTransform.rect.width;
        var h = inputImageUI.rectTransform.rect.height;

        // Use predicted pose landmark results on the ComputeBuffer (GPU) memory.
        material.SetBuffer("_vertices", detecter.outputBuffer);
        // Set pose landmark counts.
        material.SetInt("_keypointCount", detecter.vertexCount);
        material.SetFloat("_humanExistThreshold", humanExistThreshold);
        material.SetVector("_uiScale", new Vector2(w * 1.5f, h * 1.5f));
        material.SetVectorArray("_linePair", linePair);

        // Draw 35 body topology lines.
        material.SetPass(0);
        Graphics.DrawProceduralNow(MeshTopology.Triangles, 6, BODY_LINE_NUM);

        // Draw 33 landmark points.
        material.SetPass(1);
        Graphics.DrawProceduralNow(MeshTopology.Triangles, 6, detecter.vertexCount);
    }

    private void OnDestroy()
    {
        if (detecter != null)
            detecter.Dispose();
    }

    private void OnApplicationQuit()
    {
        // Must call Dispose method when no longer in use.
        if (detecter != null)
            detecter.Dispose();
    }
}
