using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool2
{
    /// <summary>
    /// この例では、古いシステムを再利用できるように、
    /// Object Poolを使って無作為な数のParticleSystemsを
    /// 生成しています。
    /// </summary>
    public class PoolExample : MonoBehaviour
    {
        public enum PoolType
        {
            Stack,
            LinkedList
        }

        public PoolType poolType;

        // Collection checks コレクションチェックでは、
        // すでにプールにあるアイテムを解放しようとすると、
        // エラーが発生します。
        public bool collectionChecks = true;
        public int maxPoolSize = 10;

        IObjectPool<ParticleSystem> m_Pool;

        public IObjectPool<ParticleSystem> Pool
        {
            get
            {
                if (m_Pool == null)
                {
                    if (poolType == PoolType.Stack)
                        m_Pool = new ObjectPool<ParticleSystem>(
                            CreatePooledItem,   // createFunc
                            OnTakeFromPool,     // actionOnGet
                            OnReturnedToPool,   // actionOnRelease
                            OnDestroyPoolObject,// actionOnDestroy
                            collectionChecks,   // collectionCheck
                            10,                 // defaultCapacity
                            maxPoolSize);       // maxSize
                    else
                        m_Pool = new LinkedPool<ParticleSystem>(
                            CreatePooledItem,   // createFunc
                            OnTakeFromPool,     // actionOnGet
                            OnReturnedToPool,   // actionOnRelease
                            OnDestroyPoolObject,// actionOnDestroy
                            collectionChecks,   // collectionCheck
                            maxPoolSize);       // maxSize
                }
                return m_Pool;
            }
        }

        ParticleSystem CreatePooledItem()
        {
            var go = new GameObject("Pooled Particle System");
            var ps = go.AddComponent<ParticleSystem>();
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            var main = ps.main;
            main.duration = 1;
            main.startLifetime = 1;
            main.loop = false;

            // ParticleSystemが停止したときに、
            // プールに戻すために使用されます。
            var returnToPool = go.AddComponent<ReturnToPool>();
            returnToPool.pool = Pool;

            return ps;
        }

        /// <summary>
        /// Releaseを使ってアイテムがプールに戻された
        /// ときに呼び出されます。
        /// </summary>
        /// <param name="system"></param>
        void OnReturnedToPool(ParticleSystem system)
        {
            system.gameObject.SetActive(false);
        }

        /// <summary>
        /// Getを使用してプールからアイテムが取得された
        /// ときに呼び出されます。
        /// </summary>
        /// <param name="system"></param>
        void OnTakeFromPool(ParticleSystem system)
        {
            system.gameObject.SetActive(true);
        }

        /// <summary>
        /// プールの容量に達した場合、返されたアイテムは
        /// すべて破壊される。
        /// ここではGameObjectを破壊している。
        /// </summary>
        /// <param name="system"></param>
        void OnDestroyPoolObject(ParticleSystem system)
        {
            Destroy(system.gameObject);
        }

        void OnGUI()
        {
            GUILayout.Label("Pool size: " + Pool.CountInactive);
            if (GUILayout.Button("Create Particles"))
            {
                var amount = Random.Range(1, 10);
                for (int i = 0; i < amount; ++i)
                {
                    var ps = Pool.Get();
                    if (ps != null)
                    {
                        ps.transform.position =
                                        Random.insideUnitSphere * 10;
                        ps.Play();
                    }
                }
            }
        }
    }
}
