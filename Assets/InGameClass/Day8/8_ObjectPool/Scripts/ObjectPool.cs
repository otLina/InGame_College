using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// �I�u�W�F�N�g�v�[���̃C���X�^���X
    /// </summary>
    public static ObjectPool Instance;

    /// <summary>
    /// �v�[�����ꂽ�I�u�W�F�N�g
    /// </summary>
    private List<GameObject> pooledObjects;

    /// <summary>
    /// �v�[���Ώۂ̃I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    private GameObject objectToPool;

    /// <summary>
    /// �v�[����
    /// </summary>
    [SerializeField]
    private int amountToPool;

    /// <summary>
    /// �I�u�W�F�N�g�v�[�����O�̃C���X�^���X���i�[
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start()���ɃI�u�W�F�N�g�v�[�����A��Active�ɂ��܂�
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
    /// �I�u�W�F�N�g�v�[������Q�[���I�u�W�F�N�g��ԋp
    /// ��A�N�e�B�u�̃Q�[���I�u�W�F�N�g��T���ĕԋp
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
