using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationExtansion
{
    public static string MSGT_Log = "Log";
    public static string MSGT_LogWarning = "LogWarning";
    public static string MSGT_LogError = "LogError";

    public static void AddObserver( this Component observer, string[] msgs)
    {
        if (msgs.Length <= 0)
            return;

        for (int i = 0; i < msgs.Length; ++i)
        {
            if (msgs[i] == null || msgs[i] == "") { Debug.Log("Null name specified for notification in AddObserver."); continue; }
            NotificationCenter.DefaultCenter().AddObserver(observer, msgs[i]);
        }
    }

    public static void PostNotification(this Component sender, string msg, object data)
    {
        NotificationCenter.DefaultCenter().PostNotification(sender, msg, data);
    }
}
