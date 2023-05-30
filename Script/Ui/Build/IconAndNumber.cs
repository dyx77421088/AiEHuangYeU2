using Assets.Script.Bag.Build;
using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class IconAndNumber : MonoBehaviour
{
    private IconSlot iconSlot; // 图片
    private Text number; // 文字

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
    /// 设置显示的number， 如1/2
    /// </summary>
    public void SetNumber(BuildItem.BuildCollect buildCollect, out bool isOk)
    {
        int hasNumber = Knapsack.Instance.GetNumberById(buildCollect.Id);
        // 查询背包是否符合条件
        string str = string.Format("<color={0}>{1}/{2}</color>", (hasNumber >= buildCollect.Count) ? "#00ff00" : "#ff0000", hasNumber, buildCollect.Count);

        number.text = str;

        isOk = hasNumber >= buildCollect.Count;
    }
}
