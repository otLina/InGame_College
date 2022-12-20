using UnityEngine;
using UnityEngine.AI;

namespace StateAnswer
{
    public class State
    {
        /// <summary>
        /// "STATE": 敵キャラが取りうる状態
        /// </summary>
        public enum STATE
        {
            IDLE, PATROL, PURSUE, ATTACK, RUNAWAY
        };

        // "EVENT": STATE内イベント
        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };

        public STATE name;            // "状態"を格納
        protected EVENT stage;        // "イベント"を格納
        protected GameObject enemy;   // 敵キャラのゲームオブジェクト
        protected Transform player;   // プレイヤーのtransform
        protected State nextState;    // 次の状態(STATEではない)
        protected NavMeshAgent agent; // 敵キャラのNavMeshAgentコンポーネント

        readonly float visDist = 10.0f;  // プレイヤーを認識する距離(単位:m)
        readonly float visAngle = 30.0f; // プレイヤーを認識する角度(単位:度)
        readonly float shootDist = 7.0f; // プレイヤーを攻撃する距離(単位:m)

        /// <summary>
        // コンストラクタ
        /// </summary>
        /// <param name="_enemy"></param>
        /// <param name="_agent"></param>
        /// <param name="_player"></param>
        public State(GameObject _enemy, NavMeshAgent _agent, Transform _player)
        {
            enemy = _enemy;
            agent = _agent;
            stage = EVENT.ENTER;
            player = _player;
        }

        // 状態におけるイベント処理
        /// <summary>
        /// ある状態になると最初に実行
        /// </summary>
        public virtual void Enter() { stage = EVENT.UPDATE; }
        /// <summary>
        /// UPDATE状態。以後、ステートが変わるまでUPDATE
        /// </summary>
        public virtual void Update() { stage = EVENT.UPDATE; }
        /// <summary>
        /// EXIT時に呼び出される。ステートが変化する時の後処理をする
        /// </summary>
        public virtual void Exit() { stage = EVENT.EXIT; }

