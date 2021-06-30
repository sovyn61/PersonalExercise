using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolEnums
{
    public enum PoolId
    {
        None                = 0,
        Ball                = 1,
    }

    public readonly static string[] PoolKey =
    {
        "0",
        "1"
    };

    public static string GetPoolKey(PoolId id)
    {
        return PoolKey[(int)id];
    }
}
