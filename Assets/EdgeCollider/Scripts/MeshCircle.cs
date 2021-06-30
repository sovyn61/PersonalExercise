﻿/*
 * mesh编程：用代码生成圆形或者扇形平面
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MeshCircle : MonoBehaviour
{
    //设置Mesh网格的半径
    public float angle, radio;
    public int angleSize;


    private int xSize,zSize;
    private float angleOffset;
    private Vector3[] vertices;   // vertices为顶点数组
    private int[] triangles;  // triangles为三角面数组，内容为顶点下标
    private Vector2[] uv;         // uv为第一套uv信息
    private Vector4[] tangents;   // tangents为切线数组
    public Mesh mesh;

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        //
        xSize = 1;
        zSize = angleSize;
        angleOffset = angle / angleSize;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Generate();
        }
    }

    void Generate()
    {
        mesh = new Mesh();
        mesh.name = "Procedural Ring";

        //创建一个圆弧上有zSize + 1个点的扇形 vertices为顶点数组
        vertices = new Vector3[zSize + 1 + 1];

        //-------------------------------------------------------
        // 圆弧上相邻的两个顶点和圆心组成一个三角面
        // 所以，总的三角面的数量为zSize
        //-------------------------------------------------------
        triangles = new int[zSize * 3];

        //初始化uv数组
        uv = new Vector2[vertices.Length];

        //初始化切线数组
        tangents = new Vector4[vertices.Length];

        //---------------------------------------------------------------------------------
        //一个扇形只有一个面：正面
        //---------------------------------------------------------------------------------

        int vericesIndex = 0;
        int trianglesIndex = 0;

        //正面
        //根据顶点计算三角面
        for (int vi = vericesIndex, z = 0; z < zSize; z++, vi++,trianglesIndex += 3)
        {
            triangles[trianglesIndex] = 0;
            triangles[trianglesIndex + 1] = vi + 1;
            triangles[trianglesIndex + 2] = vi + 2;
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        //---------------------------------------------------------------------------------
        //给圆心顶点赋值
        //---------------------------------------------------------------------------------
        vertices[vericesIndex] = new Vector3(0, 0, 0);
        //给第一套uv赋值
        uv[vericesIndex] = new Vector2(0.5f, 0.5f);
        //给切线数据赋值
        tangents[vericesIndex++] = tangent;
        //---------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------
        //给圆弧上的顶点赋值
        //---------------------------------------------------------------------------------
        float angleCurrent = 0;
        for (int z = 0; z <= zSize; z++, vericesIndex++)
        {
            Vector3 pos = new Vector3(-radio, 0, 0);
            if (angleCurrent != 0)
            {
                pos = Quaternion.AngleAxis(angleCurrent, Vector3.up) * pos;
            }
            vertices[vericesIndex] = new Vector3(pos.x, 0, pos.z);
            //给第一套uv赋值
            //按照角度赋予uv
            //uv[vericesIndex] = new Vector2( angleCurrent / angle, 1);
            //按照坐标赋予uv
            uv[vericesIndex] = new Vector2((pos.x+radio) / (radio*2), (pos.z+radio) / (radio*2));
            //给切线数据赋值
            tangents[vericesIndex] = tangent;
            //
            angleCurrent += angleOffset;
        }
        //---------------------------------------------------------------------------------


        //mesh的顶点信息
        mesh.vertices = vertices;

        //mesh的三角面信息
        mesh.triangles = triangles;

        // 网格自动计算法线向量
        mesh.RecalculateNormals();

        //mesh的uv信息
        mesh.uv = uv;

        //mesh的切线信息
        mesh.tangents = tangents;

        meshFilter.mesh = mesh;
    }

    //------------------------------------------------------
    //Gizmos在OnDrawGizmos方法中执行绘制，它被Unity编辑器自动调用。
    //另一个可选的方法是OnDrwaGizmosSelected，它只能被可选的对象调用。
    //------------------------------------------------------
    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(transform.TransformPoint(vertices[i]), 0.1f);
        }

        //for (int i = 0; i < mesh.triangles.Length; i += 3)
        //{
        //    Color prevColor = Gizmos.color;

        //    Gizmos.color = Color.yellow;

        //    Gizmos.DrawLine(mesh.vertices[mesh.triangles[i]], mesh.vertices[mesh.triangles[i+1]]);
        //    Gizmos.DrawLine(mesh.vertices[mesh.triangles[i+1]], mesh.vertices[mesh.triangles[i+2]]);
        //    Gizmos.DrawLine(mesh.vertices[mesh.triangles[i+2]], mesh.vertices[mesh.triangles[i]]);

        //    Gizmos.color = prevColor;
        //}
    }
}