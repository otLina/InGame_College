using UnityEngine;
using UnityEngine.AI;  // NavMeshのため

namespace StateExample
{
    public class EnemyAI : MonoBehaviour
    {
        // Stateが必要としている変数
        NavMeshAgent agent;     // 敵キャラのNavMeshAgentコンポーネント
        public Transform player;// プレイヤーのTransformコンポーネント

        State currentState;     // 現在の状態

        void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();

            // 最初の状態
            currentState = new Idle(this.gameObject, agent, player);
        }

        void Update()
        {
            // 現在の状態を実行。戻り値は次の状態
            currentState = currentState.Process();
        }
    }
}
