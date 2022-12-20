using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack2
{
    public interface IDamageable : IEventSystemHandler
    {
        void OnDamage(int damageValue, Vector3 hitPos);
    }
}
