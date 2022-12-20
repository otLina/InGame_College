using UnityEngine.EventSystems;
using UnityEngine;

namespace HitEffect
{
    public class PlayerAttackBehaviour : StateMachineBehaviour
    {
        private PlayerController playerController;
        private float defaultSpeed;

        // OnStateEnterは遷移が始まり、ステートマシンがこの状態を
        // 評価し始めると呼び出されます。
        override public void OnStateEnter(
                Animator animator, AnimatorStateInfo stateInfo,
                int layerIndex)
        {
            // PlayerConttollerへの参照を取得
            playerController = animator.GetComponentInParent<PlayerController>();
            // 現在のスピードを保存
            defaultSpeed = playerController.MoveSpeed;
            // スピードを0に設定
            playerController.MoveSpeed = 0;

            // 武器のコライダーを有効化
            ExecuteEvents.Execute<IWeaponControl>(
              target: playerController.WeaponGameObject,
              eventData: null,
              functor: (reciever, eventData) => reciever.EnableWeaponCollider()
            );
        }

        // OnStateUpdateは、OnStateEnterとOnStateExitの
        // コールバック間の各Updateフレームで呼び出されます。
        override public void OnStateUpdate(
                Animator animator,AnimatorStateInfo stateInfo,
                int layerIndex)
        {

        }

        // OnStateExitは、遷移が終了し、ステートマシンが
        // この状態の評価を終了したときに呼び出されます。
        override public void OnStateExit(
                Animator animator, AnimatorStateInfo stateInfo,
                int layerIndex)
        {
            // スピードを元に戻す
            playerController.MoveSpeed = defaultSpeed;

            // 武器のコライダーを無効化
            ExecuteEvents.Execute<IWeaponControl>(
              target: playerController.WeaponGameObject,
              eventData: null,
              functor: (reciever, eventData) => reciever.DisableWeaponCollider()
            );
        }
    }

}
