using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

/// <summary>
/// 上传建筑物的event
/// </summary>
public class UploadBuildEvent : Event
{
    public override void OnEvent(EventData eventData)
    {
        // 获得角色的位置信息
        string text = DictUtils.GetStringValue(eventData.Parameters, (byte)ParameterCode.JsonData);
        UploadBuildModel buildModel = MyJsonUtils.GetObject<UploadBuildModel>(text);
        // 进来更新位置了!!
        // 更新这个位置的角色的位置信息
        BuildManage.Instance.InBulidOnFloor(new Vector2(buildModel.Position.X, buildModel.Position.Y), buildModel.SpriteUrl);
    }
}
