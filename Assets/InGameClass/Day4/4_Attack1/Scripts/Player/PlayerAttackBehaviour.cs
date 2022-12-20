using UnityEngine.EventSystems;
using UnityEngine;

namespace Attack1
{
    public class PlayerAttackBehaviour : StateMachineBehaviour
    {
        private PlayerController playerController;
        
        // OnStateEnterは遷移が始まり、ステートマシンがこの状態を
        // 評価し始めると呼び出されます。
        override public void OnStateEnter(
                Animator animator, AnimatorStateInfo stateInfo,
                int layerIndex)
        {
            //PlayerControllerへの参照を取得
            playerController = animator.GetComponentInParent<PlayerController>();

            //武器のコライダーを有効化
            ExecuteEvents.Execute<IWeaponControl>(
                target: playerController.WeaponGameObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.EnableWeaponCollider()
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
            //武器のコライダーを無効化
            ExecuteEvents.Execute<IWeaponControl>(
                target: playerController.WeaponGameObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.DisableWeaponCollider()
            );
        }
    }

}
