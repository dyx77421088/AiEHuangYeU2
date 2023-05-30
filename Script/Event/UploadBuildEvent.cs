using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

/// <summary>
/// �ϴ��������event
/// </summary>
public class UploadBuildEvent : Event
{
    public override void OnEvent(EventData eventData)
    {
        // ��ý�ɫ��λ����Ϣ
        string text = DictUtils.GetStringValue(eventData.Parameters, (byte)ParameterCode.JsonData);
        UploadBuildModel buildModel = MyJsonUtils.GetObject<UploadBuildModel>(text);
        // ��������λ����!!
        // �������λ�õĽ�ɫ��λ����Ϣ
        BuildManage.Instance.InBulidOnFloor(new Vector2(buildModel.Position.X, buildModel.Position.Y), buildModel.SpriteUrl);
    }
}
