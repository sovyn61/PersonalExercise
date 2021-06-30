using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int maxCount = 100;

        for (int i = 0; i < maxCount; ++i)
        {
            //unity random
            //Debug.Log("随机数：" + Random.value);
            //Debug.Log("随机数：" + Random.Range(0,100));

            //system random
            int seed = GetRandomSeed();
            System.Random random = new System.Random(seed);
            Debug.Log("随机数：" + random.Next(0,100)) ;
        }
    }
    private static int GetRandomSeed()
    {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
