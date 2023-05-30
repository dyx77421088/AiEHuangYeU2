using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;

public class TextSendMessage : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendMessage();
        }
    }

    private void SendMessage()
    {
        PhotonPeer peer = PhotonEngine.Peer;

        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, "我是第一个消息");
        data.Add(2, "无敌啊啊啊啊");
        data.Add(3, "我打到 阿萨发");
        data.Add(4, "但是的手法首发");

        // 给服务器发送数据
        peer.OpCustom(1, data, true);
    }
}
