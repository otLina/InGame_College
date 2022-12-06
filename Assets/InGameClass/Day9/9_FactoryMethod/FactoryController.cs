using UnityEngine;
using Factory;

namespace UseFactory
{
    public class FactoryController : MonoBehaviour
    {
        void Start()
        {
            EnemyCreator goblinCreator = new GoblinCreator();
            Enemy goblin = goblinCreator.Create("ゴブリン", 100);
            goblin.Attack();

            EnemyCreator dragonCreator = new DragonCreator();
            Enemy dragon = dragonCreator.Create("ドラゴン", 9999);
            dragon.Attack();
        }

    }
}
