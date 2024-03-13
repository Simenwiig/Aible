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
        [Range(0.1f, 3f)][SerializeField] private float _handSensitivity = 1f;

        [HideInInspector] public HandPoints HandParent;
        public List<NormalizedLandmarkList> HandLandmarks;
        public List<LandmarkList> HandWorldLandmarks;

        private float _timer;

        private void Awake()
        {
            GameObject handClone = GameObject.Instantiate(_hand) as GameObject;
            handClone.name = "Hand";
            HandParent = handClone.GetComponent<HandPoints>();
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
                }
                else
                {
                    _timer += Time.deltaTime;
                }
                return;
            }

            if (_timer > 0)
            {
                _timer = 0;
            }

            HandParent.gameObject.SetActive(true);

            float handX = HandLandmarks[0].Landmark[5].X - HandLandmarks[0].Landmark[17].X;
            float handY = HandLandmarks[0].Landmark[5].Y - HandLandmarks[0].Landmark[17].Y;
            float handZ = HandLandmarks[0].Landmark[5].Z - HandLandmarks[0].Landmark[17].Z;

            float handWidht_pix = MathF.Sqrt(MathF.Pow(handX, 2) + MathF.Pow(handY, 2) + MathF.Pow(handZ, 2));

            float parentX = HandLandmarks[0].Landmark[9].X - 0.5f;
            float parentY = 0.5f - HandLandmarks[0].Landmark[9].Y;
            float parentZ = 0;

            HandParent.gameObject.transform.localPosition = new Vector3(parentX * 25 * _handSensitivity,
                                                            parentY * 15 * _handSensitivity, parentZ);

            for (int i = 0; i < HandParent._HandPoints.Count; i++)
            {
                float x = HandWorldLandmarks[0].Landmark[i].X * 20;
                float y = HandWorldLandmarks[0].Landmark[i].Y * 20;
                float z = HandWorldLandmarks[0].Landmark[i].Z * 20;


            
                HandParent._HandPoints[i].transform.localPosition = Vector3.Lerp(HandParent._HandPoints[i].transform.localPosition, new Vector3(x, y, z), 15f);
                
            }
        }
    }
}
