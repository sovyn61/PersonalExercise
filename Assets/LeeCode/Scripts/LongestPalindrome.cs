
/*
 * 题目：给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。
 *
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongestPalindrome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string src = "aaaa";

        System.DateTime before = System.DateTime.Now;
        Debug.Log(GetLongestPalindrome_3(src));
        Debug.Log(System.DateTime.Now - before);
    }

    #region 解法一
    /// <summary>
    /// 思路：用两个循环嵌套，一个正向遍历所有开头，一个反向遍历从长到短的所有字符串，并判断是否为回文。
    /// </summary>

    public string GetLongestPalindrome(string s)
    {
        string result = "";

        string temSubStr = null;
        for (int i = 0; i < s.Length; ++i)
        {
            temSubStr = s.Substring(i, s.Length - i);
            while (temSubStr.Length > 0)
            {
                if (IsPalindrome(temSubStr))
                {
                    if (result.Length <= temSubStr.Length)
                        result = temSubStr;

                    break;
                }
                else
                {
                    temSubStr = temSubStr.Remove(temSubStr.Length - 1);
                }
            }
        }

        return result;
    }

    private bool IsPalindrome(string src)
    {
        int count = src.Length;
        if (count == 1)
            return true;

        for (int i = 0; i < count; ++i)
        {
            if (!src[i].Equals(src[count - 1 - i]))
                return false;
        }

        return true;
    }

    #endregion


    #region 解法二
    /// <summary>
    /// 思路：从长度出发，从长到短，枚举所有子串，并判断是否为回文串。
    /// </summary>

    public string GetLongestPalindrome_2(string s)
    {
        int subLength = s.Length;

        while (subLength > 0)
        {
            string subStr = null;
            for (int i = 0; i < s.Length - subLength + 1; ++i)
            {
                subStr = s.Substring(i,subLength);
                if (IsPalindrome(subStr))
                    return subStr;
            }
            subLength--;
        }

        return "";
    }

    #endregion


    #region 解法三
    /// <summary>
    /// 思路：遍历源字符串，以当前字符为回文串的中心字符，尝试构建回文
    /// </summary>

    public string GetLongestPalindrome_3(string s)
    {
        int subLength = s.Length;
        if (subLength == 0)
            return "";

        if (subLength == 1)
            return s;

        char[] srcArr = s.ToCharArray();
        List<char> result = new List<char>();

        for (int i = 0; i < subLength; ++i)
        {
            //边缘
            if (i - 1 < 0)
            {
                if (srcArr[i] == srcArr[i + 1])
                {
                    if(result.Count < 2)
                    {
                        result.Clear();
                        result.Add(srcArr[i]);
                        result.Add(srcArr[i + 1]);
                    }
                }
                else
                {
                    if (result.Count < 1)
                    {
                        result.Clear();
                        result.Add(srcArr[i]);
                    }
                }
                continue;
            }
            if (i+1>=srcArr.Length)
            {
                if (srcArr[i] == srcArr[i - 1])
                {
                    if(result.Count < 2)
                    {
                        result.Clear();
                        result.Add(srcArr[i - 1]);
                        result.Add(srcArr[i]);
                    }
                }
                else
                {
                    if (result.Count < 1)
                    {
                        result.Clear();
                        result.Add(srcArr[i]);
                    }
                }
                continue;
            }

            //构建回文
            int lindex = i - 1;
            int rindex = i + 1;

            while (isEquals(ref srcArr, lindex, srcArr[i]) || isEquals(ref srcArr, rindex, srcArr[i]))
            {
                if (isEquals(ref srcArr, lindex, srcArr[i]) && isEquals(ref srcArr, rindex, srcArr[i]))
                {
                    lindex--;
                    rindex++;
                }
                else if (isEquals(ref srcArr, lindex, srcArr[i]))
                    lindex--;
                else if (isEquals(ref srcArr, rindex, srcArr[i]))
                    rindex++;
            }

            while (lindex >= 0 && rindex < subLength)
            {
                if(srcArr[lindex] != srcArr[rindex])
                {
                    break;
                }
                lindex--;
                rindex++;
            }
            if (result.Count < rindex - lindex - 1)
            {
                result.Clear();
                for (int n = lindex + 1; n < rindex; ++n)
                {
                    result.Add(srcArr[n]);
                }
            }
        }

        return string.Join("",result);
    }

    private bool isEquals(ref char[] srcArr, int index, char targ)
    {
        if (index < 0 || index >= srcArr.Length)
            return false;
        else
            return srcArr[index] == targ;
    }

    #endregion
}
