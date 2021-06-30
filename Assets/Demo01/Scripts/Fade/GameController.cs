using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public LoadingController loading;
    public FadeController fadeController;
    public FadeByPlayable byPlayable;
    public Image img_test;

    private Button switchBtn;

    private SpriteAtlas testAtlas;

    // Start is called before the first frame update
    void Start()
    {
        var btnObj = transform.Find("SwitchBtn");
        if (btnObj != null)
        {
            switchBtn = btnObj.GetComponent<Button>();

            if (SceneManager.GetActiveScene().name.Equals("Dotween"))
                switchBtn.onClick.AddListener(SwitchLevel);
            else if (SceneManager.GetActiveScene().name.Equals("Animator"))
                switchBtn.onClick.AddListener(CallFadeInAnimation);
            else if (SceneManager.GetActiveScene().name.Equals("Playable"))
                switchBtn.onClick.AddListener(CallFadeInPlayable);
            else if(SceneManager.GetActiveScene().name.Equals("PageScrollView"))
                switchBtn.onClick.AddListener(SwitchImgSprite);
        }

        float f = 100000000;
        Debug.Log("f:" + f.ToString("f0"));
        f += 5;
        Debug.Log("f:" + f.ToString("f0"));

        Debug.Log("长度：" + sizeof(float));
        Debug.Log("最大值：" + float.MaxValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switchBtn.transform.DOMoveX(50, 3f);
        }
    }

    public void SwitchLevel()
    {
        if (loading != null)
        {
            loading.gameObject.SetActive(true);
            loading.FadeIn();
        }
    }

    public void CallFadeInAnimation()
    {
        if (fadeController != null)
        {
            fadeController.FadeIn();
        }
    }

    public void CallFadeInPlayable()
    {
        if (byPlayable != null)
        {
            byPlayable.FadeIn();
        }
    }

    public void SwitchImgSprite()
    {
        if (testAtlas == null)
            testAtlas = Resources.Load<SpriteAtlas>("Atlas/fruit_atlas");

        int level = Random.Range(1, 4);
        int index = 0;
        switch (level)
        {
            case 1:
                index = Random.Range(1, 13);
                break;
            case 2:
                index = Random.Range(1, 10);
                break;
            case 3:
                index = Random.Range(1, 5);
                break;
        }
        int totalID = level * 100 + index;
        Sprite sprite = testAtlas.GetSprite(totalID.ToString());
        img_test.sprite = sprite;
        img_test.SetNativeSize();
    }
}
