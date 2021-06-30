/*
 * mesh编程：用代码生成矩阵平面
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MeshGrid : MonoBehaviour
{
    //设置Mesh网格的长度，x*y 为Mesh的面积 从0开始
    public int xSize, ySize;
    public Vector2 normalLength;
    private Vector3[] vertices; //vertices为顶点数组
    public Mesh mesh;

    private void Awake()
    {

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
        mesh.name = "Procedural Grid";

        //创建一个x*y的网格 vertices为顶点数组
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        //---------------------------------------------------------------------------------
        //点击Play播放按钮之后，在Scene视图中我们只能看见一个球体在世界的原点。
        //因为我们还没有定位顶点的位置，所以所有的球体重叠在一个位置。我们必须会用双重循环遍历所有的位置
        //---------------------------------------------------------------------------------
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x*normalLength.x, y*normalLength.y);
            }
        }
        //mesh的顶点信息
        mesh.vertices = vertices;

        //-------------------------------------------------------
        // xsize*6 是一位  每个三角面有3个顶点，也就是而每个正方形有6个顶点
        // 一行有xsiz个正方形  也就有xsize*6的顶点
        //-------------------------------------------------------
        int[] triangles = new int[xSize * ySize * 6];

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        //mesh的三角面信息
        mesh.triangles = triangles;

        // 网格自动计算法线向量
        mesh.RecalculateNormals();

        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                //vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }
        //mesh的uv信息
        mesh.uv = uv;

        //mesh的切线信息
        Vector4[] tangents = new Vector4[vertices.Length];

        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                //vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
            }
        }
        mesh.tangents = tangents;

        //
        GetComponent<MeshFilter>().mesh = mesh;
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