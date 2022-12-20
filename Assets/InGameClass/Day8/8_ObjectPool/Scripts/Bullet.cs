using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _power = 5.0f;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody.AddForce(transform.up * _power, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        //SetActive(false)された後にもrigidbodyが計算を続けてしまわないように
        _rigidbody.velocity = default;
        _rigidbody.angularVelocity = default;
    }

    /// <summary>
    /// 一定時間後に非アクティブになる
    /// </summary>
    /// <param name="t"></param>
    public void DelayDeactivation(float t)
    {
        DOVirtual.DelayedCall(
                t,      //遅延時間
                () =>   //遅延処理
                {
                    gameObject.SetActive(false);
                },
                false   //Unityのtimescale依存　
            );
    }
}
