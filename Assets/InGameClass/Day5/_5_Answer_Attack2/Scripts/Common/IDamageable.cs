using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack2Answer
{
    public interface IDamagable : IEventSystemHandler
    {
        void OnDamage(int damageValue, Vector3 hitPos);
    }
}
