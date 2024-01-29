using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileCam : MonoBehaviour
{
    private bool camAvaiable;
    private WebCamTexture frontCam;
    private Texture defaultBackground;

    private RenderTexture inputRT;

    [SerializeField] Vector2 webCamResolution = new Vector2(624, 360);
    public RawImage background;

    public Texture inputImageTexture
    {
        get
        {
            return inputRT;
        }
    }

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvaiable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                frontCam = new WebCamTexture(devices[i].name, (int)webCamResolution.x, (int)webCamResolution.y);
            }
        }

        if(frontCam == null)
        {
            return;
        }

        frontCam.Play();
        background.texture = frontCam;
        inputRT = new RenderTexture((int)webCamResolution.x, (int)webCamResolution.y, 0);

        camAvaiable = true;
    }

    private void Update()
    {
        if (!camAvaiable)
            return;

        float ratio = (float)frontCam.width / (float)frontCam.height;

        float scaleY = frontCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = frontCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        var aspect2 = (float)inputRT.width / inputRT.height;
        var aspectGap = aspect2 / ratio;
        var vMirrored = frontCam.videoVerticallyMirrored;
        var scale = new Vector2(aspectGap, vMirrored ? -1 : 1);
        var offset = new Vector2((1 - aspectGap) / 2, vMirrored ? 1 : 0);

        //Copy the pixel data from a texture into a render texture. A Render Texture is a type of Texture that Unity creates and updates at run time. 
        Graphics.Blit(frontCam, inputRT, scale, offset);
    }
}
