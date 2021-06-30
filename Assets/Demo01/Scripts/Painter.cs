using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public Color painterColor = Color.black;
    private LineRenderer currentLineRenderer;
    private int currentPoint = -1;
    private Vector3 lastPoint;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //鼠标抬起后，清除上一次的笔记记录。
            currentLineRenderer = null;
            currentPoint = -1;
        }
        if (Input.GetMouseButton(0))
        {
            //鼠标按下时，将鼠标位置转换成世界坐标
            //SetZ（）是我写的扩展方法，用于为Vector2添加一个Z
            //如果Z不设置，默认为0，就会直接在相机位置创建，这样你看不到的
            Vector3 point = Input.mousePosition;
            point.z = 1;
            point = Camera.main.ScreenToWorldPoint(point);
            //判断现在是否有LineRenderer，配合鼠标抬起，可以再次下笔的时候新建一个画笔
            if (!currentLineRenderer)
            {
                GameObject line = new GameObject("Line");
                currentLineRenderer = line.AddComponent<LineRenderer>();
                //给一个你喜欢的材质球
                currentLineRenderer.material = new Material(Shader.Find("Standard"));
                //设置你喜欢的宽度
                currentLineRenderer.startWidth = currentLineRenderer.endWidth = .02f;
                //设置你喜欢的颜色
                currentLineRenderer.material.SetColor("_Color", painterColor);
                //如果你不希望受灯照影响
                currentLineRenderer.material.SetColor("_EmissionColor", painterColor);
                //记得开启自发光属性，不然即使上一条代码改了也没用
                currentLineRenderer.material.EnableKeyword("_EMISSION");
                //LineRenderer默认会有一个点，在0,0,0位置，如果不把它清除，之后你画的线段
                //末尾总会连接到0,0,0
                currentLineRenderer.positionCount = 0;
            }
            //如果鼠标停在一个地方，就没必要一直加点了，提升效率
            if (!lastPoint.Equals(point))
            {
                //画之前，先给LineRenderer扩容，它并不聪明
                currentLineRenderer.positionCount++;
                //把点给它，它自己会画
                currentLineRenderer.SetPosition(++currentPoint, point);
                lastPoint = point;
            }
        }
    }
}
