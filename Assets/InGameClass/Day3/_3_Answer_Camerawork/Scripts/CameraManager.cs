using System;
using UnityEngine;

namespace CameraWorkAnswer
{
    // シーンを実行しなくてもカメラワークがゲームビューに反映されるように
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

        /// <summary>
        /// カメラの向きや位置を得る
        /// </summary>
        public Transform CameraTransform => _camera.transform;

        [SerializeField]
        private Parameter _parameter;
        public Parameter Param => _parameter;

        // 被写体などの移動更新が済んだ後にカメラを更新するために
        // LateUpdateを使用
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

        /// <summary> カメラのパラメータ </summary>
        [Serializable]
        public class Parameter
        {
            // 被写体のTransform
            public Transform trackTarget;

            // Camera Parent に使用
            public Vector3 position;
            public Vector3 angles = new Vector3(10f, 0f, 0f);

            // Camera Child に使用
            public float distance = 7f;

            // Main Cameraに使用
            public float fieldOfView = 45f;
            public Vector3 offsetPosition = new Vector3(0f, 1f, 0f);
            public Vector3 offsetAngles;

            /// <summary>
            /// パラメータのクローンの作成
            /// </summary>
            /// <returns></returns>
            public Parameter Clone()
            {
                return MemberwiseClone() as Parameter;
            }

            public static Parameter Lerp(Parameter a, Parameter b, float t, Parameter ret)
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
