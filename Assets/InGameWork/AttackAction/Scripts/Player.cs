using UnityEngine;

namespace AttackAction
{
    public class Player : MonoBehaviour
    {
        private static readonly int PARAMETER_IS_MOVE = Animator.StringToHash("IsMove");

        private static readonly int PARAMETER_TRIGGER_ATTACK = Animator.StringToHash("Attack");

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private PlayerAnimationEventHandler _animHandler;

        [SerializeField]
        private Collider _attackCllider;

        [SerializeField]
        private CameraManager _cameraMgr;

        /// <summary>プレイヤーの移動速度</summary>
        [SerializeField]
        private float _speed = 3f;

        /// <summary>プレイヤーの移動に伴う向き回転速度</summary>
        [SerializeField]
        private float _angularVelocity = 360f;

        /// <summary>カメラの回転速度</summary>
        [SerializeField]
        private float _cameraAngularVelocity = 120f;

        private Transform _camera;

        private Vector2 _moveVector;

        /// <summary>攻撃中か否かの判定</summary>
        private bool IsAttacking => _attackCllider.enabled == true;

        private void Awake()
        {
            _camera = Camera.main.transform;

            // AnimationEventに処理を登録
            // AttackBeginのタイミングで攻撃判定をON
            _animHandler.OnAttackBeginEvent = () => _attackCllider.enabled = true;
            // AttackEndのタイミングで攻撃判定をOFF
            _animHandler.OnAttackEndEvent = () => _attackCllider.enabled = false;
        }

        private void Update()
        {
            UpdateRollCamera();

            if (IsAttacking)
            {
                // 攻撃中は攻撃モーション、移動処理を行わないためにここで処理を抜ける
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // エンターキー押下時、攻撃モーションを再生
                _animator.SetTrigger(PARAMETER_TRIGGER_ATTACK);
            }

            // 移動処理
            UpdateMove();
        }

        /// <summary>
        /// キーボード入力で移動とアニメーション再生をする処理
        /// </summary>
        private void UpdateMove()
        {
            // 移動操作と移動処理
            var moveVector = GetMoveInput();
            Move(moveVector);

            // 前フレームと今フレームの入力ベクトルを比較して、Animatorのパラメータを更新
            if (moveVector != Vector2.zero && _moveVector == Vector2.zero)
            {
                _animator.SetBool(PARAMETER_IS_MOVE, true);
            }
            else if (moveVector == Vector2.zero && _moveVector != Vector2.zero)
            {
                _animator.SetBool(PARAMETER_IS_MOVE, false);
            }

            _moveVector = moveVector;
        }

        /// <summary>
        /// キーボード入力から、進行ベクトルを取得する
        /// </summary>
        private Vector2 GetMoveInput()
        {
            var vector = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                vector += Vector2.up;
            }
            if (Input.GetKey(KeyCode.D))
            {
                vector += Vector2.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                vector += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                vector += Vector2.left;
            }
            vector.Normalize();
            return vector;
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        /// <param name="vector">入力ベクトル</param>
        private void Move(Vector2 vector)
        {
            if (vector == Vector2.zero)
            {
                // 入力が0なら何もせず処理を抜ける
                return;
            }

            // 座標の移動
            var distanceDelta = _speed * Time.deltaTime;
            var offset = Quaternion.Euler(0f, _camera.eulerAngles.y, 0f) * new Vector3(vector.x, 0f, vector.y) * distanceDelta;
            transform.position += offset;

            // 移動先に向けて徐々に角度を変化させる処理
            var eulerAngles = transform.eulerAngles;
            var moveAngles = Quaternion.LookRotation(offset).eulerAngles;
            var angleDiff = Mathf.DeltaAngle(eulerAngles.y, moveAngles.y);
            var angularVelocity = Mathf.Lerp(0f, _angularVelocity, Mathf.Clamp01(Mathf.Abs(angleDiff) / 90f));
            eulerAngles.y += Mathf.Min(angularVelocity * Time.deltaTime, Mathf.Abs(angleDiff)) * Mathf.Sign(angleDiff);
            transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// キーボード入力でカメラを回転させる処理
        /// </summary>
        private void UpdateRollCamera()
        {
            var angles = _cameraMgr.Param.angles;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                angles.y += _cameraAngularVelocity * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                angles.y -= _cameraAngularVelocity * Time.deltaTime;
            }
            _cameraMgr.Param.angles = angles;
        }
    }
}
