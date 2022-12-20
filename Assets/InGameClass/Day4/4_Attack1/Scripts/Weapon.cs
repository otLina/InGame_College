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
            _weaponCollider.enabled = false; //�ŏ��̓R���C�_�[����
        }

        /// <summary>
        /// Animator Controller�ɂ����āuAttack�v�X�e�[�g�̊J�n��
        /// �ꎞ�I�ɕ���̃R���C�_�[��L���ɂ��邽�߂̃R�[���o�b�N���\�b�h
        /// </summary>
        public void EnableWeaponCollider()
        {
            if(_weaponCollider != null)
            {
                _weaponCollider.enabled = true;
            }
        }

        /// <summary>
        /// Animator Controller�ɂ����āuAttack�v�X�e�[�g�̊J�n��
        /// �ꎞ�I�ɕ���̃R���C�_�[��L���ɂ��邽�߂̃R�[���o�b�N���\�b�h
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