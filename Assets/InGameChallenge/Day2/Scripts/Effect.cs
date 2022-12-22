using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Vector3 slimePos;

    [SerializeField]
    private GameObject slimeGroup;
    [SerializeField]
    private float _lifeTime = 1f;

    private float _lifeTimeCount;

    private float offset = 20f;

    // Start is called before the first frame update
    void Update()
    {
        slimePos = slimeGroup.GetComponent<RectTransform>().localPosition;
        transform.position = slimePos + new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0);
    }

    public void Attacked()
    {
        _lifeTimeCount = _lifeTime;
        gameObject.SetActive(true);
    }
}
