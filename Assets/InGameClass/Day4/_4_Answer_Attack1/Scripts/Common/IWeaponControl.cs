using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack1Answer
{
    public interface IWeaponControl : IEventSystemHandler
    {
        void EnableWeaponCollider();
 
        void DisableWeaponCollider();
    }
}
