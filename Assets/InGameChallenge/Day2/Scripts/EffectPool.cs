using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public static EffectPool Instance => _instance;
    private static EffectPool _instance;

    private List<Effect> _pooledObjects;

    [SerializeField]
    private Effect _objectToPool;

    [SerializeField]
    private int _amountToPool;

    [SerializeField]
    private Transform _parent;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pooledObjects = new List<Effect>();
        for(int i = 0; i < _amountToPool; i++)
        {
            var effect = Instantiate(_objectToPool, _parent);
            effect.gameObject.SetActive(false);
            _pooledObjects.Add(effect);
        }
    }

    public Effect GetPooledObject()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            var effect = _pooledObjects[i];
            if(!effect.gameObject.activeInHierarchy)
            {
                return effect;
            }
        }
        return null;
    }
}
