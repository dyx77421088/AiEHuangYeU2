using Assets.Script.Bag.Build;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Script.Bag.Build.BuildItem;

public class BuildCaiLiao : MonoBehaviour
{
    public GameObject iconAndNumberPrefab; // 图片和个数
    private Transform caiLiaoIcon; // 存放材料的父布局
    private static BuildCaiLiao instance;
    public static BuildCaiLiao Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }



    private void Start()
    {
        caiLiaoIcon = transform.Find("CaiLiaoIcon");
    }

    
    public void SetCailiao(List<BuildItem.BuildCollect> buildCollects, out bool isOk) 
    {
        // 清空孩子
        //循环删除子对象
        foreach (Transform child in caiLiaoIcon.transform)
        {
            Destroy(child.gameObject);
        }
        bool ok = true;
        foreach (var  build in buildCollects)
        {
            GameObject go = Instantiate(iconAndNumberPrefab, caiLiaoIcon);
            go.transform.SetParent(caiLiaoIcon);

            bool tempOk;
            IconAndNumber ian = go.GetComponent<IconAndNumber>();
            ian.StorArticle(build.Id); // 先储存
            ian.SetNumber(build, out tempOk); // 再设置个数
            ok = ok && tempOk;
        }
        isOk = ok;
    }

    public bool KoChuCaiLiao(List<BuildItem.BuildCollect> buildCollects)
    {
        bool ok = true;
        foreach (BuildCollect build in buildCollects)
        {
            ok = ok && Knapsack.Instance.ReduceNumberById(build.Id, build.Count);
        }
        return ok;
    }
}
