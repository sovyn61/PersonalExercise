using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    private void Awake()
    {
        //JsonData jsonData = new JsonData();
        //jsonData["name"] = SystemInfo.deviceName;
        //jsonData["os"] = SystemInfo.operatingSystem;
        //jsonData["event"] = SystemInfo.deviceUniqueIdentifier;

        //Debug.Log("Info:" + jsonData.ToJson());

        //DeviceInfo deviceInfo = LitJson.JsonMapper.ToObject<DeviceInfo>(jsonData.ToJson());

        //Debug.Log("info.event:" + deviceInfo.@event);

        Debug.Log(QString.Concat("age:",16,"! name:",'Y'));

        int ten = 48;
        char tenchar = (char)ten;
        Debug.Log(tenchar);

        //
        string intChars = "456abcde";
        List<int> listInt = new List<int>();

        for (int i = 0; i < intChars.Length; ++i)
        {
            char temChar = intChars[i];
            int temInt = (int)temChar - 48;
            Debug.Log(temInt);
            listInt.Add(temInt);
        }

        string result = string.Join("", listInt);
        Debug.Log(result);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < listInt.Count; ++i)
        {
            char temChar = (char)(listInt[i] + 48);
            builder.Append(temChar);
        }
        Debug.Log(builder.ToString());
    }
}

[Serializable]
public class DeviceInfo
{
    public string name;
    public string os;
    public string @event;
}
