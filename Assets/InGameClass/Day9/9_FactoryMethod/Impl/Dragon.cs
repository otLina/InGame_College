using UnityEngine;

namespace Factory
{
    public class Dragon : Enemy
    {
        public Dragon(string name, int hp)
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
