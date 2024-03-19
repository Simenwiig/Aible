using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEngine;
using static Mediapipe.ImageFrame;

namespace Mediapipe.Unity.Sample.PoseTracking
{
    public class PoseLandmarkSolution : ImageSourceSolution<PoseTrackingGraph>
    {
        [HideInInspector] public NormalizedLandmarkList _LandmarkList;
        [HideInInspector] public LandmarkList _LandmarkWordList;

        protected override void SetupScreen(ImageSource imageSource) => base.SetupScreen(imageSource);

        protected override void OnStartRun()
        {
            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnPoseLandmarksOutput += OnPoseLandmarksOutput;
                graphRunner.OnPoseWorldLandmarksOutput += OnPoseWorldLandmarksOutput;
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
            _LandmarkList = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);
        }

        private void OnPoseWorldLandmarksOutput(object stream, OutputStream<LandmarkList>.OutputEventArgs eventArgs)
        {
            var packet = eventArgs.packet;
            _LandmarkWordList = packet == null ? default : packet.Get(LandmarkList.Parser);
        }
    }
}
