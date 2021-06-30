using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    #region 需要控制的子控件
    public Transform tran_content;
    #endregion
    public GameObject prefab_type1;
    public GameObject prefab_type2;

    private TypeOneController type_one;
    private TypeOneController type_two;

    //高度 往下拉是负数   往上拉是正数
    float f = -30f;
    //是否刷新
    bool isRef = false;
    //是否处于拖动
    bool isDrag = false;

    /// <summary>
    /// 当ScrollRect被拖动时
    /// </summary>
    /// <param name="vector">被拖动的距离与Content的大小比例</param>
    void ScrollValueChanged(Vector2 vector)
    {
        //如果不拖动 当然不执行之下的代码
        if (!isDrag)
            return;
        //这个就是Content
        RectTransform rect = GetComponentInChildren<ContentSizeFitter>().GetComponent<RectTransform>();
        //如果拖动的距离大于给定的值
        if (f > rect.rect.height * vector.y)
        {
            isRef = true;
            Debug.Log("拖动的距离大于给定的值");
            //callback1?.Invoke(true);
        }
        else
        {
            Debug.Log("拖动的距离小于等于给定的值");
            isRef = false;
            //callback1?.Invoke(false);
        }
    }

    //public override void OnBeginDrag(PointerEventData eventData)
    //{
    //    base.OnBeginDrag(eventData);
    //    isDrag = true;
    //}

    //public override void OnEndDrag(PointerEventData eventData)
    //{
    //    base.OnEndDrag(eventData);
    //    //callback1?.Invoke(false);
    //    if (isRef)
    //        //callback2?.Invoke();
    //    isRef = false;
    //    isDrag = false;
    //}


    public void AddTypeOne()
    {
        if (type_one == null)
        {
            type_one = Instantiate(prefab_type1, tran_content).GetComponent<TypeOneController>();
            type_one.RegistClickTitleCallback(CalcContentSize);
        }
        //
        type_one.AddItem();
        //
        tran_content.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
        CalcContentSize();
    }

    public void AddTypeTwo()
    {
        if (type_two == null)
        {
            type_two = Instantiate(prefab_type2, tran_content).GetComponent<TypeOneController>();
            type_two.RegistClickTitleCallback(CalcContentSize);
        }
        //
        type_two.AddItem();
        //
        tran_content.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
        CalcContentSize();
    }

    private void CalcContentSize()
    {
        Vector2 size = tran_content.GetComponent<RectTransform>().sizeDelta;
        size.y = 0;
        //
        if (type_one != null)
            size.y += type_one.GetComponent<RectTransform>().sizeDelta.y;
        if (type_two != null)
            size.y += type_two.GetComponent<RectTransform>().sizeDelta.y;
        //
        tran_content.GetComponent<RectTransform>().sizeDelta = size;
    }
}
