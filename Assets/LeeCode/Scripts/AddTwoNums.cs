
/*
 * 题目：用链表来存储非负整数，链表每个节点存一位数字，并且存储方向为逆序。如 1->4->3，表示341.
 * 给出两个链表，计算这两个数的和。也以同样的方式存储在链表中。
 * 
 */


using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


 // Definition for singly-linked list.
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; }
}


public class AddTwoNums : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ListNode l1 = new ListNode(5);

        ListNode l2 = new ListNode(5);

        ListNode result = AddTwoNumbers(l1, l2);

        StringBuilder stringBuilder = QString.GetShareStringBuilder();
        stringBuilder.Append(result.val);
        result = result.next;
        while (result != null)
        {
            stringBuilder.Append("->").Append(result.val);
            result = result.next;
        }

        Debug.Log(stringBuilder.ToString());
    }


    #region 解法一

    /// <summary>
    /// 顺序加，注意进位的问题。
    /// </summary>

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        if (l1 == null)
        {
            if (l2 == null)
                return null;
            else
            {
                return l2;
            }
        }
        else
        {
            if (l2 == null)
                return l1;
            else
            {
                int iOne = (l1.val + l2.val) % 10;
                int iTen = (l1.val + l2.val) / 10;
                ListNode result = new ListNode(iOne);

                ListNode next1 = l1.next;
                ListNode next2 = l2.next;
                ListNode resultNext = result;

                while (next1 != null || next2 != null || iTen > 0)
                {
                    int sum = iTen;
                    if (next1 == null)
                    {
                        if(next2 != null)
                        {
                            sum += next2.val;
                            next2 = next2.next;
                        }
                    }
                    else
                    {
                        if (next2 == null)
                        {
                            sum += next1.val;
                            next1 = next1.next;
                        }
                        else
                        {
                            sum += next1.val + next2.val;
                            next1 = next1.next;
                            next2 = next2.next;
                        }
                    }

                    iOne = sum % 10;
                    iTen = sum / 10;
                    ListNode temNode = new ListNode(iOne);
                    resultNext.next = temNode;
                    resultNext = temNode;
                }


                return result;
            }
        }
    }

    #endregion
}
