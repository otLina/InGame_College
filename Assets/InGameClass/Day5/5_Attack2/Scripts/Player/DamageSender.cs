using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack2
{
    public class DamageSender : MonoBehaviour
    {
        //ダメージの値
        [SerializeField]
        private int _damage;

        [SerializeField]
        private string _effectiveTag;

        private void OnTriggerEnter(Collider other)
        {
            //自分の武器が敵に当たったか
            if (other.gameObject.CompareTag(_effectiveTag))
            {
                //衝突位置を検出
                Vector3 hitPos = other.ClosestPointOnBounds(transform.position);

                //ダメージ処理の呼び出し
                ExecuteEvents.Execute<IDamageable>(
                        target: other.gameObject,
                        eventData: null,
                        functor: (receiver, eventData) => receiver.OnDamage(_damage, hitPos)
                    );
            }
        }
    }
}

