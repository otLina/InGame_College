using UnityEngine;
using DG.Tweening;

namespace AttackAction
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        /// <summary>AnimatorのDamageトリガー名</summary>
        private static readonly int PARAMETER_DAMAGE = Animator.StringToHash("Damage");

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private int _hp = 3;

        public bool IsDead => _hp <= 0;

        /// <summary>
        /// 被ダメージ時の処理
        /// IDamagableで継承した機能。DamageSenderから呼び出されます
        /// </summary>
        /// <param name="hitPos">攻撃がヒットした位置</param>
        /// <param name="attackPos">攻撃者の位置</param>
        public void OnDamage(Vector3 hitPos, Vector3 attackPos)
        {
            if (IsDead)
            {
                return;
            }

            _hp--;

            // ダメージモーションを再生
            _animator.SetTrigger(PARAMETER_DAMAGE);

            if (_hp > 0)
            {
                // まだ倒していない時
                DamageAnimation(attackPos);
            }
            else
            {
                // 撃破時
                DeadAnimation();
            }
        }

        /// <summary>
        /// ダメージアニメーション
        /// </summary>
        /// <param name="attackPos">攻撃者の位置</param>
        private void DamageAnimation(Vector3 attackPos)
        {
            // 攻撃者の方向を向く
            transform.LookAt(attackPos);

            // 吹っ飛ばすアニメーション
            // 攻撃者の位置と攻撃がヒットした位置を使って吹っ飛び方向のベクトルを作る
            var knockBackVector = transform.position - attackPos;
            // 上下の位置変化が起きないように（浮いたり沈んだりしないように）y座標の差分を0にする
            knockBackVector.y = 0f;
            // ベクトルの長さを1にする
            knockBackVector.Normalize();
            // 吹っ飛び距離を2mにする
            knockBackVector *= 2f;
            transform.DOMove(transform.position + knockBackVector, 0.3f);
        }

        /// <summary>
        /// 撃破アニメーション
        /// </summary>
        private void DeadAnimation()
        {
            // 拡縮してオブジェクトを非アクティブにする
            DOTween.Sequence()
                .Append(_animator.transform.DOScale(1.5f, 0.15f))
                .Append(_animator.transform.DOScale(0f, 0.3f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
