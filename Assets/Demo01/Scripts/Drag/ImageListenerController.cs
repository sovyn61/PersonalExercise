using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageListenerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(QString.Concat("控件：",gameObject.name,",监听到鼠标进入消息！"));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(QString.Concat("控件：", gameObject.name, ",监听到鼠标离开消息！"));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(QString.Concat("控件：", gameObject.name, ",监听到鼠标放开消息！"));
    }
}
