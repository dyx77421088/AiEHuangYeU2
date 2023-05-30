using Common;
using Common.Model;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
    public static PhotonEngine instance;
    [HideInInspector]
    public List<Player> otherPlayerInitList; // �û���һ�ε�¼��ʼ�����û�
    [HideInInspector]
    public Player player; // ����ҵ���Ϣ
    [HideInInspector]
    public MyList<MapResource> mapResources = new MyList<MapResource>(); // ��ͼ��Ϣ

    private static PhotonPeer peer;
    private Dictionary<OperationCode, Requset> requestDict = new Dictionary<OperationCode, Requset>();
    private Dictionary<EventCode, Event> eventDict = new Dictionary<EventCode, Event>();

    public static PhotonPeer Peer
    {
        get { return peer; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        //peer.Connect("server.natappfree.cc:39201", "MyGame1");
        peer.Connect("127.0.0.1:5055", "aiEHuangYe");
    }

    // Update is called once per frame
    void Update()
    {
        if (peer != null)
        {
            peer.Service();
        }
    }

    private void OnDestroy()
    {
        if (peer != null)
        {
            peer.Disconnect();
        }
    }
    public void DebugReturn(DebugLevel level, string message)
    {
    }

    /// <summary>
    /// ֱ���յ�������������Ϣ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEvent(EventData eventData)
    {
        EventCode code = (EventCode)eventData.Code;
        Event myEvent = null;
        bool isOk = eventDict.TryGetValue(code, out myEvent);
        if (isOk)
        {
            myEvent.OnEvent(eventData);
        }
        else
        {

        }
    }

    /// <summary>
    /// �յ�����������Ӧ
    /// </summary>
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode code = (OperationCode)operationResponse.OperationCode;
        Requset request = null;
        bool isOk =  requestDict.TryGetValue(code, out request);
        if (isOk)
        {
            request.OnOperationResponse(operationResponse);
        }
        else
        {

        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode.ToString());
    }

    public void AddRequest(Requset request)
    {
        requestDict.Add(request.code, request);
    }

    public void RemoveRequest(Requset request)
    {
        requestDict.Remove(request.code);
    }
    public void AddEvent(Event mEvent)
    {
        eventDict.Add(mEvent.code, mEvent);
    }

    public void RemoveEvent(Event mEvent)
    {
        eventDict.Remove(mEvent.code);
    }
}
