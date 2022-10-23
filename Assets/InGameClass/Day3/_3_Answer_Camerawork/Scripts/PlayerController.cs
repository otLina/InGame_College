using DG.Tweening;
using UnityEngine;

namespace CameraWorkAnswer
{
    public class PlayerController : MonoBehaviour
    {
        #region Definitions
        private static readonly int IS_MOVE_HASH = Animator.StringToHash("IsMove");

        private enum CameraModeType
        {
            Default,
            LookItem,
        }
        #endregion // Definitions

        #region Variables Move
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _speed = 5f;

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

        private Sequence _cameraSeq;

        [SerializeField]
        private bool _isCursorLock = false;

        #endregion // Variables Camera

        private void Awake()
        {
            // フレームレートを60fpsに設定
            Application.targetFrameRate = 60;

            // カーソルを非表示にするか?
            if (_isCursorLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            _defaultCamParam = _cameraMgr.Param.Clone();
        }

        /// <summary>
        /// Update処理
        /// </summary>
        private void Update()
        {
            ControlMove();

            ControlCamera();
        }

        #region Methods Move
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

            //            return moveVector.normalized;
            // カメラの向きに合わせて移動方向を変える
            Quaternion cameraRotate = Quaternion.Euler(
                0f, 
                _cameraMgr.CameraTransform.eulerAngles.y,
                0f);
            return cameraRotate * moveVector.normalized;
        }

        /// <summary>
        /// キャラクターの移動制御
        /// </summary>
        private void ControlMove()
        {
            Vector3 moveVector = GetMoveVector();
            bool isMove = moveVector != Vector3.zero;

            if (_animator != null)
            {
                _animator.SetBool(IS_MOVE_HASH, isMove);
            }

            if (isMove)
            {
                // 移動処理
                transform.position += moveVector * Time.deltaTime * _speed;

                // 向きの変更処理
                // ステップサイズは、フレーム時間 x 速度
                // 現在のフレームで回転させる角度をしています。
                Quaternion lookRotation = Quaternion.LookRotation(
                    new Vector3(moveVector.x, 0f, moveVector.z));
                transform.rotation = Quaternion.RotateTowards(
                    from: transform.rotation, 
                    to:   lookRotation, 
                    maxDegreesDelta: Time.deltaTime * _rollSpeed);
            }
        }
        #endregion // Methods Move

        #region // Methods Camera

        private CameraManager.Parameter GetCameraParameter(CameraModeType type)
        {
            switch (type)
            {
                case CameraModeType.Default:
                    return _defaultCamParam;
                case CameraModeType.LookItem:
                    return _itemCamParam;
                default:
                    return null;
            }
        }

        /// <summary>
        /// カメラの切り替え処理
        /// </summary>
        /// <param name="type"></param>
        private void SwitchCamera(CameraModeType type)
        {
            // Defaultに戻るとき、プレイヤーの向きに合わてカメラを移動させる
            if (type == CameraModeType.Default)
            {
                // プレイヤーの座標を設定
                _defaultCamParam.position =
                    _defaultCamParam.trackTarget.position;
                // X軸を少し補正し、Y軸は現在の値を使用(好みで調整)
                _defaultCamParam.angles =
                    new Vector3(15f, transform.eulerAngles.y, 0f);
            }

            // カメラモードを更新
            _cameraModeType = type;

            // 予めターゲットはnullに設定
            // これをしないと、DefaultからLookItemに移動するときに
            // マウスを動かすと画面がぶれます
            _cameraMgr.Param.trackTarget = null;

            // 開始及び終了パラメータの設定
            CameraManager.Parameter startCamParam =
                                            _cameraMgr.Param.Clone();
            CameraManager.Parameter endCamParam =
                                            GetCameraParameter(_cameraModeType);

            // カメラの移動時間
            float duration = 2f;

            // カメラの移動
            _cameraSeq?.Kill();
            _cameraSeq = DOTween.Sequence();
            _cameraSeq.Append(
                // durationの間にtの値を0～1に変化させて、Parameterを線形補完する
                DOTween.To( 
                    () => 0f,
                    t => CameraManager.Parameter.Lerp(
                            a:   startCamParam, 
                            b:   endCamParam, 
                            t:   t, 
                            ret: _cameraMgr.Param), 
                    1f,
                    duration)
                .SetEase(Ease.OutQuart)); // イージングを設定
            // アニメーション時のブレンドを実行
            _cameraSeq.OnUpdate(() => 
                    CameraManager.UpdateTrackTargetBlend(_defaultCamParam));
            // 上のシーケンス終了後のコールバックで、trackTargetを設定
            _cameraSeq.AppendCallback(
                        () => _cameraMgr.Param.trackTarget = endCamParam.trackTarget);
        }

        private void ControlCamera()
        {
            if ( _cameraModeType == CameraModeType.Default &&
                ( _cameraSeq == null || 
                 !(_cameraSeq.IsActive() && _cameraSeq.IsPlaying())))
            {
                Vector3 diffAngles = new Vector3(
                            x: -Input.GetAxis("Mouse Y"),
                            y: Input.GetAxis("Mouse X")
                        ) * 3f;
                _cameraMgr.Param.angles += diffAngles;
            }

            // LookItemとDefaultの切り替え
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
        }

        #endregion
    }
}

