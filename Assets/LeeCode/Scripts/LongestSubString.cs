
/*
 * 题目：给定一个字符串，找出其中不含有重复字符的 最长子串 的长度。
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongestSubString : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string src = "pwwkew";

        System.DateTime before = System.DateTime.Now;
        Debug.Log(LengthOfLongestSubstring_2(src));
        Debug.Log(System.DateTime.Now - before);
    }

    #region 解法一

    /// <summary>
    /// 用一个char的list来存储最长子串，每加入一个char之前，查找是否有重复的，如果有，则移除重复字符及其之前加入的所有字符。
    /// 这里，有点弄巧成拙了。反序存储，并不会更快。
    /// </summary>

    public int LengthOfLongestSubstring(string s)
    {
        List<char> result = new List<char>();
        int maxLength = 0;

        for (int i = 0; i < s.Length; ++i)
        {
            char tem = s[i];
            int removeIndex = -1;
            for (int j = 0; j < result.Count; ++j)
            {
                if(tem == result[j])
                {
                    removeIndex = j;
                    if (result.Count > maxLength)
                        maxLength = result.Count;
                    break;
                }
            }
            if (removeIndex >= 0)
            {
                result.RemoveRange(removeIndex, result.Count - removeIndex);
            }
            result.Insert(0, tem);
        }

        return result.Count > maxLength ? result.Count : maxLength;
    }

    #endregion


    #region 解法二

    /// <summary>
    /// 用一个char的list来存储最长子串，每加入一个char之前，查找是否有重复的，如果有，则移除重复字符及其之前加入的所有字符。
    /// 直接正序存储，之前移除的时候想岔了，以为反序会更方便移除。其实，c#list的removerange，正序反而更方便。
    /// </summary>

    public int LengthOfLongestSubstring_2(string s)
    {
        List<char> result = new List<char>();
        int maxLength = 0;

        for (int i = 0; i < s.Length; ++i)
        {
            char tem = s[i];
            if (result.Contains(tem))
            {
                if (result.Count > maxLength)
                    maxLength = result.Count;

                result.RemoveRange(0, result.IndexOf(tem) + 1);
            }
            result.Add(tem);
        }

        return result.Count > maxLength ? result.Count : maxLength;
    }

    #endregion
}
