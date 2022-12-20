using UnityEngine;

namespace PhysicsExample
{
    public class CollisionTest : MonoBehaviour
    {
        /// <summary>
        /// 衝突開始時のコールバック
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"{other.gameObject.name} CollisionEnter");
        }

        /// <summary>
        /// 衝突中のコールバック
        /// </summary>
        /// <param name="other"></param>
        // 衝突中の判定
        private void OnCollisionStay(Collision other)
        {
            Debug.Log($"{other.gameObject.name} CollisionStay");
        }

        /// <summary>
        /// 衝突離脱時のコールバック
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionExit(Collision other)
        {
            Debug.Log($"{other.gameObject.name} CollisionExit");
        }

        /// <summary>
        /// トリガー開始時のコールバック
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{other.gameObject.name} TriggerEnter");
        }

        /// <summary>
        /// トリガー開始時のコールバック
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            Debug.Log($"{other.gameObject.name} TriggerStay");
        }

        /// <summary>
        /// トリガー開始時のコールバック
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"{other.gameObject.name} TriggerExit");
        }
    }
}
