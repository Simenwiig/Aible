using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEngine;
using static Mediapipe.ImageFrame;

namespace Mediapipe.Unity.Sample.PoseTracking
{
    public class PoseLandmarkSolution : ImageSourceSolution<PoseTrackingGraph>
    {
        public bool HideDebugSprite = true;

        [HideInInspector] public NormalizedLandmarkList LandmarkList;

        [SerializeField] private PoseLandmarkListAnnotationController _poseLandmarksAnnotationController;

        protected override void SetupScreen(ImageSource imageSource) => base.SetupScreen(imageSource);

        protected override void OnStartRun()
        {
            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnPoseLandmarksOutput += OnPoseLandmarksOutput;
            }

            //Make a script for this
            if (!HideDebugSprite || _poseLandmarksAnnotationController != null)
            {
                var imageSource = ImageSourceProvider.ImageSource;
                SetupAnnotationController(_poseLandmarksAnnotationController, imageSource);
            }     
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame) => graphRunner.AddTextureFrameToInputStream(textureFrame);

        protected override IEnumerator WaitForNextValue()
        {
            var task = graphRunner.WaitNextAsync();
            yield return new WaitUntil(() => task.IsCompleted);

            //Make a script for this
            if (!HideDebugSprite)
            {
                var result = task.Result;
                _poseLandmarksAnnotationController.DrawNow(result.poseLandmarks);
            }        
        }

        private void OnPoseLandmarksOutput(object stream, OutputStream<NormalizedLandmarkList>.OutputEventArgs eventArgs)
        {
            var packet = eventArgs.packet;
            LandmarkList = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);

            if (!HideDebugSprite)
                _poseLandmarksAnnotationController.DrawLater(LandmarkList);
        }

    }
}
