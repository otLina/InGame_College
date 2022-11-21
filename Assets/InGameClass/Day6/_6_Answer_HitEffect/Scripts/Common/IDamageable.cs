using UnityEngine;
using UnityEngine.EventSystems;

namespace HitEffectAnswer
{
    public interface IDamagable : IEventSystemHandler
    {
        void OnDamage(int damageValue, Vector3 hitPos);
    }
}
