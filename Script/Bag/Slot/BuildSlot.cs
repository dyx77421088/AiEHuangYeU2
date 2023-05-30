using Assets.Script.Bag.Collect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSlot : Slot
{
    private Article article;

    public override void Start()
    {
        article = transform.GetChild(0).GetComponent<ItemUi>().article;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        BuildManage.Instance.OnShowOrHideIconDialog(article);
    }
}
