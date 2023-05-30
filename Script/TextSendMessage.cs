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
        data.Add(1, "���ǵ�һ����Ϣ");
        data.Add(2, "�޵а�������");
        data.Add(3, "�Ҵ� ������");
        data.Add(4, "���ǵ��ַ��׷�");

        // ����������������
        peer.OpCustom(1, data, true);
    }
}
