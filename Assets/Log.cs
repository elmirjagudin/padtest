using UnityEngine;

public class Log
{
    public static void Msg(string format, params object[] args)
    {
        Debug.LogFormat(format, args);
    }
}
