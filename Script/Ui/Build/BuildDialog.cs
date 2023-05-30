using Assets.Script.Bag.Build;
using Assets.Script.Bag.Collect;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BuildDialog : MonoBehaviour
{

    private Text title; // 名字
    private Text description; // 描述
    private Image icon; // 显示的图片
    //private GameObject cailiao; // 需要的材料的对象

    private Text hungerDegreeText; // 饥饿度
    private Text moistureText; // 水分
    private Text strengthText; // 体力

    private Article article; // 保存对话框的article
    private bool canBuild; // 材料是否齐全

    private static BuildDialog instance;

    public static BuildDialog Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        title = transform.Find("Name").GetComponent<Text>();
        description = transform.Find("Description").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();

        //cailiao = transform.Find("CaiLiao").gameObject;

        Transform xiaohao = transform.Find("XiaoHao").Find("XiaoHaoNumber");
        hungerDegreeText = xiaohao.GetChild(0).GetComponent<Text>();
        moistureText = xiaohao.GetChild(1).GetComponent<Text>();
        strengthText = xiaohao.GetChild(2).GetComponent<Text>();
        Debug.Log(xiaohao);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                HideIconDialog();
            }
        }
    }

    public void OnShowOrHideIconDialog(Article article)
    {
        if (article == null)
        {
            HideIconDialog();
        }
        else
        {
            ShowIconDialog(article);
        }
    }

    private void ShowIconDialog(Article article)
    {
        this.article = article;
        gameObject.SetActive(true);

        BuildItem item = article as BuildItem;

        title.text = item.Name;
        description.text = item.Description;
        icon.sprite = Resources.Load<Sprite>(article.Sprite);


        // 设置材料的text
        //BuildCaiLiao cai = cailiao.GetComponent<BuildCaiLiao>();
        //cai.SetCailiao();
        BuildCaiLiao.Instance.SetCailiao(item.CompositeList, out canBuild);

        // 设置饥饿度，体力，水分的个数
        hungerDegreeText.text = item.HungerDegree.ToString();
        moistureText.text = item.Moisture.ToString();
        strengthText.text = item.Strength.ToString();
    }

    private void HideIconDialog()
    {
        gameObject.SetActive(false);
    }

    public void OnCreate()
    {
        HideIconDialog();
        BuildItem item = article as BuildItem;
        
        if (!canBuild)
        {
            PlayerMessageManage.Instance.ShowMessage("材料不够");
            return;
        }
        // 判断体能够不够
        if (!PlayerInfo.Instance.TiNengIsOk(item))
        {
            PlayerMessageManage.Instance.ShowMessage("我太累了，想休息一下");
            return;
        }

        if (item.Type == BuildItem.BuildItemType.Tool) // 如果是工具的话就直接放入背包
        {
            Knapsack.Instance.StoryItem(article);
            PlayerMessageManage.Instance.ShowMessage("制作成功");
        }
        else if (item.Type == BuildItem.BuildItemType.Build) // 建筑就 建立
        {
            InventoryManage.Instance.ShowBuildDrag(article, item, 1);
        }
    }
}
