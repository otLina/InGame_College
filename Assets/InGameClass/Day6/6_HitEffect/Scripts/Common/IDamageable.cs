using UnityEngine;
using UnityEngine.EventSystems;

namespace HitEffect
{
    public interface IDamagable : IEventSystemHandler
    {
        void OnDamage(int damageValue, Vector3 hitPos);
    }
}
