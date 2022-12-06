using System;
using UnityEngine;

namespace AttackAction
{
    /// <summary>
    /// プレイヤーのAnimationEventを検出して、登録された処理を実行するコンポーネント
    /// 参考: https://qiita.com/aimy-07/items/58e77d3396ded286affc
    /// </summary>
    public class PlayerAnimationEventHandler : MonoBehaviour
    {
        public Action OnAttackBeginEvent;

        public Action OnAttackEndEvent;

        private void AttackBegin()
        {
            OnAttackBeginEvent?.Invoke();
        }

        private void AttackEnd()
        {
            OnAttackEndEvent?.Invoke();
        }
    }
}
