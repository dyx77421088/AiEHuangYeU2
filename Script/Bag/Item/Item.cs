using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

/// <summary>
/// ��Ʒ�࣬������Ʒ���е����ԣ�����
/// </summary>
[System.Serializable]
public class Item:Article
{
    private ItemType type;
    
    private int buyprice;
    private int sellprice;

    /// <summary>
    /// ��Ʒ���ͣ�����Ʒ��װ��������������
    /// </summary>
    public ItemType Type { get => type; set => type = value; }
    /// <summary>
    /// ����۸�
    /// </summary>
    public int Buyprice { get => buyprice; set => buyprice = value; }
    /// <summary>
    /// ���ۼ۸�
    /// </summary>
    public int Sellprice { get => sellprice; set => sellprice = value; }

    public Item()
    {
        
    }
    // ������
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

    
    #region ����ö����
    /// <summary>
    /// ����Ʒ װ�� ���� ����
    /// Consumable Equipment Weapon Material
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// ����Ʒ
        /// </summary>
        Consumable,
        /// <summary>
        /// װ��
        /// </summary>
        Equipment,
        /// <summary>
        /// ����
        /// </summary>
        Weapon,
        /// <summary>
        /// ����
        /// </summary>
        Material
    }
    #endregion

    private string GetTypeStr()
    {
        switch(Type)
        {
            case ItemType.Consumable: return "����Ʒ";
            case ItemType.Equipment: return "װ��";
            case ItemType.Weapon: return "����";
            case ItemType.Material: return "����";
        }
        return "δ֪";
    }

    public override string TipShow()
    {
        
        //"<color={4}>��������</color>"
        string tip = string.Format("<color={0}><size=18>{1}</size></color>\n<size=12>����:{2}</size>\n{3}\n" +
            "<color=yellow>����۸�:{4} ���ۼ۸�:{5}</color>\n"
            , GetQualityColor(), Name, GetTypeStr(), Description, buyprice, sellprice);
        return tip;
    }

    
}
