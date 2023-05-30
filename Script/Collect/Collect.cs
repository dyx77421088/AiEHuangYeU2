using Assets.Script.Bag.Collect;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public int collectItmeId; // 所对应的id

    private CollectItem collect; // 根据id对应的article信息
    private bool isCollecting; // 是否正在采集，如果正在采集其它用户不能操作
    private Sprite image; // 显示的图片

    public bool IsCollecting { get => isCollecting; set => isCollecting = value; }

    private void Start()
    {
        image = GetComponent<SpriteRenderer>().sprite;

        collect = InventoryManage.Instance.GetArticleById(collectItmeId) as CollectItem;
    }

    public Sprite GetImage() { return image; }

    public string GetDescription()
    {
        return collect.Description;
    }

    public float GetCollectTime()
    {
        return collect.CollectTime;
    }

    public int GetCount()
    {
        Debug.Log("格式是" +  collect.Count);
        return collect.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollectManage.Instance.ShowCai(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollectManage.Instance.HideCai();
        }
    }
}
