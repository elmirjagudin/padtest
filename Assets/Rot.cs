using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    public Vector3 RotateAround;

    void Update()
    {
        gameObject.transform.RotateAround(RotateAround, Vector3.up, 20 * Time.deltaTime);
    }
}
