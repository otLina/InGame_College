using UnityEngine;

namespace Attack2
{
    public class Weapon : MonoBehaviour, IWeaponControl
    {
        private Collider _weaponCollider;

        void Start()
        {
            _weaponCollider = GetComponent<Collider>();
            _weaponCollider.enabled = false; // 普段はコライダー無効
        }

        /// <summary>
        /// Animator Controllerにおいて「Attack」ステートの開始に
        /// 一時的に武器のコライダーを有効にするためにのコールバックメソッド
        /// </summary>
        public void EnableWeaponCollider()
        {
            if (_weaponCollider != null)
            {
                _weaponCollider.enabled = true;
            }
        }

        /// <summary>
        /// Animator Controllerにおいて「Attack」ステートの開始に
        /// 一時的に武器のコライダーを有効にするためにのコールバックメソッド
        /// </summary>
        public void DisableWeaponCollider()
        {
            if (_weaponCollider != null)
            {
                _weaponCollider.enabled = false;
            }
        }
    }
}
