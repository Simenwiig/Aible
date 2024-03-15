using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Mediapipe.ImageFrame;

namespace Mediapipe.Unity.Sample.PoseTracking
{
    public class PoseLandmarkSolution : ImageSourceSolution<PoseTrackingGraph>
    {
        [HideInInspector] public NormalizedLandmarkList LandmarkList;

        protected override void SetupScreen(ImageSource imageSource) => base.SetupScreen(imageSource);

        protected override void OnStartRun()
        {
            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnPoseLandmarksOutput += OnPoseLandmarksOutput;
            }
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame) => graphRunner.AddTextureFrameToInputStream(textureFrame);

        protected override IEnumerator WaitForNextValue()
        {
            var task = graphRunner.WaitNextAsync();
            yield return new WaitUntil(() => task.IsCompleted);
        }

        private void OnPoseLandmarksOutput(object stream, OutputStream<NormalizedLandmarkList>.OutputEventArgs eventArgs)
        {
            var packet = eventArgs.packet;
            LandmarkList = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);
        }

    }
}
