using System;
using UnityEngine;

namespace CameraWork
{
    // シーンを実行しなくてもカメラワークが反映されるように
    // ExecuteInEditModeを付与
    [ExecuteInEditMode]
    public class CameraManager : MonoBehaviour
    {
        [Serializable]
        public class Parameter
        {
            //Camera Parentに使用
            public Vector3 position;
            public Vector3 angles = new Vector3(10f, 0f, 0f);

            //Camera Childに使用
            public float distance = 7f;

            //Main Cameraに使用
            public float fieldOfView = 45f;
            public Vector3 offsetPosition = new Vector3(0f, 1f, 0f);
            public Vector3 offsetAngles;

            //追跡する被写体
            public Transform trackTarget;

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
                //被写体がTransformで指定されている場合、positionパラメーターに座標を上書き
                //遅延なく追いかける場合
                //_parameter.position = _parameter.trackTarget.position;
                //少し遅れて追いかける場合
                UpdateTrackTargetBlend(_parameter);
            }

            //パラメータを各種オブジェクトに反映
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
                t: Time.deltaTime * 3f
                );
        }
    }
}
