using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = transform.GetComponent<Rigidbody>();
        if (rig != null)
        {
            rig.AddForce(Vector3.down * 3, ForceMode.VelocityChange);
        }


        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
