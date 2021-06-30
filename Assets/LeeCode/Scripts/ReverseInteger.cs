
/*
 * 题目：给出一个 32 位的有符号整数，你需要将这个整数中每位上的数字进行反转。

    示例 1:

    输入: 123
    输出: 321
     示例 2:

    输入: -123
    输出: -321
    示例 3:

    输入: 120
    输出: 21
 * 
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ReverseInteger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.DateTime before = System.DateTime.Now;
        int result = Reverse(-153423000);
        Debug.Log(System.DateTime.Now - before);
        Debug.Log(result);
    }


    #region 解法一

    /// <summary>
    /// 转换为字符串，然后反转字符串。注意符号和原数字末尾的0；还要注意溢出的情况。
    /// </summary>

    public int Reverse(int x)
    {
        // 1.先处理数字，将末尾的0都去掉
        while(true)
        {
            int remainder = x % 10;
            if (remainder == 0)
                x /= 10;
            else
                break;
        }


        // 2.预处理负数
        bool isMinus = x < 0;
        if (isMinus)
        {
            x *= -1;
        }

        // 3.字符串反转
        string temStr = x.ToString();
        char[] temArr = temStr.ToCharArray();
        Array.Reverse(temArr);

        // 4.生成新的字符串，再转换为int
        int result;
        try
        {
            result = int.Parse(new string(temArr));
            if (isMinus)
                result *= -1;
        }
        catch
        {
            result = 0;
        }
        return result;
    }

    #endregion
}
