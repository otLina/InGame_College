using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Attack2
{
    public class DamageReceiver : MonoBehaviour, IDamageable
    {
        #region Variables Damage
        [SerializeField]
        private float _knockBackPower = 3.0f;

        [SerializeField]
        private float _invincibleTime = 2.0f;

        private bool _isInvincible;

        [SerializeField]
        private bool _useRigidbody;

        private Rigidbody _rigidbody;
        #endregion

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 敵からの攻撃を受けた時の処理
        /// </summary>
        /// <param name="damageValue"></param>
        /// <param name="hitPos"></param>
        public void OnDamage(int damageValue, Vector3 hitPos)
        {
            if(_isInvincible)
            {
                //無敵状態の時、ダメージを受けない
                
            } else
            {

            }

            Debug.Log($"Receiver: Damage = {damageValue}, hitPos = {hitPos}!!");

            //一定時間無敵
            var seq = DOTween.Sequence()
                .AppendInterval(_invincibleTime) //無敵時間
                .OnStart(() => _isInvincible = true)
                .OnComplete(() => _isInvincible = false);


            //後方に移動
            Vector3 knockBackVector = GetAngleVec(hitPos, transform.position);
            if (_useRigidbody)
            {
                _rigidbody.AddForce(knockBackVector * _knockBackPower, ForceMode.Impulse);
            } else
            {
                transform
                    .DOMove(knockBackVector * _knockBackPower, 0.5f)
                    .SetRelative();
            }
        }

        /// <summary>
        /// 吹き飛ばす方向を決める
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private Vector3 GetAngleVec(Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;

            return Vector3.Normalize(to - from);
        }
    }
}
