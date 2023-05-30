using Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CollectManage : MonoBehaviour
{

    public RectTransform caiji; // �ɼ���gameobject

    private GameObject player; // ����
    private Animator playerAnimator; // ���ǵĶ���
    private Vector2 offset = new Vector2(1, 2f);
    private static CollectManage instance;
    private GameObject target; // ���ɼ��Ķ���
    private Collect targetCollect; // ���ɼ������collect����
    private CollectInfo targetCollectInfo; // ���ɼ������map�е�����
    private PlayerCollectItemsRequest collectReequest; // �ɼ������Ϣ���ͷ�����

    // ��ɫ����Ϣ
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

        if (!target.TryGetComponent(out targetCollectInfo)) // ���Ի��component
        {
            Debug.Log("Ϊ��");
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
    /// �鿴��ť�����
    /// </summary>
    public void OnLook()
    {
        Debug.Log("�鿴��ť");
        PlayerMessageManage.Instance.ShowMessage(targetCollect.GetDescription());
    }

    /// <summary>
    /// �ɼ���ť�����
    /// </summary>
    public void OnCai()
    {
        if (PlayerAnimatorManagerUtils.IsCollect(playerAnimator) || target == null) return;
        if (targetCollect.IsCollecting)
        {
            // Ӧ��ʾ���ڱ��ɼ��Ի� 
            PlayerMessageManage.Instance.ShowMessage("���ڲɼ�������");
            return;
        }
        StartCoroutine("Cai", targetCollect.GetCollectTime());
    }

    private IEnumerator Cai(float seconds)
    {
        // ���Ųɼ�����
        PlayerAnimatorManagerUtils.SelectAnimation(playerAnimator, PlayerAnimation.Collect);
        // �����ϴ�������
        playerInfo.SetAnimation(PlayerAnimation.Collect); // �����ƶ���������
        animationRequest.DefaultRequest();
        yield return new WaitForSeconds(seconds);
        PlayerAnimatorManagerUtils.SelectAnimation(playerAnimator, PlayerAnimation.Idle);
        
        // �����ʾ�����Ʒ
        PlayerMessageManage.Instance.ShowMessage("X" + targetCollect.GetCount(), targetCollect.GetImage());
        // ���浽��������
        Knapsack.Instance.StoryItem(targetCollect.collectItmeId, targetCollect.GetCount());
        // ���汳����player
        Knapsack.Instance.SetPlayerSlots();
        // �û�����ɼ�����Ϣ
        playerInfo.SetCollectResource(targetCollectInfo.Resource);
        // �����ϴ�������
        playerInfo.SetAnimation(PlayerAnimation.Idle); // �����ƶ���������
        animationRequest.DefaultRequest();
        // ���ɼ�����Ʒ����
        Destroy(target);
        // ֪ͨ������
        collectReequest.DefaultRequest();
        HideCai();
    }
}
