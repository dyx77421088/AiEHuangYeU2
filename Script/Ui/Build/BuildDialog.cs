using Assets.Script.Bag.Build;
using Assets.Script.Bag.Collect;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BuildDialog : MonoBehaviour
{

    private Text title; // ����
    private Text description; // ����
    private Image icon; // ��ʾ��ͼƬ
    //private GameObject cailiao; // ��Ҫ�Ĳ��ϵĶ���

    private Text hungerDegreeText; // ������
    private Text moistureText; // ˮ��
    private Text strengthText; // ����

    private Article article; // ����Ի����article
    private bool canBuild; // �����Ƿ���ȫ

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


        // ���ò��ϵ�text
        //BuildCaiLiao cai = cailiao.GetComponent<BuildCaiLiao>();
        //cai.SetCailiao();
        BuildCaiLiao.Instance.SetCailiao(item.CompositeList, out canBuild);

        // ���ü����ȣ�������ˮ�ֵĸ���
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
            PlayerMessageManage.Instance.ShowMessage("���ϲ���");
            return;
        }
        // �ж����ܹ�����
        if (!PlayerInfo.Instance.TiNengIsOk(item))
        {
            PlayerMessageManage.Instance.ShowMessage("��̫���ˣ�����Ϣһ��");
            return;
        }

        if (item.Type == BuildItem.BuildItemType.Tool) // ����ǹ��ߵĻ���ֱ�ӷ��뱳��
        {
            Knapsack.Instance.StoryItem(article);
            PlayerMessageManage.Instance.ShowMessage("�����ɹ�");
        }
        else if (item.Type == BuildItem.BuildItemType.Build) // ������ ����
        {
            InventoryManage.Instance.ShowBuildDrag(article, item, 1);
        }
    }
}
