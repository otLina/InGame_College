using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack2Answer
{
    public interface IWeaponControl : IEventSystemHandler
    {
        void EnableWeaponCollider();
 
        void DisableWeaponCollider();
    }
}
