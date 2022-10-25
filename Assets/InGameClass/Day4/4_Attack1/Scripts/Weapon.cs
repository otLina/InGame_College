using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attack1
{
    public class Weapon : MonoBehaviour, IWeaponControl
    {
        private Collider _weaponCollider;

        // Start is called before the first frame update
        void Start()
        {
            _weaponCollider = GetComponent<Collider>();
            _weaponCollider.enabled = false; //最初はコライダー無効
        }

        /// <summary>
        /// Animator Controllerにおいて「Attack」ステートの開始に
        /// 一時的に武器のコライダーを有効にするためのコールバックメソッド
        /// </summary>
        public void EnableWeaponCollider()
        {
            if(_weaponCollider != null)
            {
                _weaponCollider.enabled = true;
            }
        }

        /// <summary>
        /// Animator Controllerにおいて「Attack」ステートの開始に
        /// 一時的に武器のコライダーを有効にするためのコールバックメソッド
        /// </summary>
        public void DisableWeaponCollider()
        {
            if(_weaponCollider != null)
            {
                _weaponCollider.enabled = false;
            }
        }
    }

}