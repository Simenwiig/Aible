using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamVisualizer : MonoBehaviour
{
    [SerializeField] WebCamInput webCamInput;
    [SerializeField] RawImage webCamImageUI;

    private void Update()
    {
        webCamImageUI.texture = webCamInput.inputImageTexture;
    }
}
