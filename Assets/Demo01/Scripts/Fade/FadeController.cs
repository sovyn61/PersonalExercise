using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public AnimationClip fadeInClip;
    public AnimationClip fadeOutClip;

    private Animation anim;

    private LoadingController loadingController;

    private void Awake()
    {
        loadingController = GetComponentInParent<LoadingController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    internal void FadeIn()
    {
        loadingController.gameObject.SetActive(true);

        if (anim == null)
        {
            anim = gameObject.AddComponent<Animation>();
            anim.clip = fadeInClip;
        }

        anim.Play();
    }

    public void OnFadeInComplete()
    {
        Debug.Log("Function OnFadeInComplete!");

        GameObject.Destroy(anim);

        Invoke("FadeOut", 2);
    }


    public void FadeOut()
    {
        if (anim == null)
        {
            anim = gameObject.AddComponent<Animation>();
            anim.clip = fadeOutClip;
        }

        anim.Play();
    }

    public void OnFadeOutComplete()
    {
        Debug.Log("Function OnFadeOutComplete!");
        GameObject.Destroy(anim);

        loadingController.gameObject.SetActive(false);
    }
}
