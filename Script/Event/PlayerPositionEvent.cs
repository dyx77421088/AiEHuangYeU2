using Common;
using Common.Model;
using Common.Util;
using ExitGames.Client.Photon;
using UnityEngine;

public class PlayerPositionEvent : Event
{
    public override void OnEvent(EventData eventData)
    {
        // ��ý�ɫ��λ����Ϣ
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        Player otherPlayer =  MyJsonUtils.GetObject<Player>(text);
        // ��������λ����!!
        // �������λ�õĽ�ɫ��λ����Ϣ
        OtherPlayerManage.Instance.UpdateOtherPosition(otherPlayer);
    }
}
