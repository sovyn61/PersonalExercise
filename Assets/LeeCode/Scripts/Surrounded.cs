
/*
 * 题目：给定一个二维的矩阵，包含 'X' 和 'O'（字母 O）。
 *       找到所有被 'X' 围绕的区域，并将这些区域里所有的 'O' 用 'X' 填充。
 * 注：类似围棋围杀逻辑
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityScript.Steps;

public class Surrounded : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        char[][] board = new char[4][];
        board[0] = new char[] { 'X', 'X', 'X', 'X' };
        board[1] = new char[] { 'X', 'O', 'O', 'X' };
        board[2] = new char[] { 'X', 'X', 'O', 'X' };
        board[3] = new char[] { 'X', 'O', 'X', 'X' };

        PrintChars(board);

        System.DateTime before = System.DateTime.Now;
        Solve(board);
        Debug.Log(System.DateTime.Now - before);
        PrintChars(board);
    }

    private void PrintChars(char[][] src)
    {
        StringBuilder stringBuilder = QString.GetShareStringBuilder();
        stringBuilder.Append('\n');
        for (int i = 0; i < src.Length; ++i)
        {
            for (int j = 0; j < src[i].Length; ++j)
            {
                if (j == src[i].Length - 1)
                    stringBuilder.Append(src[i][j]);
                else
                    stringBuilder.Append(src[i][j]).Append(',');
            }
            stringBuilder.Append('\n');
        }
        Debug.Log(stringBuilder.ToString());
    }


    #region 解法一
    /// <summary>
    /// 思路：遍历，找到‘O’以后，用围棋计算气的方法计算是否被包围。不过，这里在边角都视为有气。
    /// </summary>

    public void Solve(char[][] board)
    {
        List<int> temList = new List<int>();

        int line = board.Length;
        int row = 0;
        if (line > 0)
            row = board[0].Length;
        for (int i = 0; i < board.Length; ++i)
        {
            for (int j = 0; j < board[i].Length; ++j)
            {
                if (board[i][j].Equals('X'))
                    continue;

                temList.Clear();
                if (!FindConnectOIsAlive(ref temList, ref board, i, j, row))
                {
                    for (int n = 0; n < temList.Count; ++n)
                    {
                        board[temList[n] / row][temList[n] % row] = 'X';
                    }
                }
            }
        }
    }

    private bool FindConnectOIsAlive(ref List<int> temlist,ref char[][] src, int i, int j, int row)
    {
        bool result = false;

        if (src[i][j].Equals('X'))
            return result;
        else
        {
            int index = i * row + j;

            if (temlist.Contains(index))
                return result;

            temlist.Add(i * row + j);

            //上
            int up = i - 1;
            if (up < 0)
                result |= true;
            else
                result |= FindConnectOIsAlive(ref temlist, ref src, up, j, row);

            //下
            int down = i + 1;
            if (down >= src.Length)
                result |= true;
            else
                result |= FindConnectOIsAlive(ref temlist, ref src, down, j, row);

            //左
            int left = j - 1;
            if (left < 0)
                result |= true;
            else
                result |= FindConnectOIsAlive(ref temlist, ref src, i, left, row);

            //右
            int right = j + 1;
            if (right >= row)
                result |= true;
            else
                result |= FindConnectOIsAlive(ref temlist, ref src, i, right, row);
        }

        return result;
    }

    #endregion
}
