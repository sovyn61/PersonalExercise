using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TypeOneController : MonoBehaviour
{
    #region 需要控制的子控件
    public RectTransform rect_title_txt;
    public RectTransform rect_content;
    #endregion

    public GameObject prefab_item;

    private List<GameObject> listItems = new List<GameObject>();

    private System.Action m_click_title_callback;

    private bool IsShowAllItems;

    private void Awake()
    {
        if (rect_title_txt == null)
            rect_title_txt = transform.Find("img_title").GetComponent<RectTransform>();
        if (rect_content == null)
            rect_content = transform.Find("obj_content").GetComponent<RectTransform>();

        //
        IsShowAllItems = true;
    }

    public void AddItem()
    {
        GameObject temItem = Instantiate(prefab_item, rect_content.transform);
        listItems.Add(temItem);
        //
        Sequence sequence = DOTween.Sequence();
        sequence.Append(temItem.transform.DOScale(0.95f, 0.5f));
        sequence.Append(temItem.transform.DOScale(1f, 0.8f));
        sequence.SetLoops(-1);
        //
        CalcSize();
    }

    private void CalcSize(bool contentActive = true)
    {
        float offset = rect_content.GetComponent<VerticalLayoutGroup>().spacing;
        int count = listItems.Count;
        float sizeY = 0;
        //
        if (count > 0)
        {
            if (contentActive)
                sizeY = rect_title_txt.sizeDelta.y + offset * (count - 1) + listItems[0].GetComponent<RectTransform>().sizeDelta.y * count;
            else
                sizeY = rect_title_txt.sizeDelta.y + listItems[0].GetComponent<RectTransform>().sizeDelta.y;
        }
        else
        {
            sizeY = rect_title_txt.sizeDelta.y;
        }
        //
        Vector2 size = transform.GetComponent<RectTransform>().sizeDelta;
        size.y = sizeY;
        transform.GetComponent<RectTransform>().sizeDelta = size;
    }

    public void OnClickTitle()
    {
        if (IsShowAllItems)
        {
            IsShowAllItems = false;
            //
            for (int i = 0; i < listItems.Count; ++i)
            {
                if (i > 0)
                    listItems[i].SetActive(false);
            }
            //
            CalcSize(false);
        }
        else
        {
            IsShowAllItems = true;
            //
            for (int i = 0; i < listItems.Count; ++i)
            {
                if (i > 0)
                    listItems[i].SetActive(true);
            }
            //
            CalcSize();
        }
        //
        m_click_title_callback?.Invoke();
    }

    public void RegistClickTitleCallback(System.Action action)
    {
        m_click_title_callback = action;
    }
}
