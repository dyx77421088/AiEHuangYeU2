using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʼ����ͼ
/// </summary>
public class CollectItemsEvent : Event
{

    public override void Start()
    {
        base.Start();
    }
    public override void OnEvent(EventData eventData)
    {
        // ��ý�ɫ��λ����Ϣ
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        MapCollectResource resource = MyJsonUtils.GetObject<MapCollectResource>(text);
        //// ��������λ����!!
        //Debug.Log("��ʼ����ͼ��++>" + mapResoure);
        //// ���µ�ͼ
        //PhotonEngine.instance.mapResources = mapResoure;
        MapManage.Instance.UpdateResource(resource);
    }
}
