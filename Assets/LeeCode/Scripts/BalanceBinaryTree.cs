
/*
 * 题目：给定一个二叉树，判断它是否是高度平衡的二叉树。
 * 本题中，一棵高度平衡二叉树定义为：
 *      一个二叉树每个节点 的左右两个子树的高度差的绝对值不超过1。
 * 
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBinaryTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TreeNode p = new TreeNode();
        p.val = 1;
        p.left = null;
        p.right = new TreeNode() { val = 2 };
        p.right.left = null;
        p.right.right = new TreeNode() { val = 3 };


        System.DateTime before = System.DateTime.Now;
        bool result = IsBalanced(p);
        Debug.Log(System.DateTime.Now - before);
        Debug.Log(result);
    }

    #region 解法一
    /// <summary>
    /// 递归遍历，并计算树的深度
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public bool IsBalanced(TreeNode root)
    {
        int height = 0;
        return GetTreeHeight(root,ref height);
    }

    public bool GetTreeHeight(TreeNode root, ref int height)
    {
        bool isBalance = true;

        if (root == null)
            return isBalance;
        else
        {
            height += 1;
        }

        int leftH, rightH;
        leftH = rightH = 0;

        if (root.left != null)
        {
            isBalance &= GetTreeHeight(root.left, ref leftH);
        }

        if (root.right != null)
        {
            isBalance &= GetTreeHeight(root.right, ref rightH);
        }

        height += Mathf.Max(leftH, rightH);

        if (isBalance && Mathf.Abs(leftH - rightH) > 1)
            isBalance = false;

        return isBalance;
    }
    #endregion
}
