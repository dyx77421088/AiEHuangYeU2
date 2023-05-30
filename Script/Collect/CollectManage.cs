using Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CollectManage : MonoBehaviour
{

    public RectTransform caiji; // 采集的gameobject

    private GameObject player; // 主角
    private Animator playerAnimator; // 主角的动画
    private Vector2 offset = new Vector2(1, 2f);
    private static CollectManage instance;
    private GameObject target; // 被采集的对象
    private Collect targetCollect; // 被采集对象的collect属性
    private CollectInfo targetCollectInfo; // 被采集对象的map中的属性
    private PlayerCollectItemsRequest collectReequest; // 采集后的信息发送服务器

    // 角色的信息
    private PlayerInfo playerInfo;
    private PlayerAnimationRequest animationRequest;
    private CollectManage() { }
    public static CollectManage Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();

        collectReequest = player.GetComponent<PlayerCollectItemsRequest>();
        animationRequest = player.GetComponent<PlayerAnimationRequest>();
        playerInfo = PlayerInfo.Instance;
    }
    public void ShowCai(GameObject target)
    {
        caiji.gameObject.SetActive(true);
        this.target = target;
        targetCollect = target.GetComponent<Collect>();

        if (!target.TryGetComponent(out targetCollectInfo)) // 尝试获得component
        {
            Debug.Log("为空");
            targetCollectInfo = target.GetComponentInParent<CollectInfo>();
        }
    }

    public void HideCai()
    {
        caiji.gameObject.SetActive(false);
        target = null;
        targetCollect = null;
        targetCollectInfo = null;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            caiji.position = screenPos + offset;
        }
    }

    /// <summary>
    /// 查看按钮被点击
    /// </summary>
    public void OnLook()
    {
        Debug.Log("查看按钮");
        PlayerMessageManage.Instance.ShowMessage(targetCollect.GetDescription());
    }

    /// <summary>
    /// 采集按钮被点击
    /// </summary>
    public void OnCai()
    {
        if (PlayerAnimatorManagerUtils.IsCollect(playerAnimator) || target == null) return;
        if (targetCollect.IsCollecting)
        {
            // 应显示正在被采集对话 
            PlayerMessageManage.Instance.ShowMessage("正在采集。。。");
            return;
        }
        StartCoroutine("Cai", targetCollect.GetCollectTime());
    }

    private IEnumerator Cai(float seconds)
    {
        // 播放采集动画
        PlayerAnimatorManagerUtils.SelectAnimation(playerAnimator, PlayerAnimation.Collect);
        // 动画上传服务器
        playerInfo.SetAnimation(PlayerAnimation.Collect); // 设置移动动画属性
        animationRequest.DefaultRequest();
        yield return new WaitForSeconds(seconds);
        PlayerAnimatorManagerUtils.SelectAnimation(playerAnimator, PlayerAnimation.Idle);
        
        // 玩家提示获得物品
        PlayerMessageManage.Instance.ShowMessage("X" + targetCollect.GetCount(), targetCollect.GetImage());
        // 储存到背包里面
        Knapsack.Instance.StoryItem(targetCollect.collectItmeId, targetCollect.GetCount());
        // 保存背包的player
        Knapsack.Instance.SetPlayerSlots();
        // 用户保存采集的信息
        playerInfo.SetCollectResource(targetCollectInfo.Resource);
        // 动画上传服务器
        playerInfo.SetAnimation(PlayerAnimation.Idle); // 设置移动动画属性
        animationRequest.DefaultRequest();
        // 被采集的物品销毁
        Destroy(target);
        // 通知服务器
        collectReequest.DefaultRequest();
        HideCai();
    }
}
