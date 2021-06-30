/*
 * mesh编程：用代码生成立方体
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MeshSquare : MonoBehaviour
{
    //设置Mesh网格的长度，x*y 为Mesh的面积 从0开始
    public int xSize, ySize, zSize;
    public Vector3 normalLength;

    private Vector3[]       vertices;   // vertices为顶点数组
    private int[]           triangles;  // triangles为三角面数组，内容为顶点下标
    private Vector2[]       uv;         // uv为第一套uv信息
    private Vector4[]       tangents;   // tangents为切线数组
    public Mesh mesh;

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
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
        mesh.name = "Procedural Square";

        //创建一个x*y*z的立方体 vertices为顶点数组
        vertices = new Vector3[(xSize + 1) * (ySize + 1) * 2 + (xSize + 1) * (zSize + 1) * 2 + (ySize + 1) * (zSize + 1) * 2/**/];

        //-------------------------------------------------------
        // xsize*6 是一位  每个三角面有3个顶点，也就是而每个正方形有6个顶点
        // 一行有xsiz个正方形  也就有xsize*6的顶点
        //-------------------------------------------------------
        triangles = new int[xSize * ySize * 6 * 2 + xSize * zSize * 12 + ySize * zSize * 12/**/];

        //初始化uv数组
        uv = new Vector2[vertices.Length];

        //初始化切线数组
        tangents = new Vector4[vertices.Length];

        //---------------------------------------------------------------------------------
        //一个立方体有六个面：正面，背面，上面，下面，左面，右面
        //---------------------------------------------------------------------------------

        int vericesIndex = 0;
        int trianglesIndex = 0;

        //正面
        //根据顶点计算三角面
        for (int vi = vericesIndex, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + xSize + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(x * normalLength.x, y * normalLength.y);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((float)x / xSize, (float)y / ySize);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }

        //背面
        //根据顶点计算三角面
        for (int vi = vericesIndex, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + xSize + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 1f, 0f, -1f);
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(x * normalLength.x, y * normalLength.y, zSize * normalLength.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((float)(xSize - x) / xSize, (float)(ySize - y) / ySize);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }

        //下面
        //根据顶点计算三角面
        for (int vi = vericesIndex, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + xSize + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 0f, 1f, -1f);
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(x*normalLength.x, 0, z*normalLength.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((float)x / xSize, (float)z / zSize);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }

        //上面
        //根据顶点计算三角面
        for (int vi = vericesIndex, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + xSize + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 1f, 0f, -1f);
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(x*normalLength.x, ySize*normalLength.y, z*normalLength.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((float)(xSize - x) / xSize, (float)(zSize - z) / zSize);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }

        //左面
        //根据顶点计算三角面
        for (int vi = vericesIndex, y = 0; y < ySize; y++, vi++)
        {
            for (int z = 0; z < zSize; z++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + zSize + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + 1;
                triangles[trianglesIndex + 5] = vi + zSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 0f, 1f, -1f);
        for (int y = 0; y <= ySize; y++)
        {
            for (int z = 0; z <= zSize; z++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(0, y*normalLength.y, z*normalLength.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((float)y / ySize, (float)z / zSize);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }

        //右面
        //根据顶点计算三角面
        for (int vi = vericesIndex, y = 0; y < ySize; y++, vi++)
        {
            for (int z = 0; z < zSize; z++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + zSize + 1;
                triangles[trianglesIndex + 5] = vi + zSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 1f, 0f, -1f);
        for (int y = 0; y <= ySize; y++)
        {
            for (int z = 0; z <= zSize; z++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(xSize*normalLength.x, y*normalLength.y, z*normalLength.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((float)(ySize - y) / ySize, (float)(zSize - z) / zSize);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }


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