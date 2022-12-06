namespace Factory
{
    public abstract class EnemyCreator
    {
        // factoryMetohd()に相当する
        public abstract Enemy Create(string name, int hp);
    }
}