        // 外部から呼び出して、各ステージで状態を進行させる
        public State Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState; // 次のStateを返却
            }
            return this; // 現在のStateを返却
        }

        #region ステート間共通メソッド
        /// <summary>
        /// 敵キャラの前方にプレイヤーがいるか?
        /// </summary>
        /// <returns></returns>
        public bool CanSeePlayer()
        {
            // 敵キャラからプレイヤーへのベクトルを計算
            Vector3 direction = player.position - enemy.transform.position;
            // 視角を計算
            float angle = Vector3.Angle(direction, enemy.transform.forward);

            // プレイヤーが敵キャラの近くにいて、かつ見える範囲にいる場合
            if (direction.magnitude < visDist && angle < visAngle)
            {
                return true;// 敵キャラはプレイヤーを見つけた
            }
            return false;   // 敵キャラはプレイヤーを見つけられなかった
        }

        public bool IsPlayerBehind()
        {
            // プレイヤーから敵キャラへのベクトルを計算
            Vector3 direction = enemy.transform.position - player.position;
            // 視角を計算
            float angle = Vector3.Angle(direction, enemy.transform.forward);

            // プレイヤーが敵キャラの近くにいて、かつ見える範囲にいる場合
            // directionの値はプレイヤー基準のため、敵キャラから見ると背後を
            // 取られている状態
            if (direction.magnitude < 2 && angle < 30)
            {
                return true; // プレイヤーは敵キャラの背後にいる
            }
            return false;    // プレイヤーは敵キャラの背後にいない
        }

        public bool CanAttackPlayer()
        {
            // 敵キャラからプレイヤーへのベクトルを計算
            Vector3 direction = player.position - enemy.transform.position;
            if (direction.magnitude < shootDist)
            {
                return true; // 敵キャラが攻撃可能な距離まで近づいている
            }
            return false; // 敵キャラが攻撃できるほど近くにいない
        }
        #endregion
    }

    // Idle状態
    public class Idle : State
    {
        public Idle(GameObject _enemy, NavMeshAgent _agent, Transform _player)
                    : base(_enemy, _agent, _player)
        {
            name = STATE.IDLE; // 現在の状態を設定
        }

        public override void Enter()
        {
            base.Enter(); // ステージを"UPDATE"にする
        }
        public override void Update()
        {
            if (CanSeePlayer())
            {
                // 本ステータスのステージがEXITになり、
                // nextStageで設定したステータスになる
                nextState = new Pursue(enemy, agent, player);
                stage = EVENT.EXIT; 
            }
            // 10%の確率でIDOL状態からPatrol状態に遷移
            else if (Random.Range(0, 100) < 10)
            {
                nextState = new Patrol(enemy, agent, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    // Patrol状態
    public class Patrol : State
    {
        int currentIndex = -1;
        public Patrol(GameObject _enemy, NavMeshAgent _agent, Transform _player)
                    : base(_enemy, _agent, _player)
        {
            name = STATE.PATROL;     // 現在の状態を設定
            agent.speed = 2;         // 巡回時の速度(2m/s)
            agent.isStopped = false; // パス検索の開始/停止制御(false=開始)
        }

        public override void Enter()
        {
            var lastDist = Mathf.Infinity; // 敵キャラとの距離

            // 各ウェイポイントをループして、敵キャラと各ウェイポイント間の
            // 距離を計算し、最も近いウェイポイントを算出する
            for (int i = 0; i < WayPointManager.Singleton.Waypoints.Count; i++)
            {
                var thisWP = WayPointManager.Singleton.Waypoints[i];
                var distance = Vector3.Distance(
                                enemy.transform.position, thisWP.transform.position);
                if (distance < lastDist)
                {
                    // Updateではiに1を加えてから次の目的地(Destination)
                    // を設定するため1を引く
                    currentIndex = i - 1;
                    lastDist = distance;
                }
            }
            base.Enter();
        }

        public override void Update()
        {
            // ウェイポイントに到達しているか確認
            if (agent.remainingDistance < 1)
            {
                // 次のウェイポイントに移動
                if (currentIndex >= WayPointManager.Singleton.Waypoints.Count - 1)
                    currentIndex = 0;
                else
                    currentIndex++;

                // 目的地の設定
                var newWayPoint = WayPointManager.Singleton.Waypoints[currentIndex];
                agent.SetDestination(newWayPoint.transform.position);
            }

            // プレイヤーを見つけたらPursue(追跡)状態に遷移
            if (CanSeePlayer())
            {
                nextState = new Pursue(enemy, agent, player);
                stage = EVENT.EXIT;
            }
            // プレイヤーに背後を取られたらRunAway(逃走)状態に遷移
            else if (IsPlayerBehind())
            {
                nextState = new RunAway(enemy, agent,player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Pursue : State
    {
        public Pursue(GameObject _enemy, NavMeshAgent _agent, Transform _player)
                    : base(_enemy, _agent, _player)
        {
            name = STATE.PURSUE;     // 現在の状態を設定
            agent.speed = 5;         // 敵キャラが走っているように速度を設定
            agent.isStopped = false; // パス検索の開始/停止制御(false=開始)
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            //敵キャラの到達目標を設定するが、navmeshの処理が行われていない
            //可能性があるため、エージェントがまだ経路を持っているか
            //どうかを確認
            agent.SetDestination(player.position);
            if (agent.hasPath)
            {
                // 敵キャラがプレイヤーを攻撃できる場合、Attack(攻撃)状態に遷移
                if (CanAttackPlayer())
                {
                    nextState = new Attack(enemy, agent, player);
                    stage = EVENT.EXIT;
                }
                // 敵キャラからプレイヤーの姿が見えない場合は、Patrol状態に遷移
                else if (!CanSeePlayer())
                {
                    nextState = new Patrol(enemy, agent, player);
                    stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Attack : State
    {
        // 敵キャラがプレイヤーに向かって回転する速度を設定(単位:度/秒)
        float rotationSpeed = 2.0f; 
        public Attack(GameObject _enemy, NavMeshAgent _agent, Transform _player)
                    : base(_enemy, _agent, _player)
        {
            name = STATE.ATTACK; // 現在の状態を設定
        }

        public override void Enter()
        {
            agent.isStopped = true; // 攻撃するため停止
            base.Enter();
        }

        public override void Update()
        {
            // プレイヤーに対する方向と角度を計算
            // 敵キャラからプレイヤーへのベクトルを求める
            Vector3 direction = player.position - enemy.transform.position;
            // 視角を計算
            float angle = Vector3.Angle(direction, enemy.transform.forward);
            direction.y = 0; // キャラクターの傾きを防止

            // 敵キャラを回転させ、常に攻撃しているプレイヤーの方を向くようにする
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotationSpeed);

            if (!CanAttackPlayer())
            {
                // 敵キャラがプレイヤーを攻撃できない場合、
                // Idle状態に遷移
                nextState = new Idle(enemy, agent, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class RunAway : State
    {
        GameObject SafePoint; // Store object used for safe location.

        public RunAway(GameObject _enemy, NavMeshAgent _agent, Transform _player)
                    : base(_enemy, _agent, _player)
        {
            name = STATE.RUNAWAY; // 状態名の設定
            // "SafePoint"タグが付いているオブジェクトを検索
            SafePoint = GameObject.FindGameObjectWithTag("SafePoint");
        }

        public override void Enter()
        {
            agent.isStopped = false; // パス検索の開始/停止制御(false=開始)
            // プレイヤーに向かって走るときよりも、
            // 少しスピードを遅くする。
            agent.speed = 6;
            // 目的地をsafeLocationに設定
            agent.SetDestination(SafePoint.transform.position);
            base.Enter();
        }

        public override void Update()
        {
            // safeLocationに到達したらIdol状態に遷移
            if (agent.remainingDistance < 1)
            {
                nextState = new Idle(enemy, agent, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
