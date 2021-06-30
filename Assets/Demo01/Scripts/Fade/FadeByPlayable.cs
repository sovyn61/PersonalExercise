using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FadeByPlayable : MonoBehaviour
{
    public AnimationClip fadeInClip;
    public AnimationClip fadeOutClip;

    private PlayableGraph playableGraph;
    private Animator animator;

    private LoadingController loadingController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = gameObject.AddComponent<Animator>();

        //
        loadingController = GetComponentInParent<LoadingController>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void FadeIn()
    {
        loadingController.gameObject.SetActive(true);

        AnimationPlayableUtilities.PlayClip(animator, fadeInClip, out playableGraph);
    }

    public void OnFadeInComplete()
    {
        Debug.Log("Function OnFadeInComplete!");

        playableGraph.Destroy();

        Invoke("FadeOut", 2);
    }

    public void FadeOut()
    {
        AnimationPlayableUtilities.PlayClip(animator, fadeOutClip, out playableGraph);
    }

    public void OnFadeOutComplete()
    {
        Debug.Log("Function OnFadeOutComplete!");

        playableGraph.Destroy();

        loadingController.gameObject.SetActive(false);
    }
}
