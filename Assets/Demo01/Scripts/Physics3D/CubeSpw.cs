using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpw : MonoBehaviour
{
    public GameObject cube_prefab;

    private List<GameObject> list_cubes = new List<GameObject>();

    private Vector3 originPos = new Vector3(0, 0.1f, 0);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int m=0;m<30;m++)
            {
                GameObject newObj = GameObject.Instantiate(cube_prefab, transform);
                newObj.transform.position = originPos;
                for (int i = 0; i < list_cubes.Count; ++i)
                {
                    list_cubes[i].transform.position = new Vector3(originPos.x, originPos.y + 0.2f * (i + 1), originPos.z);
                    if (list_cubes[i].TryGetComponent(out Rigidbody rigidbody))
                    {
                        rigidbody.velocity = Vector3.zero;
                    }
                }
                list_cubes.Insert(0, newObj);
                if (newObj.TryGetComponent(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }
}
