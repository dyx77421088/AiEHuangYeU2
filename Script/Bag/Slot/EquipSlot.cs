using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class EquipSlot : Slot
{
    public Equipment.EquipmentType equipmentType;
    public Weapon.WeaponType weaponType;

    private Text text;

    public override void Start()
    {
        base.Start();
        text = transform.GetChild(0).GetComponent<Text>();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        // �������������Ҽ�
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // ж��װ��
            if (!InventoryManage.Instance.isDrag)
            {
                if (transform.childCount > 1)
                {
                    ItemUi iui = transform.GetChild(1).GetComponent<ItemUi>();
                    // �ŵ���������
                    if (Knapsack.Instance.StoryItem(iui.article.Id))
                    {
                        Destroy(iui.gameObject);
                        TextActive(true);
                    }
                }
                
            }
        }else if (InventoryManage.Instance.isDrag)
        {
            ItemUi temp = InventoryManage.Instance.dragItem;
            Item item = temp.article as Item;
            // �����жϴ����Ƿ���ȷ,����ǲ���֮��ľ�ֱ�ӷ�����
            if (!(item.Type == Item.ItemType.Weapon || item.Type == Item.ItemType.Equipment))
            {
                // ���Ͳ�ƥ�䣬���ܴ��ڽ�ɫ��
                // ��ʾ��ʾ TODD
                Debug.Log("���Ͳ�ƥ��!!");
                return;
            }

            // �����װ��
            if (item.Type == Item.ItemType.Equipment)
            {
                Equipment eq = item as Equipment;
                if (eq.EquipType != equipmentType)
                {
                    Debug.Log("���Ͳ�ƥ��!!");
                    return;
                }
            } 
            else if (item.Type == Item.ItemType.Weapon) // ���������
            {
                Weapon eq = item as Weapon;
                if (eq.WType != weaponType)
                {
                    Debug.Log("���Ͳ�ƥ��!!");
                    return;
                }
            }
            if (transform.childCount > 1 ) // �������һ������������Ʒ��һ������ʾ�����֣�һ����ͼƬ
            {
                // �滻
                ItemUi itemUi = transform.GetChild(1).GetComponent<ItemUi>();

                Item tempUi = itemUi.article as Item;
                int amount = itemUi.amount;
                itemUi.SetArticle(item, temp.amount);
                InventoryManage.Instance.ShowDrag(tempUi, amount);
            }
            else
            {
                // ֱ�ӷ���ȥ
                StorArticle(item, temp.amount);
                InventoryManage.Instance.HideDrag();
                // ��������
                TextActive(false);
            }
            
        }
        else // �������������ק
        {
            if (transform.childCount > 1)
            {
                ItemUi itemUi = transform.GetChild(1).GetComponent<ItemUi>();
                InventoryManage.Instance.ShowDrag(itemUi.article, 1);
                Destroy(itemUi.gameObject);
                // ��ʾ����
                TextActive(true);
            }
            
        }

        // ���汳����player
        Knapsack.Instance.SetPlayerSlots();
        //CharacterAttribute.Instance.showText();
    }

    public void TextActive(bool active = false)
    {
        text.gameObject.SetActive(active);
    }
}
