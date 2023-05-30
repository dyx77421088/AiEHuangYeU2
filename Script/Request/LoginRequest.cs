using Common;
using Common.Model;
using Common.Util;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginRequest : Requset
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
        Dictionary<byte, object> objs = operationResponse.Parameters;
        string text = DictUtils.GetStringValue<byte, object>(objs, (byte)ParameterCode.JsonData);
        string message;
        int status;
        Debug.Log(text);
        List<Player> players = MyJsonUtils.GetList<Player>(text, out message, out status);
        Message.Instance.ShowMessage(message);
        Debug.Log(status);
        if (status == 200)
        {
            // 初始化当前在线的用户
            PhotonEngine.instance.otherPlayerInitList = players.Where<Player>(a =>
            {
                if (a.Name == username) PhotonEngine.instance.player = a;
                return a.Name != username;
            }).ToList();
            SceneManager.LoadScene("Scenes/Main", LoadSceneMode.Single);
        }
    }


}
