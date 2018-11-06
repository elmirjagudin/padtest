using UnityEngine;
using UnityEngine.UI;

public class ToggleOrien : MonoBehaviour
{
    public GameObject camCont;

    bool rotate = true;

    public void Toggle()
    {
        rotate = !rotate;
        var t = GetComponentInChildren<Text>();
        var q = new Quaternion();

        if (rotate)
        {
            t.text = "90";

            q.eulerAngles = new Vector3(90f, 0, 0);
        }
        else
        {
            t.text = "0";
            q.eulerAngles = Vector3.zero;
        }

        camCont.transform.rotation = q;
    }
}
