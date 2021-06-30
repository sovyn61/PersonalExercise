using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDragController : MonoBehaviour, IDragHandler
{
    public Collider2D collider2;
    public void OnDrag(PointerEventData eventData)
    {
        if(collider2.OverlapPoint(eventData.position))
            Debug.Log("进入listener的范围");

        gameObject.transform.position = eventData.position;
        //
        if(eventData.pointerDrag !=null && eventData.pointerDrag != gameObject)
            Debug.Log(eventData.pointerDrag.name);
        if (eventData.pointerPress != null && eventData.pointerPress != gameObject)
            Debug.Log(eventData.pointerPress.name);
        if (eventData.pointerEnter != null && eventData.pointerEnter != gameObject)
            Debug.Log(eventData.pointerEnter.name);
        if (eventData.rawPointerPress != null && eventData.rawPointerPress != gameObject)
            Debug.Log(eventData.rawPointerPress.name);
    }
}
