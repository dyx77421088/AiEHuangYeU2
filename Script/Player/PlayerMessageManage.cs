using UnityEngine;
using UnityEngine.UI;

public class PlayerMessageManage : MonoBehaviour
{
    public GameObject message;

    private GameObject player; // 玩家
    private Transform playerShowMessageT; // 玩家显示消息的位置
    private Text toolMessageText; // 占位的

    // 文字加图片
    private GameObject messageAndImage; // 文字和图片
    private Text messageAndImageText; // 真正显示的文字
    private Image messageAndImageImage; // 显示图片的

    private Text messageText; // 只有文字

    private CanvasGroup cg; // 控制透明度的
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
        // 显示的消息跟随玩家
        if (message.activeSelf)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(playerShowMessageT.transform.position);
            message.transform.position = screenPos;

            if (message.transform.rotation != player.transform.rotation)
            {
                message.transform.rotation = player.transform.rotation;
                messageText.transform.localRotation = player.transform.rotation; // 文字不需要旋转
                messageAndImage.transform.localRotation = player.transform.rotation; // 文字和图片不需要旋转
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
        // 显示消息
        message.SetActive(true);

        Debug.Log("image = = " + image);
        if (image != null)
        {
            // 只有文字消息的隐藏
            messageText.gameObject.SetActive(false);
            // 显示图片和文字的显示
            messageAndImage.gameObject.SetActive(true);

            messageAndImageText.text = text;
            // 占位的显示
            toolMessageText.text = text + "   ";
            // 显示图片
            messageAndImageImage.sprite = image;
        }
        else
        {
            // 只有文字消息的显示
            messageText.gameObject.SetActive(true);
            // 显示图片和文字的隐藏
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
