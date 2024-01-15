using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNectModel : MonoBehaviour
{
    public class JointPoint
    {
        public Vector3 pos3D = new Vector3();
        public float score3D;

        // Bones
        public Transform transform = null;
        public Quaternion initRotation;
        public Quaternion inverse;
        public Quaternion inverseRotation;

        public JointPoint child = null;
        public JointPoint parent = null;
    }

    public class Skeleton
    {
        public GameObject lineObject;
        public LineRenderer line;
        public JointPoint start = null;
        public JointPoint end = null;
    }

    private List<Skeleton> skeletons = new List<Skeleton>();
    public Material skeletonMaterial;

    public bool showSkeleton;
    private bool useSkeleton;

    public float skeletonX;
    public float skeletonY;
    public float skeletonZ;
    public float skeletonScale;

    // Joint position and bone
    private JointPoint[] jointPoints;
    public JointPoint[] JointPoints { get { return jointPoints; } }
    private Vector3 initPos; // Initial center position
    private Vector3 jointPosOffset;

    // Avatar
    public GameObject modelObject;
    public GameObject nose;
    private Animator anim;
    private Vector3 avatarDimensions;
    private Vector3 avatarCenter;

    public PoseVisualizer poseVisualizer;

    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            RunCalibration();

        if (jointPoints != null)
            PoseUpdate();
    }

    // Initialize joint points
    public JointPoint[] Initialize()
    {
        jointPoints = new JointPoint[PositionIndex.Count.Int()];
        for (var i = 0; i < PositionIndex.Count.Int(); i++)
        {
            JointPoints[i] = new JointPoint();
        }

        anim = modelObject.GetComponent<Animator>();

        avatarDimensions.x = Vector3.Distance(anim.GetBoneTransform(HumanBodyBones.RightHand).position, 
            anim.GetBoneTransform(HumanBodyBones.LeftHand).position);
        avatarDimensions.y = nose.transform.position.y;
        avatarCenter = GetCenter(gameObject);

        // Right Arm
        jointPoints[PositionIndex.rShoulder.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
        jointPoints[PositionIndex.rElbow.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
        jointPoints[PositionIndex.rWrist.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightHand);
        jointPoints[PositionIndex.rThumb.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
        jointPoints[PositionIndex.rPinky.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightLittleIntermediate);

        //Left Arm
        jointPoints[PositionIndex.lShoulder.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        jointPoints[PositionIndex.lElbow.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        jointPoints[PositionIndex.lWrist.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftHand);
        jointPoints[PositionIndex.lThumb.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
        jointPoints[PositionIndex.lPinky.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftLittleIntermediate);


        // Head
        jointPoints[PositionIndex.lEar.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Head);
        jointPoints[PositionIndex.lEye.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftEye);
        jointPoints[PositionIndex.rEar.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Head);
        jointPoints[PositionIndex.rEye.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightEye);
        jointPoints[PositionIndex.Nose.Int()].transform = nose.transform;

        // Right Leg
        jointPoints[PositionIndex.rHip.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        jointPoints[PositionIndex.rKnee.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        jointPoints[PositionIndex.rAnkle.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightFoot);
        jointPoints[PositionIndex.rFootIndex.Int()].transform = anim.GetBoneTransform(HumanBodyBones.RightToes);

        // Left Leg
        jointPoints[PositionIndex.lHip.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        jointPoints[PositionIndex.lKnee.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        jointPoints[PositionIndex.lAnkle.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        jointPoints[PositionIndex.lFootIndex.Int()].transform = anim.GetBoneTransform(HumanBodyBones.LeftToes);

        // Spine
        jointPoints[PositionIndex.head.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Head);
        jointPoints[PositionIndex.neck.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Neck);
        jointPoints[PositionIndex.chest.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Chest);
        jointPoints[PositionIndex.spine.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Spine);
        jointPoints[PositionIndex.hips.Int()].transform = anim.GetBoneTransform(HumanBodyBones.Hips);

        // Parent-Child Setup
        // Right Arm
        jointPoints[PositionIndex.rShoulder.Int()].child = jointPoints[PositionIndex.rElbow.Int()];
        jointPoints[PositionIndex.rElbow.Int()].child = jointPoints[PositionIndex.rWrist.Int()];
        jointPoints[PositionIndex.rElbow.Int()].parent = jointPoints[PositionIndex.rShoulder.Int()];

        // Left Arm
        jointPoints[PositionIndex.lShoulder.Int()].child = jointPoints[PositionIndex.lElbow.Int()];
        jointPoints[PositionIndex.lElbow.Int()].child = jointPoints[PositionIndex.lWrist.Int()];
        jointPoints[PositionIndex.lElbow.Int()].parent = jointPoints[PositionIndex.lShoulder.Int()];

        // Right Leg
        jointPoints[PositionIndex.rHip.Int()].child = jointPoints[PositionIndex.rKnee.Int()];
        jointPoints[PositionIndex.rKnee.Int()].child = jointPoints[PositionIndex.rAnkle.Int()];
        jointPoints[PositionIndex.rAnkle.Int()].child = jointPoints[PositionIndex.rFootIndex.Int()];
        jointPoints[PositionIndex.rAnkle.Int()].parent = jointPoints[PositionIndex.rKnee.Int()];

        // Left Leg
        jointPoints[PositionIndex.lHip.Int()].child = jointPoints[PositionIndex.lKnee.Int()];
        jointPoints[PositionIndex.lKnee.Int()].child = jointPoints[PositionIndex.lAnkle.Int()];
        jointPoints[PositionIndex.lAnkle.Int()].child = jointPoints[PositionIndex.lFootIndex.Int()];
        jointPoints[PositionIndex.lAnkle.Int()].parent = jointPoints[PositionIndex.lKnee.Int()];

        // Spine
        jointPoints[PositionIndex.spine.Int()].child = jointPoints[PositionIndex.chest.Int()];
        jointPoints[PositionIndex.chest.Int()].child = jointPoints[PositionIndex.neck.Int()];
        jointPoints[PositionIndex.neck.Int()].child = jointPoints[PositionIndex.head.Int()];

        useSkeleton = showSkeleton;
        if (useSkeleton)
        {
            // Skeleton Lines
            // Right Arm
            AddSkeleton(PositionIndex.rShoulder, PositionIndex.rElbow);
            AddSkeleton(PositionIndex.rElbow, PositionIndex.rWrist);
            AddSkeleton(PositionIndex.rWrist, PositionIndex.rThumb);
            AddSkeleton(PositionIndex.rWrist, PositionIndex.rPinky);

            // Left Arm
            AddSkeleton(PositionIndex.lShoulder, PositionIndex.lElbow);
            AddSkeleton(PositionIndex.lElbow, PositionIndex.lWrist);
            AddSkeleton(PositionIndex.lWrist, PositionIndex.lThumb);
            AddSkeleton(PositionIndex.lWrist, PositionIndex.lPinky);

            // Head
            AddSkeleton(PositionIndex.lEar, PositionIndex.lEye);
            AddSkeleton(PositionIndex.lEye, PositionIndex.Nose);
            AddSkeleton(PositionIndex.rEar, PositionIndex.rEye);
            AddSkeleton(PositionIndex.rEye, PositionIndex.Nose);

            // Right Leg
            AddSkeleton(PositionIndex.rHip, PositionIndex.rKnee);
            AddSkeleton(PositionIndex.rKnee, PositionIndex.rAnkle);
            AddSkeleton(PositionIndex.rAnkle, PositionIndex.rFootIndex);

            // Left Leg
            AddSkeleton(PositionIndex.lHip, PositionIndex.lKnee);
            AddSkeleton(PositionIndex.lKnee, PositionIndex.lAnkle);
            AddSkeleton(PositionIndex.lAnkle, PositionIndex.lFootIndex);

            // Torso
            AddSkeleton(PositionIndex.hips, PositionIndex.spine);
            AddSkeleton(PositionIndex.spine, PositionIndex.chest);
            AddSkeleton(PositionIndex.chest, PositionIndex.neck);
            AddSkeleton(PositionIndex.neck, PositionIndex.head);
            AddSkeleton(PositionIndex.chest, PositionIndex.rShoulder);
            AddSkeleton(PositionIndex.chest, PositionIndex.lShoulder);
            AddSkeleton(PositionIndex.hips, PositionIndex.rHip);
            AddSkeleton(PositionIndex.hips, PositionIndex.lHip);
        }

        // Set Inverse
        var forward = TriangleNormal(jointPoints[PositionIndex.hips.Int()].transform.position,
            jointPoints[PositionIndex.lHip.Int()].transform.position,
            jointPoints[PositionIndex.rHip.Int()].transform.position);

        foreach (var jointPoint in jointPoints)
        {
            if (jointPoint != null)
            {
                if (jointPoint.transform != null)
                {
                    jointPoint.initRotation = jointPoint.transform.rotation;
                }

                if (jointPoint.child != null && jointPoint.child.transform != null && jointPoint.child.transform.position != null)
                {
                    jointPoint.inverse = GetInverse(jointPoint, jointPoint.child, forward);
                    jointPoint.inverseRotation = jointPoint.inverse * jointPoint.initRotation;
                }
            }
        }

        // Hips Rotation
        var hips = jointPoints[PositionIndex.hips.Int()];
        initPos = jointPoints[PositionIndex.hips.Int()].transform.position;
        hips.inverse = Quaternion.Inverse(Quaternion.LookRotation(forward));
        hips.inverseRotation = hips.inverse * hips.initRotation;

        // Head Rotation
        var head = jointPoints[PositionIndex.head.Int()];
        head.initRotation = jointPoints[PositionIndex.head.Int()].transform.rotation;
       
        var gaze = jointPoints[PositionIndex.Nose.Int()].transform.position - jointPoints[PositionIndex.head.Int()].transform.position;
        head.inverse = Quaternion.Inverse(Quaternion.LookRotation(gaze));
        head.inverseRotation = head.inverse * head.initRotation;

        // Wrists rotation
        var lWrist = jointPoints[PositionIndex.lWrist.Int()];
        var lf = TriangleNormal(lWrist.pos3D, jointPoints[PositionIndex.lPinky.Int()].pos3D, jointPoints[PositionIndex.lThumb.Int()].pos3D);
        lWrist.initRotation = lWrist.transform.rotation;
        lWrist.inverse = Quaternion.Inverse(Quaternion.LookRotation(jointPoints[PositionIndex.lThumb.Int()].transform.position - jointPoints[PositionIndex.lPinky.Int()].transform.position, lf));
        lWrist.inverseRotation = lWrist.inverse * lWrist.initRotation;

        var rWrist = jointPoints[PositionIndex.rWrist.Int()];
        var rf = TriangleNormal(rWrist.pos3D, jointPoints[PositionIndex.rThumb.Int()].pos3D, jointPoints[PositionIndex.rPinky.Int()].pos3D);
        rWrist.initRotation = jointPoints[PositionIndex.rWrist.Int()].transform.rotation;
        rWrist.inverse = Quaternion.Inverse(Quaternion.LookRotation(jointPoints[PositionIndex.rThumb.Int()].transform.position - jointPoints[PositionIndex.rPinky.Int()].transform.position, rf));
        rWrist.inverseRotation = rWrist.inverse * rWrist.initRotation;

        return JointPoints;
    }

    public void PoseUpdate()
    {
        // movement and rotatation of the center
        var forward = TriangleNormal(jointPoints[PositionIndex.hips.Int()].pos3D, 
            jointPoints[PositionIndex.lHip.Int()].pos3D, jointPoints[PositionIndex.rHip.Int()].pos3D);

        jointPoints[PositionIndex.hips.Int()].transform.position = jointPoints[PositionIndex.hips.Int()].pos3D + initPos - jointPosOffset;
        jointPoints[PositionIndex.hips.Int()].transform.rotation = Quaternion.LookRotation(forward) * jointPoints[PositionIndex.hips.Int()].inverseRotation;

        // rotation of each of the bones
        foreach (var jointPoint in jointPoints)
        {
            if (jointPoint.parent != null)
            {
                var fv = jointPoint.parent.pos3D - jointPoint.pos3D;
                jointPoint.transform.rotation = Quaternion.LookRotation(jointPoint.pos3D - jointPoint.child.pos3D, fv) * jointPoint.inverseRotation;
            }
            else if (jointPoint.child != null)
            {
                jointPoint.transform.rotation = Quaternion.LookRotation(jointPoint.pos3D - jointPoint.child.pos3D, forward) * jointPoint.inverseRotation;
            }
        }

        // Head Rotation
        var gaze = jointPoints[PositionIndex.Nose.Int()].pos3D - jointPoints[PositionIndex.head.Int()].pos3D;
        var f = TriangleNormal(jointPoints[PositionIndex.Nose.Int()].pos3D, jointPoints[PositionIndex.rEar.Int()].pos3D, jointPoints[PositionIndex.lEar.Int()].pos3D);
        var head = jointPoints[PositionIndex.head.Int()];
        head.transform.rotation = Quaternion.LookRotation(gaze, f) * head.inverseRotation;

        // Wrist rotation
        var lWrist = jointPoints[PositionIndex.lWrist.Int()];
        var lf = TriangleNormal(lWrist.pos3D, jointPoints[PositionIndex.lPinky.Int()].pos3D, jointPoints[PositionIndex.lThumb.Int()].pos3D);
        lWrist.transform.rotation = Quaternion.LookRotation(jointPoints[PositionIndex.lThumb.Int()].pos3D - jointPoints[PositionIndex.lPinky.Int()].pos3D, lf) * lWrist.inverseRotation;

        var rWrist = jointPoints[PositionIndex.rWrist.Int()];
        var rf = TriangleNormal(rWrist.pos3D, jointPoints[PositionIndex.rThumb.Int()].pos3D, jointPoints[PositionIndex.rPinky.Int()].pos3D);
        rWrist.transform.rotation = Quaternion.LookRotation(jointPoints[PositionIndex.rThumb.Int()].pos3D - jointPoints[PositionIndex.rPinky.Int()].pos3D, rf) * rWrist.inverseRotation;

        foreach (var sk in skeletons)
        {
            var s = sk.start;
            var e = sk.end;

            sk.line.SetPosition(0, new Vector3(s.pos3D.x * skeletonScale + skeletonX, s.pos3D.y * skeletonScale + skeletonY
                , s.pos3D.z * skeletonScale + skeletonZ));
            sk.line.SetPosition(1, new Vector3(e.pos3D.x * skeletonScale + skeletonX, e.pos3D.y * skeletonScale + skeletonY
                , e.pos3D.z * skeletonScale + skeletonZ));
        }
    }

    Vector3 TriangleNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = a - b;
        Vector3 d2 = a - c;

        // The cross product of two vectors results in a third vector which is perpendicular to the two input vectors.
        Vector3 dd = Vector3.Cross(d1, d2);
        dd.Normalize();

        return dd;
    }

    private Quaternion GetInverse(JointPoint p1, JointPoint p2, Vector3 forward)
    {
        return Quaternion.Inverse(Quaternion.LookRotation(p1.transform.position - p2.transform.position, forward));
    }

    // Add skelton from joint points
    private void AddSkeleton(PositionIndex s, PositionIndex e)
    {
        var sk = new Skeleton()
        {
            lineObject = new GameObject("Line"),
            start = jointPoints[s.Int()],
            end = jointPoints[e.Int()]
        };

        sk.line = sk.lineObject.AddComponent<LineRenderer>();
        sk.line.startWidth = 0.025f;
        sk.line.endWidth = 0.005f;

        // define the number of vertex
        sk.line.positionCount = 2;
        sk.line.material = skeletonMaterial;

        skeletons.Add(sk);
    }

    private Vector3 GetCenter(GameObject obj)
    {
        Vector3 sumVector = Vector3.zero;

        foreach(Transform child in obj.transform)
        {
            sumVector += child.position;
        }

        //Vector3 groupCenter = sumVector / obj.transform.childCount;

        return sumVector;
    }

    private IEnumerator RunCalibration()
    {
        var time = 5;
        Debug.Log($"Avatar calibration will begin in {time} seconds, please stand in T-pose!");
        yield return new WaitForSeconds(time);

        ScaleAvatar(poseVisualizer.PoseCalibrationRoutine());
        Debug.Log("Avatar calibration done!");
    }

    // Scale the avatar based on the physical dimensions of the user's body
    private void ScaleAvatar(Vector3 bodyTDimensions)
    {
        Vector3 scaling;
        scaling.x = bodyTDimensions.x / avatarDimensions.x;
        scaling.y = bodyTDimensions.y / avatarDimensions.y;
        scaling.z = (scaling.x + scaling.y) / 2f;
        transform.localScale = scaling;
        jointPosOffset.y = avatarDimensions.y - avatarDimensions.y * scaling.y;
        Debug.Log("Avatar scaling done");
    }
}
