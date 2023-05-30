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
    /// 有新的角色上线了，new它
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
    /// 有角色掉线了，移除它
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
                Debug.Log("匹配上了" + other.GetId());
                otherPlayerList.Remove(go);
                Destroy(go);
                return;
            }
        }
    }

    /// <summary>
    /// 角色位置发生改变了
    /// </summary>
    public void UpdateOtherPosition(Player other)
    {
        foreach(GameObject go in otherPlayerList)
        {
            OtherPlayer player = go.GetComponent<OtherPlayer>();
            //Debug.Log("更新位置了！！！");
            if (player.GetId() == other.Id)
            {
                // 更新位置
                // 看是否需要旋转
                PlayerMove2.PlayerQuaternion(other.IsLeft, go.transform);
                go.transform.position = new Vector3(other.Position.X, other.Position.Y, other.Position.Z);
                return ;
            }
        }
    }

    /// <summary>
    /// 角色动画改变
    /// </summary>
    /// <param name="other"></param>
    public void UpdateOtherAnimation(Player other)
    {
        Debug.Log("更新画了=>" + other.Animation.ToString());
        foreach (GameObject go in otherPlayerList)
        {
            OtherPlayer player = go.GetComponent<OtherPlayer>();
            //Debug.Log("更新位置了！！！");
            if (player.GetId() == other.Id)
            {
                // 播放动画

                player.SelectAnimation(other.Animation);
                return;
            }
        }

    }
}
