using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

/// <summary>
/// 物品类，所有物品共有的属性，父类
/// </summary>
[System.Serializable]
public class Item:Article
{
    private ItemType type;
    
    private int buyprice;
    private int sellprice;

    /// <summary>
    /// 物品类型，消耗品，装备，武器，材料
    /// </summary>
    public ItemType Type { get => type; set => type = value; }
    /// <summary>
    /// 购买价格
    /// </summary>
    public int Buyprice { get => buyprice; set => buyprice = value; }
    /// <summary>
    /// 出售价格
    /// </summary>
    public int Sellprice { get => sellprice; set => sellprice = value; }

    public Item()
    {
        
    }
    // 构造器
    public Item(int id, string name, ItemType type, ArticleQuality quality, 
        string description, int capacity, int buyprice, int sellprice, string sprite)
    {
        this.Id = id;
        this.Name = name;
        this.Type = type;
        this.Quality = quality;
        this.Description = description;
        this.Sprite = sprite;
        this.Capacity = capacity; 
        this.Buyprice = buyprice;
        this.Sellprice = sellprice;
    }

    
    #region 类型枚举类
    /// <summary>
    /// 消耗品 装备 武器 材料
    /// Consumable Equipment Weapon Material
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 消耗品
        /// </summary>
        Consumable,
        /// <summary>
        /// 装备
        /// </summary>
        Equipment,
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// 材料
        /// </summary>
        Material
    }
    #endregion

    private string GetTypeStr()
    {
        switch(Type)
        {
            case ItemType.Consumable: return "消耗品";
            case ItemType.Equipment: return "装备";
            case ItemType.Weapon: return "武器";
            case ItemType.Material: return "材料";
        }
        return "未知";
    }

    public override string TipShow()
    {
        
        //"<color={4}>基本属性</color>"
        string tip = string.Format("<color={0}><size=18>{1}</size></color>\n<size=12>类型:{2}</size>\n{3}\n" +
            "<color=yellow>购买价格:{4} 出售价格:{5}</color>\n"
            , GetQualityColor(), Name, GetTypeStr(), Description, buyprice, sellprice);
        return tip;
    }

    
}
