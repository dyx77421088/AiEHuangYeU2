using UnityEngine;
using UnityEngine.UI;

public class PlayerMessageManage : MonoBehaviour
{
    public GameObject message;

    private GameObject player; // ���
    private Transform playerShowMessageT; // �����ʾ��Ϣ��λ��
    private Text toolMessageText; // ռλ��

    // ���ּ�ͼƬ
    private GameObject messageAndImage; // ���ֺ�ͼƬ
    private Text messageAndImageText; // ������ʾ������
    private Image messageAndImageImage; // ��ʾͼƬ��

    private Text messageText; // ֻ������

    private CanvasGroup cg; // ����͸���ȵ�
    private float jiShu = 0, targetTime = 2; 

    private static PlayerMessageManage instance;


    public static PlayerMessageManage Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShowMessageT = player.transform.Find("ShowMessagePosition");

        cg = message.GetComponent<CanvasGroup>();
        toolMessageText = message.GetComponent<Text>();

        messageAndImage = message.transform.GetChild(1).gameObject;
        messageAndImageText = messageAndImage.transform.GetChild(1).GetComponent<Text>();
        messageAndImageImage = messageAndImage.transform.GetChild(0).GetComponent<Image>();

        messageText = message.transform.GetChild(2).GetComponent<Text>();
    }

    
    void Update()
    {
        // ��ʾ����Ϣ�������
        if (message.activeSelf)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(playerShowMessageT.transform.position);
            message.transform.position = screenPos;

            if (message.transform.rotation != player.transform.rotation)
            {
                message.transform.rotation = player.transform.rotation;
                messageText.transform.localRotation = player.transform.rotation; // ���ֲ���Ҫ��ת
                messageAndImage.transform.localRotation = player.transform.rotation; // ���ֺ�ͼƬ����Ҫ��ת
            }

            jiShu += Time.deltaTime;
            if (jiShu > targetTime)
            {
                cg.alpha = Mathf.Lerp(cg.alpha, 0, 2f * Time.deltaTime);
                if (Mathf.Abs(cg.alpha - 0) < 0.02f)
                {
                    HideMessage();
                }
            }
        }
        
    }

    public void ShowMessage(string text, Sprite image = null)
    {
        // ��ʾ��Ϣ
        message.SetActive(true);

        Debug.Log("image = = " + image);
        if (image != null)
        {
            // ֻ��������Ϣ������
            messageText.gameObject.SetActive(false);
            // ��ʾͼƬ�����ֵ���ʾ
            messageAndImage.gameObject.SetActive(true);

            messageAndImageText.text = text;
            // ռλ����ʾ
            toolMessageText.text = text + "   ";
            // ��ʾͼƬ
            messageAndImageImage.sprite = image;
        }
        else
        {
            // ֻ��������Ϣ����ʾ
            messageText.gameObject.SetActive(true);
            // ��ʾͼƬ�����ֵ�����
            messageAndImage.gameObject.SetActive(false);

            messageText.text = text;
            toolMessageText.text = text;
        }

        cg.alpha = 1;
        jiShu = 0;
    }

    public void HideMessage()
    {
        cg.alpha = 0;
        jiShu = 0;
        message.SetActive(false);
    }
}
