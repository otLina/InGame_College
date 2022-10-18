using UnityEngine;
using UnityEngine.UI;

namespace Moving
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
        private Terrain _terrain;

        [SerializeField]
        private bool isConstantSlopeSpeed = false;


        [SerializeField]
        private bool _isRaycast = false;    //レイを使うかどうか

        private float _rayDistance = 10.0f;     //レイの長さ
        private Vector3 _rayOffset = new Vector3(0.0f, 5f, 0.0f);   //始点の位置
        private float _radius = 0.1f;   //レイの半径


        [SerializeField]
        private float _speed = 3f;

        [SerializeField]
        private float _rollSpeed = 360f;

        Vector3 normalVector = Vector3.zero;

        #endregion //  Variables Move

        #region Variables Camera
        [SerializeField]
        private CameraManager _cameraMgr;

        private CameraManager.Parameter _defaultCamParam;

        [SerializeField]
        private CameraManager.Parameter _itemCamParam;

        [SerializeField]
        private CameraManager.Parameter _aimCamParam;

        [SerializeField]
        private Image _cursor;

        [SerializeField]
        private bool _useMouseRoll;

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

        private void Update()
        {
            ControlMove();
            ControlCamera();
        }

        #region Methods Move
        private Vector3 GetMoveVector()
        {
            Vector3 moveVector = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                moveVector += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                moveVector += Vector3.back;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                moveVector += Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                moveVector += Vector3.right;
            }
            Quaternion cameraRotate = Quaternion.Euler(
                0f, _cameraMgr.CameraTransform.eulerAngles.y, 0f);
            return cameraRotate * moveVector.normalized;
        }

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
                //Terrainによる高さ合わせ
                if (_terrain != null)
                {
                    //斜面の移動速度調整
                    if (isConstantSlopeSpeed)
                    {
                        //斜面でも移動速度を一定にする
                        ConstantSlopeSpeed(moveVector);
                    } else
                    {
                        //斜面の移動速度の調整はしない
                        MatchHeightByTerrain(moveVector);
                    }
                }

                if(_isRaycast)
                {
                    //高さ合わせ
                    MatchHeightByRaycast(moveVector);
                }

                Quaternion lookRotation = Quaternion.LookRotation(
                    new Vector3(moveVector.x, 0f, moveVector.z));
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    lookRotation, Time.deltaTime * _rollSpeed);
            }
        }
        ///<summary>
        ///テレインに沿って高さを合わせる
        ///</summary>
        ///<param name="moveVector"></param>
        void MatchHeightByTerrain(Vector3 moveVector)
        {
            //移動処理
            Vector3 moveDelta;
            moveDelta = moveVector * Time.deltaTime * _speed;
            transform.position += moveDelta;

            //高さ合わせ
            Vector3 position = transform.position;
            position.y = _terrain.SampleHeight(position);
            transform.position = position;
        }

        /// <summary>
        /// 斜面でも移動速度を一定にする。
        /// 
        /// 現愛座標と目標座標の差分ベクトルを求めて
        /// 差分ベクトルの長さを移動速度に合わせて調整
        /// </summary>
        /// <param name="moveVector"></param>
        private void ConstantSlopeSpeed(Vector3 moveVector)
        {
            //移動前の位置を保存
            Vector3 currentPosition = transform.position;

            //目標座標を求め、テレイン上の高さを取得
            Vector3 moveDelta;
            moveDelta = moveVector * Time.deltaTime * _speed;
            Vector3 targetPosition = transform.position + moveDelta;
            targetPosition.y = _terrain.SampleHeight(targetPosition);

            //目標座標から現在座標のベクトルの差を求めて
            //normalizeして、1フレームあたりのベクトルを求める
            moveDelta = targetPosition - transform.position;
            moveDelta = moveDelta.normalized * Time.deltaTime * _speed;

            //求めた差分ベクトルを加算する
            transform.position += moveDelta;
        }

        /// <summary>
        /// レイを利用して高さを合わせる
        /// </summary>
        /// <param name="moveVector"></param>
        void MatchHeightByRaycast(Vector3 moveVector)
        {
            //移動処理
            Vector3 moveDelta;
            moveDelta = moveVector * Time.deltaTime * _speed;
            transform.position += moveDelta;

            //レイの視点と方向を決める
            Vector3 rayPosition = transform.position + _rayOffset;
            Ray ray = new Ray(rayPosition, Vector3.down);

            RaycastHit hit;
            bool isHit = Physics.SphereCast(ray, _radius, out hit);

            //レイの衝突を検出
            if (isHit)
            {
                //衝突先の高さに合わせる
                Vector3 position = transform.position;
                position.y = hit.point.y;
                transform.position = position;
            }
        }

        /// <summary>
        /// シーンにSphereCastの様子を表示
        /// </summary>
        void OnDrawGizmos()
        {
            if (_isRaycast == false)
            {
                return;
            }

            //レイの視点と方向を決める
            Vector3 rayPosition = transform.position + _rayOffset;
            Ray ray = new Ray(rayPosition, Vector3.down);

            RaycastHit hit;
            bool isHit = Physics.SphereCast(ray, _radius, out hit);

            //レイの衝突を検出
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(rayPosition, -transform.up * hit.distance);
                Gizmos.DrawWireSphere(rayPosition - transform.up * hit.distance, _radius);

            } else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(rayPosition, -transform.up * _rayDistance);
            }
        }

        #endregion // Methods Move

        #region Methods Camera

        private void ControlCamera()
        {
            if (_useMouseRoll)
            {
                Vector3 diffAngles = new Vector3(
                    x: -Input.GetAxis("Mouse Y"),
                    y: Input.GetAxis("Mouse X")
                ) * 3f;
                _cameraMgr.Param.angles += diffAngles;
            }
        }
        #endregion // #region Methods Camera
    }
}