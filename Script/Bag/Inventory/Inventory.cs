using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ����
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
    /// �ŵ�slot�У��ɹ�����true
    /// </summary>
    public bool StoryItem(int id, int count=1)
    {
        return StoryItem(InventoryManage.Instance.GetArticleById(id), count);
    }
    /// <summary>
    /// �жϱ�λ���Ƿ�����з���
    /// </summary>
    public bool StoryItem(Article article, int count=1)
    {
        if (article == null)
        {
            Debug.LogError("��ƷΪ��");
            return false;
        }

        if (article.Capacity == 1) // �����������Ϊ1����ô��ֱ������Ʒ��
        {
            Slot slot = FindEmptySlot();
            if (slot == null) 
            {
                Debug.LogError("��Ʒ������!!!");
                return false;
            }
            Debug.Log("slot = " + slot);
            Debug.Log("item = " + article);
            // �����Ʒ
            slot.StorArticle(article, count);
        } else
        {
            // �Ƿ���Ե���
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
                    Debug.LogError("��Ʒ������");
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// �ҵ�һ���յ���Ʒ��
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
    /// ��slot������ͬ�ģ���������
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
