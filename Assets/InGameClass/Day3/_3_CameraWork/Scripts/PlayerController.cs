using UnityEngine;

namespace CameraWork
{
    public class PlayerController : MonoBehaviour
    {
        #region Definitions

        private static readonly int IS_MOVE_HASH = Animator.StringToHash("IsMove");

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
        #endregion // Variables Camera

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

            //return moveVector.normalized;

            //カメラの向きに合わせて移動方向を変える
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

                //向きの変更処理
                //ステップサイズは、フレーム時間＊速度
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveVector.x, 0f, moveVector.z));
                transform.rotation = Quaternion.RotateTowards(
                        from: transform.rotation,
                        to: lookRotation,
                        maxDegreesDelta: Time.deltaTime * _rollSpeed
                    );
            }
        }
        #endregion // Methods Move

        #region // Methods Camera
        private void ControlCamera()
        {
            Vector3 diffAngles = new Vector3(
                    x: -Input.GetAxis("Mouse Y"),
                    y: Input.GetAxis("Mouse X")
                ) * 3f;

            _cameraMgr.Param.angles += diffAngles;
        }
        #endregion
    }
}

