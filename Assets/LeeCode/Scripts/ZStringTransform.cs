
/*
 * 题目：将一个给定字符串根据给定的行数，以从上往下、从左到右进行 Z 字形排列。
 * 比如输入字符串为 "LEETCODEISHIRING" 行数为 3 时，排列如下：
 *      L   C   I   R
 *      E T O E S I I G
 *      E   D   H   N
 * 之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："LCIRETOESIIGEDHN"。
 * 
 * 
 */



using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ZStringTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string src = "AB";

        System.DateTime before = System.DateTime.Now;
        string result = Convert(src,1);
        Debug.Log(System.DateTime.Now - before);
        Debug.Log(result);
    }


    #region 解法一
    /// <summary>
    /// 思路：n个list来依次接收各个字符。最后，把n个list拼接起来
    /// </summary>

    public string Convert(string s, int numRows) 
    {
        if (s.Length <= numRows)
            return s;

        if (numRows <= 1)
            return s;

        List<char>[] listArr = new List<char>[numRows];
        int index = 0;
        int offset = 1;
        for (int i = 0; i < s.Length; ++i)
        {
            if (listArr[index] == null)
                listArr[index] = new List<char>();

            if (index == 0)
                offset = 1;
            else if (index == numRows - 1)
                offset = -1;

            listArr[index].Add(s[i]);
            index += offset;
        }

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < listArr.Length; ++i)
        {
            stringBuilder.Append(string.Join("", listArr[i]));
        }
        return stringBuilder.ToString();
    }
    #endregion
}
