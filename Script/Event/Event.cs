using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : MonoBehaviour
{
    public EventCode code;
    public abstract void OnEvent(EventData eventData);

    public virtual void Start()
    {
        PhotonEngine.instance.AddEvent(this);
    }

    private void OnDestroy()
    {
        PhotonEngine.instance.RemoveEvent(this);
    }
}
