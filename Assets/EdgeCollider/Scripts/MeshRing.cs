/*
 * mesh编程：用代码生成横截面为长方型的圆环体
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MeshRing: MonoBehaviour
{
    //设置Mesh网格的半径
    public float angle,radio,width,height;
    public int angleSize;

    private int xSize, ySize, zSize;
    private float angleOffset;

    private Vector3[]       vertices;   // vertices为顶点数组
    private int[]           triangles;  // triangles为三角面数组，内容为顶点下标
    private Vector2[]       uv;         // uv为第一套uv信息
    private Vector4[]       tangents;   // tangents为切线数组
    public Mesh mesh;

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        //
        xSize = 1;
        ySize = 1;
        zSize = angleSize;
        angleOffset = angle / angleSize;
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos1 = new Vector3(1, 1, 1);
        Vector3 pos2 = new Vector3(1, 2, 1);
        Vector3 pos3 = new Vector3(1, 1, 2);
        Vector3 pos4 = new Vector3(1f, 1.5f,1.5f);


        //Gizmos.DrawSphere(pos1, 0.1f);
        //Gizmos.DrawSphere(pos2, 0.1f);
        //Gizmos.DrawSphere(pos3, 0.1f);
        //Gizmos.DrawSphere(pos4, 0.1f);

        Vector3 offsetY = pos2 - pos1;
        Vector3 offsetX = pos3 - pos1;
        Vector3 offset3 = pos4 - pos1;
        Debug.Log(offset3);
        float Y = Vector3.Project(offset3, offsetY).magnitude;
        float X = Vector3.Project(offset3, offsetX).magnitude;

        Vector2 result = new Vector2(X,Y);
        Debug.Log(result);
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
        if (angle <= 0)
            return;

        mesh = new Mesh();
        mesh.name = "Procedural Ring";

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
        //一个弧形立方体有六个面：上圆面，下圆面，内圆弧面，外圆弧面，左截面，右截面
        //---------------------------------------------------------------------------------

        int vericesIndex = 0;
        int trianglesIndex = 0;

        //上圆面
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
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        float angleCurrent = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                Vector3 pos = new Vector3(-radio - width + width * x*2, 0, 0);
                if (angleCurrent != 0)
                {
                    pos = Quaternion.AngleAxis(angleCurrent, Vector3.up) * pos;
                }
                vertices[vericesIndex] = new Vector3(pos.x, height, pos.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((pos.x + radio), (pos.z + radio));
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
            angleCurrent += angleOffset;
        }

        //下圆面
        //根据顶点计算三角面
        for (int vi = vericesIndex, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + xSize + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi  + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 1f, 0f, -1f);
        angleCurrent = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                Vector3 pos = new Vector3(-radio - width + width * x*2, 0, 0);
                if (angleCurrent != 0)
                {
                    pos = Quaternion.AngleAxis(angleCurrent, Vector3.up) * pos;
                }
                vertices[vericesIndex] = new Vector3(pos.x, 0, pos.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2((pos.x + radio), (pos.z + radio));
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
            angleCurrent += angleOffset;
        }

        //内圆弧面
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
        angleCurrent = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                Vector3 pos = new Vector3(-radio+width, x * height, 0);
                if (angleCurrent != 0)
                {
                    pos = Quaternion.AngleAxis(angleCurrent, Vector3.up) * pos;
                }
                vertices[vericesIndex] = new Vector3(pos.x, pos.y, pos.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2(angleCurrent * 2 * Mathf.PI * radio / 360, pos.y);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
            angleCurrent += angleOffset;
        }

        //外圆弧面
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
        angleCurrent = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                Vector3 pos = new Vector3(-radio-width, x * height, 0);
                if (angleCurrent != 0)
                {
                    pos = Quaternion.AngleAxis(angleCurrent, Vector3.up) * pos;
                }
                vertices[vericesIndex] = new Vector3(pos.x, pos.y, pos.z);
                //给第一套uv赋值
                uv[vericesIndex] = new Vector2(angleCurrent * 2 * Mathf.PI * radio / 360, pos.y);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
            angleCurrent += angleOffset;
        }

        //左面
        //根据顶点计算三角面
        for (int vi = vericesIndex, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi  + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi + xSize + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 1f, 0f, -1f);
        int firstPos = vericesIndex;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                vertices[vericesIndex] = new Vector3(-radio-width + x*width*2, y*height, 0);
                //给第一套uv赋值
                Vector3 uvOffset = vertices[vericesIndex] - vertices[firstPos];
                uv[vericesIndex] = new Vector2(uvOffset.x, uvOffset.y);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }

        //右面
        //根据顶点计算三角面
        for (int vi = vericesIndex, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, trianglesIndex += 6, vi++)
            {
                triangles[trianglesIndex] = vi;
                triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2] = vi + xSize + 1;
                triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1] = vi  + 1;
                triangles[trianglesIndex + 5] = vi + xSize + 2;
            }
        }
        //确定每一个顶点的位置。（注意：确定顶点位置时，需要考虑Mesh中点，也就是所有顶点的坐标的原点）
        tangent = new Vector4(0f, 1f, 0f, -1f);
        firstPos = vericesIndex;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, vericesIndex++)
            {
                Vector3 pos = new Vector3(-radio - width + x * width * 2, y * height, 0);
                pos = Quaternion.AngleAxis(angle, Vector3.up) * pos;
                vertices[vericesIndex] = new Vector3(pos.x, pos.y, pos.z);
                ////给第一套uv赋值
                //Vector3 uvOffset = vertices[vericesIndex] - vertices[firstPos];
                //uv[vericesIndex] = new Vector2(uvOffset.x, uvOffset.y);
                //给切线数据赋值
                tangents[vericesIndex] = tangent;
            }
        }
        Vector3 vecX = (vertices[firstPos + 1] - vertices[firstPos]).normalized;
        Vector3 vecY = (vertices[firstPos + 2] - vertices[firstPos]).normalized;
        for (int vi = firstPos; vi < vericesIndex; vi++)
        {
            //给第一套uv赋值
            Vector3 uvOffset = vertices[vi] - vertices[firstPos];
            float uvX = Vector3.Project(uvOffset, vecX).magnitude;
            float uvY = Vector3.Project(uvOffset, vecY).magnitude;
            uv[vi] = new Vector2(uvX, uvY);
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

        //设置原点
        Bounds bounds = mesh.bounds;
        bounds.center = Vector3.zero;
        mesh.bounds = bounds;

        meshFilter.mesh = mesh;

        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        }
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