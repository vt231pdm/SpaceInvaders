namespace gamelibrary
{
    public class Block : GameObject
    {
        public int Health { get; private set; }
        public bool IsActive => Health > 0;

        public Block(int x, int y, int width, int height, int health) : base(x, y, width, height)
        {
            this.Health = health;
        }

        public override void Move() { }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}