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
public class CollectItemsEvent : Event
{

    public override void Start()
    {
        base.Start();
    }
    public override void OnEvent(EventData eventData)
    {
        // 获得角色的位置信息
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        MapCollectResource resource = MyJsonUtils.GetObject<MapCollectResource>(text);
        //// 进来更新位置了!!
        //Debug.Log("初始化地图了++>" + mapResoure);
        //// 更新地图
        //PhotonEngine.instance.mapResources = mapResoure;
        MapManage.Instance.UpdateResource(resource);
    }
}
