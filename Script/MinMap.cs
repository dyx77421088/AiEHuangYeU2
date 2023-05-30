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
        player = GameObject.FindGameObjectWithTag("Player").transform; // 获取玩家对象
        miniMapCamera = GetComponent<Camera>();
        //miniMapCamera.targetTexture = miniMapTexture; // 将 Render Texture 绑定到相机上
        //miniMapImage.texture = miniMapTexture; // 将 Render Texture 绑定到 Raw Image 上
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position; // 获取玩家位置
        newPosition.z = miniMapCamera.transform.position.z; // 保持相机位置的高度不变
        miniMapCamera.transform.position = newPosition; // 将相机的位置移动到玩家处

        //miniMapCamera.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // 旋转相机以保持其俯视视角
    }
}
