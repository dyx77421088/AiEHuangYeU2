using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 lastV3 = Vector3.zero;
    private PlayerNewPositionRequest request;
    private bool isMove = false;
    private PlayerInfo playerInfo;
    void Start()
    {
        request = GetComponent<PlayerNewPositionRequest>();
        playerInfo = PlayerInfo.Instance;
    }
    private float speed = 4f;
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h * Time.deltaTime * speed, 0, v * Time.deltaTime * speed);
        // ����ƶ��ľ��볬��ָ�����룬���͸�����������֪ͨ�����û�
        if (Vector3.Distance(lastV3, transform.position) > 0.05f && (h != 0 || v != 0))
        //if (h != 0 || v != 0)
        {
            //playerInfo.SetPlayerPosition(lastV3, false);
            isMove = true;
            lastV3 = transform.position;
        }

        if (isMove)
        {
            Debug.Log("�ƶ�����" + isMove);
            isMove = false;
            //request.DefaultRequest();
            
        }

    }
}
