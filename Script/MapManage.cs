using Common;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapManage : MonoBehaviour
{
    public GameObject[] trees;
    public GameObject[] grass;
    public GameObject[] stones;
    public GameObject[] collects;
    public Transform[] sigejiao; // 四个角
    public GameObject map;

    private int treeCount = 50;
    private int grassCount = 100;
    private int stonesCount = 20;
    private int collectCount = 100;
    private int id = 1; // 每个地图物体的id

    private Transform treeMap;
    private Transform grassMap;
    private Transform stoneMap;
    private Transform collectMap;

    private List<CollectInfo> mapCollectInfo;
    public int reduce = 8; // 随机资源减少的距离


    //private List<GameObject> grassList = new List<GameObject>();
    //private List<GameObject> treeList = new List<GameObject>();
    //private List<GameObject> stoneList = new List<GameObject>();
    private MyList<MapResource> mapResource = new();

    private UploadMapRequest uploadMapRequest;

    private static MapManage instance;
    private MapManage() { }
    public static MapManage Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        treeMap = map.transform.Find("Tree");
        grassMap = map.transform.Find("Grass");
        stoneMap = map.transform.Find("Stone");
        collectMap = map.transform.Find("Collect");
        uploadMapRequest = GetComponent<UploadMapRequest>();

        InitMapSelect(mapResource = PhotonEngine.instance.mapResources);
        mapCollectInfo = collectMap.GetComponentsInChildren<CollectInfo>().ToList();
        Debug.Log("所有的租金啊" + mapCollectInfo.Count);
        //InitMap();
    }

    public void InitMapSelect(MyList<MapResource> maps)
    {
        if (maps.Count == 0 )
        {
            InitMap();
            // 把初始化的地图上传服务器
            uploadMapRequest.DefaultRequest();
        }
        else
        {
            InitMap(maps);
        }
    }

    private void InitMap()
    {
        mapResource.Clear();
        float xLeft = sigejiao[0].position.x + reduce;
        float xRight = sigejiao[1].position.x - reduce;

        float yTop = sigejiao[0].position.y - reduce;
        float yBottom = sigejiao[2].position.y + reduce;
        // 随机生成树
        for (int i = 0; i< treeCount; i++)
        {
            // 随机坐标
            float x = Random.Range(xLeft, xRight);
            float y = Random.Range(yBottom, yTop);
            // 随机树类
            int index = Random.Range(0, trees.Length);

            GameObject go = GameObject.Instantiate(trees[index]);
            go.transform.position = new Vector3(x, y, 0);
            go.transform.parent = treeMap;

            mapResource.Add(new MapResource() { 
                Id = id++,
                Type=MapResourceType.Tree,
                Index = index,
                Position = new Vector3DPosition() { X = x, Y = y },
            });
        }

        // 随机生成石头
        for (int i = 0; i < stonesCount; i++)
        {
            // 随机坐标
            float x = Random.Range(xLeft, xRight);
            float y = Random.Range(yBottom, yTop);
            // 随机树类
            int index = Random.Range(0, stones.Length);

            GameObject go = Instantiate(stones[index]);
            go.transform.position = new Vector3(x, y, 0);
            go.transform.parent = stoneMap;

            mapResource.Add(new MapResource()
            {
                Id = id++,
                Type = MapResourceType.Stone,
                Index = index,
                Position = new Vector3DPosition() { X = x, Y = y },
            });
        }

        // 随机生成草
        for (int i = 0; i < grassCount; i++)
        {
            // 随机坐标
            float x = UnityEngine.Random.Range(xLeft, xRight);
            float y = Random.Range(yBottom, yTop);
            // 随机树类
            int index = Random.Range(0, grass.Length);

            GameObject go = GameObject.Instantiate(grass[index]);
            go.transform.position = new Vector3(x, y, 0);
            go.transform.parent = grassMap;

            mapResource.Add(new MapResource()
            {
                Id = id++,
                Type = MapResourceType.Grass,
                Index = index,
                Position = new Vector3DPosition() { X = x, Y = y },
            });
        }

        // 随机生成可采集物
        for (int i = 0; i < collectCount; i++)
        {
            // 随机坐标
            float x = Random.Range(xLeft, xRight);
            float y = Random.Range(yBottom, yTop);
            // 随机树类
            int index = Random.Range(0, collects.Length);

            GameObject go = Instantiate(collects[index]);
            Regeneration regeneration = go.GetComponent<Regeneration>(); // 是否是可再生的（存在这个脚本就是可再生的）
            go.transform.position = new Vector3(x, y, 0);
            go.transform.parent = collectMap;

            CollectInfo collectInfo = go.GetComponent<CollectInfo>(); // 采集的信息
            collectInfo.Resource = new MapCollectResource()
            {
                Id = id++,
                CanRegeneration = regeneration,
                IsDestruction = false,
                Type = MapResourceType.Collect,
                Index = index,
                Position = new Vector3DPosition() { X = x, Y = y },
            };
            mapResource.Add(collectInfo.Resource);
        }
    }

    /// <summary>
    /// 从服务器得到的资源更新地图
    /// </summary>
    /// <param name="maps"></param>
    private void InitMap(MyList<MapResource> maps)
    {
        foreach (MapResource map in maps)
        {
            //Debug.Log(map.Type + " " + map.Index);
            GameObject go;
            switch (map.Type)
            {
                case MapResourceType.Tree:
                    go = GameObject.Instantiate(trees[map.Index]);
                    go.transform.position = new Vector3(map.Position.X, map.Position.Y, 0);
                    go.transform.parent = treeMap;
                    break;
                case MapResourceType.Stone:
                    go = GameObject.Instantiate(stones[map.Index]);
                    go.transform.position = new Vector3(map.Position.X, map.Position.Y, 0);
                    go.transform.parent = stoneMap;
                    break;
                case MapResourceType.Grass:
                    go = GameObject.Instantiate(grass[map.Index]);
                    go.transform.position = new Vector3(map.Position.X, map.Position.Y, 0);
                    go.transform.parent = grassMap;
                    break;
                case MapResourceType.Collect:
                    go = GameObject.Instantiate(collects[map.Index]);
                    go.transform.position = new Vector3(map.Position.X, map.Position.Y, 0);
                    go.transform.parent = collectMap;

                    CollectInfo collectInfo = go.GetComponent<CollectInfo>(); // 采集的信息
                    //Debug.Log(map);
                    //Debug.Log(map as MapCollectResource);
                    collectInfo.Resource = map as MapCollectResource;
                    break;
                default:
                    break;
            }
        }
    }

    public MyList<MapResource> GetMapResource()
    {
        return mapResource;

    }

    public void UpdateResource(MapResource resource)
    {
        if (resource is MapCollectResource r)
        {
            if (!r.CanRegeneration && r.IsDestruction)  // 是否已经是销毁状态且不能再生
            {
                // 删除
                foreach (var a in mapResource)
                {
                    if (a.Id == resource.Id)
                    {
                        mapResource.Remove(a);
                        break;
                    }
                }
                // 删除对象
                foreach (var t in mapCollectInfo)
                {
                    if (t.GetId() == resource.Id)
                    {
                        //Debug.Log("进来删除了");
                        Destroy(t.gameObject);
                        break;
                    }
                }
            }
            else
            {
                // 修改
                //foreach (var a in mapResource)
                //{
                //    if (a.Id == resource.Id)
                //    {
                //        mapResource.Remove(a);
                //        break;
                //    }
                //}
                // 修改对象
                foreach (CollectInfo t in mapCollectInfo)
                {
                    if (t.GetId() == resource.Id)
                    {
                        Regeneration re = t.GetComponent<Regeneration>();
                        re.DestroyTargetCollect(); // 表示被采集了
                        break;
                    }
                }
            }
        }
    }
}
