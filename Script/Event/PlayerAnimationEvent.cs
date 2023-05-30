using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : Event
{
    public override void OnEvent(EventData eventData)
    {
        // ��ý�ɫ��λ����Ϣ
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        Player otherPlayer = MyJsonUtils.GetObject<Player>(text);
        // ��������λ����!!
        // �������λ�õĽ�ɫ�Ķ���
        OtherPlayerManage.Instance.UpdateOtherAnimation(otherPlayer);
    }

}
