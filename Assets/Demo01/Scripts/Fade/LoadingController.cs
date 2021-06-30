using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private Image blackMask;

    private Color colorIn;
    private Color colorOut;

    private void Awake()
    {
        var obj = transform.Find("BlackMask");
        if (obj != null)
        {
            blackMask = obj.GetComponent<Image>();
        }

        colorIn = new Color(0, 0, 0, 0);
        colorOut = new Color(0, 0, 0, 1);

        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        if (blackMask is null)
            return;

        //gameObject.SetActive(true);
        blackMask.color = colorIn;
        blackMask.DOFade(1, 1f).onComplete += OnFadeInComplete;
    }


    void OnFadeInComplete()
    {
        Invoke("FadeOut", 1f);
    }

    public void FadeOut()
    {
        if (blackMask is null)
            return;

        blackMask.color = colorOut;
        blackMask.DOFade(0, 1f).onComplete += OnFadeOutComplete;
    }
    void OnFadeOutComplete()
    {
        gameObject.SetActive(false);
    }
}
