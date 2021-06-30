using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassC : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            string msg = "Hello world!";
            this.PostNotification(NotificationExtansion.MSGT_Log, msg);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            string msg = "Hello world!";
            this.PostNotification(NotificationExtansion.MSGT_LogWarning, msg);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            string msg = "Hello world!";
            this.PostNotification(NotificationExtansion.MSGT_LogError, msg);
        }
    }
}
