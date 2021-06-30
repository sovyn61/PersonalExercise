
/*
 * 题目：给定两个大小为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。请你找出这两个正序数组的中位数，并且要求算法的时间复杂度为 O(log(m + n))。
 * 注：中位数就是一串数字排序后，处于中间的数。如果数量为偶数，则取中间两个数，求平均。
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindMedianNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] nums1 = new int[] { 1,2,3,4,5,6,7 };
        int[] nums2 = new int[] { 4,5,6,7,8,9,10 };

        System.DateTime before = System.DateTime.Now;
        Debug.Log(FindMedianSortedArrays_2(nums1,nums2));
        Debug.Log(System.DateTime.Now - before);
    }

    #region 解法一
    /// <summary>
    /// 先忽略算法时间复杂度的限制，把较少的数组往较多的数组里填。然后找到中位数
    /// </summary>

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        double result = 0;

        int[] lessArr;
        List<int> totalList;

        if (nums1.Length > nums2.Length)
        {
            lessArr = nums2;
            totalList = nums1.ToList();
        }
        else
        {
            lessArr = nums1;
            totalList = nums2.ToList();
        }

        for (int i = 0; i < lessArr.Length; ++i)
        {
            int moreIndex = 0;
            for (moreIndex = 0; moreIndex < totalList.Count; ++moreIndex)
            {
                if (lessArr[i] >= totalList[moreIndex])
                {
                    if (moreIndex != totalList.Count - 1)
                        continue;

                    totalList.Add(lessArr[i]);
                    break;
                }
                else
                {
                    totalList.Insert(moreIndex, lessArr[i]);
                    break;
                }
            }
        }

        if (totalList.Count % 2 == 0)
        {
            result = (totalList[totalList.Count / 2] + totalList[totalList.Count / 2 - 1]) * 0.5f;
        }
        else
        {
            result = totalList[totalList.Count / 2];
        }

        return result;
    }
    #endregion



    #region 解法二
    /// <summary>
    /// 用归并排序
    /// </summary>

    public double FindMedianSortedArrays_2(int[] nums1, int[] nums2)
    {
        int count1 = nums1.Length;
        int count2 = nums2.Length;
        if (count1 <= 0)
        {
            return GetMedian(nums2);
        }

        if (count2 <= 0)
        {
            return GetMedian(nums1);
        }

        int sIndex, lIndex;
        if ((count1 + count2) % 2 == 0)
        {
            lIndex = (count1 + count2) / 2;
            sIndex = lIndex - 1;
        }
        else
        {
            sIndex = lIndex = (count1 + count2) / 2;
        }

        List<int> totalList = new List<int>();
        int i, j;
        for (i=j=0;i<count1 && j<count2;)
        {
            if (nums1[i] <= nums2[j])
            {
                totalList.Add(nums1[i++]);
            }
            else
            {
                totalList.Add(nums2[j++]);
            }

            if (totalList.Count >= lIndex+1)
            {
                return (totalList[sIndex] + totalList[lIndex]) * 0.5f;
            }
        }
        while (i < count1)
        {
            totalList.Add(nums1[i++]);
            if (totalList.Count >= lIndex+1)
            {
                return (totalList[sIndex] + totalList[lIndex]) * 0.5f;
            }
        }
        while (j < count2)
        {
            totalList.Add(nums2[j++]);
            if (totalList.Count >= lIndex+1)
            {
                return (totalList[sIndex] + totalList[lIndex]) * 0.5f;
            }
        }

        return GetMedian(totalList.ToArray());
    }


    private double GetMedian(int[] nums)
    {
        int count = nums.Length;
        if (count % 2 == 0)
        {
            return (nums[count / 2] + nums[count / 2 - 1]) * 0.5f;
        }
        else
        {
            return nums[count / 2];
        }
    }
    #endregion



    #region 解法三
    /// <summary>
    /// 
    /// </summary>

    public double FindMedianSortedArrays_3(int[] nums1, int[] nums2)
    {
        int count1 = nums1.Length;
        int count2 = nums2.Length;
        if (count1 <= 0)
        {
            return GetMedian(nums2);
        }

        if (count2 <= 0)
        {
            return GetMedian(nums1);
        }
        double result = 0;
        return result;
    }
    #endregion
}
