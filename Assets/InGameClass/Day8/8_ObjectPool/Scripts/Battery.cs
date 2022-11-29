using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField]
    private GameObject turrent;

    public void Fire()
    {
        var go = ObjectPool.Instance.GetPooledObejct();
        if (go != null)
        {
            go.transform.position = turrent.transform.position;
            go.transform.rotation = turrent.transform.rotation;
            go.SetActive(true);

            var bullet = go.GetComponent<Bullet>();
            bullet.DelayDeactivation(3.0f);
        }
    }
}
