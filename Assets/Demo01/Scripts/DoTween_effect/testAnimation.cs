using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class testAnimation : MonoBehaviour
{
    public Image img_render;

    public void PlayAnim()
    {
        Sequence sequence = DOTween.Sequence();

        //
        Vector3 position = img_render.transform.position;
        Tweener move = img_render.transform.DOMoveX(position.x + 400, 2f);
        move.OnComplete(() => { GameLog.Log("move completed!"); });
        sequence.Append(move);

        //
        Tweener fade = img_render.DOFade(0, 1f);
        fade.OnComplete(() => { GameLog.Log("fade completed!"); });
        sequence.Join(fade);

        //
        sequence.OnComplete(() => { GameLog.Log("sequence completed!"); });
    }
}
