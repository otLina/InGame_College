using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack2
{
    public class DamageSender : MonoBehaviour
    {
        //�_���[�W�̒l
        [SerializeField]
        private int _damage;

        [SerializeField]
        private string _effectiveTag;

        private void OnTriggerEnter(Collider other)
        {
            //�����̕��킪�G�ɓ���������
            if (other.gameObject.CompareTag(_effectiveTag))
            {
                //�Փˈʒu�����o
                Vector3 hitPos = other.ClosestPointOnBounds(transform.position);

                //�_���[�W�����̌Ăяo��
                ExecuteEvents.Execute<IDamageable>(
                        target: other.gameObject,
                        eventData: null,
                        functor: (receiver, eventData) => receiver.OnDamage(_damage, hitPos)
                    );
            }
        }
    }
}

