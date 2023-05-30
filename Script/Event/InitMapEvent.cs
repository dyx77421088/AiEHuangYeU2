using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化地图
/// </summary>
public class InitMapEvent : Event
{

    public override void Start()
    {
        base.Start();
    }
    public override void OnEvent(EventData eventData)
    {
        Debug.Log("进来了    地图");
        // 获得角色的位置信息
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        MyList<MapResource> mapResoure = MyJsonUtils.GetMapResourceList(text);
        // 进来更新位置了!!
        // 更新地图
        PhotonEngine.instance.mapResources = mapResoure;
    }
}
