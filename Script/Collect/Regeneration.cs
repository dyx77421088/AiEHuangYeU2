using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ������
/// </summary>
public class Regeneration : MonoBehaviour
{
    public GameObject regenerationPrefabs; // ����ֲ���perfab
    public Sprite[] stageSprite; // �׶�ͼƬ
    public float[] regenerationSeconds; // ������Ҫ������


    private GameObject targetCollect; // �������ɵ�������Բɼ��Ķ���
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
        Debug.Log("����������");
        
        while (true)
        {
            // �ȴ�������� ����һֱΪ��ʱ��
            yield return new WaitUntil(() => targetCollect == null);
            // ����г����׶ξ� ����ʾ�����׶�
            for(int i = 0; i < regenerationSeconds.Length; i++) 
            {
                if (sr != null && stageSprite != null) sr.sprite = stageSprite[i];
                yield return new WaitForSeconds(regenerationSeconds[i]);
            }
            // ����ƷΪ���ڵȴ���ô������ٴ�����
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
        Debug.Log("target�Ƿ�Ϊ��" + targetCollect);
    }

    public class RegenerationStage
    {
        public string url; // ������ͼƬ
        public float regenerationSeconds; // ������Ҫ������ 
    }
}
