using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物再生
/// </summary>
public class Regeneration : MonoBehaviour
{
    public GameObject regenerationPrefabs; // 再生植物的perfab
    public Sprite[] stageSprite; // 阶段图片
    public float[] regenerationSeconds; // 再生需要多少秒


    private GameObject targetCollect; // 再生生成的这个可以采集的对象
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        NewRegenerationTarget();
        StartCoroutine("StartRegeneration");
    }

    void Update()
    {
        
    }

    private IEnumerator StartRegeneration()
    {
        Debug.Log("进来生成了");
        
        while (true)
        {
            // 等待这个物体 当它一直为空时，
            yield return new WaitUntil(() => targetCollect == null);
            // 如果有初级阶段就 先显示初级阶段
            for(int i = 0; i < regenerationSeconds.Length; i++) 
            {
                if (sr != null && stageSprite != null) sr.sprite = stageSprite[i];
                yield return new WaitForSeconds(regenerationSeconds[i]);
            }
            // 若物品为空在等待这么多秒后再次生成
            //yield return new WaitForSeconds(regenerationSeconds);
            NewRegenerationTarget();
        }
    }

    private void NewRegenerationTarget()
    {
        GameObject go = Instantiate(regenerationPrefabs, transform);
        go.transform.parent = transform;
        //go.transform.position = Vector2.zero;
        targetCollect = go;
    }

    public void DestroyTargetCollect()
    {
        Destroy(targetCollect);
        Debug.Log("target是否为空" + targetCollect);
    }

    public class RegenerationStage
    {
        public string url; // 再生的图片
        public float regenerationSeconds; // 再生需要多少秒 
    }
}
