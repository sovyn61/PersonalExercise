
/*
 * 题目：给定一个正整数集合和一个目标正整数。找出集合中两数之和为目标数的组合下标。
 * 可以假定，组合唯一，并且不可重复使用集合中的数。
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoNumSum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] src = new int[]{ 2,7,11,15};
        int tg = 9;

        int[] result = TwoSum(src, tg);
        Debug.Log("[" + result[0] + "," + result[1] + "]");
    }

    #region 解法一
    public int[] TwoSum(int[] nums, int target)
    {
        int[] result = new int[2];

        for (int i = 0; i < nums.Length; ++i)
        {
            int offset = target - nums[i];
            for (int j = i + 1; j < nums.Length; ++j)
            {
                if(nums[j] == offset)
                {
                    result[0] = i;
                    result[1] = j;
                    return result;
                }
            }
        }

        return result;
    }

    #endregion
}
