using System;
using UnityEngine;
using UnityEngine.UI;

public class GNSSPos : MonoBehaviour
{
    Text text;
    bool SuccessInit = false;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        try
        {
            HiperUSB.Init();
        }
        catch (Exception e)
        {
            text.text = e.Message;
            return;
        }
        SuccessInit = true;
    }

    void Update()
    {
        if (!SuccessInit)
        {
            return;
        }
        text.text = string.Format("{0}", HiperUSB.ReadPos());
    }

    void OnApplicationQuit()
    {
        HiperUSB.Cleanup();
    }
}
