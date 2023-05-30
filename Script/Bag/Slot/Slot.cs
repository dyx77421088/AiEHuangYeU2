using Assets.Script.Bag.Collect;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// ����
/// </summary>
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject articlePrefab;


    public virtual void Start()
    {
        
    }

    /// <summary>
    /// ����ǵڼ���������ʾ��ͼƬ,��Ϊװ���µĸ��ӵڶ������Ӳ�����ʾ��ͼƬ
    /// </summary>
    /// <returns></returns>
    public int GetCount()
    {
        int count = 0;
        if (this is EquipSlot) count = 1;
        return count;
    }
    /// <summary>
    /// ������Ʒ
    /// </summary>
    /// <param name="item"></param>
    public void StorArticle(int id, int amount = 1)
    {
        StorArticle(InventoryManage.Instance.GetArticleById(id), amount);
    }
    public void StorArticle(Article article, int amount = 1)
    {
        
        if (transform.childCount == GetCount()) // ��ʾΪ��
        {
            // ����һ��������
            GameObject go = Instantiate(articlePrefab);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<ItemUi>().SetArticle(article, amount);
        } else
        {
            AddStorArticle(amount);
        }
    }
    public void AddStorArticle(int count = 1)
    {
        // ������1
        transform.GetChild(GetCount()).GetComponent<ItemUi>().AddItem(count);
        
    }

    /// <summary>
    /// �ж��Ƿ��Ѿ����˵��ӵ��������
    /// </summary>
    /// <returns></returns>
    public bool IsFilled()
    {
        ItemUi itemUi = transform.GetChild(GetCount()).GetComponent<ItemUi>();
        return itemUi.article.Capacity <= itemUi.amount;
    }

    public int GetId()
    {
        if(transform.childCount <= GetCount()) return -1;
        ItemUi itemUi = transform.GetChild(GetCount()).GetComponent<ItemUi>();
        return itemUi.article.Id;
    }

    public int GetNumber()
    {
        if (transform.childCount <= GetCount()) return 0;
        ItemUi itemUi = transform.GetChild(GetCount()).GetComponent<ItemUi>();
        return itemUi.amount;
    }



    #region �����Ϻ��Ƴ�,���
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > GetCount())
        {
            string info = transform.GetChild(GetCount()).GetComponent<ItemUi>().article.TipShow();
            InventoryManage.Instance.ShowTip(info);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManage.Instance.HideTip();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        // �������������Ҽ�
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!InventoryManage.Instance.isDrag)
            {
                if (transform.childCount > 0)
                {
                    ItemUi itemUi = transform.GetChild(0).GetComponent<ItemUi>();
                    if (itemUi.article is Consumable || itemUi.article is Food)
                    {
                        // ֱ��ʹ������Ʒ
                        if (itemUi.article is Food food)
                        {
                            PlayerInfo.Instance.AddTiNeng(hungerDegree:food.HungerDegree, 
                                moisture:food.Moisture, resistance:food.Resistance);
                            ShowPlayerTiNengManage.Instance.SetUi();
                        }
                        itemUi.AddItem(-1);
                    } 
                    // ��װ��
                    Knapsack.Instance.PutOn(itemUi);
                }
            }
        } 
        else if (transform.childCount != 0)
        {
            ItemUi itemUi = transform.GetChild(0).GetComponent<ItemUi>();
            if (InventoryManage.Instance.isDrag) // ������Ʒ����
            {
                // ȡ����ק�Ķ���
                ItemUi dragItem = InventoryManage.Instance.dragItem;
                // ��������������id��һ���ģ��ǾͿ��ܲ��ܵ���
                if (itemUi.article.Id == dragItem.article.Id)
                {
                    // 1.���������CTRL��һ��һ���ķ���
                    int addAmount = dragItem.amount;
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        addAmount = 1;
                    }
                    if (addAmount + itemUi.amount > itemUi.article.Capacity) 
                        addAmount = itemUi.article.Capacity - itemUi.amount;
                    if (addAmount > 0)
                    {
                        itemUi.AddItem(addAmount);
                        if (dragItem.amount - addAmount <= 0)
                        {
                            InventoryManage.Instance.HideDrag();
                        } else
                        {
                            dragItem.AddItem(-addAmount);
                        }
                    }
                } 
                else
                {
                    // �滻(������Ŀ��)
                    Article tempUi = itemUi.article;
                    int amount = itemUi.amount;
                    itemUi.SetArticle(dragItem.article, dragItem.amount);
                    InventoryManage.Instance.dragItem.SetArticle(tempUi, amount);
                }

                
            } 
            else
            {
                // ��������ƶ�
                // �Ƿ�ctrl��
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    int dragAmount = (itemUi.amount + 1) / 2;
                    if (dragAmount == itemUi.amount) Destroy(itemUi.gameObject);
                    else itemUi.AddItem(-dragAmount);
                    InventoryManage.Instance.ShowDrag(itemUi.article, dragAmount);
                }
                else
                {
                    InventoryManage.Instance.ShowDrag(itemUi.article, itemUi.amount);
                    Destroy(itemUi.gameObject);
                }
            }
            
        }else // �����壬������drag����Ʒ�ͷ��������������
        {
            if (InventoryManage.Instance.isDrag)
            {
                // �Ƿ�סctrl������ס�Ļ�����һ��һ����
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    // �հ׵ط���һ��
                    StorArticle(InventoryManage.Instance.dragItem.article);
                    InventoryManage.Instance.dragItem.AddItem(-1, true);
                    if (InventoryManage.Instance.dragItem.amount == 0) InventoryManage.Instance.HideDrag();
                }
                else
                {
                    // ��ק������Ʒ,ֱ�ӷ���
                    StorArticle(InventoryManage.Instance.dragItem.article, InventoryManage.Instance.dragItem.amount);
                    InventoryManage.Instance.HideDrag();
                }
            }
        }

        // ���汳����player
        Knapsack.Instance.SetPlayerSlots();
        //CharacterAttribute.Instance.showText();
    }
    #endregion
}
