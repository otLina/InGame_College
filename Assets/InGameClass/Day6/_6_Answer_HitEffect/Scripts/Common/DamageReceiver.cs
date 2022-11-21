using DG.Tweening;
using System;
using UnityEngine;

namespace HitEffectAnswer
{
    public class DamageReceiver : MonoBehaviour, IDamagable
    {
        #region Variables Damage

        [SerializeField]
        private float _knockBackPower = 3.0f;

        [SerializeField]
        private float _invincibleTime = 2.0f;

        private bool _isInvincible;

        [SerializeField]
        private GameObject _damageEffectPrefab;

        private GameObject _damageEffect;

        #endregion

        #region Callback

        public event Action OnDamageReceived;

        #endregion

        void Start()
        {
            // ダメージエフェクトをインスタンス化
            if (_damageEffectPrefab != null)
            {
                _damageEffect = Instantiate(_damageEffectPrefab);
                _damageEffect.SetActive(false);
            }
        }

        /// <summary>
        /// 敵からの攻撃を受けた時の処理
        /// </summary>
        /// <param name="other"></param>

        public void OnDamage(int damageValue, Vector3 hitPos)
        {
            // 無敵中の場合は攻撃を受けない
            if (_isInvincible)
            {
                Debug.Log("Invincible!!!");
                return;
            }
            else
            {
                Debug.Log($"Receiver: Damage={damageValue} hitPos={hitPos}!!!");
            }

            // 衝突位置にパーティクルを発生
            if (_damageEffect != null)
            {
                _damageEffect.transform.position = hitPos;
                _damageEffect.SetActive(true);
            }

            // 一定時間無敵
            var seq = DOTween.Sequence()
                .AppendInterval(_invincibleTime) // 無敵時間
                .OnStart(() => _isInvincible = true)
                .OnComplete(() => _isInvincible = false);

            // 各種演出
            var seqE = DOTween.Sequence();

            // 敵のみ振動するように、振動処理はコールバックに移動

            // 後方に移動
            Vector3 knockBackVector = GetAngleVec(hitPos, transform.position);
            seqE.Append(transform
                            .DOMove(knockBackVector * _knockBackPower, 0.5f)
                            .SetRelative());

            // コールバック処理
            OnDamageReceived?.Invoke();
        }

        /// <summary>
        /// 吹き飛ばす方向を決める(Y軸を無視する)
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <returns></returns>
        private Vector3 GetAngleVec(Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;

            return Vector3.Normalize(to - from);
        }
    }
}
