using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    #region 基础属性
    private int baseStrength = 10;
    private int baseIntellect = 10;
    private int baseAgility = 10;
    private int baseStamina = 10;

    public int BaseStrength { get => baseStrength; set => baseStrength = value; }
    public int BaseIntellect { get => baseIntellect; set => baseIntellect = value; }
    public int BaseAgility { get => baseAgility; set => baseAgility = value; }
    public int BaseStamina { get => baseStamina; set => baseStamina = value; }
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    List<Article> articles = InventoryManage.Instance.articles;
        //    Debug.Log(articles.Count);
        //    int index = Random.Range(0, articles.Count);
        //    //int index = Random.Range(0, 2);
        //    Knapsack.Instance.StoryItem(articles[index].Id);
        //}

        if (Input.GetKeyUp(KeyCode.B))
        {
            OnClickBag();
        }
        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    Chest.Instance.OpenOrHide();
        //}

    }

    /// <summary>
    /// 点击背包
    /// </summary>
    public void OnClickBag()
    {
        Knapsack.Instance.OpenOrHideBag();
        Knapsack.Instance.NoAttributeAndCharacter();
    }

    /// <summary>
    /// 点击属性
    /// </summary>
    public void OnClickAttribute()
    {
        Knapsack.Instance.OnClickAttribute();
    }
    /// <summary>
    /// 点击角色
    /// </summary>
    public void OnClickCharacter()
    {
        Knapsack.Instance.OnClickCharacter();
    }
}
