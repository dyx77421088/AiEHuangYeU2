using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //��ֱͬ����������Ϊ0��������֡��������֡������Ч��
        QualitySettings.vSyncCount = 0;
        ////������Ϸ֡��
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
