using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolRegister : MonoBehaviour
{
    [System.Serializable]
    public struct PoolData
    {
        public PoolEnums.PoolId poolID;
        public Transform        poolPrefab;
        public int              quantity;
        public bool             IsExpand;
    }

    [SerializeField] private PoolData[] poolDatas = null;

    private void Awake()
    {
        //实例化自身管理的对象
        InstancePool();
        //添加到PoolManager中统一管理
        PoolExtension.RegisterPoolExtension(this);
    }

    private void OnDestroy()
    {
        //从PoolManager的管理中移除
        PoolExtension.RemovePoolExtension(this);
        //释放自身管理的对象
        RemovePoolKey();
    }

    /// <summary>
    /// 移除所有对象池
    /// </summary>
    private void RemovePoolKey()
    {
        for (int i = 0; i < poolDatas.Length; i++)
        {
            PoolExtension.RemovePoolKeyExtension(poolDatas[i].poolID, false);
        }
    }

    /// <summary>
    /// 实例化一个指定id的对象
    /// </summary>
    /// <param name="poolId">对象ID</param>
    /// <returns>实例化对象的Transform组件引用</returns>
    public Transform InstancePool(PoolEnums.PoolId poolId)
    {
        for (int i = 0; i < poolDatas.Length; i++)
        {
            var item = poolDatas[i];

            if (item.poolID == poolId)
            {
                if (item.IsExpand == false)
                    return null;

                var poolInstance = Instantiate(item.poolPrefab.gameObject, transform);

                return poolInstance.transform;
            }
        }

        return null;
    }

    /// <summary>
    /// 实例化所有对象池
    /// </summary>
    private void InstancePool()
    {
        for (int i = 0; i < poolDatas.Length; i++)
        {
            var item = poolDatas[i];

            for (int j = 0; j < item.quantity; j++)
            {
                var poolInstance = Instantiate(item.poolPrefab.gameObject, transform);
                PoolExtension.SetPool(item.poolID, poolInstance.transform);
            }
        }
    }
}
