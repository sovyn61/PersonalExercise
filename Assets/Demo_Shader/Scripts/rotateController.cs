using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Sequence sequence = DOTween.Sequence();
        //sequence.Join(transform.DORotate(new Vector3(0, 0, 180), 1f));
        //sequence.Join(transform.DORotate(new Vector3(0, 0, 360), 1f));
        //sequence.SetUpdate(true);
        //sequence.SetDelay(0.0f);
        //sequence.SetLoops(-1);

        Tweener tweener = transform.DOLocalRotate(new Vector3(0,0,1 - 360*2), 2f);
        tweener.SetUpdate(true);
        tweener.SetDelay(0.0f);
        tweener.SetLoops(-1);

        //Time.timeScale = 0;
    }
}
