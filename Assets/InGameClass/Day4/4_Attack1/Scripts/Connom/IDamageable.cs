using UnityEngine;
using UnityEngine.EventSystems;

namespace Attack1
{
    public interface IDamagable : IEventSystemHandler
    {
        void OnDamage(int damageValue, Vector3 hitPos);
    }
}
