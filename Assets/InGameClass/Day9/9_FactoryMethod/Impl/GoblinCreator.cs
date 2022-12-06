namespace Factory
{
    public class GoblinCreator : EnemyCreator
    {
        public override Enemy Create(string name, int hp)
        {
            Enemy enemy = new Goblin(name, hp);
            return enemy;
        }
    }
}
