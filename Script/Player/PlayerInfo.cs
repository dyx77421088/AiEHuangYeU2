using Assets.Script.Bag.Build;
using Common;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Player myPlayer;

    private MapResource resource; // 上一次采集的物品,或者需上传的建筑
    private static PlayerInfo instance;
    public static PlayerInfo Instance
    {
        get { return instance; }
    }
    public Player player
    {
        get
        {
            return myPlayer ??= PhotonEngine.instance.player;
        }
        set => myPlayer = value;
    }
    private void Awake()
    {
        instance = this;
        //player = PhotonEngine.instance.player;
    }
    private void Start()
    {
        
        Debug.Log("playerinfo是否为空" + player);
        // 根据player 初始化位置和背包
        InitPlayer();
    }

    private void InitPlayer()
    {
        transform.position = new Vector2 (player.Position.X, player.Position.Y);
        Debug.Log("初始化player了");
        Knapsack.Instance.InitSlot(player.Slots);
    }

    public void SetPlayerPosition(Vector3 v3, bool isLeft)
    {
        player.Position.X = v3.x;
        player.Position.Y = v3.y;
        player.Position.Z = v3.z;
        player.IsLast = player.IsLeft;
        player.IsLeft = isLeft;
    }

    public void SetAnimation(PlayerAnimation animation)
    {
        player.Animation = animation;
    }

    public void SetCollectResource(MapCollectResource resource)
    {
        resource.IsDestruction = true; // 销毁
        this.resource = resource;
    }

    /// <summary>
    /// 设置地图上的最近操作的集的资源
    /// </summary>
    public void SetBuildResource(UploadBuildModel buildResource)
    {
        this.resource = buildResource;
    }

    public MapResource MapResource { get { return resource; } }

    /// <summary>
    /// 增加体能
    /// </summary>
    public void AddTiNeng(int strength=0, int resistance=0, int hungerDegree=0, int moisture=0)
    {
        player.Strength += strength;
        player.Resistance += resistance;
        player.HungerDegree += hungerDegree;
        player.Moisture += moisture;

        player.Strength = player.Strength > player.StrengthUpperLimit ? player.StrengthUpperLimit : player.Strength;

        player.Resistance = player.Resistance > player.ResistanceUpperLimit ? player.ResistanceUpperLimit : player.Resistance;

        player.HungerDegree = player.HungerDegree > player.HungerDegreeUpperLimit ? player.HungerDegreeUpperLimit : player.HungerDegree;
        
        player.Moisture = player.Moisture > player.MoistureUpperLimit ? player.MoistureUpperLimit : player.Moisture;
    }
    /// <summary>
    /// 减少体能
    /// </summary>
    public bool ReduceTiNeng(BuildItem item)
    {
        AddTiNeng(hungerDegree:-item.HungerDegree, moisture:-item.Moisture, strength:-item.Strength);
        return player.HungerDegree < 0 || player.Moisture < 0 || player.Strength < 0;
    }

    /// <summary>
    /// 判断当前的体能是否可以建造建筑
    /// </summary>
    public bool TiNengIsOk(BuildItem item)
    {
        return player.HungerDegree >= item.HungerDegree &&
               player.Moisture >= item.Moisture &&
               player.Strength >= item.Strength;
    }


}
