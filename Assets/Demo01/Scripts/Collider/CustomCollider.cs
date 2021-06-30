using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public Color painterColor = Color.black;
    public GameObject prefab;

    public float radius = 2;

    private List<Vector3> pathPos = new List<Vector3>();
    private List<Vector2> finalPos = new List<Vector2>();
    private List<Vector2> otherSidePos = new List<Vector2>();

    private LineRenderer currentLineRender;
    private GameObject currentLine;
    private Vector3 lastPoint;
    private int currentPoint = -1;

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(0)||Input.GetMouseButtonDown(0))
        {
            //记录路径点
            Vector3 mousePos = Input.mousePosition;
            //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //判断现在是否有LineRenderer，配合鼠标抬起，可以再次下笔的时候新建一个画笔
            if (!currentLineRender)
            {
                currentLine = GameObject.Instantiate(prefab, transform);
                currentLineRender = currentLine.GetComponent<LineRenderer>();
                currentLineRender.startWidth = currentLineRender.endWidth = radius;
            }
            //如果鼠标停在一个地方，就没必要一直加点了，提升效率
            if (!lastPoint.Equals(mousePos))
            {
                //画之前，先给LineRenderer扩容，它并不聪明
                currentLineRender.positionCount++;
                //把点给它，它自己会画
                currentLineRender.SetPosition(++currentPoint, mousePos);
                lastPoint = mousePos;
                //记录路径点，以供后续生成碰撞盒逻辑使用
                pathPos.Add(mousePos);
            }

            //if (!pathPos.Contains(mousePos))
            //{
            //    pathPos.Add(mousePos);
            //}
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLineRender = null;
            currentPoint = -1;

            if (pathPos.Count < 2)
            {
                Debug.Log("获得的路径点少于2个。无法生成！");
                return;
            }

            //预处理路径点
            CalPathPos();

            //用路径点绘制并生成collider
            CreateObj();
        }
    }

    private void CalPathPos()
    {
        if (finalPos.Count > 0 || otherSidePos.Count > 0)
        {
            finalPos.Clear();
            otherSidePos.Clear();
        }

        Vector2 beginPos = pathPos[0];
        for (int i = 1; i < pathPos.Count; ++i)
        {
            Vector2 endPos = pathPos[i];
            //计算beginpos的左右偏移点
            CalOffsetPos(beginPos, endPos);

            if (i + 1 == pathPos.Count)
            {
                //如果是最后一个点，反过来计算它的左右偏移点
                CalOffsetPos(endPos, beginPos, true);
            }
            else
            {
                beginPos = endPos;
            }
        }

        //将两个list合并起来
        int count = otherSidePos.Count;
        for (int i = 0; i < count; ++i)
        {
            finalPos.Add(otherSidePos[count - 1 - i]);
        }
        otherSidePos.Clear();
    }

    private void CalOffsetPos(Vector2 begin, Vector2 end, bool isEnd = false)
    {
        Vector2 normal = end - begin;
        normal.Normalize();
        Vector2 new1 = Quaternion.AngleAxis(90, Vector3.forward) * normal;
        Vector2 new2 = Quaternion.AngleAxis(90, Vector3.back) * normal;

        if (isEnd)
        {
            otherSidePos.Add(begin + new1 * radius);
            finalPos.Add(begin + new2 * radius);
        }
        else
        {
            finalPos.Add(begin + new1 * radius);
            otherSidePos.Add(begin + new2 * radius);
        }
    }

    private void CreateObj()
    {
        //GameObject obj = GameObject.Instantiate(prefab, transform);
        GameObject obj = currentLine;
        obj.AddComponent<Rigidbody2D>();
        PolygonCollider2D polygon = obj.AddComponent<PolygonCollider2D>();
        polygon.SetPath(0, finalPos.ToArray());

        //
        pathPos.Clear();
        finalPos.Clear();
    }
}
