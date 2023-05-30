using Common;
using Common.Util;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterRequest : Requset
{
    [HideInInspector]
    public string username;
    [HideInInspector]
    public string password;
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Name, username);
        data.Add((byte)ParameterCode.Password, password);
        PhotonEngine.Peer.OpCustom((byte)code, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        Dictionary<byte,object> objs =  operationResponse.Parameters;
        string message = DictUtils.GetStringValue<byte, object>(objs, (byte)ParameterCode.Message);
        Message.Instance.ShowMessage(message);

        
    }


}
