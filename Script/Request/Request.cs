using Common;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Requset : MonoBehaviour
{
    public OperationCode code;
    public abstract void DefaultRequest();
    public abstract void OnOperationResponse(OperationResponse operationResponse);

    public virtual void Start()
    {
        PhotonEngine.instance.AddRequest(this);
    }

    private void OnDestroy()
    {
        PhotonEngine.instance.RemoveRequest(this);
    }
}
