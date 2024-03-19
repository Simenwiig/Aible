// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class HandTracking : ImageSourceSolution<HandTrackingGraph>
    {
        [SerializeField] private GameObject _hand;
        [SerializeField] private float _handSlerpSpeeed = 20f;
        [Range(0.1f, 3f)][SerializeField] private float _handSensitivity = 1f;

        [HideInInspector] public LandmarkPoints HandParent;
        [HideInInspector] public int HandIndex;
        [HideInInspector] public int HandWorldIndex;

        public List<NormalizedLandmarkList> HandLandmarks;
        public List<LandmarkList> HandWorldLandmarks;

        private float _timer;
        private bool _isHandRemovedFromCamera;
        //private float _handMiddleWidht_pix;

        private void Awake()
        {
            GameObject handClone = GameObject.Instantiate(_hand) as GameObject;
            handClone.name = "Hand";
            HandParent = handClone.GetComponent<LandmarkPoints>();
        }

        protected override void OnStartRun()
        {
            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnHandLandmarksOutput += OnHandLandmarksOutput;
                graphRunner.OnHandWorldLandmarksOutput += OnHandWorldLandmarksOutput;
            }

            var imageSource = ImageSourceProvider.ImageSource;
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
        {
            graphRunner.AddTextureFrameToInputStream(textureFrame);
        }

        protected override IEnumerator WaitForNextValue()
        {
            var task = graphRunner.WaitNext();
            yield return new WaitUntil(() => task.IsCompleted);
        }

        private void OnHandWorldLandmarksOutput(object stream, OutputStream<List<LandmarkList>>.OutputEventArgs eventArgs)
        {
            var packet = eventArgs.packet;
            HandWorldLandmarks = packet == null ? default : packet.Get(LandmarkList.Parser);
        }

        private void OnHandLandmarksOutput(object stream, OutputStream<List<NormalizedLandmarkList>>.OutputEventArgs eventArgs)
        {
            var packet = eventArgs.packet;
            HandLandmarks = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);
        }

        private void Update()
        {
            if (HandLandmarks == null || HandWorldLandmarks == null || HandParent == null)
            {
                if (_timer > 0.1f)
                {
                    HandParent.gameObject.SetActive(false);
                    _isHandRemovedFromCamera = true;
                }
                else
                {
                    _timer += Time.deltaTime;
                }
                return;
            }

            // Makes the hand that you started with stay as the target for the handPoints
            if(HandLandmarks.Count == 2)
            {
                HandIndex = 1;
            }
            else
            {
                HandIndex = 0;
            }

            if (HandWorldLandmarks.Count == 2)
            {
                HandWorldIndex = 1;
            }
            else
            {
                HandWorldIndex = 0;
            }

            NormalizedLandmarkList handLandmarks = HandLandmarks[HandIndex];
            LandmarkList handWorldLandmarks = HandWorldLandmarks[HandWorldIndex];

            MoveHandParent(handLandmarks);

            MoveHanPoints(handWorldLandmarks);
        }

        private void MoveHandParent(NormalizedLandmarkList handLandmarks)
        {
            // All the stuff thats commented out is for Z movement for the Parent Hand

            //float handX = handLandmarks.Landmark[5].X - handLandmarks.Landmark[17].X; 
            //float handY = handLandmarks.Landmark[5].Y - handLandmarks.Landmark[17].Y;
            //float handZ = handLandmarks.Landmark[5].Z - handLandmarks.Landmark[17].Z;

            if (_isHandRemovedFromCamera)
            {
                
                HandParent.gameObject.SetActive(true);
                //_handMiddleWidht_pix = MathF.Sqrt(MathF.Pow(handX, 2) + MathF.Pow(handY, 2) + MathF.Pow(handZ, 2));
            }

            if (_timer > 0)
            {
                _timer = 0;
                _isHandRemovedFromCamera = false;
            }            

            //float handWidht_pix = MathF.Sqrt(MathF.Pow(handX, 2) + MathF.Pow(handY, 2) + MathF.Pow(handZ, 2));

            //float widhtDiff = handWidht_pix - _handMiddleWidht_pix;

            float parentX = handLandmarks.Landmark[9].X - 0.5f;
            float parentY = 0.6f - handLandmarks.Landmark[9].Y;
            float parentZ = 0;

            HandParent.gameObject.transform.localPosition = Vector3.Slerp(HandParent.gameObject.transform.localPosition,
            new Vector3(parentX * 25 * _handSensitivity, parentY * 25 * _handSensitivity, parentZ),
            _handSlerpSpeeed * Time.deltaTime);
        }

        private void MoveHanPoints(LandmarkList handWorldLandmarks)
        {
            for (int i = 0; i < HandParent.Points.Count; i++)
            {
                float x = handWorldLandmarks.Landmark[i].X * 20;
                float y = handWorldLandmarks.Landmark[i].Y * 20;
                float z = handWorldLandmarks.Landmark[i].Z * 20;

                HandParent.Points[i].transform.localPosition = Vector3.Slerp(HandParent.Points[i].transform.localPosition, new Vector3(x, y, z), _handSlerpSpeeed * Time.deltaTime);
            }
        }
    }
}
