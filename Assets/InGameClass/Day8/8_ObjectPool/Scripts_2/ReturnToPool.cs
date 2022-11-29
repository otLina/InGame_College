using System.Text;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool2
{
    /// <summary>
    /// OnParticleSystemStopped イベントを受信すると、
    /// パーティクルシステムをプールに戻すコンポーネントです。
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class ReturnToPool : MonoBehaviour
    {
        public ParticleSystem system;
        public IObjectPool<ParticleSystem> pool;

        void Start()
        {
            system = GetComponent<ParticleSystem>();
            var main = system.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        void OnParticleSystemStopped()
        {
            // Return to the pool
            pool.Release(system);
        }
    }
}