
/*
 * 题目：给出一个字符串集合，找出其中能拼接为回文字符串的原字符串组合，输入其下标。
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuiwenString : MonoBehaviour
{
    private void Start()
    {
        string[] strs = { "abcd", "dcba", "lls", "s", "sssll" };
        PalindromePairs(strs);
    }


    #region 解法一
    /// <summary>
    /// 用两层嵌套穷尽所有字符串拼接组合，新建一个函数来判断字符串是否为回文字符串。
    /// </summary>
    public List<int[]> PalindromePairs(string[] words)
    {
        List<int[]> result = new List<int[]>();


        for (int i = 0; i < words.Length; ++i)
        {
            string subA = words[i];
            for (int j = i + 1; j < words.Length; ++j)
            {
                string subB = words[j];

                string one = string.Concat(subA, subB);
                string two = string.Concat(subB, subA);

                if (IsHuiWen(one))
                {
                    int[] temList = new int[2];
                    temList[0] = i;
                    temList[1] = j;
                    result.Add(temList);
                }

                if (IsHuiWen(two))
                {
                    int[] temList = new int[2];
                    temList[0] = j;
                    temList[1] = i;
                    result.Add(temList);
                }
            }
        }

        for (int i = 0; i < result.Count; ++i)
        {
            int[] tem = result[i];
            Debug.Log("[" + tem[0] + "," + tem[1] + "]");
        }

        return result;
    }

    public bool IsHuiWen(string src)
    {
        int count = src.Length;
        for (int i = 0; i < count; ++i)
        {
            if (!src[i].Equals(src[count - 1 - i]))
                return false;
        }

        return true;
    }
    #endregion
}
