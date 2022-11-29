using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyweightExample
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField]
        private List<EnemyParam> _EnemyParam = new();
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private Transform enemyContainer;

        public void OnCreateEnemy(int idx)
        {
            var enemy = Instantiate(enemyPrefab, enemyContainer);
            var enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.Param = _EnemyParam[idx];
            enemyAI.Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
