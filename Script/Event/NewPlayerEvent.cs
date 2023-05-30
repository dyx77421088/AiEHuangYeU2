using Common;
using Common.Model;
using Common.Util;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerEvent : Event
{
    public override void OnEvent(EventData eventData)
    {
        // ���µĽ�ɫ�����ˣ�����new����
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        Player other = MyJsonUtils.GetObject<Player>(text);
        OtherPlayerManage.Instance.NewOtherPlayer(other);
    }
}
