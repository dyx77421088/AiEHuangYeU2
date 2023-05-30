using Assets.Script.Bag.Collect;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public int collectItmeId; // ����Ӧ��id

    private CollectItem collect; // ����id��Ӧ��article��Ϣ
    private bool isCollecting; // �Ƿ����ڲɼ���������ڲɼ������û����ܲ���
    private Sprite image; // ��ʾ��ͼƬ

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
        Debug.Log("��ʽ��" +  collect.Count);
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
