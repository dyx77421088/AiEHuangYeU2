using UnityEngine;

public class Material : Item
{
    public Material(int id, string name, ItemType type, ArticleQuality quality,
        string description, int capacity, int buyprice, int sellprice, string sprite)
        : base(id, name, type, quality, description, capacity, buyprice, sellprice, sprite)
    {
    }
}
