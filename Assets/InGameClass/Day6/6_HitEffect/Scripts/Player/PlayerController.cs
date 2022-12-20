using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HitEffect
{
    public class PlayerController : MonoBehaviour, IDamagable
    {
        #region Definitions
        private static readonly int IS_MOVE_HASH =
                        Animator.StringToHash("IsMove");
        private static readonly int ATTACK_HASH = 
                        Animator.StringToHash("Attack");

        private enum CameraModeType
        {
            Default,
            LookItem,
            Aim,
        }

        #endregion // Definitions

        #region Variables Move
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Transform _camera;

        [SerializeField]
        private float _moveSpeed = 5f;
        public float MoveSpeed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }

        [SerializeField]
        private float _rollSpeed = 360f;
        #endregion //  Variables Move

        #region Variables Camera
        [SerializeField]
        private CameraManager _cameraMgr;

        private CameraModeType _cameraModeType;

        private CameraManager.Parameter _defaultCamParam;

        [SerializeField]
        private CameraManager.Parameter _itemCamParam;

        [SerializeField]
        private CameraManager.Parameter _aimCamParam;

        private Sequence _cameraSeq;

        [SerializeField]
        private Image _cursor;

        [SerializeField]
        private bool _useMouseRoll;
        [SerializeField]
        private bool isCursurLock = true;

        #endregion // Variables Camera

        #region Variables Weapon
        [SerializeField]

        private GameObject _weaponGameObject;
        public GameObject WeaponGameObject => _weaponGameObject;

        #endregion

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
        #endregion

        /// <summary>
        /// Awake処理
        /// </summary>
        private void Awake()
        {
            _defaultCamParam = _cameraMgr.Param.Clone();
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

        #region for Hit Effect
        private void OnDamageReceived()
        {
            //カメラを揺らす
            float duration = EffectConst.HitStopTime;
            Vector3 strength = new Vector3(1f, 1f, 0);
            int vibrato = 20;
            _cameraMgr.Shake(duration, strength, vibrato);
        }

        private void OnDamageSended()
        {
            //プレイヤーのヒットストップ

            //アニメーションを止める
            _animator.speed = 0f;

            var seq = DOTween.Sequence();
            seq.SetDelay(EffectConst.HitStopTime);
            //アニメーションを再開
            seq.AppendCallback(() => _animator.speed = 1f);
        }
        #endregion

        /// <summary>
        /// Start処理
        /// </summary>
        private void Start()
        {
            // フレームレートを60fpsに設定
            Application.targetFrameRate = 60;

            // カーソルを非表示にするか?
            if (isCursurLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // カメラの設定をデフォルトに設定
            _defaultCamParam = _cameraMgr.Param.Clone();

            _rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Update処理
        /// </summary>
        private void Update()
        {
            ControlMove();
            ControlAttack();

            ControlCamera();
        }

        /// <summary>
        /// カメラの向きに合わせて移動
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMoveVector()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector3 moveVector = Vector3.zero;
            moveVector.z = vertical;
            moveVector.x = horizontal;

            // カメラの向きに合わせてキャラの移動方向を決定
            Quaternion cameraRotate;
            cameraRotate = Quaternion.Euler(
                        0f,
                        _camera.eulerAngles.y,
                        0f);
            return cameraRotate * moveVector.normalized;
        }

        /// <summary>
        /// キャラクターの移動制御
        /// </summary>
        private void ControlMove()
        {
            var moveVector = GetMoveVector();
            var isMove = moveVector != Vector3.zero;

            if (_animator != null)
            {
                _animator.SetBool(IS_MOVE_HASH, isMove);
            }

            if (isMove)
            {
                transform.position +=
                    moveVector * Time.deltaTime * _moveSpeed;

                // エイムモード時はカメラの向きにプレイヤーの向きを合わせるため、処理しない
                if (_cameraModeType != CameraModeType.Aim)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveVector.x, 0f, moveVector.z));
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rollSpeed);
                }

            }
        }

        /// <summary>
        /// キャラクターの攻撃制御
        /// </summary>
        private void ControlAttack()
        {
            // 攻撃ボタンを押した
            if (_animator != null && Input.GetButtonDown("Fire1"))
            {
                _animator.SetTrigger(ATTACK_HASH);
            }
        }

        #region Methods Camera
        private CameraManager.Parameter GetCameraParameter(CameraModeType type)
        {
            switch (type)
            {
                case CameraModeType.Default:
                    return _defaultCamParam;
                case CameraModeType.LookItem:
                    return _itemCamParam;
                case CameraModeType.Aim:
                    return _aimCamParam;
                default:
                    return null;
            }
        }

        private void SwitchCamera(CameraModeType type)
        {
            float duration = 2f;
            // エイムモード切り替え時は素早くカメラを遷移させる
            if (type == CameraModeType.Aim || _cameraModeType == CameraModeType.Aim)
            {
                duration = 0.3f;
            }

            switch (type)
            {
                case CameraModeType.Default:
                    _defaultCamParam.position = _defaultCamParam.trackTarget.position;
                    switch (_cameraModeType)
                    {
                        case CameraModeType.LookItem:
                            _defaultCamParam.angles = new Vector3(15f, transform.eulerAngles.y, 0f);
                            break;
                        default:
                            _defaultCamParam.angles = _cameraMgr.Param.angles;
                            break;
                    }
                    break;
                case CameraModeType.Aim:
                    _aimCamParam.position = _aimCamParam.trackTarget.position;
                    _aimCamParam.angles = _cameraMgr.Param.angles;
                    transform.eulerAngles = new Vector3(0f, _cameraMgr.Param.angles.y, 0f);
                    break;
            }

            _cameraModeType = type;
            _cursor.enabled = _cameraModeType == CameraModeType.Aim;

            _cameraMgr.Param.trackTarget = null;
            CameraManager.Parameter startCamParam = _cameraMgr.Param.Clone();
            CameraManager.Parameter endCamParam = GetCameraParameter(_cameraModeType);

            _cameraSeq?.Kill();
            _cameraSeq = DOTween.Sequence();
            _cameraSeq.Append(DOTween.To(() => 0f, t => CameraManager.Parameter.Lerp(startCamParam, endCamParam, t, _cameraMgr.Param), 1f, duration).SetEase(Ease.OutQuart));

            switch (_cameraModeType)
            {
                case CameraModeType.Default:
                    _cameraSeq.OnUpdate(() => CameraManager.UpdateTrackTargetBlend(_defaultCamParam));
                    break;
                case CameraModeType.Aim:
                    _cameraSeq.OnUpdate(() => _aimCamParam.position = _aimCamParam.trackTarget.position);
                    break;
            }

            _cameraSeq.AppendCallback(() => _cameraMgr.Param.trackTarget = endCamParam.trackTarget);
        }

        private void ControlCamera()
        {
            // デフォルト／エイムモード時のみマウス操作によるカメラ回転を受け付ける
            if (_useMouseRoll &&
               (_cameraModeType == CameraModeType.Default || _cameraModeType == CameraModeType.Aim) &&
               (_cameraSeq == null || !(_cameraSeq.IsActive() && _cameraSeq.IsPlaying())))
            {
                Vector3 diffAngles = new Vector3(
                    x: -Input.GetAxis("Mouse Y"),
                    y: Input.GetAxis("Mouse X")
                ) * 3f;
                _cameraMgr.Param.angles += diffAngles;

                // エイムモード時はカメラはイージング無しで追いかけさせる
                if (_cameraModeType == CameraModeType.Aim)
                {
                    _cameraMgr.Param.position = _cameraMgr.Param.trackTarget.position;
                    transform.eulerAngles = new Vector3(0f, _cameraMgr.Param.angles.y, 0f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (_cameraModeType)
                {
                    case CameraModeType.Default:
                        SwitchCamera(CameraModeType.LookItem);
                        break;
                    case CameraModeType.LookItem:
                        SwitchCamera(CameraModeType.Default);
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (_cameraModeType)
                {
                    case CameraModeType.Default:
                        SwitchCamera(CameraModeType.Aim);
                        break;
                    case CameraModeType.Aim:
                        SwitchCamera(CameraModeType.Default);
                        break;
                }
            }
        }

        #endregion // #region Methods Camera

        #region Hit Effect

        public void OnDamage(int damageValue, Vector3 hitPos)
        {
            // 無敵中の場合は攻撃を受けない
            if (_isInvincible)
            {
                Debug.Log("Invincible!!!");
                return;
            }
            else
            {
                Debug.Log($"Receiver: Damage={damageValue} hitPos={hitPos}!!!");
            }

            // 一定時間無敵
            var seq = DOTween.Sequence()
                .AppendInterval(_invincibleTime) // 無敵時間
                .OnStart(() => _isInvincible = true)
                .OnComplete(() => _isInvincible = false);

            // 後方に移動
            Vector3 knockBackVector = GetAngleVec(hitPos, transform.position);
            if (_useRigidbody)
            {
                _rigidbody.AddForce(
                            knockBackVector * _knockBackPower,
                            ForceMode.Impulse);
            }
            else
            {
                transform
                    .DOMove(knockBackVector * _knockBackPower, 0.5f)
                    .SetRelative();
            }
        }

        /// <summary>
        /// 吹き飛ばす方向を決める(Y軸を無視する)
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <returns></returns>
        private Vector3 GetAngleVec(Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;

            return Vector3.Normalize(to - from);
        }

        #endregion
    }
}
