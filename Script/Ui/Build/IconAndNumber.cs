using Assets.Script.Bag.Build;
using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class IconAndNumber : MonoBehaviour
{
    private IconSlot iconSlot; // ͼƬ
    private Text number; // ����

    private void Awake()
    {
        iconSlot = transform.GetChild(0).GetComponent<IconSlot>();
        number = transform.GetChild(1).GetComponent<Text>();
    }
    void Start()
    {
        //iconSlot = transform.GetChild(0).GetComponent<IconSlot>();
        //number = transform.GetChild(1).GetComponent<Text>();
    }

    public void StorArticle(Article article)
    {
        iconSlot.StorArticle(article);
    }

    public void StorArticle(int id)
    {
        Article ac =  InventoryManage.Instance.GetArticleById(id);
        Debug.Log(iconSlot); 
        iconSlot.StorArticle(ac);
    }

    public void AddStorArticle(int number)
    {
        iconSlot.AddStorArticle(number);
    }

    /// <summary>
    /// ������ʾ��number�� ��1/2
    /// </summary>
    public void SetNumber(BuildItem.BuildCollect buildCollect, out bool isOk)
    {
        int hasNumber = Knapsack.Instance.GetNumberById(buildCollect.Id);
        // ��ѯ�����Ƿ��������
        string str = string.Format("<color={0}>{1}/{2}</color>", (hasNumber >= buildCollect.Count) ? "#00ff00" : "#ff0000", hasNumber, buildCollect.Count);

        number.text = str;

        isOk = hasNumber >= buildCollect.Count;
    }
}
