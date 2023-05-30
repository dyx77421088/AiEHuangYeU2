using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMap : MonoBehaviour
{
    private Camera miniMapCamera;
    //public RenderTexture miniMapTexture;
    //public RawImage miniMapImage;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // ��ȡ��Ҷ���
        miniMapCamera = GetComponent<Camera>();
        //miniMapCamera.targetTexture = miniMapTexture; // �� Render Texture �󶨵������
        //miniMapImage.texture = miniMapTexture; // �� Render Texture �󶨵� Raw Image ��
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position; // ��ȡ���λ��
        newPosition.z = miniMapCamera.transform.position.z; // �������λ�õĸ߶Ȳ���
        miniMapCamera.transform.position = newPosition; // �������λ���ƶ�����Ҵ�

        //miniMapCamera.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // ��ת����Ա����丩���ӽ�
    }
}
