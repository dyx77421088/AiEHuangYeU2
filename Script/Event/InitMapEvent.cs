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
public class InitMapEvent : Event
{

    public override void Start()
    {
        base.Start();
    }
    public override void OnEvent(EventData eventData)
    {
        Debug.Log("������    ��ͼ");
        // ��ý�ɫ��λ����Ϣ
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        MyList<MapResource> mapResoure = MyJsonUtils.GetMapResourceList(text);
        // ��������λ����!!
        // ���µ�ͼ
        PhotonEngine.instance.mapResources = mapResoure;
    }
}
