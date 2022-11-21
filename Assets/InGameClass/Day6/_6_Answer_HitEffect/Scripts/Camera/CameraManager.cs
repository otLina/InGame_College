using DG.Tweening;
using System;
using UnityEngine;

namespace HitEffectAnswer
{
    // シーンを実行しなくてもカメラワークが反映されるよう、
    // ExecuteInEditModeを付与
    [ExecuteInEditMode]
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private Transform _child;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Parameter _parameter;

        public Parameter Param => _parameter;

        private bool _isShake = false;

        private void LateUpdate()
        {
            if (_parent == null || _child == null || _camera == null)
            {
                return;
            }

            if (_parameter.trackTarget != null)
            {
                // 被写体がTransformで指定されている場合、
                // positionパラメータに座標を上書き
                UpdateTrackTargetBlend(_parameter);
            }

            // 振動中は強制的にメインカメラを動かす
            if (_isShake)
                return;

            // パラメータを各種オブジェクトに反映
            _parent.position = _parameter.position;
            _parent.eulerAngles = _parameter.angles;

            var childPos = _child.localPosition;
            childPos.z = -_parameter.distance;
            _child.localPosition = childPos;

            _camera.fieldOfView = _parameter.fieldOfView;
            _camera.transform.localPosition = _parameter.offsetPosition;
            _camera.transform.localEulerAngles = _parameter.offsetAngles;
        }

        public static void UpdateTrackTargetBlend(Parameter _parameter)
        {
            _parameter.position = Vector3.Lerp(
                            a: _parameter.position,
                            b: _parameter.trackTarget.position,
                            t: Time.deltaTime * 4f
                        );
        }

        /// <summary>
        /// 振動演出
        /// </summary>
        /// <param name="width">触れ幅</param>
        /// <param name="count">往復回数</param>
        /// <param name="duration">時間</param>
        public void Shake(float width, int count, float duration)
        {
            var camera = _camera.transform;
            var seq = DOTween.Sequence();
            // 振れ演出の片道の揺れ分の時間
            var partDuration = duration / count / 2f;
            // 振れ幅の半分の値
            var widthHalf = width / 2f;

            // 往復回数-1回分の振動演出を作る
            for (int i = 0; i < count - 1; i++)
            {
                seq.Append(camera.DOLocalRotate(
                                        new Vector3(-widthHalf, 0f), partDuration));
                seq.Append(camera.DOLocalRotate(
                                        new Vector3(widthHalf, 0f), partDuration));
            }
            // 最後の揺れは元の角度に戻す工程とする
            seq.Append(camera.DOLocalRotate(
                                        new Vector3(-widthHalf, 0f), partDuration));
            seq.Append(camera.DOLocalRotate(Vector3.zero, partDuration));
            seq.OnStart(() => _isShake = true);
            seq.OnComplete(() => _isShake = false);
                
        }

        /// <summary> カメラのパラメータ </summary>
        [Serializable]
        public class Parameter
        {
            public Transform trackTarget;
            public Vector3 position;
            public Vector3 angles = new Vector3(10f, 0f, 0f);
            public float distance = 7f;
            public float fieldOfView = 45f;
            public Vector3 offsetPosition = new Vector3(0f, 1f, 0f);
            public Vector3 offsetAngles;

            public Parameter Clone()
            {
                return MemberwiseClone() as Parameter;
            }

            public static Parameter Lerp(
                                        Parameter a,
                                        Parameter b,
                                        float t,
                                        Parameter ret)
            {
                ret.position = Vector3.Lerp(a.position, b.position, t);
                ret.angles = LerpAngles(a.angles, b.angles, t);
                ret.distance = Mathf.Lerp(a.distance, b.distance, t);
                ret.fieldOfView = Mathf.Lerp(a.fieldOfView, b.fieldOfView, t);
                ret.offsetPosition = Vector3.Lerp(a.offsetPosition, b.offsetPosition, t);
                ret.offsetAngles = LerpAngles(a.offsetAngles, b.offsetAngles, t);

                return ret;
            }

            private static Vector3 LerpAngles(Vector3 a, Vector3 b, float t)
            {
                Vector3 ret = Vector3.zero;
                ret.x = Mathf.LerpAngle(a.x, b.x, t);
                ret.y = Mathf.LerpAngle(a.y, b.y, t);
                ret.z = Mathf.LerpAngle(a.z, b.z, t);
                return ret;
            }
        }
    }
}
