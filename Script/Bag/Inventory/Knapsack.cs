using Assets.Script.Bag.Collect;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
public class Knapsack : Inventory
{
    public Animator bagAnimator;

    private bool showBag = false;
    

    private static Knapsack instance;
    private Player player; // ����
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

        // ���¼�����������
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
    /// ��id��Ӧ�ļ��������
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

                // ����������ȫ�������������
                if (number <= 0) { return true; } 
            }
        }
        return false;
    }

    /// <summary>
    /// ����װ��
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
    /// ��ʼ��slot�еĶ�����
    /// </summary>
    public void InitSlot(List<SlotInfo> slotInfos)
    {
        Debug.Log("������ϵĸ���" + slotInfos.Count);
        foreach (SlotInfo slotInfo in slotInfos)
        {
            Debug.Log("�����ж���a ");
            Slots[slotInfo.Position].StorArticle(slotInfo.ArticleId, slotInfo.Count);
        }
    }
    /// <summary>
    /// ����slot�еĶ�����
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayerSlots()
    {
        player.Slots.Clear();
        int i = 0;
        foreach(Slot slot in Slots)
        {
            int number = slot.GetNumber();
            //Debug.Log("λ��" + i + "  ��number��" + number);
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
