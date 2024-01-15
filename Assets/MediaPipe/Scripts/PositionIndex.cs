// Position index of joint point
public enum PositionIndex : int
{
    Nose = 0,
    lEyeInner,
    lEye,
    lEyeOuter,
    rEyeInner,
    rEye,
    rEyeOuter,
    lEar,
    rEar,
    mouthL,
    mouthR,
    lShoulder,
    rShoulder,
    lElbow,
    rElbow,
    lWrist,
    rWrist,
    lPinky,
    rPinky,
    lIndex,
    rIndex,
    lThumb,
    rThumb,
    lHip,
    rHip,
    lKnee,
    rKnee,
    lAnkle,
    rAnkle,
    lHeel,
    rHeel,
    lFootIndex,
    rFootIndex,
    humanVisible,

    //Calculated coordinates
    head,
    neck,
    chest,
    spine,
    hips,
    lController,
    rController,
    centerHead,

    Count,
    None,
}

public static partial class EnumExtend
{
    public static int Int(this PositionIndex i)
    {
        return (int)i;
    }
}
