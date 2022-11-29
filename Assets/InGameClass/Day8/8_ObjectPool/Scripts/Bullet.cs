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
        //SetActive(false)���ꂽ��ɂ�rigidbody���v�Z�𑱂��Ă��܂�Ȃ��悤��
        _rigidbody.velocity = default;
        _rigidbody.angularVelocity = default;
    }

    /// <summary>
    /// ��莞�Ԍ�ɔ�A�N�e�B�u�ɂȂ�
    /// </summary>
    /// <param name="t"></param>
    public void DelayDeactivation(float t)
    {
        DOVirtual.DelayedCall(
                t,      //�x������
                () =>   //�x������
                {
                    gameObject.SetActive(false);
                },
                false   //Unity��timescale�ˑ��@
            );
    }
}
