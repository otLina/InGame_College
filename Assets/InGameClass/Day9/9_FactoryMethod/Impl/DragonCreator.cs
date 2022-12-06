namespace Factory
{
    public class DragonCreator : EnemyCreator
    {
        public override Enemy Create(string name, int hp)
        {
            name = name+"(ボス)";
            Enemy enemy = new Dragon(name, hp);
            return enemy;
        }
    }
}
