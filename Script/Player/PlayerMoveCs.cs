using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerMoveCs : MonoBehaviour
{
    private float speed = 4f;
    private Animator animator;
    private bool isLeftZ = false; // 方向是否是朝向左边
    private GameObject body;

    private float leftX, rightX;
    private float topY, bottomY;

    private Vector3 lastV3 = Vector3.zero;
    //private PlayerNewPositionRequest request;
    //private PlayerAnimationRequest animationRequest;
    private bool isMove = false;
    private PlayerInfo playerInfo;
    void Start()
    {
        animator = GetComponent<Animator>();
        body = transform.Find("body").gameObject;

        int reduce = MapManage.Instance.reduce;
        leftX = MapManage.Instance.sigejiao[0].position.x + reduce;
        rightX = MapManage.Instance.sigejiao[1].position.x - reduce;

        topY = MapManage.Instance.sigejiao[0].position.y - reduce;
        bottomY = MapManage.Instance.sigejiao[2].position.y + reduce;

        //request = GetComponent<PlayerNewPositionRequest>();
        //animationRequest = GetComponent<PlayerAnimationRequest>();
        playerInfo = PlayerInfo.Instance;
    }
    private bool isAnimation = false;
    // Update is called once per frame
    void Update()
    {
        // 判断是否在采集,如果是在采集就不能做其它的操作
        if (PlayerAnimatorManagerUtils.IsCollect(animator)) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            if (h < 0 && !isLeftZ)
            {
                isLeftZ = true;

                PlayerQuaternion(true, transform);
                Vector3 tempV = body.transform.localPosition;
                tempV.z = -0.01f;
                body.transform.localPosition = tempV;
            }
            else if (h > 0 && isLeftZ)
            {
                isLeftZ = false;

                PlayerQuaternion(false, transform);
                Vector3 tempV = body.transform.localPosition;
                tempV.z = 0.01f;
                body.transform.localPosition = tempV;
            }
            transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        }

        if (Vector3.Distance(lastV3, transform.position) > 0.05f && (h != 0 || v != 0))
        {
            
            isMove = true;
            lastV3 = transform.position;
        }
        else
        {
            if (isAnimation && v == 0 && h == 0)
            {
                isAnimation = false;
                PlayerAnimatorManagerUtils.SelectAnimation(animator, PlayerAnimation.Idle);
                Debug.Log("待机");
                //animationRequest.DefaultRequest();
            }
        }

        if (isMove)
        {
            isMove = false;

            if (!isAnimation && (v != 0 || h != 0))
            {
                isAnimation = true;

                PlayerAnimatorManagerUtils.SelectAnimation(animator, PlayerAnimation.Move);
                Debug.Log("移动");
            }
        }

        Vector3 v3 = transform.position;
        // 边界值判定
        if (v3.x < leftX) v3.x = leftX;
        if (v3.x > rightX) v3.x = rightX;
        if (v3.y < bottomY) v3.y = bottomY;
        if (v3.y > topY) v3.y = topY;
        transform.position = v3;
    }

    /// <summary>
    /// 转换位置
    /// </summary>
    public static void PlayerQuaternion(bool is180, Transform transform)
    {
        if (is180)
        {
            if (transform.rotation.y != 1)
            {
                transform.rotation = new Quaternion() { y = 1 };
                transform.position -= new Vector3(1, 0);
            }
        }
        else
        {
            if (transform.rotation.y != 0)
            {
                transform.rotation = new Quaternion() { y = 0 };
                transform.position += new Vector3(1, 0);
            }
        }
    }
}
