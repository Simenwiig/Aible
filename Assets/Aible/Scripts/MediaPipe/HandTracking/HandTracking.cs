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

        private HandPoints _handParent;
        public List<NormalizedLandmarkList> _handLandmarks;
        public List<LandmarkList> _handWorldLandmarks;

        private void Awake()
        {
            GameObject handClone = GameObject.Instantiate(_hand) as GameObject;
            handClone.name = "Hand";
            _handParent = handClone.GetComponent<HandPoints>();
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
            _handWorldLandmarks = packet == null ? default : packet.Get(LandmarkList.Parser);     
        }

        private void OnHandLandmarksOutput(object stream, OutputStream<List<NormalizedLandmarkList>>.OutputEventArgs eventArgs)
        {
            var packet = eventArgs.packet;
            _handLandmarks = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);
        }    

        private void Update()
        {
            if (_handLandmarks == null || _handWorldLandmarks == null || _handParent == null)
            {
                _handParent.gameObject.SetActive(false);
                return;
            }
            _handParent.gameObject.SetActive(true);

            for (int i = 0; i < _handParent._HandPoints.Count; i++)
            {
                float x = _handLandmarks[0].Landmark[i].X - 0.5f;
                float y = _handLandmarks[0].Landmark[i].Y - 0.5f;
                float z = _handWorldLandmarks[0].Landmark[i].Z;

                _handParent._HandPoints[i].transform.localPosition = new Vector3(x * 8, y * 6, z * 20);
            }
        }
    }
}
