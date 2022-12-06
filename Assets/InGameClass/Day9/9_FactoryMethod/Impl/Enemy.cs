namespace Factory
{
    public abstract class Enemy 
    {
        protected string name;
        protected int hp;

        public abstract string GetName();
        public abstract int GetHp();
        public abstract void Attack();
    }
}
