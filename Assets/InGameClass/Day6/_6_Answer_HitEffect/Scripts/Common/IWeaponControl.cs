using UnityEngine;
using UnityEngine.EventSystems;

namespace HitEffectAnswer
{
    public interface IWeaponControl : IEventSystemHandler
    {
        void EnableWeaponCollider();
 
        void DisableWeaponCollider();
    }
}
