
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


using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ReverseInteger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Reverse(1534236469));
    }


    #region 解法一

    /// <summary>
    /// 转换为字符串，然后反转字符串。注意符号和原数字末尾的0；还要注意溢出的情况。
    /// </summary>

    public int Reverse(int x)
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (x < 0)
        {
            stringBuilder.Append("-");
            x *= -1;
        }

        string temStr = x.ToString();
        bool zero = true;
        for (int i = temStr.Length - 1; i >= 0; --i)
        {
            if (zero && temStr[i].Equals("0"))
                break;

            if(zero) zero = false;

            stringBuilder.Append(temStr[i]);
        }

        int result;

        try {
            result = int.Parse(stringBuilder.ToString());
        } catch {
            result = 0;
        }

        return result;
    }

    #endregion
}
