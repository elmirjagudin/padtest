using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orien : MonoBehaviour
{
    public Text debug;

    void Start()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.01f;
    }

    static Quaternion GyroToUnity(Quaternion q)
    {
        //var x = new Quaternion();
        //x.eulerAngles = new Vector3(90f, 0, 0);

        //return x * new Quaternion(-q.x, -q.y, q.z, q.w);

        return new Quaternion(-q.x, -q.y, q.z, q.w); // almost right, 90 degress off
        //return new Quaternion(q.x, q.y, q.z, -q.w);
    }

    // Update is called once per frame
    void Update () {
        //transform.rotation = GyroToUnity(Input.gyro.attitude);
        transform.localRotation = GyroToUnity(Input.gyro.attitude);

        debug.text = string.Format("W{0} {1}|", Input.gyro.attitude.eulerAngles, Input.gyro.updateInterval);
        //transform.Rotate(new Vector3(0, 90f, 0));
    }
}
