using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    Text text;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        text.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }
}
