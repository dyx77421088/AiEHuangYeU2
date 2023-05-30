using Assets.Script.Bag.Build;
using Common;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Player myPlayer;

    private MapResource resource; // ��һ�βɼ�����Ʒ,�������ϴ��Ľ���
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
        
        Debug.Log("playerinfo�Ƿ�Ϊ��" + player);
        // ����player ��ʼ��λ�úͱ���
        InitPlayer();
    }

    private void InitPlayer()
    {
        transform.position = new Vector2 (player.Position.X, player.Position.Y);
        Debug.Log("��ʼ��player��");
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
        resource.IsDestruction = true; // ����
        this.resource = resource;
    }

    /// <summary>
    /// ���õ�ͼ�ϵ���������ļ�����Դ
    /// </summary>
    public void SetBuildResource(UploadBuildModel buildResource)
    {
        this.resource = buildResource;
    }

    public MapResource MapResource { get { return resource; } }

    /// <summary>
    /// ��������
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
    /// ��������
    /// </summary>
    public bool ReduceTiNeng(BuildItem item)
    {
        AddTiNeng(hungerDegree:-item.HungerDegree, moisture:-item.Moisture, strength:-item.Strength);
        return player.HungerDegree < 0 || player.Moisture < 0 || player.Strength < 0;
    }

    /// <summary>
    /// �жϵ�ǰ�������Ƿ���Խ��콨��
    /// </summary>
    public bool TiNengIsOk(BuildItem item)
    {
        return player.HungerDegree >= item.HungerDegree &&
               player.Moisture >= item.Moisture &&
               player.Strength >= item.Strength;
    }


}
