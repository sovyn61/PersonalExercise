
/*
 * 题目：给定一个只包含数字的字符串，找出其中所有可能的IP地址。
 * 注：ip地址各段中的数字不能以0开始，除非为0。比如，不能为["01.010.011.01"]，可以是["0.0.0.0"]
 * 例如：
 * 输入: "25525511135"
 * 输出: ["255.255.11.135", "255.255.111.35"]
 * 
 */


using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RestoreIPAddr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string src = "010010";

        System.DateTime before = System.DateTime.Now;
        List<string> result = RestoreIpAddresses(src);
        Debug.Log(System.DateTime.Now - before);

        StringBuilder stringBuilder = QString.GetShareStringBuilder();
        for (int i = 0; i < result.Count; ++i)
        {
            stringBuilder.Append(result[i]).Append("\n");
        }
        Debug.Log(stringBuilder.ToString());
    }

    #region 解法一
    /// <summary>
    /// 思路：用递归逐字符切分父串
    /// 详细描述：
    /// </summary>

    public List<string> RestoreIpAddresses(string s)
    {
        List<string> result = new List<string>();
        List<string> subStr = new List<string>();

        GetSubStr(ref result,ref subStr, s, 4);

        return result;
    }

    public void GetSubStr(ref List<string> result,ref List<string> subStr, string src, int count)
    {
        if (count == 1)
        {
            if (src.Length > 3)
            {
                subStr.RemoveAt(subStr.Count-1);
            }
            else
            {
                if (src.Length > 1 && src[0] == '0')
                {
                    subStr.RemoveAt(subStr.Count - 1);
                    return;
                }

                int num = int.Parse(src);
                if (num > 255)
                {
                    subStr.RemoveAt(subStr.Count - 1);
                }
                else
                {
                    subStr.Add(src);
                    result.Add(string.Join(".", subStr));
                    //
                    subStr.RemoveAt(subStr.Count - 1);
                    subStr.RemoveAt(subStr.Count - 1);
                }
            }
        }
        else
        {
            for (int i = 0; i < src.Length; ++i)
            {
                string subtem = src.Substring(0, i + 1);
                if (i > 2 || (i == 2 && int.Parse(subtem) > 255) || i + 1 == src.Length || (subtem.Length > 1 && subtem[0] == '0'))
                {
                    if (subStr.Count > 0)
                        subStr.RemoveAt(subStr.Count - 1);
                    break;
                }
                subStr.Add(subtem);
                GetSubStr(ref result, ref subStr, src.Substring(i + 1, (src.Length - i - 1)), count - 1);
            }
        }
    }

    #endregion
}
