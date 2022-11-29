using UnityEngine;
using UnityEngine.AI;

namespace FlyweightExample
{

    public class EnemyAI : MonoBehaviour
    {
        private EnemyParam _param;
        public EnemyParam Param { 
            set { _param = value; }
            get { return _param; }
        }

        private int _currentHp;

        // Stateが必要としている変数
        NavMeshAgent agent;     // NPCのNavMeshAgentコンポーネント
        private Transform _player;// プレイヤーのTransformコンポーネント
        public Transform Player
        {
            set { _player = value; }
        }

        public Renderer bodyRenderer;

        State currentState;     // 現在の状態

        // プロパティIDを事前計算
        private static int colorId = Shader.PropertyToID("_Color");

        void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();

            // 体力の設定
            _currentHp = _param.MaxHp;

            // カラーの設定
            // bodyRenderer.material.SetColor(colorId, _param.BodyColor);
            // ↑のように記述するとマテリアルが複製されるので、
            // マテリアルプロパティブロックを使って複製を防ぐ
            MaterialPropertyBlock materialPropertyBlock = new();
            materialPropertyBlock.SetColor(colorId, _param.BodyColor);
            bodyRenderer.SetPropertyBlock(materialPropertyBlock);

            // 最初の状態
            currentState = new Idle(this.gameObject, agent, _player);
        }

        void Update()
        {
            // 現在の状態を実行。戻り値は次の状態
            currentState = currentState.Process();
        }
    }
}
