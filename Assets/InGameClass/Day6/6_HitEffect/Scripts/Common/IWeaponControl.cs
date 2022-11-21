using UnityEngine;
using UnityEngine.EventSystems;

namespace HitEffect
{
    public interface IWeaponControl : IEventSystemHandler
    {
        void EnableWeaponCollider();
 
        void DisableWeaponCollider();
    }
}
