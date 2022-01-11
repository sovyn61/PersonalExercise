using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    public Image img_clock;

    public async void LoadPrefab(string names)
    {
        var obj = await Addressables.LoadAssetAsync<GameObject>(names).Task;

        if (obj != null)
        {
            GameObject newpanel = Instantiate(obj, transform);
            newpanel.name = names;
            newpanel.transform.localScale = Vector3.one;
            newpanel.transform.localPosition = Vector3.zero;
            newpanel.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("LoadPrefab failed! name:" + names);
        }
    }

    public async void LoadTexture(string name)
    {
        var spr = await Addressables.LoadAssetAsync<Sprite>(name).Task;


        if (spr != null)
        {
            img_clock.sprite = spr;
            img_clock.SetNativeSize();
        }
        else
        {
            Debug.LogError("LoadTexture failed! name:" + name);
        }
    }
}
