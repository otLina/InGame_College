using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack2
{
    public interface IWeaponControl : IEventSystemHandler
    {
        void EnableWeaponCollider();
 
        void DisableWeaponCollider();
    }
}
