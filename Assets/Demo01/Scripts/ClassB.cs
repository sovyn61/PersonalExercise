using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.AddObserver(new string[] {
            NotificationExtansion.MSGT_Log,
            NotificationExtansion.MSGT_LogWarning
        });
    }

    public void Log(Notification notification)
    {
        Debug.Log("Observer is " + transform.name + ". Sender is " + notification.sender.transform.name + ". Message:" + notification.data.ToString());
    }

    public void LogWarning(Notification notification)
    {
        Debug.LogWarning("Observer is " + transform.name + ". Sender is " + notification.sender.transform.name + ". Message:" + notification.data.ToString());
    }
}
