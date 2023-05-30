using Common.Util;
using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动画同步给各个玩家
/// </summary>
public class PlayerAnimationRequest : Requset
{
    private PlayerInfo playerInfo;
    public override void Start()
    {
        playerInfo = PlayerInfo.Instance;
    }
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.JsonData, ToGson.Success(playerInfo.player));
        PhotonEngine.Peer.OpCustom((byte)code, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        //throw new System.NotImplementedException();
    }
}
