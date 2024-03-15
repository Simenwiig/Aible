using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamInput : MonoBehaviour
{
    public bool isMobile = false;
    [SerializeField, WebCamName] string webCamName;
    [SerializeField] Vector2 webCamResolution = new Vector2(640, 360);
    public bool mirrorHorizontally = false;

    private RenderTexture inputRT;
    private WebCamTexture webCamTexture;

    public Texture inputImageTexture
    {
        get
        {
            return inputRT;
        }
    }

    private void Start()
    {
        if (isMobile)
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.Log("No camera detected");
                return;
            }

            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].isFrontFacing)
                {
                    webCamTexture = new WebCamTexture(devices[i].name, (int)webCamResolution.x, (int)webCamResolution.y);
                    break;
                }
            }
        }
        else
        {
            webCamTexture = new WebCamTexture(webCamName, (int)webCamResolution.x, (int)webCamResolution.y);
        }

        if (webCamTexture == null)
        {
            return;
        }

        //Initialize WebCamTexture to use propper Web Camera    
        webCamTexture.Play();

        inputRT = new RenderTexture((int)webCamResolution.x, (int)webCamResolution.y, 0);
    }

    private void Update()
    {
        if (!webCamTexture.didUpdateThisFrame) return; //Use this to check if the video buffer has changed since the last frame

        var aspect1 = (float)webCamTexture.width / webCamTexture.height;
        var aspect2 = (float)inputRT.width / inputRT.height;
        var aspectGap = aspect2 / aspect1;
        var hMirrored = mirrorHorizontally ? -1 : 1;
        aspectGap = aspectGap * hMirrored;
        var vMirrored = webCamTexture.videoVerticallyMirrored;
        var scale = new Vector2(aspectGap, vMirrored ? -1 : 1);
        var offset = new Vector2((1 - aspectGap) / 2, vMirrored ? 1 : 0);

        //Copy the pixel data from a texture into a render texture. A Render Texture is a type of Texture that Unity creates and updates at run time. 
        Graphics.Blit(webCamTexture, inputRT, scale, offset);
    }

    private void OnDestroy()
    {
        if (webCamTexture != null) webCamTexture.Stop(); ;
        if (webCamTexture != null) Destroy(webCamTexture);
        if (inputRT != null) Destroy(inputRT);
    }
}
