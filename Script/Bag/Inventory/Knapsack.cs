using Assets.Script.Bag.Collect;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理背包
/// </summary>
public class Knapsack : Inventory
{
    public Animator bagAnimator;

    private bool showBag = false;
    

    private static Knapsack instance;
    private Player player; // 主角
    public static Knapsack Instance 
    { 
        get
        {
            if (instance == null) 
            { 
                instance = GameObject.Find("Bag Slot").GetComponent<Knapsack>();
            }
            return instance;
        }
    }

    public override void Start()
    {
        player = PlayerInfo.Instance.player;
    }


    public void OpenOrHideBag()
    {
        if (showBag) bagAnimator.SetTrigger("NoBag");
        else
        {
            bagAnimator.SetTrigger("Bag");
        }
        showBag = !showBag;
    }

    public void NoAttributeAndCharacter()
    {
        bagAnimator.SetBool("Attribute", false);
        bagAnimator.SetBool("Character", false);
    }
    public void OnClickAttribute()
    {
        bagAnimator.SetBool("Attribute", true);
        bagAnimator.SetBool("Character", false);

        // 重新计算以下属性
        //CharacterAttribute.Instance.showText();
    }

    public void OnClickCharacter()
    {
        bagAnimator.SetBool("Character", true);
        bagAnimator.SetBool("Attribute", false);
    }

    public int GetNumberById(int id)
    {
        int number = 0;
        foreach(Slot slot in Slots)
        {
            if (slot.GetId() == id) number += slot.GetNumber();
        }
        return number;
    }

    /// <summary>
    /// 给id对应的减少这个多
    /// </summary>
    public bool ReduceNumberById(int id, int number=1)
    {
        int itemNumber, reduceNumber;
        foreach (Slot slot in Slots)
        {
            if (slot.GetId() == id)
            {
                itemNumber = slot.GetNumber();
                reduceNumber = number >= itemNumber ? itemNumber : number;
                number -= reduceNumber;
                slot.AddStorArticle(-reduceNumber);

                // 背包里面能全部减少这个东西
                if (number <= 0) { return true; } 
            }
        }
        return false;
    }

    /// <summary>
    /// 穿上装备
    /// </summary>
    public void PutOn(ItemUi item)
    {
        
        if (item.article is Weapon)
        {
            Weapon w = (Weapon)item.article;

            Character.Instance.PutOnWeapon(item, w);
        } 
        else if (item.article is Equipment)
        {
            Equipment eq = (Equipment)item.article;
            Character.Instance.PutOnEquipt(item, eq);
        }
    }

    /// <summary>
    /// 初始化slot中的东西啊
    /// </summary>
    public void InitSlot(List<SlotInfo> slotInfos)
    {
        Debug.Log("这个集合的个数" + slotInfos.Count);
        foreach (SlotInfo slotInfo in slotInfos)
        {
            Debug.Log("背包有东西a ");
            Slots[slotInfo.Position].StorArticle(slotInfo.ArticleId, slotInfo.Count);
        }
    }
    /// <summary>
    /// 保存slot中的东西啊
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayerSlots()
    {
        player.Slots.Clear();
        int i = 0;
        foreach(Slot slot in Slots)
        {
            int number = slot.GetNumber();
            //Debug.Log("位置" + i + "  的number是" + number);
            if (number > 0) player.Slots.Add(new SlotInfo()
            {
                ArticleId = slot.GetId(),
                Position = i,
                Count = number,
            }); ;
            i++;
        }
    }
}
