using Assets.Script.Bag.Build;
using Assets.Script.Bag.Collect;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManage : MonoBehaviour
{
    public GameObject buildSlotPrefabs; // 显示的图标的prefab
    public GameObject buildOnFloorPrefabs; // 营地的prefab

    private CanvasGroup showBuildIcon; // 显示的buildicon

    private GameObject player;
    private UploadBuildRequest buildRequest;
    private PlayerInfo playerInfo;


    private static BuildManage instance;

    public static BuildManage Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        showBuildIcon = transform.Find("ShowBuildIcon").GetComponent<CanvasGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
        buildRequest = player.GetComponent<UploadBuildRequest>();
        playerInfo = PlayerInfo.Instance;

        HideIcon();
        // 初始化里面的东西
        InitBuildIcon();
        OnShowOrHideIconDialog(null);
    }

    #region 显示或隐藏制作的图标
    public void OnShowOrHideIcon()
    {
        if (showBuildIcon.alpha == 0)
        {
            ShowIcon();
        }
        else
        {
            HideIcon();
        }
    }

    private void ShowIcon()
    {
        showBuildIcon.alpha=1f;
        showBuildIcon.gameObject.SetActive(true);
        
    }

    private void HideIcon()
    {
        showBuildIcon.alpha = 0f;
        showBuildIcon.gameObject.SetActive(false);
    }
    #endregion

    #region 显示或隐藏制作对话框
    public void OnShowOrHideIconDialog(Article article)
    {
        BuildDialog.Instance.OnShowOrHideIconDialog(article);
    }

    #endregion

    private void InitBuildIcon()
    {
        foreach(Article ac in InventoryManage.Instance.articles)
        {
            if (ac is BuildItem)
            {
                GameObject go =  Instantiate(buildSlotPrefabs, showBuildIcon.transform);
                go.transform.SetParent(showBuildIcon.transform);
                BuildSlot slot = go.GetComponent<BuildSlot>();
                //showBuildIcon
                slot.StorArticle(ac);
            }
        }
    }
    /// <summary>
    /// 建造建筑
    /// </summary>
    private void InBulidOnFloor(Vector2 v2, Sprite image)
    {
        Debug.Log("建造");
        GameObject go = Instantiate(buildOnFloorPrefabs);
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = image;
        go.transform.localPosition = v2;


    }

    public void InBulidOnFloor(Vector2 v2, string url, bool upload=false)
    {
        InBulidOnFloor(v2, Resources.Load<Sprite>(url));
        if (upload)
        {
            // 上传服务器
            UploadBuildModel model = new UploadBuildModel()
            {
                Position = new Vector3DPosition() { X = v2.x, Y = v2.y },
                SpriteUrl = url
            };

            playerInfo.SetBuildResource(model);
            buildRequest.DefaultRequest();
        }
    }
}
