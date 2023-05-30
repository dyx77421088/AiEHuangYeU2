using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存放地图上的数据的
/// </summary>
public class CollectInfo : MonoBehaviour
{
    private MapCollectResource resource;

    public MapCollectResource Resource { get => resource; set => resource = value; }

    public int GetId()
    {
        return Resource.Id;
    }

}
