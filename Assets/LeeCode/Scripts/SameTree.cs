
/*
 * 题目：比较两个二叉树是否相等，二叉树存储非零正整数
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class SameTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TreeNode p = new TreeNode();
        p.val = 1;
        p.left = new TreeNode() { val = 2 };
        p.right = new TreeNode() { val = 3 };
        //p.right.left = new TreeNode() { val = 4 };
        //p.right.right = new TreeNode() { val = 5 };


        TreeNode q = new TreeNode();
        q.val = 1;
        q.left = new TreeNode() { val = 2 };
        q.right = new TreeNode() { val = 3 };


        Debug.Log(IsSameTree_4(p,q));
    }

    #region 解法一 运行时间：112ms 内存消耗：24.2MB
    /// <summary>
    /// 思路：先递归遍历两个树，把树转化为list；再比较list是否相等
    /// </summary>
    public bool IsSameTree(TreeNode p, TreeNode q) 
    {
        List<int> plist = new List<int>();
        List<int> qlist = new List<int>();

        GetTreeValue(p, ref plist);
        GetTreeValue(q, ref qlist);

        if(plist.Count != qlist.Count)
            return false;
        else{
            for(int i=0;i<plist.Count;++i){
                if(plist[i] != qlist[i])
                    return false;
            }
            return true;
        }
    }

    public void GetTreeValue(TreeNode n, ref List<int> result)
    {
        if (n == null)
            return;

        result.Add(n.val);

        if (n.left == null && n.right == null)
            return;

        if (n.left == null)
            result.Add(0);
        else
        {
            GetTreeValue(n.left, ref result);
        }

        if (n.right == null)
        {
            result.Add(0);
        }
        else
        {
            GetTreeValue(n.right, ref result);
        }
    }
    #endregion



    #region 解法二  运行时间：116ms 内存消耗：24.3MB

    /// <summary>
    /// 思路：直接同时递归两个树，比较他们的各个子节点
    /// </summary>
    public bool IsSameTree_2(TreeNode p, TreeNode q) 
    {
        bool result = true;
        if (p != null && q != null)
        {
            if(p.val == q.val)
            {
                result = IsSameTree_2(p.left, q.left);
                if (result)
                {
                    result = IsSameTree_2(p.right, q.right);
                }
            }
            else
            {
                result = false;
            }

        }
        else if (p == null && q == null)
        {
        }
        else
        {
            result = false;
        }

        return result;
    }


    #endregion


    #region 解法三     运行时间：120ms 内存消耗：24.3MB

    /// <summary>
    /// 思路：对解法一进行微调，只序列化一棵树，然后新写一个函数来判断序列化数值与另一棵树是否相等
    /// </summary>

    public bool IsSameTree_3(TreeNode p, TreeNode q)
    {
        if (p == null && q == null)
            return true;

        List<int> plist = new List<int>();

        GetTreeValue(p, ref plist);

        return IsSameWithList(q,ref plist) && plist.Count==1;
    }

    public bool IsSameWithList(TreeNode n, ref List<int> list)
    {
        bool result = true;
        if (n != null)
        {
            if (list.Count == 0 || n.val != list[0])
                result = false;
            else
            {
                if (n.left != null || n.right != null)
                {
                    list.RemoveAt(0);
                    result = IsSameWithList(n.left, ref list);

                    if (result)
                    {
                        list.RemoveAt(0);
                        result = IsSameWithList(n.right, ref list);
                    }
                }
            }
        }
        else
        {
            if (list.Count == 0 || list[0] == 0)
                result = true;
            else
                result = false;
        }

        return result;
    }

    #endregion


    #region 解法四     运行时间：100ms 内存消耗：24.4MB

    /// <summary>
    /// 不用单边递归，而用层级递归；然后结合解法一
    /// </summary>

    public bool IsSameTree_4(TreeNode p, TreeNode q)
    {
        List<int> plist = new List<int>();
        List<int> qlist = new List<int>();

        if (p != null)
            GetTreeValueByLayer(new List<TreeNode> { p }, ref plist);
        if (q != null)
            GetTreeValueByLayer(new List<TreeNode> { q }, ref qlist);

        if (plist.Count != qlist.Count)
            return false;
        else
        {
            for (int i = 0; i < plist.Count; ++i)
            {
                if (plist[i] != qlist[i])
                    return false;
            }
            return true;
        }
    }

    public void GetTreeValueByLayer(List<TreeNode> n, ref List<int> result)
    {
        List<TreeNode> childs = new List<TreeNode>();

        for (int i = 0; i < n.Count; ++i)
        {
            var node = n[i];
            if (node == null)
                result.Add(0);
            else
            {
                result.Add(node.val);
                if (node.left != null || node.right != null)
                {
                    childs.Add(node.left);
                    childs.Add(node.right);
                }
            }
        }

        if (childs.Count > 0)
            GetTreeValueByLayer(childs,ref result);
    }
    #endregion


    #region 解法五  运行时间：108ms 内存消耗：24.3MB

    /// <summary>
    /// 思路：用循环替代递归，来实现解法四
    /// </summary>
    public bool IsSameTree_5(TreeNode p, TreeNode q)
    {
        List<int> plist = new List<int>();
        List<int> qlist = new List<int>();

        if (p != null)
        {
            List<TreeNode> rootP = new List<TreeNode> { p };
            GetTreeValueByLayer_5(ref rootP, ref plist);
        }
        if (q != null)
        {
            List<TreeNode> rootQ = new List<TreeNode> { q };
            GetTreeValueByLayer_5(ref rootQ, ref qlist);
        }

        if (plist.Count != qlist.Count)
            return false;
        else
        {
            for (int i = 0; i < plist.Count; ++i)
            {
                if (plist[i] != qlist[i])
                    return false;
            }
            return true;
        }
    }
    public void GetTreeValueByLayer_5(ref List<TreeNode> parents, ref List<int> result)
    {
        List<TreeNode> childs = new List<TreeNode>();

        while (parents.Count > 0)
        {
            for (int i = 0; i < parents.Count; ++i)
            {
                var node = parents[i];
                if (node == null)
                    result.Add(0);
                else
                {
                    result.Add(node.val);
                    if (node.left != null || node.right != null)
                    {
                        childs.Add(node.left);
                        childs.Add(node.right);
                    }
                }
            }

            parents.Clear();
            if (childs.Count > 0)
            {
                for (int n = 0; n < childs.Count; ++n)
                {
                    parents.Add(childs[n]);
                }
                childs.Clear();
            }
        }
    }


    #endregion
}
