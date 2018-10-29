using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListCams : MonoBehaviour
{
    public RawImage rawimage;

    void ListDevs()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        Log.Msg("have {0} devices", devices.Length);
        for (int i = 0; i < devices.Length; i++)
        {
            var dev = devices[i];
            Log.Msg("dev {0} name {1} front facing {2}", i, dev.name, dev.isFrontFacing);
        }
    }

    void Start()
    {
            WebCamTexture webcamTexture = new WebCamTexture();
            rawimage.texture = webcamTexture;
            rawimage.material.mainTexture = webcamTexture;
            webcamTexture.Play();
    }
}
