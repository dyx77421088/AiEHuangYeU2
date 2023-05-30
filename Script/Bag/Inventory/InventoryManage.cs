using Assets.Script.Bag.Build;
using Assets.Script.Bag.Collect;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManage : MonoBehaviour 
{
    #region 单例
    private static InventoryManage instance;
    public static InventoryManage Instance { 
        get {
            if (instance == null) instance = GameObject.Find("Inventory Manage").GetComponent<InventoryManage>();
            return instance;
        }
    }
    #endregion
    [HideInInspector]
    public List<Article> articles = new();
    /// <summary>
    /// 拖拽的对象
    /// </summary>
    [HideInInspector]
    public ItemUi dragItem;
    /// <summary>
    /// 建筑物的拖拽的对象
    /// </summary>
    [HideInInspector]
    public ItemUi dragBuildItem;
    /// <summary>
    /// 是否有拖拽对象
    /// </summary>
    [HideInInspector]
    public bool isDrag;
    public bool isBuildDrag;

    private TipTool tipTool;
    private bool isTipShow;
    //private Canvas canvas;
    private Vector3 transV = new Vector2(50, -30);
    private Rect dragBuildRect; // 拖拽的建筑的四边形

    private string noBuild = "NoBuild";
    private RectTransform canvasRectTransform; // 画布

    private Article tempArticle; // 临时的article，用来存放dragbuild的信息
    private BuildItem buildItem; // 存放材料列表
    private void Awake()
    {
        InitStory();
        InitCollect();
        InitBuild();
    }
    void Start()
    {
        
        tipTool = GameObject.FindAnyObjectByType<TipTool>();
        dragItem = GameObject.Find("Drag").GetComponent<ItemUi>();
        dragBuildItem = GameObject.Find("DragBuild").GetComponent<ItemUi>();
        dragBuildItem.gameObject.SetActive(false);
        canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>(); 
        HideDrag();
        //canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    
    void Update()
    {
        if (isTipShow)
        {
            //Vector2 v;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out v);
            Vector3 v3 = Input.mousePosition;
            if (v3.y < 200) transV.y = 100;
            else transV.y = -30;
            tipTool.SetPosition(v3 + transV);
        }
        if (isDrag)
        {
            dragItem.SetPosition(Input.mousePosition);
        }
        if (isDrag && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HideDrag();
        }

        // 建筑物的图片
        if (isBuildDrag)
        {
            dragBuildItem.SetPosition(Input.mousePosition);

            // 输出检测结果
            //if (isOverlapping)
            //{
            //    Debug.Log("有有有有有有有有有有有有 " + noBuild + " 的碰撞体。");
            //}
            //else
            //{
            //    Debug.Log("目标物体所在矩形区域上没有图层为 " + noBuild + " 的碰撞体。");
            //}
            if (Input.GetKey(KeyCode.Escape)) // esc 取消
            {
                HideBuildDrag();
            }
        }
        if (isBuildDrag && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //HideBuildDrag();
            
            int targetLayer = LayerMask.NameToLayer(noBuild);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 检测目标物体所在矩形区域上是否有任何图层为目标图层的碰撞体
            //bool isOverlapping = Physics2D.OverlapArea(worldPosition,
            //    worldPosition + dragBuildRect.size, targetLayer);
            Debug.Log(1 << 6);
            Collider2D coll = Physics2D.OverlapArea(worldPosition - new Vector2(2, 2), worldPosition + new Vector2(2, 2), 1 << 6);
            if (coll == null)
            {
                // 扣除材料
                if(BuildCaiLiao.Instance.KoChuCaiLiao(buildItem.CompositeList) && !PlayerInfo.Instance.ReduceTiNeng(buildItem))
                {
                    BuildManage.Instance.InBulidOnFloor(worldPosition, tempArticle.Sprite, true);
                }
                HideBuildDrag();
            }
            else
            {
                PlayerMessageManage.Instance.ShowMessage("不能建到此处");
            }
            Debug.Log(coll);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    #region 初始化数据
    /// <summary>
    /// 初始化武器数据
    /// </summary>
    private void InitStory()
    {
        JObject jo = JObject.Parse(Resources.Load<TextAsset>("Items").text);
        JArray ja =  JArray.Parse(jo["data"].ToString());
        foreach (JObject item in ja)
        {
            //Debug.Log(item["type"].ToString());
            Item.ItemType it = Enum.Parse<Item.ItemType>(item["type"].ToString());
            switch (it)
            {
                case Item.ItemType.Material:
                    Material mt = item.ToObject<Material>();
                    articles.Add(mt);
                    break;
                case Item.ItemType.Consumable:
                    Consumable c = item.ToObject<Consumable>();
                    articles.Add(c);
                    break;
                case Item.ItemType.Equipment:
                    Equipment eq = item.ToObject<Equipment>();
                    articles.Add(eq);
                    break;
                case Item.ItemType.Weapon:
                    Weapon weapon = item.ToObject<Weapon>();
                    articles.Add(weapon);
                    break;
            }
        }

        //foreach (var item in items)
        //{
        //    switch (item.Type)
        //    {
        //        case Item.ItemType.Material:
        //            Material mt = item as Material;
        //            break;
        //        case Item.ItemType.Consumable:
        //            Consumable c = item as Consumable;
        //            break;
        //        case Item.ItemType.Equipment:
        //            Equipment eq = item as Equipment;
        //            break;
        //        case Item.ItemType.Weapon:
        //            Weapon weapon = item as Weapon;
        //            break;
        //    }
        //}
        //ja.ToList().ForEach(item => Debug.Log(item.ToObject<Item>()));
        //List<Item> it = jo["data"].ToObject<List<Item>>();
        //Debug.Log(items.Count);
    }

    /// <summary>
    /// 初始化采集的物品
    /// </summary>
    private void InitCollect()
    {
        JObject jo = JObject.Parse(Resources.Load<TextAsset>("Collect").text);
        JArray ja = JArray.Parse(jo["data"].ToString());
        foreach (JObject item in ja)
        {
            //Debug.Log(item["type"].ToString());
            CollectItem.CollectType it = Enum.Parse<CollectItem.CollectType>(item["type"].ToString());
            switch (it)
            {
                case CollectItem.CollectType.Material:
                    Assets.Script.Bag.Collect.Material mt = item.ToObject<Assets.Script.Bag.Collect.Material>();
                    articles.Add(mt);
                    break;
                case CollectItem.CollectType.Food:
                    Food c = item.ToObject<Food>();
                    articles.Add(c);
                    break;
                case CollectItem.CollectType.Other:
                    Other eq = item.ToObject<Other>();
                    articles.Add(eq);
                    break;
            }
        }
    }
    
    /// <summary>
    /// 初始化建筑物
    /// </summary>
    private void InitBuild()
    {
        JObject jo = JObject.Parse(Resources.Load<TextAsset>("Synthesis").text);
        JArray ja = JArray.Parse(jo["data"].ToString());
        foreach (JObject item in ja)
        {
            //Debug.Log(item["type"].ToString());
            BuildItem buildItem =  item.ToObject<BuildItem>();
            articles.Add(buildItem);
            Debug.Log(buildItem);
        }
    }
    #endregion

    public Rect GetRect(GameObject go)
    {
        RectTransform targetRectTransform = go.GetComponent<RectTransform>();
        Rect rect = new Rect(
            targetRectTransform.anchoredPosition.x + canvasRectTransform.rect.width * targetRectTransform.anchorMin.x,
            targetRectTransform.anchoredPosition.y + canvasRectTransform.rect.height * targetRectTransform.anchorMin.y,
            targetRectTransform.rect.width,
            targetRectTransform.rect.height);
        return rect;
    }

    public Article GetArticleById(int id)
    {
        return articles.Find(a=>a.Id == id);
    }

    public void ShowTip(string text)
    {
        isTipShow = true;
        tipTool.Show(text);
    }

    public void HideTip()
    {
        isTipShow = false;
        tipTool.Hide();
    }

    public void ShowDrag(Article article, int amount)
    {
        dragItem.Show(article, amount);
        isDrag = true;
    }

    public void HideDrag()
    {
        dragItem.Hide();
        isDrag = false;
    }

    /// <summary>
    /// 建筑物的拖动图片
    /// </summary>
    public void ShowBuildDrag(Article article, BuildItem buildItem, int amount)
    {
        tempArticle = article;
        this.buildItem = buildItem;
        dragBuildItem.Show(article, amount);
        isBuildDrag = true;
        // 四边形
        dragBuildRect = GetRect(dragBuildItem.gameObject);
    }

    public void HideBuildDrag()
    {
        dragBuildItem.Hide();
        isBuildDrag = false;
    }
}
