using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.AddObserver(new string[] {
            NotificationExtansion.MSGT_Log
        });
    }

    public void Log()
    {
        Debug.Log("Observer is " + transform.name);
    }
}
