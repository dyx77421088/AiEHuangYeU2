using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 管理
/// </summary>
public class Inventory : MonoBehaviour
{
    private Slot[] slots;

    protected Slot[] Slots { get
        {
            slots ??= GetComponentsInChildren<Slot>();
            return slots;
        }
    }

    public virtual void Start()
    {
        //Slots = GetComponentsInChildren<Slot>();
    }

    /// <summary>
    /// 放到slot中，成功返回true
    /// </summary>
    public bool StoryItem(int id, int count=1)
    {
        return StoryItem(InventoryManage.Instance.GetArticleById(id), count);
    }
    /// <summary>
    /// 判断本位置是否可以有放入
    /// </summary>
    public bool StoryItem(Article article, int count=1)
    {
        if (article == null)
        {
            Debug.LogError("物品为空");
            return false;
        }

        if (article.Capacity == 1) // 如果叠加上限为1，那么就直接找物品槽
        {
            Slot slot = FindEmptySlot();
            if (slot == null) 
            {
                Debug.LogError("物品槽已满!!!");
                return false;
            }
            Debug.Log("slot = " + slot);
            Debug.Log("item = " + article);
            // 添加物品
            slot.StorArticle(article, count);
        } else
        {
            // 是否可以叠加
            Slot slot = FindSameTypeSlot(article);
            if (slot != null)
            {
                slot.AddStorArticle(count);
            }
            else
            {
                slot = FindEmptySlot();
                if ( slot != null )
                {
                    slot.StorArticle(article, count);
                } else
                {
                    Debug.LogError("物品槽已满");
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 找到一个空的物品槽
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in Slots)
        {
            if (slot.transform.childCount == 0) return slot;
        }
        return null;
    }

    /// <summary>
    /// 在slot中找相同的，用来叠加
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private Slot FindSameTypeSlot(Article article)
    {
        foreach(Slot slot in Slots)
        {
            if (slot.transform.childCount >= 1 &&  slot.GetId() == article.Id && !slot.IsFilled()) 
                return slot;
        }
        return null;
    }
}
