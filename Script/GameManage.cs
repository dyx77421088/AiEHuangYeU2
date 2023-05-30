using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //垂直同步计数设置为0，才能锁帧，否则锁帧代码无效。
        QualitySettings.vSyncCount = 0;
        ////设置游戏帧数
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
