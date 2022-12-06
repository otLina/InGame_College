using UnityEngine;
using UnityEngine.EventSystems;

namespace AttackAction
{
    /// <summary>
    /// 攻撃をヒットさせたことをIDamagableに通知するコンポーネント
    /// </summary>
    public class DamageSender : MonoBehaviour
    {
        [SerializeField]
        private string _effectiveTag;

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.CompareTag(_effectiveTag))
            {
                // 当たっていなければ何もせず処理を抜ける
                return;
            }

            // 攻撃がヒットした座標
            var hitPos = collider.ClosestPoint(transform.position);

            // ダメージ処理の呼び出し
            ExecuteEvents.Execute<IDamagable>(
                target: collider.gameObject,
                eventData: null,
                functor: (reciever, eventData) => reciever.OnDamage(hitPos, transform.position)
            );
        }
    }
}
