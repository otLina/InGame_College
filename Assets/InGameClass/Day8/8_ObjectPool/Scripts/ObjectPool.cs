using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// オブジェクトプールのインスタンス
    /// </summary>
    public static ObjectPool Instance;

    /// <summary>
    /// プールされたオブジェクト
    /// </summary>
    private List<GameObject> pooledObjects;

    /// <summary>
    /// プール対象のオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject objectToPool;

    /// <summary>
    /// プール数
    /// </summary>
    [SerializeField]
    private int amountToPool;

    /// <summary>
    /// オブジェクトプーリングのインスタンスを格納
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start()時にオブジェクトプールし、非Activeにします
    /// </summary>
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject go;
        for (int i = 0; i < amountToPool; i++)
        {
            go = Instantiate(objectToPool);
            go.SetActive(false);
            pooledObjects.Add(go);
        }
    }

    /// <summary>
    /// オブジェクトプールからゲームオブジェクトを返却
    /// 非アクティブのゲームオブジェクトを探して返却
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObejct()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
