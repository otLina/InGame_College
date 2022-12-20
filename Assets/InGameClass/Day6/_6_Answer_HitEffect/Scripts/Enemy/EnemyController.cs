using DG.Tweening;
using UnityEngine;

namespace HitEffectAnswer
{
    public class EnemyController : MonoBehaviour
    {
        #region for Damage Callbacks

        [SerializeField]
        private DamageReceiver _damageReceiver;

        [SerializeField]
        private DamageSender _damageSender;

        [SerializeField]
        private Renderer _enemyBodyRederer;

        [SerializeField]
        private Color _damageColor = Color.red;

        private Color _originalColor;

        #endregion

        private void Start()
        {
            if (_enemyBodyRederer != null)
            {
                _originalColor = _enemyBodyRederer.material.color;
            }
        }

        private void OnEnable()
        {
            _damageReceiver.OnDamageReceived += OnDamageReceived;
            _damageSender.OnDamageSended += OnDamageSended;
        }

        private void OnDisable()
        {
            _damageSender.OnDamageSended -= OnDamageSended;
            _damageReceiver.OnDamageReceived -= OnDamageReceived;

        }

        private void OnDamageReceived()
        {
            // 各種演出
            var seqE = DOTween.Sequence();

            //            seqE.SetDelay(EffectConst.HitStopTime);
            seqE.Append(  // 振動処理
                transform.DOShakePosition(
                            EffectConst.HitStopTime,
                            0.15f,
                            25,
                            fadeOut: false));

            if (_enemyBodyRederer != null)
            {
                seqE
                    .OnStart(
                        () => _enemyBodyRederer.material.color = _damageColor)
                    .Insert(
                        0,
                        _enemyBodyRederer.material.DOColor(_originalColor, 0.3f));
            }
        }

        private void OnDamageSended()
        {
            // 未実装
        }
    }
}
