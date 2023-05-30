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
        // 如果鼠标点击的是右键
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 卸下装备
            if (!InventoryManage.Instance.isDrag)
            {
                if (transform.childCount > 1)
                {
                    ItemUi iui = transform.GetChild(1).GetComponent<ItemUi>();
                    // 放到背包里面
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
            // 首先判断大类是否正确,如果是材料之类的就直接返回了
            if (!(item.Type == Item.ItemType.Weapon || item.Type == Item.ItemType.Equipment))
            {
                // 类型不匹配，不能穿在角色上
                // 显示提示 TODD
                Debug.Log("类型不匹配!!");
                return;
            }

            // 如果是装备
            if (item.Type == Item.ItemType.Equipment)
            {
                Equipment eq = item as Equipment;
                if (eq.EquipType != equipmentType)
                {
                    Debug.Log("类型不匹配!!");
                    return;
                }
            } 
            else if (item.Type == Item.ItemType.Weapon) // 如果是武器
            {
                Weapon eq = item as Weapon;
                if (eq.WType != weaponType)
                {
                    Debug.Log("类型不匹配!!");
                    return;
                }
            }
            if (transform.childCount > 1 ) // 如果大于一就是有两个物品，一个是提示的名字，一个是图片
            {
                // 替换
                ItemUi itemUi = transform.GetChild(1).GetComponent<ItemUi>();

                Item tempUi = itemUi.article as Item;
                int amount = itemUi.amount;
                itemUi.SetArticle(item, temp.amount);
                InventoryManage.Instance.ShowDrag(tempUi, amount);
            }
            else
            {
                // 直接放上去
                StorArticle(item, temp.amount);
                InventoryManage.Instance.HideDrag();
                // 文字隐藏
                TextActive(false);
            }
            
        }
        else // 把这个东西给拖拽
        {
            if (transform.childCount > 1)
            {
                ItemUi itemUi = transform.GetChild(1).GetComponent<ItemUi>();
                InventoryManage.Instance.ShowDrag(itemUi.article, 1);
                Destroy(itemUi.gameObject);
                // 显示文字
                TextActive(true);
            }
            
        }

        // 保存背包的player
        Knapsack.Instance.SetPlayerSlots();
        //CharacterAttribute.Instance.showText();
    }

    public void TextActive(bool active = false)
    {
        text.gameObject.SetActive(active);
    }
}
