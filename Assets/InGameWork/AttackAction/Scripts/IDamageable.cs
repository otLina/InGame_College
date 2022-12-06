using UnityEngine;
using UnityEngine.EventSystems;

namespace AttackAction
{
    /// <summary>
    /// ダメージを受けたことを検出する用のインターフェース
    /// </summary>
    public interface IDamagable : IEventSystemHandler
    {
        /// <summary>
        /// 被ダメージ時の処理
        /// </summary>
        /// <param name="hitPos">攻撃がヒットした位置</param>
        /// <param name="attackPos">攻撃者の位置</param>
        void OnDamage(Vector3 hitPos, Vector3 attackPos);
    }
}
