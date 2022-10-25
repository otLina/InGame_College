using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack1
{
    public interface IWeaponControl : IEventSystemHandler
    {
        void EnableWeaponCollider();
 
        void DisableWeaponCollider();
    }
}
