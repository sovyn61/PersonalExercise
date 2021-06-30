using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallController : MonoBehaviour
{
    public float maxLiftTime = 2f;

    private float liftTime;
    private void OnEnable()
    {
        liftTime = maxLiftTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (liftTime <= 0)
        {
            //kill self
            PoolExtension.SetPool(PoolEnums.PoolId.Ball, transform);
        }
        else
        {
            liftTime -= Time.deltaTime;
        }
    }
}
