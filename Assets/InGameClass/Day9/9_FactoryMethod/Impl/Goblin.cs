using UnityEngine;

namespace Factory
{
    public class Goblin : Enemy
    {
        public Goblin(string name, int hp)
        {
            this.name = name;
            this.hp = hp;
        }
        public override string GetName() => this.name;
        public override int GetHp() => this.hp;

        public override void Attack()
        {
            Debug.Log( $"{this.name}への攻撃!");
        }
    }
}
