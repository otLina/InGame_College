using DG.Tweening;
using UnityEngine;

namespace HitEffect
{
    public class EnemyController : MonoBehaviour
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

        #region for Damage Callbacks

        [SerializeField]
        private DamageReceiver damageReceiver;

        [SerializeField]
        private DamageSender damageSender;

        [SerializeField]
        private Renderer enemyBodyRenderer;

        [SerializeField]
        private Color damageColor = Color.red;

        private Color originalColor;

        #endregion


        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            if(enemyBodyRenderer != null)
            {
                originalColor = enemyBodyRenderer.material.color;
            }
        }

        private void OnEnable()
        {
            damageReceiver.OnDamageReceived += OnDamageReceived;
            damageSender.OnDamageSended += OnDamageSended;
        }

        private void OnDisable()
        {
            damageSender.OnDamageSended -= OnDamageSended;
            damageReceiver.OnDamageReceived -= OnDamageReceived;
        }

        private void OnDamageReceived()
        {
            var seqE = DOTween.Sequence();

            //振動処理
            seqE.Append(
               transform.DOShakePosition(
                   EffectConst.HitStopTime,
                   0.15f,
                   25,
                   fadeOut: false));
            
            //色変更
            if(enemyBodyRenderer != null)
            {
                seqE
                    .OnStart(
                        () => enemyBodyRenderer.material.color = damageColor)
                    .Insert(
                        0,
                        enemyBodyRenderer.material.DOColor(originalColor, 0.5f));
            }
        }

        private void OnDamageSended()
        { 

        }
    }
}
