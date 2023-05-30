using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÉÏ´«µØÍ¼
/// </summary>
public class UploadMapRequest : Requset
{
    private MapManage mapManage;

    private void Awake()
    {
        mapManage = GetComponent<MapManage>();
    }
    public override void Start()
    {
        base.Start();
        
    }
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.JsonData, ToGson.Success(mapManage.GetMapResource(), true));
        PhotonEngine.Peer.OpCustom((byte)code, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new System.NotImplementedException();
    }

}
