using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public Transform root;
    public void SpawnBall()
    {
        Transform ball = PoolExtension.GetPool(PoolEnums.PoolId.Ball);
        if (ball != null)
        {
            ball.parent = root != null ? root : PoolManager.InstanceAwake().transform;
            ball.position = Vector3.zero;

            GameLog.Log("获取一个小球对象成功！");
        }
    }
}
