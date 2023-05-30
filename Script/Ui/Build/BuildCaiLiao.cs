using Assets.Script.Bag.Build;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Script.Bag.Build.BuildItem;

public class BuildCaiLiao : MonoBehaviour
{
    public GameObject iconAndNumberPrefab; // ͼƬ�͸���
    private Transform caiLiaoIcon; // ��Ų��ϵĸ�����
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
        // ��պ���
        //ѭ��ɾ���Ӷ���
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
            ian.StorArticle(build.Id); // �ȴ���
            ian.SetNumber(build, out tempOk); // �����ø���
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
