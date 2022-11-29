using UnityEngine;

namespace ObjectPoolAnswer
{
    public class Battery : MonoBehaviour
    {
        [SerializeField]
        private GameObject turrent;

        public void Fire()
        {
            var go = ObjectPool.Instance.GetPooledObject();
            if (go != null)
            {
                go.transform.position = turrent.transform.position;
                go.transform.rotation = turrent.transform.rotation;
                go.SetActive(true);

                var buttle = go.GetComponent<Bullet>();
                buttle.DelayDeactivation(3.0f);
            }
        }
    }
}
