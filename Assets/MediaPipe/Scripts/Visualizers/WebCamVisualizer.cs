using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamVisualizer : MonoBehaviour
{
    [SerializeField] bool showCam = true;
    [SerializeField] WebCamInput webCamInput;
    [SerializeField] RawImage webCamImageUI;

    private void Start()
    {
        webCamImageUI.gameObject.SetActive(showCam);      
    }

    private void LateUpdate()
    {
        if (!showCam)
            return;

        webCamImageUI.texture = webCamInput.inputImageTexture;
    }

    public void ChangeCam()
    {
        if (showCam)
        {
            showCam = false;
            webCamImageUI.gameObject.SetActive(false);
        }
        else
        {
            showCam = true;
            webCamImageUI.gameObject.SetActive(true);
        }     
    }
}
