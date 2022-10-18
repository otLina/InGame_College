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
