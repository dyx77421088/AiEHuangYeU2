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
        // 获得角色的位置信息
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        Player otherPlayer = MyJsonUtils.GetObject<Player>(text);
        // 进来更新位置了!!
        // 更新这个位置的角色的动画
        OtherPlayerManage.Instance.UpdateOtherAnimation(otherPlayer);
    }

}
