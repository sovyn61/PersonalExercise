
/*
 * 题目：给定一个字符串，找出其中相同字符具有相同数量的字串（这里是二进制字符串，字符只有‘0’和‘1’），并且相同字符要连续。
 * 例如：
 * 输入: "00110011"
 * 输出: 6
 * 解释: 有6个子串具有相同数量的连续1和0：“0011”，“01”，“1100”，“10”，“0011” 和 “01”。
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountSubStrings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string src = "00110011";

        System.DateTime before = System.DateTime.Now;
        Debug.Log(CountBinarySubstrings_2(src));
        Debug.Log(System.DateTime.Now - before);
    }


    #region 解法一

    /// <summary>
    /// 思路：遍历父串，找到第一个相同字符，并记录其长度，如果其后的字符长度不满足条件，则取第二个字符重复此逻辑。
    /// </summary>
    /// 
    public int CountBinarySubstrings(string s)
    {
        int result = 0;

        for (int i = 0; i < s.Length;)
        {
            bool first = true;
            int num1 = 1;
            int num2 = 0;
            for (int n = i + 1; n < s.Length; ++n)
            {
                if (s[n] == s[i])
                {
                    if (first)
                        num1 += 1;
                    else
                        break;
                }
                else
                {
                    first = false;
                    num2 += 1;
                    if (num2 == num1)
                    {
                        result += 1;
                        break;
                    }
                }
            }
            i += num1 == num2 ? 1 : (num1 - num2);
        }

        return result;
    }
    #endregion



    #region 解法二

    /// <summary>
    /// 思路：遍历父串，先把相同字符的子串全找出来，并记录其长度。
    /// </summary>
    /// 
    public int CountBinarySubstrings_2(string s)
    {
        int result = 0;
        if (s.Length > 1)
        {
            List<int> subCount = new List<int>();
            int subnum = 1;
            char temchar = s[0];
            for (int i = 1; i < s.Length; ++i)
            {
                if (s[i]==temchar)
                {
                    subnum += 1;
                }
                else
                {
                    subCount.Add(subnum);
                    subnum = 1;
                    temchar = s[i];
                }
            }
            subCount.Add(subnum);

            for (int i = 0; i < subCount.Count-1; ++i)
            {
                result += subCount[i] <= subCount[i + 1] ? subCount[i] : subCount[i + 1];
            }
        }

        return result;
    }
    #endregion
}
