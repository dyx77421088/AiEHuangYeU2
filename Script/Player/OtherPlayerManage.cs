using Common;
using Common.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OtherPlayerManage : MonoBehaviour
{
    private static OtherPlayerManage instance;
    private List<GameObject> otherPlayerList = new List<GameObject>();

    public GameObject otherPlayerPerfab;
    [HideInInspector]
    public static OtherPlayerManage Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        List<Player> p = PhotonEngine.instance.otherPlayerInitList;
        if (p != null)
        {
            foreach (Player p2 in p)
            {
                NewOtherPlayer(p2);
            }
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���µĽ�ɫ�����ˣ�new��
    /// </summary>
    /// <param name="player"></param>
    public void NewOtherPlayer(Player player)
    {
        GameObject go =  GameObject.Instantiate(otherPlayerPerfab);
        OtherPlayer other = go.GetComponent<OtherPlayer>();

        other.SetTitle(player.Name);

        Vector3DPosition v3 = player.Position;
        go.transform.position = new Vector3(v3.X, v3.Y, v3.Z);
        other.SetPlayer(player);
        otherPlayerList.Add(go);
    }

    /// <summary>
    /// �н�ɫ�����ˣ��Ƴ���
    /// </summary>
    /// <param name="player"></param>
    public void CloseOtherPlayer(Player player)
    {
        Debug.Log("player===" + player.Name);
        foreach (GameObject go in otherPlayerList)
        {
            OtherPlayer other = go.GetComponent<OtherPlayer>();
            if (other.GetId() == player.Id)
            {
                Debug.Log("ƥ������" + other.GetId());
                otherPlayerList.Remove(go);
                Destroy(go);
                return;
            }
        }
    }

    /// <summary>
    /// ��ɫλ�÷����ı���
    /// </summary>
    public void UpdateOtherPosition(Player other)
    {
        foreach(GameObject go in otherPlayerList)
        {
            OtherPlayer player = go.GetComponent<OtherPlayer>();
            //Debug.Log("����λ���ˣ�����");
            if (player.GetId() == other.Id)
            {
                // ����λ��
                // ���Ƿ���Ҫ��ת
                PlayerMove2.PlayerQuaternion(other.IsLeft, go.transform);
                go.transform.position = new Vector3(other.Position.X, other.Position.Y, other.Position.Z);
                return ;
            }
        }
    }

    /// <summary>
    /// ��ɫ�����ı�
    /// </summary>
    /// <param name="other"></param>
    public void UpdateOtherAnimation(Player other)
    {
        Debug.Log("���»���=>" + other.Animation.ToString());
        foreach (GameObject go in otherPlayerList)
        {
            OtherPlayer player = go.GetComponent<OtherPlayer>();
            //Debug.Log("����λ���ˣ�����");
            if (player.GetId() == other.Id)
            {
                // ���Ŷ���

                player.SelectAnimation(other.Animation);
                return;
            }
        }

    }
}
